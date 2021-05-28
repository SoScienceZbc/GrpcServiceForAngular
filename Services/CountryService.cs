using Grpc.Core;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Logging;
using Proto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcServiceForAngular
{
    /// <summary>
    /// Pure Test and exmple on how a grpc service with ssl looks like..
    /// DONT USE IN PRODUCTION!.
    /// </summary>
    public class CountryService : Proto.CountryServices.CountryServicesBase
    {
        private readonly ILogger<CountryService> _logger;
        public CountryService(ILogger<CountryService> logger)
        {
            _logger = logger;
        }

        public override Task<CountriesReply> GetAll(EmptyRequest caller, ServerCallContext context)
        {
            CountriesReply reply = new CountriesReply();
            reply.Countries.Add(new CountryReply { Description = "gfh", Id = 1, Name = "dfg" });
            reply.Countries.Add(new CountryReply { Description = "gdfgfh", Id = 5, Name = "dfgfdh" });
            return Task.FromResult(reply);
        }
    }
}
