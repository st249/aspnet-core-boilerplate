using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CompanyName.ProjectName.Infrastructure.Utilities
{
    public class PrefixEndpointNameFormatter : DefaultEndpointNameFormatter
    {
        private readonly string _prefix;

        private static readonly Regex Pattern = new Regex("(?<=[a-z0-9])[A-Z]", RegexOptions.Compiled);

        private readonly string _separator;

        public PrefixEndpointNameFormatter(string prefix, string separator = "-")
        {
            _prefix = prefix;
            _separator = separator;
        }

        public override string SanitizeName(string name)
        {
            return Pattern.Replace(_prefix + name, (Match m) => _separator + m.Value).ToLowerInvariant();
        }
    }
}


