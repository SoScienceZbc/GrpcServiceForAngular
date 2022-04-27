using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;

namespace GrpcServiceForAngular.Services
{
    public class VideoService
    {
        private static VideoToDataServerHandler videoHandler = new VideoToDataServerHandler();
    }
}
