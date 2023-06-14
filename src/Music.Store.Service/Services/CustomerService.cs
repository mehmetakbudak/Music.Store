using Microsoft.EntityFrameworkCore;
using Music.Store.Data.Repositories;
using Music.Store.Domain;
using Music.Store.Domain.Entities;
using Music.Store.Domain.Models;
using Music.Store.Infrastructure.Helpers;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Music.Store.Service.Services
{
    public interface ICustomerService
    {
        PaginationModel<Customer> GetByFilter(CustomerFilterModel model);
        Task<Customer> GetById(int id);
        Task<ServiceResult> Post(Customer model);
        Task<ServiceResult> Put(Customer model);
        Task<ServiceResult> Delete(int id);
    }

    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public PaginationModel<Customer> GetByFilter(CustomerFilterModel model)
        {
            var data = _unitOfWork.Repository<Customer>()
                .GetAll()
                .Include(x => x.SupportRep)
                .OrderByDescending(x => x.Id)
                .AsQueryable();

            if (!string.IsNullOrEmpty(model.Address))
            {
                data = data.Where(x => x.Address.ToLower().Contains(model.Address.ToLower()));
            }
            if (!string.IsNullOrEmpty(model.Email))
            {
                data = data.Where(x => x.Email.ToLower().Contains(model.Email.ToLower()));
            }
            if (!string.IsNullOrEmpty(model.FirstName))
            {
                data = data.Where(x => x.FirstName.ToLower().Contains(model.FirstName.ToLower()));
            }
            if (!string.IsNullOrEmpty(model.LastName))
            {
                data = data.Where(x => x.LastName.ToLower().Contains(model.LastName.ToLower()));
            }
            if (!string.IsNullOrEmpty(model.Phone))
            {
                data = data.Where(x => x.Phone.ToLower().Contains(model.Phone.ToLower()));
            }
            if (!string.IsNullOrEmpty(model.Company))
            {
                data = data.Where(x => x.Company.ToLower().Contains(model.Company.ToLower()));
            }

            var list = PaginationHelper<Customer>.Paginate(data, model);

            return list;
        }

        public async Task<Customer> GetById(int id)
        {
            var customer = await _unitOfWork.Repository<Customer>().Find(id);
            return customer;
        }

        public async Task<ServiceResult> Post(Customer model)
        {
            var result = new ServiceResult { HttpStatusCode = HttpStatusCode.OK };

            await _unitOfWork.Repository<Customer>().Add(model);

            await _unitOfWork.Save();

            return result;
        }

        public async Task<ServiceResult> Put(Customer model)
        {
            var result = new ServiceResult { HttpStatusCode = HttpStatusCode.OK };

            _unitOfWork.Repository<Customer>().Update(model);

            await _unitOfWork.Save();

            return result;
        }

        public async Task<ServiceResult> Delete(int id)
        {
            var result = new ServiceResult { HttpStatusCode = HttpStatusCode.OK };

            var customer = await GetById(id);
            if (customer == null)
            {
                result.HttpStatusCode = HttpStatusCode.NotFound;
                result.Message = "Customer not found.";
                return result;
            }

            _unitOfWork.Repository<Customer>().Remove(customer);

            await _unitOfWork.Save();

            return result;
        }
    }

}
