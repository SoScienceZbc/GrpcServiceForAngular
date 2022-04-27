using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Proto;

namespace GrpcServiceForAngular.Services
{
    public class VideoService : RemoteMediaService.RemoteMediaServiceBase
    {
        private static VideoToDataServerHandler videoHandler = new VideoToDataServerHandler();

        public override Task<VideoReply> SendVideo(VideoRequest request, ServerCallContext context)
        {
            Console.WriteLine($"Host:{context.Host} called Method:{context.Method}");
            return videoHandler.SendVideo(request);
        }
    }
}
