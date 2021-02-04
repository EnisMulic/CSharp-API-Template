using System;
using System.Collections.Generic;
using System.Text;

namespace Template.Contracts.HealthChecks
{
    public class HealthCheckResponse
    {
        public string Status { get; set; }
        public IEnumerable<HealthCheck> HealthChecks { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
