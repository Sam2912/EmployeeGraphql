using AutoMapper;
using EmployeeGraphql.API.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeGraphql.Unit.Tests.MappingTest
{
    public abstract class MappingBaseTests
    {
        protected readonly IMapper _mapper;
        public MappingBaseTests()
        {
            // Setup AutoMapper
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>(); // Add your AutoMapper profile here
            });

            _mapper = configuration.CreateMapper();
        }
    }
}
