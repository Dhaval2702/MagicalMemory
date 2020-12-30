using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Helpers;
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
            Guid childId = _childService.AddNewChildDetails(model);
            //Update ChildPayment History
            _childService.AddChildrenPaymentHistory(model.AccountId, childId, model.ChildDOB);
            return Ok(new { message = ValidationMessages.Messages.NewChildrenAddConfirmation });
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

        [HttpPut("upload-photo-memory")]
        public IActionResult UploadPhotoMemory(Guid childId, Guid memoryId, string memoryName, string memoryYear, List<IFormFile> formFiles)
        {
            var response = _childService.AddUpdateChildMemories(childId, memoryId, memoryName, memoryYear, formFiles);
            return response ?
                 Ok("Hey Mom! You have Added our Memory Successfuly.") : Problem("Hey Mom! It looks like We are not subscribed to This Year.");
        }


        [HttpGet("get-child-photo-memory")]
        public async Task<IActionResult> GetChildmemory(Guid childId, string memoryYear)
        {
            var response = _childService.getChildmemory(childId, memoryYear);
            return response.Count > 0 ? 
                Ok(response) : Problem("Hey Mom! Either We are not subscribed to This Year or We have not uploded any Photos.");
        }

        [HttpGet("get-child-vidio-memory")]
        public FileResult Get(Guid childId, Guid memoryId, string memoryYear)
        {
            string vidiopath = _childService.GetVideoByMemoryId(childId, memoryId, memoryYear);
            return PhysicalFile(vidiopath, "application/octet-stream", enableRangeProcessing: true);
        }

    }
}
