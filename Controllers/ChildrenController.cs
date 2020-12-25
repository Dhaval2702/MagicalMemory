using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Children;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChildrenController : BaseController
    {
        private readonly IChildrenService _childrenService;
        private readonly IMapper _mapper;

        /// <summary>
        /// This is constructor for Account Controller
        /// </summary>
        /// <param name="childrenService">account Service</param>
        /// <param name="mapper">mapper</param>
        public ChildrenController(
            IChildrenService childrenService,
            IMapper mapper)
        {
            _childrenService = childrenService;
            _mapper = mapper;
        }


        /// <summary>
        /// Register new User with Registration Request.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// 
        [Authorize]
        [HttpPost("add-new-children")]
        public IActionResult AddNewChildren(int accountID ,  DependentChildrenRequest model)
        {
            _childrenService.AddDependentChildren(accountID,model);
            return Ok(new { message = "Dependent Children Added SuccessFully!!!" });
        }



    }
}
