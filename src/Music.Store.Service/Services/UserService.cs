using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Music.Store.Data.Repositories;
using Music.Store.Domain.Entities;
using Music.Store.Domain.Enums;
using Music.Store.Domain.Models;
using Music.Store.Domain.Models.MailTemplate;
using Music.Store.Domain.Models.User;
using Music.Store.Infrastructure;
using Music.Store.Infrastructure.Exceptions;
using Music.Store.Infrastructure.Extensions;
using Music.Store.Infrastructure.Helpers;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Music.Store.Service.Services
{
    public interface IUserService
    {
        Task<TokenReponseModel> Login(LoginModel model);
        Task<ProfileModel> GetProfile();
        Task<ServiceResult> UpdateProfile(ProfileModel model);
        Task<ServiceResult> Register(RegisterModel model);
        Task<ServiceResult> ForgotPassword(ForgotPasswordModel model);
        Task<ServiceResult> ChangePassword(ChangePasswordModel model);
        Task<ServiceResult> EmailVerified(string code);
        Task<ServiceResult> ResetPassword(ResetPasswordModel model);
        Task<ResetPasswordInfoModel> GetUserByCode(string code);
        Task<User> GetDmo(int id);
        Task<UserModel> GetById(int id);
        Task<ServiceResult> Post(UserModel model);
        Task<ServiceResult> Put(UserModel model);
        Task<ServiceResult> Delete(int id);
    }

    public class UserService : IUserService
    {
        private readonly IJwtHelper _jwtHelper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMailService _mailService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMemoryCache _memoryCache;

        public UserService(
            IJwtHelper jwtHelper,
            IUnitOfWork unitOfWork,
            IMailService mailService,
            IHttpContextAccessor httpContextAccessor,
            IMemoryCache memoryCache)
        {
            _jwtHelper = jwtHelper;
            _unitOfWork = unitOfWork;
            _mailService = mailService;
            _httpContextAccessor = httpContextAccessor;
            _memoryCache = memoryCache;
        }


        public async Task<TokenReponseModel> Login(LoginModel model)
        {
            if (model == null)
            {
                throw new BadRequestException("model null olamaz.");
            }
            var hashedPassword = SecurityHelper.Sha256Hash(model.Password);

            var user = await _unitOfWork.Repository<User>()
                .GetAll(x => x.EmailAddress == model.EmailAddress && x.Password == hashedPassword && !x.Deleted).FirstOrDefaultAsync();

            if (user == null)
            {
                throw new NotFoundException("Email adresi veya şifre hatalıdır.");
            }

            if (!user.IsActive)
            {
                throw new BadRequestException("Hesabınız aktif değildir.");
            }

            var jwtToken = _jwtHelper.GenerateJwtToken(user);

            user.Token = jwtToken.Token;
            user.TokenExpireDate = jwtToken.ExpireDate;

            await _unitOfWork.Save();

            var result = new TokenReponseModel()
            {
                UserId = user.Id,
                Token = jwtToken.Token,
                UserType = (int)user.UserType,
                NameSurname = $"{user.FirstName} {user.LastName}",
                TokenExpireDate = jwtToken.ExpireDate
            };
            return result;
        }

        public async Task<ProfileModel> GetProfile()
        {
            var loginUser = _httpContextAccessor.HttpContext.User.Parse();

            var user = await _unitOfWork.Repository<User>().Find(loginUser.UserId);

            if (user == null)
            {
                throw new NotFoundException("Kullanıcı bulunamadı.");
            }
            var model = new ProfileModel
            {
                EmailAddress = user.EmailAddress,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Phone = user.Phone,
                UserId = user.Id,

            };
            return model;
        }

        public async Task<ServiceResult> UpdateProfile(ProfileModel model)
        {
            var result = new ServiceResult { HttpStatusCode = HttpStatusCode.OK };

            var loginUser = _httpContextAccessor.HttpContext.User.Parse();

            var user = await _unitOfWork.Repository<User>()
                .Get(x => x.Id == loginUser.UserId && !x.Deleted);

            if (user == null)
            {
                throw new NotFoundException("Kullanıcı bulunamadı.");
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Phone = model.Phone;
            user.UpdatedDate = DateTime.Now;

            await _unitOfWork.Save();

            return result;
        }

        public async Task<ServiceResult> Register(RegisterModel model)
        {
            var result = new ServiceResult { HttpStatusCode = HttpStatusCode.OK };

            if (model.Password != model.RePassword)
            {
                throw new BadRequestException("Şifre alanları uyuşmamaktadır.");
            }

            var isExistEmail = await _unitOfWork.Repository<User>()
                .Any(x => x.EmailAddress == model.EmailAddress && !x.Deleted);

            if (isExistEmail)
            {
                throw new FoundException($"{model.EmailAddress} mail adresiyle üye mevcuttur.");
            }

            var user = new User()
            {
                Deleted = false,
                EmailAddress = model.EmailAddress,
                HashCode = SecurityHelper.RandomBase64(),
                FirstName = model.FirstName,
                LastName = model.LastName,
                InsertedDate = DateTime.Now,
                IsActive = true,
                Password = SecurityHelper.MD5Crypt(model.Password),
                Phone = model.Phone,
                PasswordExpireDate = DateTime.Now.AddMonths(3),
                UserType = UserType.Member,
                UserStatus = UserStatus.EmailNotVerified
            };

            await _unitOfWork.Repository<User>().Add(user);

            //kullanıcıya email gönderiliyor
            var emailVerifyTemplateModel = new TemplateModel
            {
                FullName = $"{user.FirstName} {user.LastName}",
                Url = $"{Global.PortalUrl}email-verify/{user.HashCode}"
            };

            result = await _mailService.SendWithTemplate(new MailWithTemplateModel()
            {
                EmailAddress = model.EmailAddress,
                TemplateType = TemplateType.EmailVerificationLink,
                Data = emailVerifyTemplateModel
            });

            result.Message = "Email adresinize onay maili gönderilmiştir.";

            await _unitOfWork.Save();

            return result;
        }

        public async Task<ServiceResult> ForgotPassword(ForgotPasswordModel model)
        {
            var result = new ServiceResult { HttpStatusCode = HttpStatusCode.OK };

            var user = await _unitOfWork.Repository<User>()
                .Get(x => x.EmailAddress == model.EmailAddress && !x.Deleted);

            if (user != null)
            {
                user.HashCode = SecurityHelper.RandomBase64();
                user.UserStatus = UserStatus.NotSetPassword;
                user.UpdatedDate = DateTime.Now;

                await _unitOfWork.Save();

                // kullanıcıya mail gönderiliyor
                result = await SendMailResetPassword(user);
            }

            result.Message = "Email adresiniz mevcutsa şifre belirleme linki gönderilmiştir.";

            return result;
        }

        public async Task<ServiceResult> ChangePassword(ChangePasswordModel model)
        {
            var result = new ServiceResult { HttpStatusCode = HttpStatusCode.OK };

            if (model.NewPassword != model.ReNewPassword)
            {
                throw new BadRequestException("Şifre alanları uyuşmamaktadır.");
            }

            var loginUser = _httpContextAccessor.HttpContext.User.Parse();

            var user = await _unitOfWork.Repository<User>()
                .Get(x => x.Id == loginUser.UserId && !x.Deleted);

            if (user == null)
            {
                throw new NotFoundException("Kullanıcı bulunamadı.");
            }
            var oldPassword = SecurityHelper.MD5Crypt(model.OldPassword);

            if (user.Password != oldPassword)
            {
                throw new BadRequestException("Mevcut şifreniz hatalıdır.");
            }

            user.Password = SecurityHelper.MD5Crypt(model.NewPassword);
            user.PasswordExpireDate = DateTime.Now.AddMonths(3);
            user.UpdatedDate = DateTime.Now;
            user.HashCode = String.Empty;

            await _unitOfWork.Save();

            return result;
        }

        public async Task<ServiceResult> EmailVerified(string code)
        {
            var result = new ServiceResult { HttpStatusCode = HttpStatusCode.OK };

            var user = await _unitOfWork.Repository<User>()
                .Get(x => x.HashCode == code && x.UserStatus == UserStatus.EmailNotVerified);

            if (user == null)
            {
                result.HttpStatusCode = HttpStatusCode.NotFound;
                return result;
            }

            user.HashCode = String.Empty;
            user.UserStatus = UserStatus.Active;

            await _unitOfWork.Save();

            return result;
        }

        public async Task<ServiceResult> ResetPassword(ResetPasswordModel model)
        {
            var result = new ServiceResult { HttpStatusCode = HttpStatusCode.OK };

            var user = await _unitOfWork.Repository<User>()
                .Get(x => x.HashCode == model.Code && x.UserStatus == UserStatus.NotSetPassword);

            if (user == null)
            {
                throw new BadRequestException("Kullanıcı bulunamadı.");
            }

            if (model.NewPassword != model.ReNewPassword)
            {
                throw new BadRequestException("Şifre alanları uyuşmamaktadır.");
            }

            user.Password = SecurityHelper.MD5Crypt(model.NewPassword);
            user.PasswordExpireDate = DateTime.Now.AddMonths(3);
            user.UpdatedDate = DateTime.Now;
            user.HashCode = String.Empty;
            user.UserStatus = UserStatus.Active;

            await _unitOfWork.Save();

            return result;
        }

        private async Task<ServiceResult> SendMailResetPassword(User user)
        {
            var forgotPasswordTemplateModel = new TemplateModel
            {
                FullName = $"{user.FirstName} {user.LastName}",
                Url = $"{Global.PortalUrl}set-password/{user.HashCode}"
            };

            var result = await _mailService.SendWithTemplate(new MailWithTemplateModel()
            {
                EmailAddress = user.EmailAddress,
                TemplateType = TemplateType.SetPasswordLink,
                Data = forgotPasswordTemplateModel
            });
            return result;
        }

        public async Task<ResetPasswordInfoModel> GetUserByCode(string code)
        {
            ResetPasswordInfoModel model = null;

            var user = await _unitOfWork.Repository<User>()
                .Get(x => x.HashCode == code && x.UserStatus == UserStatus.NotSetPassword);

            if (user != null)
            {
                model = new ResetPasswordInfoModel
                {
                    FullName = $"{user.FirstName} {user.LastName}",
                    Code = code
                };
            }
            return model;
        }

        public async Task<User> GetDmo(int id)
        {
            return await _unitOfWork.Repository<User>().Find(id);
        }

        public async Task<UserModel> GetById(int id)
        {
            var user = await _unitOfWork.Repository<User>().Find(id);
            if (user == null)
            {
                throw new NotFoundException("Kullanıcı bulunamadı.");
            }

            var model = new UserModel
            {
                EmailAddress = user.EmailAddress,
                FirstName = user.FirstName,
                Id = id,
                InsertedDate = DateTime.Now,
                IsActive = true,
                LastName = user.LastName,
                Phone = user.Phone,
                UserStatus = (int)user.UserStatus,
                UserStatusName = EnumHelper.GetDescription(user.UserStatus),
                UpdatedDate = user.UpdatedDate,
                UserType = (int)user.UserType,
                UserTypeName = EnumHelper.GetDescription(user.UserType)
            };
            return model;
        }

        public async Task<ServiceResult> Post(UserModel model)
        {
            var result = new ServiceResult { HttpStatusCode = HttpStatusCode.OK };

            var checkEmail = await _unitOfWork.Repository<User>()
                .Any(x => x.Id != model.Id && x.EmailAddress == model.EmailAddress && !x.Deleted);

            if (checkEmail)
            {
                throw new FoundException("Email adresi ile daha önce kullanıcı kaydedilmiş.");
            }

            var user = new User
            {
                Deleted = false,
                EmailAddress = model.EmailAddress,
                IsActive = model.IsActive,
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserType = (UserType)model.UserType,
                InsertedDate = DateTime.Now,
                HashCode = SecurityHelper.RandomBase64(),
                UserStatus = UserStatus.NotSetPassword,
                Phone = model.Phone
            };

            await _unitOfWork.Repository<User>().Add(user);

            await _unitOfWork.Save();

            // kullanıcıya email gönderiliyor                
            result = await SendMailResetPassword(user);

            return result;
        }

        public async Task<ServiceResult> Put(UserModel model)
        {
            var result = new ServiceResult { HttpStatusCode = HttpStatusCode.OK };

            var loginUser = _httpContextAccessor.HttpContext.User.Parse();

            var isExistUser = await _unitOfWork.Repository<User>()
                .Any(x => x.EmailAddress == model.EmailAddress && !x.Deleted && x.Id != model.Id);

            if (isExistUser)
            {
                throw new FoundException("Email adresi ile daha önce kullanıcı kaydedilmiş.");
            }

            var user = await _unitOfWork.Repository<User>()
                .Get(x => x.Id == model.Id && !x.Deleted);

            if (user == null)
            {
                throw new NotFoundException("Kullanıcı bulunamadı.");
            }

            user.EmailAddress = model.EmailAddress;
            user.IsActive = model.IsActive;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.UserType = (UserType)model.UserType;
            user.Phone = model.Phone;

            await _unitOfWork.Save();

            if (user.UserType != UserType.Member)
            {
                _memoryCache.Remove($"userMenu_{model.Id}");
            }

            return result;
        }

        public async Task<ServiceResult> Delete(int id)
        {
            var result = new ServiceResult { HttpStatusCode = HttpStatusCode.OK };

            var user = await _unitOfWork.Repository<User>()
                .Get(x => x.Id == id && !x.Deleted);

            if (user == null)
            {
                throw new NotFoundException("Kullanıcı bulunamadı.");
            }

            user.Deleted = true;

            await _unitOfWork.Save();

            if (user.UserType != UserType.Member)
            {
                _memoryCache.Remove($"userMenu_{id}");
            }

            return result;
        }
    }
}
