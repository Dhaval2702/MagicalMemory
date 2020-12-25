using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models.Children;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChildController : ControllerBase
    {
        private readonly IChildService _childService;
        private readonly IMapper _mapper;
        public ChildController(IChildService childService, IMapper mapper)
        {
            _childService = childService;
            _mapper = mapper;
        }

        [HttpPost("add-new-memory")]
        public IActionResult AddNewMemory(ChildrenRequest model)
        {
            _childService.AddNewChildMemory(model);
            return Ok(new { message = "Registration successful, please check your email for verification instructions" });
        }


    }

  

}
