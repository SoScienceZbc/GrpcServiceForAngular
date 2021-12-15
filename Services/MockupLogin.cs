using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Proto;

namespace GrpcServiceForAngular.Services
{
    public class MockupLogin : loginMockup.loginMockupBase
    {
        public override Task<MockLoginReply> loginMockupSuccessful(MockLoginRequest request, ServerCallContext context)
        {
            Console.WriteLine("Request is: " + request.Username);
            MockLoginReply mockLoginReply = new MockLoginReply();
            mockLoginReply.MockLoginSuccess = true;
            return Task.FromResult(mockLoginReply);
        }
    }
}
