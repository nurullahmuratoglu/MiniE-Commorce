using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniE_Commorce.Application.Interfaces.Services.RabbitMQ
{
    public interface IRabbitMQService
    {
        void SendMeesageToExchange(string exchangeName, string userId);
        IModel Connect(string exchangeName, string exchangeType);
    }
}
