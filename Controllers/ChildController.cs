﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using WebApi.Models.Children;
using WebApi.Services;

namespace WebApi.Controllers
{
    /// <summary>
    /// This is Children Controller And It has details for Chidren.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ChildController : ControllerBase
    {
        private readonly IChildService _childService;
       
        /// <summary>
        /// Constructor for Child Controller
        /// </summary>
        /// <param name="childService"></param>
        /// <param name="mapper"></param>
        public ChildController(IChildService childService, IMapper mapper)
        {
            _childService = childService;
        }

        /// <summary>
        /// Add New Child Details
        /// </summary>
        /// <param name="model">model</param>
        /// <returns></returns>
        [HttpPost("add-new-children")]
        public IActionResult AddNewChildDetails(ChildrenRequest model)
        {
            _childService.AddNewChildDetails(model);
            return Ok(new { message = "Registration successful, please check your email for verification instructions" });
        }

        /// <summary>
        /// Update Children Details
        /// </summary>
        /// <param name="ChildId">ChildId</param>
        /// <param name="model">model</param>
        /// <returns></returns>
        [HttpPut("update-existing-children")]
        public IActionResult UpdateChildrenDetails(Guid ChildId, ChildrenRequest model)
        {
            var response = _childService.UpdateChildrenDetails(ChildId, model);
            return Ok(response);
        }

        /// <summary>
        /// Get Chidren By Child Id
        /// </summary>
        /// <param name="childId">childId</param>
        /// <returns></returns>
        [HttpGet("get-children-By-ChildId")]
        public IActionResult GetChidrenByChildId(Guid childId)
        {
            var response = _childService.GetChildrenDetails(childId);
            return Ok(response);
        }


        /// <summary>
        /// Get Chidren By Child Id
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        [HttpGet("get-all-children-By-AccountId")]
        public IActionResult GetChidrenByChildId(int accountId)
        {
            var response = _childService.GetAllChildrenDetailsByAccountId(accountId);
            return Ok(response);
        }

        /// <summary>
        /// Delete Children Details
        /// </summary>
        /// <param name="childId"></param>
        /// <returns></returns>
        [HttpDelete("delete-child")]
        public IActionResult DeleteChildrenDetails(Guid childId)
        {
            var response = _childService.DeleteChildrenDetails(childId);
            return Ok(response);
        }

    }
}