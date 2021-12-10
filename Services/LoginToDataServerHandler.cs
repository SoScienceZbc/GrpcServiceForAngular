using Grpc.Core;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using Proto;
using System;
using System.Collections.Generic;
using System.IO;
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
        public static LoginService.LoginServiceClient channel;

        public LoginToDataServerHandler()
        {
            //var cacert = File.ReadAllText("/etc/ssl/certs/ca-bundle.trust.crt");
            //var servercert = File.ReadAllText("/home/SoScienceUser/soscience_ssl_certificate/soscience.dk.crt");
            //var serverkey = File.ReadAllText("/home/SoScienceUser/soscience_ssl_certificate/soscience.dk.key");
            //var keypair = new KeyCertificatePair(servercert, serverkey);

            GrpcWebHandler handler = new GrpcWebHandler(GrpcWebMode.GrpcWebText, new HttpClientHandler());
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2Support", true);
            channel = new LoginService.LoginServiceClient(GrpcChannel.ForAddress(new Uri("https://127.0.0.1:33701"),
                new GrpcChannelOptions
                {
                    HttpClient = new HttpClient(handler),
                    Credentials = new SslCredentials() //Grpc.Core.ChannelCredentials.Insecure,


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
        public Task<LoginRepley> ValidateToken(LoginRepley requset)
        {
            return Task.FromResult(channel.ValidateToken(requset));

        }
    }
}
