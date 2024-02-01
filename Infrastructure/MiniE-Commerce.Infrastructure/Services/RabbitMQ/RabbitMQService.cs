using Microsoft.AspNetCore.Connections;
using Microsoft.Extensions.Logging;
using MiniE_Commorce.Application.Interfaces.Services.RabbitMQ;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace MiniE_Commerce.Infrastructure.Services.RabbitMQ
{
    public class RabbitMQService:IDisposable, IRabbitMQService
    {

        private readonly ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _channel;
        private readonly ILogger<RabbitMQService> _logger;

        public RabbitMQService(ILogger<RabbitMQService> logger, ConnectionFactory connectionFactory)
        {
            _logger = logger;
            _connectionFactory = connectionFactory;
        }

        public void SendMeesageToExchange(string exchangeName,string email)
        {

            var channel = Connect(exchangeName, ExchangeType.Fanout);
            var bodyByte = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(email));
            channel.BasicPublish(exchangeName, "", null, bodyByte);
        }

        public IModel Connect(string exchangeName,string exchangeType)
        {
            if (_channel is { IsOpen: true})
            {
                _logger.LogInformation("return channel");
                return _channel;
            }
            _connection = _connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(exchangeName, type: exchangeType, durable:true, autoDelete:false);
            _logger.LogInformation("RabbitMQ ile bağlantı kuruldu...");
            return _channel;

        }

        public void Dispose()
        {
            _channel?.Close();
            _channel?.Dispose();
            _connection?.Close();
            _connection?.Dispose();

            _logger.LogInformation("RabbitMQ ile bağlantı koptu...");
        }


    }
}
