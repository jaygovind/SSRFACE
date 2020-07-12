using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SMPAPI.Services
{
    public interface IEmailService
    {
        Task SendEmailWithPasswordCreatedByAdminAsync(string EmailAddress, string Password, string username);
        Task SendAsync(string EmailDisplayName, string Subject, string Body, string From, string To);

        Task SendEmailConfirmationAsync(string Email, string CallbackUrl);

        Task SendPasswordResetAsync(string Email, string CallbackUrl);

        Task SendException(Exception ex);

        Task SendSqlException(SqlException ex);
    }
}
