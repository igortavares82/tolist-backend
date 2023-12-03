using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using Standard.ToList.Model.Options;
using Standard.ToList.Model.ValueObjects;

namespace Standard.ToList.Application.Services
{
    public class SmtpService
	{
		private readonly AppSettingOptions _settings;
		private readonly NetworkCredential _networkCredential;
		private readonly SmtpClient _smtpClient;

        public SmtpService(IOptions<AppSettingOptions> settings)
		{
			_settings = settings.Value;
			_networkCredential = new NetworkCredential(_settings.Smtp.Username, _settings.Smtp.Password);
			_smtpClient = new SmtpClient(_settings.Smtp.Host, _settings.Smtp.Port)
			{
				Credentials = _networkCredential,
				EnableSsl = true
			};
        }

		public void Send(SmtpMessageValueObject message)
		{
            using var mail = new MailMessage(_settings.Smtp.From, message.To, message.Subject, message.Body)
            {
                IsBodyHtml = true
            };

            _smtpClient.Send(mail);
        }

		public string GetTemplate(string templateName)
		{
			var path = $"{AppDomain.CurrentDomain.BaseDirectory}MailMessages/{templateName}.html";
            using var reader = new StreamReader(path);
			
            return reader.ReadToEnd();
		}
	}
}

