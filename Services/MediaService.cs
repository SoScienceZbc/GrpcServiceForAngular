using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Proto;

namespace GrpcServiceForAngular.Services
{
    public class MediaService : RemoteMediaService.RemoteMediaServiceBase
    {
        private static MediaToDataServerHandler mediaHandler = new MediaToDataServerHandler();
        public override Task<MediaReply> SendMedia(MediaRequest request, ServerCallContext context)
        {
            MediaReply mr = new MediaReply();
            try
            {
                Console.WriteLine($"Host:{context.Host} called Method:{context.Method}");
                mr = mediaHandler.SendMedia(request).Result;
                Console.WriteLine("MediaReply: " + mr.ReplySuccessfull);
            }
            catch(Exception e)
            {
                Console.WriteLine("InnerException: " + e.InnerException);
                Console.WriteLine("Error: " + e.Message);
            }

            return Task.FromResult(mr);
        }
        public override Task<MediaRequests> GetMedias(ProjectInformation project, ServerCallContext context)
        {
            Console.WriteLine($"Host:{context.Host} called Method:{context.Method}");
            return mediaHandler.GetMedias(project);
        }
        public override Task<RetrieveMediaReply> RetrieveMedia(RetrieveMediaRequest request, ServerCallContext context)
        {
            Console.WriteLine($"Host:{context.Host} called Method:{context.Method}");
            return mediaHandler.RetrieveMedia(request);
        }
        public override Task<MediaReply> DeleteMedia(RetrieveMediaRequest request, ServerCallContext context)
        {
            Console.WriteLine($"Host:{context.Host} called Method:{context.Method}");
            return mediaHandler.DeleteMedia(request);
        }
        public override Task<MediaReply> UpdateMedia(ChangeTitleRequest request, ServerCallContext context)
        {
            Console.WriteLine($"Host:{context.Host} called Method:{context.Method}");
            return mediaHandler.UpdateMedia(request);
        }
    }
}
