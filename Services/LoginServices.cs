using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Proto;

namespace GrpcServiceForAngular.Services
{
    public class LoginServices : LoginService.LoginServiceBase
    {
        public override Task<LoginRepley> LoginAD(LoginRequset requset, ServerCallContext context)
        {

            Console.WriteLine($"Host:{context.Host} called Method:{context.Method}\nPeer Addresse : {context.Peer}\n" +
                $"RequstHeader: {context.RequestHeaders}\n");
            //$"HttpContex_Connection.RemoteIpAddress:{context.GetHttpContext().Connection.RemoteIpAddress}");
                return new LoginToDataServerHandler().LoginAD(requset);
        }
    }
}
