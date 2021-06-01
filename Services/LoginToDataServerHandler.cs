using Grpc.Net.Client;
using Proto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcServiceForAngular.Services
{
    /// <summary>
    /// This class has the resoponseblity of meaking calls to the dataserver.
    /// </summary>
    public class LoginToDataServerHandler
    {
        LoginServcie.LoginServcieClient channel;
        public LoginToDataServerHandler()
        {

            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2Support", true);            
            channel = new LoginServcie.LoginServcieClient(GrpcChannel.ForAddress("http://192.168.1.102:33700",
                new GrpcChannelOptions
                {
                    Credentials = /*new Grpc.Core.SslCredentials()*/ Grpc.Core.ChannelCredentials.Insecure,

                }));
        }
        /// <summary>
        /// This is acting as the client on the proxt and making the rpc call to the dataserver.
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        public Task<LoginRepley> LoginAD(LoginRequset requset)
        {
            return Task.FromResult(channel.LoginAD(requset));
        }
    }
}
