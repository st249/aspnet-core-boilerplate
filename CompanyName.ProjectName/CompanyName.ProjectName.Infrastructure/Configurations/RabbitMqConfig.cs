using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyName.ProjectName.Infrastructure.Configurations;

public class RabbitMqConfig
{
    public const string Key = "RabbitMq";

    public string Host { get; set; }
    public string VirtualHost { get; set; }
    public ushort? Port { get; set; } = 5672;
    public string Username { get; set; }
    public string Password { get; set; }
    public ICollection<string> ClusterNodes { get; set; }
    public bool ClusterEnabled { get; set; }
}


