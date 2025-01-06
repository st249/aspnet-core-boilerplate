using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyName.ProjectName.Infrastructure.Configurations;

public class RedisCacheConfig
{
    public const string Key = "RedisCache";

    public string SingleNode { get; set; }

    public string[] ClusterNodes { get; set; }

    public bool ClusterEnabled { get; set; }

    public string[] Connections
    {
        get
        {
            if (!ClusterEnabled)
            {
                return new string[1] { SingleNode };
            }

            return ClusterNodes;
        }
    }
}


