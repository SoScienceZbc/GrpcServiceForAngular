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
        public override Task<D_Project> GetProject(UserDbInfomation Request, ServerCallContext context)
        {
            Console.WriteLine($"Host:{context.Host} called Method:{context.Method}");
            return proxy.GetProject(Request);
        }
        public override Task<intger> AddProject(ProjectUserInfomation Request, ServerCallContext context)
        {
            Console.WriteLine($"Host:{context.Host} called Method:{context.Method}");
            return proxy.AddProject(Request);
        }
        public override Task<intger> EditProject(ProjectUserInfomation Request, ServerCallContext context)
        {
            return proxy.EditProject(Request);
        }
        public override Task<intger> RemoveProject(ProjectUserInfomation Request, ServerCallContext context)
        {
            return proxy.RemoveProject(Request);
        }
        public override Task<D_Projects> GetProjects(UserDbInfomation Request, ServerCallContext context)
        {
            Console.WriteLine("Entered databaseServerService GetProjects");
            return proxy.GetProjects(Request);
        }
        public override Task<intger> AddProjectMember(MemberInformation Request, ServerCallContext context)
        {
            return proxy.AddProjectMember(Request);
        }
        public override Task<intger> RemoveProjectMember(MemberInformation Request, ServerCallContext context)
        {
            return proxy.RemoveProjectMember(Request);
        }
        #endregion
        #region Docoment
        public override Task<D_Documents> GetDocuments(UserDbInfomation Request,ServerCallContext context)
        {
            return proxy.GetDocuments(Request);
        }
        // documents
        public override Task<intger> AddDocument(D_Document Request, ServerCallContext context)
        {
            return proxy.AddDocument(Request);
        }
        public override Task<D_Document> GetDocument(UserDbInfomation Request, ServerCallContext context)
        {
            return proxy.GetDocument(Request);
        }
        public override Task<intger> UpdateDocument(D_Document Request, ServerCallContext context)
        {
            return proxy.UpdateDocument(Request);
        }

        public override Task<intger> RemoveDocument(ProjectUserInfomation Request,ServerCallContext context) 
        {
            return proxy.RemoveDocument(Request);
        }
        #endregion
        #region Remote
        public override Task<intger> AddRemoteFile(D_RemoteFile Request, ServerCallContext context) {
            return proxy.AddRemoteFile(Request);
        }
        public override Task<D_RemoteFile> GetRemoteFile(UserDbInfomation Request, ServerCallContext context) {
            return proxy.GetRemoteFile(Request);
        }
        public override Task<D_RemoteFile> UpdateRemoteFile(D_RemoteFile Request, ServerCallContext context) {
            return proxy.UpdateRemoteFile(Request);
        }
        public override Task<intger> RemoveRemoteFile(UserDbInfomation Request, ServerCallContext context) 
        {
            return proxy.RemoveRemoteFile(Request);
        }
        public override Task<D_RemoteFiles> GetRemoteFiles(UserDbInfomation Request, ServerCallContext context) {
            return proxy.GetRemoteFiles(Request);
        }
        #endregion
        #region Subject
        public override Task<intger> AddSubject(D_Subject request, ServerCallContext context)
        {
            return proxy.AddSubject(request);
        }

        public override Task<D_Subjects> GetSubjects(UserDbInfomation request, ServerCallContext context)
        {
            return proxy.GetSubjects(request);
        }
        #endregion
        #region Project Theme
        public override Task<intger> AddProjectTheme(D_ProjectTheme request, ServerCallContext context)
        {
            return proxy.AddProjectTheme(request);
        }
        public override Task<D_ProjectThemes> GetProjectThemes(UserDbInfomation request, ServerCallContext context)
        {
            return proxy.Get_ProjectThemes(request);
        }
        public override Task<D_ProjectThemes> GetProjectThemesFromSubject(ThemeFromSubject request, ServerCallContext context)
        {
            return proxy.Get_ProjectThemesFromSubject(request);
        }
        public override Task<intger> AddProjectThemeCoTeacher(ProjectThemeUserInfomation request, ServerCallContext context)
        {
            return proxy.AddProjectThemeCoTeacher(request);
        }
        public override Task<intger> RemoveProjectTheme(ProjectThemeUserInfomation request, ServerCallContext context)
        {
            return proxy.RemoveProjectTheme(request);
        }
        public override Task<intger> RemoveProjectThemeCoTeacher(ProjectThemeUserInfomation request, ServerCallContext context)
        {
            return proxy.RemoveProjectThemeCoTeacher(request);
        }
        #endregion
    }
}
