using Hosting= Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniE_Commorce.Application.Interfaces.Services.RabbitMQ;
using RabbitMQ.Client;
using System.Threading.Channels;
using RabbitMQ.Client.Events;
using Microsoft.Extensions.Logging;
using MiniE_Commerce.Infrastructure.Services.RabbitMQ;
using MiniE_Commorce.Application.Interfaces.Services.Email;

namespace MiniE_Commerce.Infrastructure.BackgroundService
{
    public class UserRegistrationEmailService : Hosting.BackgroundService
    {
        private readonly IRabbitMQService _rabbitMQService;
        private IModel _channel;
        private readonly ILogger<UserRegistrationEmailService> _logger;
        private readonly IEmailService _emailService;

        public UserRegistrationEmailService(IRabbitMQService rabbitMQService, ILogger<UserRegistrationEmailService> logger, IEmailService emailService)
        {
            _rabbitMQService = rabbitMQService;
            _logger = logger;
            _emailService = emailService;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _channel = _rabbitMQService.Connect("UserFanoutExchange", ExchangeType.Fanout);
            _channel.QueueDeclare("UserRegistrationEmailQueue", true, false, false, null);
            _channel.QueueBind("UserRegistrationEmailQueue", "UserFanoutExchange", "", null);
            _channel.BasicQos(0, 1, false);
            return base.StartAsync(cancellationToken);
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);

            _channel.BasicConsume("UserRegistrationEmailQueue", false, consumer);

            _logger.LogInformation("loglar dinleniyor");

            consumer.Received += (object sender, BasicDeliverEventArgs e) =>
            {
                var message = Encoding.UTF8.GetString(e.Body.Span);

                
                _logger.LogInformation("Gelen Mesaj:" + message);
                _emailService.SendEmailAsync(message.Substring(1, message.Length - 2), "Hoşgeldiniz","Merhaba");
                _channel.BasicAck(e.DeliveryTag, false);
            };
            
            return Task.CompletedTask;
        }
    }
}
