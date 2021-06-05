using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using Proto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GrpcServiceForAngular.Services
{
    /// <summary>
    /// This class has the resoponseblity of meaking calls to the dataserver.
    /// </summary>
    public class LoginToDataServerHandler
    {
        public static LoginServcie.LoginServcieClient channel;

        public LoginToDataServerHandler()
        {

            //AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            //AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2Support", true);
            GrpcWebHandler handler = new GrpcWebHandler(GrpcWebMode.GrpcWebText, new HttpClientHandler());
            channel = new LoginServcie.LoginServcieClient(GrpcChannel.ForAddress(new Uri("http://localhost:33700/"),
                new GrpcChannelOptions
                {
                    HttpClient = new HttpClient(handler),
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
            //LoginServcie.LoginServcieClient hhtp2Channel = new LoginServcie.LoginServcieClient(GrpcChannel.ForAddress(new Uri("http://localhost:5001"),
            //    new GrpcChannelOptions
            //    {
            //        Credentials = /*new Grpc.Core.SslCredentials()*/ Grpc.Core.ChannelCredentials.Insecure,


            //    }));

            //    LoginRepley repley = hhtp2Channel.LoginAD(requset);
            //    Console.WriteLine($"loginattampt:\n login was:{repley.LoginSucsefull}\nUsername: {requset.Username}");            
                return Task.FromResult(channel.LoginAD(requset));

            
        }
    }
}
