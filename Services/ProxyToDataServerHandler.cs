﻿using DatabaseService_Grpc;
using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcServiceForAngular.Services
{
    /// <summary>
    /// This is responsle for talking to the ZbcMainDataServer via grpc and ssh tunnel.
    /// </summary>
    public class ProxyToDataServerHandler
    {
        private GrpcDatabaseProject.GrpcDatabaseProjectClient channel;
        public ProxyToDataServerHandler()
        {
            AppContext.SetSwitch(
    "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2Support", true);
            channel = new GrpcDatabaseProject.GrpcDatabaseProjectClient(
                GrpcChannel.ForAddress("http://192.168.1.101:5200",
                new GrpcChannelOptions
                {
                    Credentials = /*new Grpc.Core.SslCredentials()*/ Grpc.Core.ChannelCredentials.Insecure,

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
        /// <summary>
        /// This is a example on how to use grpc.. 
        /// </summary>
        /// <param name="infomation"></param>
        /// <returns></returns>
        public D_Projects GetProjectsLite(UserDbInfomation infomation)
        {
            using var channel = GrpcChannel.ForAddress("http://192.168.1.101:5003", new GrpcChannelOptions { Credentials = /*new Grpc.Core.SslCredentials()*/ Grpc.Core.ChannelCredentials.Insecure });
            var ts = new GrpcDatabaseProject.GrpcDatabaseProjectClient(channel);
            UserDbInfomation mortUser = new UserDbInfomation { DbName = "mort286f", ID = 199 };
            return ts.GetProjects(mortUser);

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

        public Task<intger> RemoveDocument(UserDbInfomation infomation)
        {
            return Task.FromResult(channel.RemoveRemoteFile(infomation));

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
    }
}
