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
            Console.WriteLine($"Host:{context.Host} called Method:{context.Method}");
            MediaReply vr = mediaHandler.SendMedia(request).Result;
            Console.WriteLine("MediaReply: " + vr.ReplySuccessfull);
            return mediaHandler.SendMedia(request);
        }
    }
}
