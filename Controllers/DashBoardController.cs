﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DashBoardController : ControllerBase
    {
        private readonly IDashBoardService _dashBoardService;

        public DashBoardController(
          IDashBoardService dashBoardService)
        {
            _dashBoardService = dashBoardService;
        }


        [HttpGet("prepare-dashboard")]
        public IActionResult PrepareUserDashBoard(int accountId,Guid childId)
        {
          var response =  _dashBoardService.PrepareUserDashboard(accountId, childId);
           return Ok(response);
        }





    }
}
