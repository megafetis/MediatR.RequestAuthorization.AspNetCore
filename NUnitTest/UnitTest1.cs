using System;
using System.Threading.Tasks;
using MediatR;
using MediatR.RequestAuthorization;
using MediatR.RequestAuthorization.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using TestCore;
using TestCore.TestCore;

namespace NUnitTest
{
    public class Tests
    {
        private IServiceProvider serviceProvider;

        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();
            services.AddMediatR(typeof(UserProfile).Assembly);
            services.AddTransient<IUserContext, HttpUserContext>();
            services.AddMediatRAuthorization(typeof(UserProfile).Assembly);

            services.AddSingleton<IHttpContextAccessor, MockHttpContextAccessor>();

            serviceProvider = services.BuildServiceProvider();
        }

        [Test]
        public async Task TestDenied()
        {

            var mediator = serviceProvider.GetService<IMediator>();

            UserProfile result = null;

            try
            {
                result = await mediator.Send(new GetUserProfileQuery());
            }
            catch (AccessDeniedException e)
            {
                result = null;
            }


            Assert.Null(result);
        }

        [Test]
        public async Task TestAllow()
        {
            var httpAccessor = serviceProvider.GetService<IHttpContextAccessor>();
            ((MockHttpContextAccessor)httpAccessor).SetUser();

            var mediator = serviceProvider.GetService<IMediator>();

            UserProfile result = null;

            try
            {
                result = await mediator.Send(new GetUserProfileQuery());
            }
            catch (AccessDeniedException e)
            {
                result = null;
            }


            Assert.NotNull(result);
        }
    }
}