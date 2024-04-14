using EmployeeGraphql.API.Authorization;
using EmployeeGraphql.API.Constants;
using EmployeeGraphql.API.Models;
using EmployeeGraphql.API.Query;
using EmployeeGraphql.API.Services;
using EmployeeGraphql.API.Types;
using EmployeeGraphql.API.Types.Authorization;

namespace EmployeeGraphql.API
{
    public class EmployeeQueryType : ObjectType<IEmployeeQueryResolver>
    {
        protected override void Configure(IObjectTypeDescriptor<IEmployeeQueryResolver> descriptor)
        {
            //descriptor.Authorize();


  descriptor.Field(f=>f.GetAllEmployeesFilteredAsync())
                .Name("employeesFiltered")
                .Type<ListType<EmployeeUnion>>()
                .UseFiltering()
                .UseSorting();

            descriptor.Field(f => f.GetAllEmployeesAsync())
                .Name("employees")
                .Type<ListType<EmployeeUnion>>()
                .Authorize(EmployeeConstant.ADMIN_POLICY);

            descriptor.Field(f => f.GetEmployeeByIdAsync(default))
                .Name("employee")
                .Type<EmployeeUnion>()
                .Argument("id", a => a.Type<IdType>()
                //.DefaultValue("0a001814934f4f3592a7bfa8ca356078")
                );


            descriptor.Field(f => f.GetAsync(default, default))
            .Name("filteredEmployee")
            .Type<ListType<EmployeeUnion>>()
            .Argument("dept", a => a.Type<DepartmentType>())
            .Argument("status", a => a.Type<StatusType>());

            descriptor.Field(f => f.GenerateJwtTokenAsync(default, default))
                .Name("generateJwtToken")
                .Type<AuthPayload>()
                .Argument("username", a => a.Type<StringType>())
                .Argument("password", a => a.Type<StringType>())
                .AllowAnonymous();
        }
    }
}


