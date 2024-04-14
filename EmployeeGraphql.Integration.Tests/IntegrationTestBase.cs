using System;
using System.Net.Http;
using System.Threading.Tasks;
using HotChocolate.AspNetCore;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace EmployeeGraphql.Integration.Tests
{
    public class IntegrationTestBase : IClassFixture<WebApplicationFactory<Program>>
    {
        protected readonly HttpClient _client;
        protected readonly WebApplicationFactory<Program> _factory;

        public IntegrationTestBase(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        // Optional: Add setup and teardown methods if needed
        // For example, you can override Dispose to clean up resources after tests
        public void Dispose()
        {
            _client.Dispose();
            _factory.Dispose();
        }
    }
}
