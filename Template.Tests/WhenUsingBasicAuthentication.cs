using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Template.Api.Middlewares.BasicAuthentication;


namespace Template.Tests
{
    [TestClass]
    public class WhenUsingBasicAuthentication
    {
        [TestMethod]
        public async Task GivenValidAuthentication_ShouldCallNext()
        {
            var wasCalled = false;

            var middleware = new BasicAuthenticationMiddleware(
                ctx => {
                    wasCalled = true;
                    return Task.CompletedTask;
                },
                new BasicAuthenticationConfiguration("test", "token"));

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Path = "/api/whatever";
            // 'dGVzdDp0b2tlbg==' is 'test:token' base64 encoded
            httpContext.Request.Headers.Add("Authorization", "Basic dGVzdDp0b2tlbg==");

            await middleware.Invoke(httpContext);

            wasCalled
                .Should()
                .BeTrue();
        }

        [TestMethod]
        public async Task GivenValidAuthenticationWithColon_ShouldCallNext()
        {
            var wasCalled = false;

            var middleware = new BasicAuthenticationMiddleware(
                ctx => {
                    wasCalled = true;
                    return Task.CompletedTask;
                },
                new BasicAuthenticationConfiguration("test", ":token"));

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Path = "/api/whatever";
            // 'dGVzdDo6dG9rZW4=' is 'test::token' base64 encoded
            httpContext.Request.Headers.Add("Authorization", "Basic dGVzdDo6dG9rZW4=");

            await middleware.Invoke(httpContext);

            wasCalled
                .Should()
                .BeTrue();
        }

        [TestMethod]
        public async Task GivenInValidAuthentication_ShouldNotCallNext()
        {
            var wasCalled = false;

            var middleware = new BasicAuthenticationMiddleware(
                ctx => {
                    wasCalled = true;
                    return Task.CompletedTask;
                },
                new BasicAuthenticationConfiguration("test", "token"));

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Path = "/api/whatever";
            // 'dGVzdDp0b2tlbg==' is 'test:token' base64 encoded
            httpContext.Request.Headers.Add("Authorization", "Basic GVzdDp0b2tlbg==");

            await middleware.Invoke(httpContext);

            wasCalled
                .Should()
                .BeFalse();
        }

        [TestMethod]
        public async Task GivenInValidAuthentication_ShouldReturn401()
        {
            var middleware = new BasicAuthenticationMiddleware(
                ctx => Task.CompletedTask,
                new BasicAuthenticationConfiguration("test", "token"));

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Path = "/api/whatever";
            // 'dGVzdDp0b2tlbg==' is 'test:token' base64 encoded
            httpContext.Request.Headers.Add("Authorization", "Basic GVzdDp0b2tlbg==");

            await middleware.Invoke(httpContext);

            httpContext.Response.StatusCode
                .Should()
                .Be(401);
        }

        [TestMethod]
        public async Task GivenNoAuthentication_ShouldNotCallNext()
        {
            var wasCalled = false;

            var middleware = new BasicAuthenticationMiddleware(
                ctx => {
                    wasCalled = true;
                    return Task.CompletedTask;
                },
                new BasicAuthenticationConfiguration("test", "token"));

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Path = "/api/whatever";

            await middleware.Invoke(httpContext);

            wasCalled
                .Should()
                .BeFalse();
        }

        [TestMethod]
        public async Task GivenNoAuthentication_ShouldReturn401()
        {
            var middleware = new BasicAuthenticationMiddleware(
                ctx => Task.CompletedTask,
                new BasicAuthenticationConfiguration("test", "token"));

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Path = "/api/whatever";

            await middleware.Invoke(httpContext);

            httpContext.Response.StatusCode
                .Should()
                .Be(401);
        }
    }
}