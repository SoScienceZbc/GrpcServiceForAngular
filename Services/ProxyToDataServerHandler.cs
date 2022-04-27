using DatabaseService_Grpc;
using Grpc.Core;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace GrpcServiceForAngular.Services
{
    /// <summary>
    /// This is responsle for talking to the ZbcMainDataServer via grpc and ssh tunnel.
    /// </summary>
    public class ProxyToDataServerHandler
    {
        private static GrpcDatabaseProject.GrpcDatabaseProjectClient client;

        //This is a hashed serial used in DangerousServerCertificateCustomValidationCallback() to validate the server certificate.
        private string HashedSerial { get; } = File.ReadAllText(Directory.GetCurrentDirectory() + "/HashedSerial.txt");

        public ProxyToDataServerHandler()
        {
            //AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2Support", true);

            client = CreateGrpcClient("https://localhost:33701");
        }

        #region GrpcClientSetup
        /// <summary>
        /// Sets up a Proto client and channel for making Grpc calls to the SSHAgent database service
        /// </summary>
        /// <param name="channelURL"></param>
        /// <returns></returns>
        private GrpcDatabaseProject.GrpcDatabaseProjectClient CreateGrpcClient(string channelURL)
        {
            HttpClientHandler http_handler = new HttpClientHandler();

            http_handler.ServerCertificateCustomValidationCallback = DangerousServerCertificateCustomValidationCallback;

            GrpcWebHandler handler = new GrpcWebHandler(GrpcWebMode.GrpcWebText, http_handler);

            GrpcDatabaseProject.GrpcDatabaseProjectClient client = new GrpcDatabaseProject.GrpcDatabaseProjectClient(
                GrpcChannel.ForAddress(new Uri(channelURL),
                new GrpcChannelOptions
                {
                    HttpClient = new HttpClient(handler),
                    Credentials = new SslCredentials()
                }));

            return client;
        }

        /// <summary>
        /// This checks whether the server's certificate should be trusted or not. More of a workaround. Implement a better way when possible.
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        /// <param name="arg3"></param>
        /// <param name="arg4"></param>
        /// <returns></returns>
        private bool DangerousServerCertificateCustomValidationCallback(HttpRequestMessage arg1, X509Certificate2 arg2, X509Chain arg3, SslPolicyErrors arg4)
        {
            /* Used for debugging purposes
            Console.WriteLine("\n******CertificateResponse******\n");
            Console.WriteLine(arg1);
            Console.WriteLine("X509Certificate2:");
            Console.WriteLine(arg2);
            Console.WriteLine("X509Chain:");
            Console.WriteLine(arg3);
            Console.WriteLine("SslPolicyErrors:");
            Console.WriteLine(arg4);
            Console.WriteLine("\n******CertificateResponse******\n");
            */

            Sha256Hasher hasher = new Sha256Hasher();
            string serverSerialHashed = hasher.HashString(arg2.SerialNumber);

            //Compares the received serial with the expected serial
            if (serverSerialHashed.ToLower() == HashedSerial.ToLower())
                return true;
            else
                return false;
        }
        #endregion

        /*
        /// <summary>
        /// This is used for testing, DONT USE IN PRODUCTION!!
        /// </summary>
        /// <param name="infomation"></param>
        public void DataServiceGet(UserDbInfomation infomation)
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2Support", true);
            // The port number(5001) must match the port of the gRPC server.
            using var channel = GrpcChannel.ForAddress("http://192.168.1.101:5200", new GrpcChannelOptions { Credentials = new Grpc.Core.SslCredentials() Grpc.Core.ChannelCredentials.Insecure });
            var ts = new GrpcDatabaseProject.GrpcDatabaseProjectClient(channel);
            //var testDataGetter = new DataGetter.DataGetterClient(channel);
            UserDbInfomation projectuser = new UserDbInfomation { DbName = "andi0137" };
            ProjectUserInfomation protobufProject = new ProjectUserInfomation();
            protobufProject.User = projectuser;

            Console.WriteLine("Presster Enter to add a new project to the database");
            Console.ReadLine();
            protobufProject.Project = new D_Project { Completed = false, EndDate = DateTime.Now.ToString(), Name = "This is with grpc" };
            UserDbInfomation mortUser = new UserDbInfomation { DbName = "alex303a", ID = 215 };
            intger replyDatabase = ts.AddProject(protobufProject);
            Console.WriteLine("This amount have change in the database:" + replyDatabase);
        }*/

        #region Project
        public Task<D_Project> GetProject(UserDbInfomation infomation)
        {
            return Task.FromResult(client.GetProject(infomation));
        }
        public Task<intger> AddProject(ProjectUserInfomation infomation)
        {
            return Task.FromResult(client.AddProject(infomation));
        }
        public Task<intger> EditProject(ProjectUserInfomation infomation)
        {
            return Task.FromResult(client.EditProject(infomation));
        }

        public Task<intger> RemoveProject(ProjectUserInfomation infomation)
        {
            return Task.FromResult(client.RemoveProject(infomation));
        }

        public Task<D_Projects> GetProjects(UserDbInfomation infomation)
        {
            Console.WriteLine("Entered ProxyToDataServerHandler GetProjects");
            return Task.FromResult(client.GetProjects(infomation));
        }
        public Task<intger> AddProjectMember(MemberInformation information)
        {
            return Task.FromResult(client.AddProjectMember(information));
        }
        public Task<intger> RemoveProjectMember(MemberInformation information)
        {
            return Task.FromResult(client.RemoveProjectMember(information));
        }
        #endregion
        #region Docoment
        public Task<D_Documents> GetDocuments(UserDbInfomation infomation)
        {
            return Task.FromResult(client.GetDocuments(infomation));
        }

        public Task<intger> AddDocument(D_Document infomation)
        {
            return Task.FromResult(client.AddDocument(infomation));
        }
        public Task<D_Document> GetDocument(UserDbInfomation infomation)
        {
            return Task.FromResult(client.GetDocument(infomation));
        }
        public Task<intger> UpdateDocument(D_Document infomation)
        {
            return Task.FromResult(client.UpdateDocument(infomation));
        }

        public Task<intger> RemoveDocument(ProjectUserInfomation infomation)
        {
            return Task.FromResult(client.RemoveDocument(infomation));

        }
        #endregion
        #region Remote
        public Task<intger> AddRemoteFile(D_RemoteFile infomation)
        {
            return Task.FromResult(client.AddRemoteFile(infomation));
        }
        public Task<D_RemoteFile> GetRemoteFile(UserDbInfomation infomation)
        {

            return Task.FromResult(client.GetRemoteFile(infomation));
        }
        public Task<D_RemoteFile> UpdateRemoteFile(D_RemoteFile infomation)
        {
            return Task.FromResult(client.UpdateRemoteFile(infomation));
        }
        public Task<intger> RemoveRemoteFile(UserDbInfomation infomation)
        {
            return Task.FromResult(client.RemoveRemoteFile(infomation));
        }
        public Task<D_RemoteFiles> GetRemoteFiles(UserDbInfomation infomation)
        {
            return Task.FromResult(client.GetRemoteFiles(infomation));
        }
        #endregion
        #region Subject
        public Task<intger> AddSubject(D_Subject subject)
        {
            return Task.FromResult(client.AddSubject(subject));
        }
        public Task<D_Subjects> GetSubjects(UserDbInfomation information)
        {
            return Task.FromResult(client.GetSubjects(information));
        }
        #endregion
        #region Project Theme
        public Task<intger> AddProjectTheme(D_ProjectTheme theme)
        {
            return Task.FromResult(client.AddProjectTheme(theme));
        }
        public Task<D_ProjectThemes> Get_ProjectThemes(UserDbInfomation infomation)
        {
            return Task.FromResult(client.GetProjectThemes(infomation));
        }
        public Task<D_ProjectThemes> Get_ProjectThemesFromSubject(ThemeFromSubject infomation)
        {
            return Task.FromResult(client.GetProjectThemesFromSubject(infomation));
        }
        public Task<intger> RemoveProjectTheme(ProjectThemeUserInfomation infomation)
        {
            return Task.FromResult(client.RemoveProjectTheme(infomation));
        }
        public Task<intger> AddProjectThemeCoTeacher(ProjectThemeUserInfomation infomation)
        {
            return Task.FromResult(client.AddProjectThemeCoTeacher(infomation));
        }
        public Task<intger> RemoveProjectThemeCoTeacher(ProjectThemeUserInfomation infomation)
        {
            return Task.FromResult(client.RemoveProjectThemeCoTeacher(infomation));
        }
        #endregion
    }
}
