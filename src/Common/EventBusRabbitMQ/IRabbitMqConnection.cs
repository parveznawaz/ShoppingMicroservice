using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;

namespace EventBusRabbitMQ
{
    public interface IRabbitMqConnection: IDisposable
    {
        bool IsConnected { get; }
        bool TryConnect();
        IModel CreateModel();
    }
}
