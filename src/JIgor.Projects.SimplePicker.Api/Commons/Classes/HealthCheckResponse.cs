﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace JIgor.Projects.SimplePicker.Api.Commons.Classes
{
    public class HealthCheckResponse
    {
        public string Status { get; set; }
        
        public IEnumerable<HealthCheck> Checks { get; set; }
        
        public TimeSpan Duration { get; set; }
        
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}