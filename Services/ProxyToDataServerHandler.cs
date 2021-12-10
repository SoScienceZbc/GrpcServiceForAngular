using DatabaseService_Grpc;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GrpcServiceForAngular.Services
{
    /// <summary>
    /// This is responsle for talking to the ZbcMainDataServer via grpc and ssh tunnel.
    /// </summary>
    public class ProxyToDataServerHandler
    {
        /// <summary>
        /// This channel is the client that calls the rpc on the sshagentMainDataServer
        /// </summary>
        GrpcWebHandler handler = new GrpcWebHandler(GrpcWebMode.GrpcWebText, new HttpClientHandler());
        private static GrpcDatabaseProject.GrpcDatabaseProjectClient channel;
        public ProxyToDataServerHandler()
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2Support", true);
            channel = new GrpcDatabaseProject.GrpcDatabaseProjectClient(
                GrpcChannel.ForAddress("https://127.0.0.1:33701",
                new GrpcChannelOptions
                {
                    HttpClient = new HttpClient(handler),
                    Credentials = new Grpc.Core.SslCredentials() // Grpc.Core.ChannelCredentials.Insecure,                    

                }));
        }

        /// <summary>
        /// This is used for testing, DONT USE IN PRODUCTION!!
        /// </summary>
        /// <param name="infomation"></param>
        public void DataServiceGet(UserDbInfomation infomation)
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2Support", true);
            // The port number(5001) must match the port of the gRPC server.
            using var channel = GrpcChannel.ForAddress("http://192.168.1.101:5200", new GrpcChannelOptions { Credentials = /*new Grpc.Core.SslCredentials()*/ Grpc.Core.ChannelCredentials.Insecure });
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
        }

        #region Project
        public Task<D_Project> GetProject(UserDbInfomation infomation)
        {
            return Task.FromResult(channel.GetProject(infomation));
        }
        public Task<intger> AddProject(ProjectUserInfomation infomation)
        {
            return Task.FromResult(channel.AddProject(infomation));
        }
        public Task<intger> EditProject(ProjectUserInfomation infomation)
        {
            return Task.FromResult(channel.EditProject(infomation));
        }

        public Task<intger> RemoveProject(ProjectUserInfomation infomation)
        {
            return Task.FromResult(channel.RemoveProject(infomation));
        }

        public Task<D_Projects> GetProjects(UserDbInfomation infomation)
        {
            return Task.FromResult(channel.GetProjects(infomation));
        }
        public Task<intger> AddProjectMember(MemberInformation information)
        {
            return Task.FromResult(channel.AddProjectMember(information));
        }
        public Task<intger> RemoveProjectMember(MemberInformation information)
        {
            return Task.FromResult(channel.RemoveProjectMember(information));
        }
        #endregion
        #region Docoment
        public Task<D_Documents> GetDocuments(UserDbInfomation infomation)
        {
            return Task.FromResult(channel.GetDocuments(infomation));
        }

        public Task<intger> AddDocument(D_Document infomation)
        {
            return Task.FromResult(channel.AddDocument(infomation));
        }
        public Task<D_Document> GetDocument(UserDbInfomation infomation)
        {
            return Task.FromResult(channel.GetDocument(infomation));
        }
        public Task<intger> UpdateDocument(D_Document infomation)
        {
            return Task.FromResult(channel.UpdateDocument(infomation));
        }

        public Task<intger> RemoveDocument(ProjectUserInfomation infomation)
        {
            return Task.FromResult(channel.RemoveDocument(infomation));

        }
        #endregion
        #region Remote
        public Task<intger> AddRemoteFile(D_RemoteFile infomation)
        {
            return Task.FromResult(channel.AddRemoteFile(infomation));
        }
        public Task<D_RemoteFile> GetRemoteFile(UserDbInfomation infomation)
        {

            return Task.FromResult(channel.GetRemoteFile(infomation));
        }
        public Task<D_RemoteFile> UpdateRemoteFile(D_RemoteFile infomation)
        {
            return Task.FromResult(channel.UpdateRemoteFile(infomation));
        }
        public Task<intger> RemoveRemoteFile(UserDbInfomation infomation)
        {
            return Task.FromResult(channel.RemoveRemoteFile(infomation));
        }
        public Task<D_RemoteFiles> GetRemoteFiles(UserDbInfomation infomation)
        {
            return Task.FromResult(channel.GetRemoteFiles(infomation));
        }
        #endregion
        #region Subject
        public Task<intger> AddSubject(D_Subject subject)
        {
            return Task.FromResult(channel.AddSubject(subject));
        }
        public Task<D_Subjects> GetSubjects(UserDbInfomation information)
        {
            return Task.FromResult(channel.GetSubjects(information));
        }
        #endregion
        #region Project Theme
        public Task<intger> AddProjectTheme(D_ProjectTheme theme)
        {
            return Task.FromResult(channel.AddProjectTheme(theme));
        }
        public Task<D_ProjectThemes> Get_ProjectThemes(UserDbInfomation infomation)
        {
            return Task.FromResult(channel.GetProjectThemes(infomation));
        }
        public Task<D_ProjectThemes> Get_ProjectThemesFromSubject(ThemeFromSubject infomation)
        {
            return Task.FromResult(channel.GetProjectThemesFromSubject(infomation));
        }
        public Task<intger> RemoveProjectTheme(ProjectThemeUserInfomation infomation)
        {
            return Task.FromResult(channel.RemoveProjectTheme(infomation));
        }
        public Task<intger> AddProjectThemeCoTeacher(ProjectThemeUserInfomation infomation)
        {
            return Task.FromResult(channel.AddProjectThemeCoTeacher(infomation));
        }
        public Task<intger> RemoveProjectThemeCoTeacher(ProjectThemeUserInfomation infomation)
        {
            return Task.FromResult(channel.RemoveProjectThemeCoTeacher(infomation));
        }
        #endregion
    }
}
