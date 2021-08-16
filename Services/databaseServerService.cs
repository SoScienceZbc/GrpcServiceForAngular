using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseService_Grpc;
using Grpc.Core;

namespace GrpcServiceForAngular.Services
{
    public class DatabaseServerService : GrpcDatabaseProject.GrpcDatabaseProjectBase
    {
        //This is the Angular Server side og the application.
        private static ProxyToDataServerHandler proxy = new ProxyToDataServerHandler();
     
        #region Project
        public override Task<D_Project> GetProject(UserDbInfomation infomation, ServerCallContext context)
        {
            Console.WriteLine($"Host:{context.Host} called Method:{context.Method}");
            return proxy.GetProject(infomation);
        }
        public override Task<intger> AddProject(ProjectUserInfomation infomation, ServerCallContext context)
        {
            Console.WriteLine($"Host:{context.Host} called Method:{context.Method}");
            return proxy.AddProject(infomation);
        }
        public override Task<intger> EditProject(ProjectUserInfomation infomation, ServerCallContext context)
        {
            return proxy.EditProject(infomation);
        }

        public override Task<intger> RemoveProject(ProjectUserInfomation infomation, ServerCallContext context)
        {
            return proxy.RemoveProject(infomation);
        }

        public override Task<D_Projects> GetProjects(UserDbInfomation infomation, ServerCallContext context)
        {            
            return proxy.GetProjects(infomation);
        }

        #endregion
        #region Docoment
        public override Task<D_Documents> GetDocuments(UserDbInfomation infomation,ServerCallContext context)
        {
            return proxy.GetDocuments(infomation);
        }
        // documents
        public override Task<intger> AddDocument(D_Document infomation, ServerCallContext context)
        {
            return proxy.AddDocument(infomation);
        }
        public override Task<D_Document> GetDocument(UserDbInfomation infomation, ServerCallContext context)
        {
            return proxy.GetDocument(infomation);
        }
        public override Task<intger> UpdateDocument(D_Document infomation, ServerCallContext context)
        {
            return proxy.UpdateDocument(infomation);
        }

        public override Task<intger> RemoveDocument(ProjectUserInfomation infomation,ServerCallContext context) 
        {
            return proxy.RemoveDocument(infomation);
        }
        #endregion
        #region Remote
        public override Task<intger> AddRemoteFile(D_RemoteFile infomation, ServerCallContext context) {
            return proxy.AddRemoteFile(infomation);
        }
        public override Task<D_RemoteFile> GetRemoteFile(UserDbInfomation infomation, ServerCallContext context) {
            return proxy.GetRemoteFile(infomation);
        }
        public override Task<D_RemoteFile> UpdateRemoteFile(D_RemoteFile infomation, ServerCallContext context) {
            return proxy.UpdateRemoteFile(infomation);
        }
        public override Task<intger> RemoveRemoteFile(UserDbInfomation infomation, ServerCallContext context) 
        {
            return proxy.RemoveRemoteFile(infomation);
        }
        public override Task<D_RemoteFiles> GetRemoteFiles(UserDbInfomation infomation, ServerCallContext context) {
            return proxy.GetRemoteFiles(infomation);
        }
        #endregion

    }
}
