using Autofac;
using EmployeeGraphql.API.Mutation;
using EmployeeGraphql.API;
using EmployeeGraphql.API.Services;
using HotChocolate.Execution;
using HotChocolate.Language;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System;
using TechTalk.SpecFlow;
using System.Net;
using System.Text.Json;
using HotChocolate.Execution.Processing;
using static HotChocolate.ErrorCodes;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Runtime.CompilerServices;
using HotChocolate;

namespace EmployeeGraphql.Integration.Tests
{
    [Binding]
    public class EmployeeStepDefinitions : IntegrationTestBase
    {
        private readonly HttpClient _client;
        public RequestExecutorProxy Executor { get; }

        public EmployeeStepDefinitions(WebApplicationFactory<Program> factory) : base(factory)
        {
            Executor = _factory.Services.GetRequiredService<RequestExecutorProxy>();
        }

        [Given(@"I have database setup")]
        public async Task GivenIHaveDatabaseSetupAsync()
        {

            // Arrange
            var query = @"query{
                              employees {
                                __typename
                                ...EmployeeDetails
                              }
                            }

                            fragment EmployeeDetails on EmployeeUnion{
                              ... on IEmployee {
                                id
                                name
                                department
                                status
                                type
                              }
                              ... on FullTimeEmployee {
                                salary
                              }
                              ... on PartTimeEmployee {
                                hourlyRate
                              }
                            }              
                        ";

            var request = QueryRequestBuilder.New()
                                     .SetQuery(query)
                                     .SetGlobalState("Authorization", "Bearer eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImxheC5jaGEyOSIsInJvbGUiOlsiSVQiLCJBZG1pbiJdLCJuYmYiOjE3MTMwNzczMzksImV4cCI6MTcxMzA4MDkzOSwiaWF0IjoxNzEzMDc3MzM5LCJpc3MiOiJodHRwOi8vbG9jYWxob3N0OjUwMTgiLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjUwMTgifQ.Wl2hkAruK0gTvMupMrEjFzTwSCCq2i0n1aI2UJiBaZLi1VNaJLrWRuizGE5LO3P0lKuQY8Vlhwpqi7knsUeK8A")
                                     .SetGlobalState("HotChocolate.Authorization.UserState", new Dictionary<string, object>
                                        {
                                            { "Roles", new List<string> { "Admin" } } // Replace with actual user roles if needed
                                        })
                                     .Create();

            IExecutionResult executionResult = await Executor.ExecuteAsync(request);
            string v = executionResult.ToJson();
        }

        [Given(@"I have employee to add")]
        public void GivenIHaveEmployeeToAdd()
        {
            //throw new PendingStepException();
        }

        [When(@"I call the graphql endpoint")]
        public void WhenICallTheGraphqlEndpoint()
        {
            //throw new PendingStepException();
        }

        [Then(@"I shoud see employee is saved")]
        public void ThenIShoudSeeEmployeeIsSaved()
        {
            //throw new PendingStepException();
        }
    }
}
