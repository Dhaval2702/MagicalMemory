using Microsoft.AspNetCore.Mvc;
using WebApi.Services;

namespace WebApi.Controllers
{
    /// <summary>
    /// Dash Board Controller
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class DashBoardController : ControllerBase
    {
        private readonly IDashBoardService _dashBoardService;

        /// <summary>
        /// Dash Board Controller
        /// </summary>
        /// <param name="dashBoardService">dash Board Service</param>
        public DashBoardController(
          IDashBoardService dashBoardService)
        {
            _dashBoardService = dashBoardService;
        }

        /// <summary>
        /// Prepare User DashBoard
        /// </summary>
        /// <param name="accountId">account Id</param>
        /// <param name="childId">child Id</param>
        /// <returns></returns>
        [HttpGet("prepare-dashboard")]
        public IActionResult PrepareUserDashBoard(int accountId)
        {
          var response =  _dashBoardService.PrepareUserDashboard(accountId);
           return Ok(response);
        }

    }
}
