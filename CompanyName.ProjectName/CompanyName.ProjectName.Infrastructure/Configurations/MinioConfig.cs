using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyName.ProjectName.Infrastructure.Configurations;

public class MinioConfig
{
    public const string Key = "Minio";
    public string Connection { get; set; }
    public string AccessKey { get; set; }
    public string SecretKey { get; set; }
    public string RootBucketName { get; set; }
    public bool WithSSl { get; set;} = false;
}


