using System.ComponentModel;

namespace Music.Store.Domain.Enums
{
    public enum UserStatus
    {
        [Description("Aktif")]
        Active = 1,
        [Description("Email Doğrulanmamış")]
        EmailNotVerified,
        [Description("Şifre Belirlenmemiş")]
        NotSetPassword,
        [Description("Erişime Engellendi")]
        Blocked
    }
}
