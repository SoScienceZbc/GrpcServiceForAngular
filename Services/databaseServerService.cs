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

        #region Project
        public override Task<D_Project> GetProject(UserDbInfomation infomation, ServerCallContext context)
        {
            Console.WriteLine($"Host:{context.Host} called Method:{context.Method}");
            return new ProxyToDataServerHandler().GetProject(infomation);
        }
        public override Task<intger> AddProject(ProjectUserInfomation infomation, ServerCallContext context)
        {
            Console.WriteLine($"Host:{context.Host} called Method:{context.Method}");
            return new ProxyToDataServerHandler().AddProject(infomation);
        }
        public override Task<intger> EditProject(ProjectUserInfomation infomation, ServerCallContext context)
        {
            return new ProxyToDataServerHandler().EditProject(infomation);
        }

        public override Task<intger> RemoveProject(ProjectUserInfomation infomation, ServerCallContext context)
        {
            return new ProxyToDataServerHandler().RemoveProject(infomation);
        }

        public override Task<D_Projects> GetProjects(UserDbInfomation infomation, ServerCallContext context)
        {            
            return new ProxyToDataServerHandler().GetProjects(infomation);
        }

        #endregion
        #region Docoment
        public override Task<D_Documents> GetDocuments(UserDbInfomation infomation,ServerCallContext context)
        {
            return new ProxyToDataServerHandler().GetDocuments(infomation);
        }
        // documents
        public override Task<intger> AddDocument(D_Document infomation, ServerCallContext context)
        {
            return new ProxyToDataServerHandler().AddDocument(infomation);
        }
        public override Task<D_Document> GetDocument(UserDbInfomation infomation, ServerCallContext context)
        {
            return new ProxyToDataServerHandler().GetDocument(infomation);
        }
        public override Task<intger> UpdateDocument(D_Document infomation, ServerCallContext context)
        {
            return new ProxyToDataServerHandler().UpdateDocument(infomation);
        }

        public override Task<intger> RemoveDocument(ProjectUserInfomation infomation,ServerCallContext context) 
        {
            return new ProxyToDataServerHandler().RemoveDocument(infomation);
        }
        #endregion
        #region Remote
        public override Task<intger> AddRemoteFile(D_RemoteFile infomation, ServerCallContext context) {
            return new ProxyToDataServerHandler().AddRemoteFile(infomation);
        }
        public override Task<D_RemoteFile> GetRemoteFile(UserDbInfomation infomation, ServerCallContext context) {
            return new ProxyToDataServerHandler().GetRemoteFile(infomation);
        }
        public override Task<D_RemoteFile> UpdateRemoteFile(D_RemoteFile infomation, ServerCallContext context) {
            return new ProxyToDataServerHandler().UpdateRemoteFile(infomation);
        }
        public override Task<intger> RemoveRemoteFile(UserDbInfomation infomation, ServerCallContext context) 
        {
            return new ProxyToDataServerHandler().RemoveRemoteFile(infomation);
        }
        public override Task<D_RemoteFiles> GetRemoteFiles(UserDbInfomation infomation, ServerCallContext context) {
            return new ProxyToDataServerHandler().GetRemoteFiles(infomation);
        }
        #endregion
        #region Subject
        public override Task<intger> AddSubject(D_Subject request, ServerCallContext context)
        {
            return new ProxyToDataServerHandler().AddSubject(request);
        }
        #endregion
    }
}
