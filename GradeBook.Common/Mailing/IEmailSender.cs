﻿using System.Threading.Tasks;

namespace GradeBook.Common.Mailing
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string toAddress, string subject, string message);
    }
}