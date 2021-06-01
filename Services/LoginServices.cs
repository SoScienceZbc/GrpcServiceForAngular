using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Proto;

namespace GrpcServiceForAngular.Services
{
    public class LoginServices : LoginServcie.LoginServcieBase
    {
        public override Task<LoginRepley> LoginAD(LoginRequset requset, ServerCallContext context)
        {
            return new LoginToDataServerHandler().LoginAD(requset);    
        }
    }
}
