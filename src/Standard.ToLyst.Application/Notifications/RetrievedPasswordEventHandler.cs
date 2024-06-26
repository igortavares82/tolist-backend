﻿using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Standard.ToLyst.Application.Services;
using Standard.ToLyst.Model.Aggregates.Users;
using Standard.ToLyst.Model.Options;
using Standard.ToLyst.Model.ValueObjects;

namespace Standard.ToLyst.Application
{
    public class RetrievedPasswordEventHandler : INotificationHandler<RetrievedPasswordEvent>
    {
        private readonly SmtpService _smtpService;
        private readonly AppSettingOptions _settings;
        private readonly ILogger<RetrievedPasswordEventHandler> _logger;

        public RetrievedPasswordEventHandler(SmtpService smtpService, 
                                             ILogger<RetrievedPasswordEventHandler> logger, 
                                             IOptions<AppSettingOptions> settings)
        {
            _smtpService = smtpService;
            _logger = logger;
            _settings = settings.Value;
        }

        public async Task Handle(RetrievedPasswordEvent notification, CancellationToken cancellationToken = default)
        {
            var message = GenerateMessage(notification.User);
            _smtpService.Send(message);
        }

        private SmtpMessageValueObject GenerateMessage(User user)
        {
            var loggerMessage = "sending password retrieve message to user = {id}. Data: name = {name}, email = email";
            _logger.LogInformation($"Start {loggerMessage}", user.Id, user.Name, user.Email);

            var template = _smtpService.GetTemplate("RetrievePassword");
            var link = $"{_settings.FrontendUrl}/#/update-password?token={user.RetrieveToken}";
            template = template.Replace("#link#", link);

            _logger.LogInformation($"End {loggerMessage}, token = {user.RetrieveToken}", user.Id, user.Name, user.Email);

            return new SmtpMessageValueObject(user.Email, "ToLyst - Password reset.", template);
        }
    }
}
