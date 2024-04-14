using HotChocolate.Execution;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeGraphql.API.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        //private  IRequestExecutor _requestExecutor;

        //public HomeController(IRequestExecutor requestExecutor)
        //{
        //    _requestExecutor = requestExecutor;
        //}
        [HttpGet]
        public  async Task<IEnumerable<string>> Get()
        {
            //var query = @"
            //                query GetEmployees {
            //                  employees {
            //                    __typename
            //                    ...EmployeeDetails
            //                  }
            //                }

            //                fragment EmployeeDetails on EmployeeUnion{
            //                  ... on IEmployee {
            //                    id
            //                    name
            //                    department
            //                    status
            //                    type
            //                  }
            //                  ... on FullTimeEmployee {
            //                    salary
            //                  }
            //                  ... on PartTimeEmployee {
            //                    hourlyRate
            //                  }
            //                }              
            //            ";

            //IExecutionResult executionResult = await _requestExecutor.ExecuteAsync(query);
            //var result = executionResult.ToJson();

            return new string[] { "Hello", "World" };

           
        }


    }
}