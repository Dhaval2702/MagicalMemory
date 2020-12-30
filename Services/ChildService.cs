using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.Children;

namespace WebApi.Services
{
    /// <summary>
    /// Interface for Children Service
    /// </summary>
    public interface IChildService
    {
        Guid AddNewChildDetails(ChildrenRequest childrenRequest);
        ChildrenResponse UpdateChildrenDetails(Guid ChildId, ChildrenRequest childrenUpdateRequest);
        int DeleteChildrenDetails(Guid childId);
        ChildrenResponse GetChildrenDetails(Guid ChildId);
        List<ChildrenResponse> GetAllChildrenDetailsByAccountId(int accountId);
        bool AddUpdateChildMemories(Guid childId, Guid memoryId, string memoryName, string memoryYear, List<IFormFile> formFiles);
        List<ChildrenPaymentHistoryResponse> AddChildrenPaymentHistory(int accountId, Guid childId, DateTime childDob);
        List<ChildrenPaymentHistoryResponse> GetChildrenPaymentHistory(Guid childId);
        bool UpdateChildPaymentHistory(int accountId, Guid childId, string paymentYear);
        List<byte[]> getChildmemory(Guid childId, string memoryYear);
        string GetVideoByMemoryId(Guid childId, Guid memoryId, string memoryYear);
    }

    /// <summary>
    /// This is Children Service Class 
    /// </summary>
    public class ChildService : IChildService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IHostingEnvironment _hostingEnvironment;


        public ChildService(
         DataContext context,
         IMapper mapper, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        /// Add New Child Details
        /// </summary>
        /// <param name="childrenRequest">childrenRequest</param>
        /// <returns></returns>
        public Guid AddNewChildDetails(ChildrenRequest childrenRequest)
        {
            // map model to new account object
            var adnewChildren = _mapper.Map<Children>(childrenRequest);
            adnewChildren.ChildId = Guid.NewGuid();
            adnewChildren.CreatedDate = DateTime.Now;
            adnewChildren.UpdatedDate = null;
            _context.Children.Add(adnewChildren);
            _context.SaveChanges();
            return adnewChildren.ChildId;
        }

        /// <summary>
        /// Delete Children By Children Id
        /// </summary>
        /// <param name="childId"></param>
        /// <returns></returns>
        public int DeleteChildrenDetails(Guid childId)
        {
            var children = _context.Children
                           .Include(c => c.ChildMemory)
                           .Include(c => c.ChildSkill).Where(x => x.ChildId == childId).FirstOrDefault();
            if (children == null) throw new KeyNotFoundException("Children Not found!!!");

            // Remove Child Memory
            foreach (var childMemory in children.ChildMemory)
            {
                FileInfo file = new FileInfo(childMemory.MemoryPhoto);
                if (file.Exists)
                {
                    file.Delete();
                }
                _context.ChildMemory.Remove(childMemory);

            }
            
            // Remove Children Payment History.
            foreach (var childPaymentHistory in children.ChildPaymentHistory)
            {
                _context.ChildPaymentHistory.Remove(childPaymentHistory);
            }

            // Remove Child Skill.
            foreach (var childSkill in children.ChildSkill)
            {
                _context.ChildSkill.Remove(childSkill);
            }

            //Remove Children
            _context.Children.Remove(children);
            int isChildDeleted = _context.SaveChanges();
            return isChildDeleted;
        }

        /// <summary>
        /// Update children Details
        /// </summary>
        /// <param name="ChildId">ChildId</param>
        /// <param name="childrenUpdateRequest">childrenUpdateRequest</param>
        /// <returns></returns>
        public ChildrenResponse UpdateChildrenDetails(Guid ChildId, ChildrenRequest childrenUpdateRequest)
        {
            var children = _context.Children
                            .Include(c => c.ChildMemory)
                            .Include(c => c.ChildSkill).Where(x => x.ChildId == ChildId).FirstOrDefault();
            if (children == null) throw new KeyNotFoundException("Children Not found!!!");

            foreach (var chmemory in children.ChildMemory)
            {
                chmemory.UpdateDate = DateTime.Now;
            }

            foreach (var chSkill in children.ChildSkill)
            {
                chSkill.UpdatedDate = DateTime.Now;
            }

            _mapper.Map(childrenUpdateRequest, children);
            children.UpdatedDate = DateTime.UtcNow;
            _context.Children.Update(children);
            _context.SaveChanges();
            return _mapper.Map<ChildrenResponse>(children);
        }

        /// <summary>
        /// Get Children Details
        /// </summary>
        /// <param name="ChildId">ChildId</param>
        /// <returns></returns>
        public ChildrenResponse GetChildrenDetails(Guid ChildId)
        {
            ChildrenResponse childrenResponse = new ChildrenResponse();
            var childrenDetails = _context.Children
                           .Include(c => c.ChildMemory)
                           .Include(c => c.ChildSkill)
                           .Include(x => x.ChildPaymentHistory).Where(x => x.ChildId == ChildId).FirstOrDefault();

            return childrenDetails != null ? _mapper.Map<ChildrenResponse>(childrenDetails) : childrenResponse;
        }

        /// <summary>
        /// Get All Children Details By Account Id
        /// </summary>
        /// <param name="accountId">accountId</param>
        /// <returns></returns>
        public List<ChildrenResponse> GetAllChildrenDetailsByAccountId(int accountId)
        {
            List<ChildrenResponse> childrenResponse = new List<ChildrenResponse>();
            var childrenDetails = _context.Children
                                   .Include(x => x.ChildMemory)
                                   .Include(x => x.ChildSkill)
                                   .Include(x => x.ChildPaymentHistory).Where(x => x.AccountId == accountId).ToList();

            return childrenDetails != null ? _mapper.Map<List<ChildrenResponse>>(childrenDetails) : childrenResponse;
        }

        /// <summary>
        /// Add UpdateChild Memories
        /// </summary>
        /// <param name="childId"></param>
        /// <param name="memoryId"></param>
        /// <param name="files"></param>
        /// <returns></returns>
        public bool AddUpdateChildMemories(Guid childId, Guid memoryId, string memoryName, string memoryYear, List<IFormFile> formFiles)
        {
            bool fileuploaded = false;
            var childPaymentHistory = GetChildrenPaymentHistory(childId)
                                   .Where(x => x.PaymentYear == memoryYear && (x.IsUserOnTrial || x.IsUserPaid)).FirstOrDefault();

            return childPaymentHistory != null && (childPaymentHistory.IsUserOnTrial || childPaymentHistory.IsUserPaid)
                ? UploadMemoryFiles(childId, memoryId, memoryName, memoryYear, formFiles) : fileuploaded;
        }


        public List<byte[]> getChildmemory(Guid childId, string memoryYear)
        {
            List<byte[]> imageBytes = new List<byte[]>();
            var childPaymentHistory = GetChildrenPaymentHistory(childId)
                                    .Where(x => x.PaymentYear == memoryYear && (x.IsUserOnTrial || x.IsUserPaid)).FirstOrDefault();

            if (childPaymentHistory != null && (childPaymentHistory.IsUserPaid || childPaymentHistory.IsUserOnTrial))
            {
                var childMemory = _context.ChildMemory
                                                     .Where(x => x.ChildId == childId && x.MemoryYear == memoryYear && x.ContentType == "image/jpeg").ToList();


                foreach (var item in childMemory)
                {
                    byte[] bytes = File.ReadAllBytes(item.MemoryPhoto);
                    imageBytes.Add(bytes);
                }
            }

            return imageBytes;
        }

        public string GetVideoByMemoryId(Guid childId, Guid memoryId, string memoryYear)
        {
            string memoryPhotoPath = string.Empty;
            var childMemory = _context.ChildMemory
                                      .Where(x => x.MemoryId == memoryId && x.MemoryYear == memoryYear).FirstOrDefault();

            var childPaymentHistory = GetChildrenPaymentHistory(childId)
                                    .Where(x => x.PaymentYear == memoryYear && (x.IsUserOnTrial || x.IsUserPaid)).FirstOrDefault();

            return childMemory != null && childPaymentHistory != null && (childPaymentHistory.IsUserOnTrial || childPaymentHistory.IsUserPaid)
                                ? childMemory.MemoryPhoto : memoryPhotoPath;
        }


        /// <summary>
        /// Add Payment History for the children
        /// </summary>
        /// <param name="childrenPaymentHistoryRequest">children Payment History Request</param>
        /// <returns></returns>
        public List<ChildrenPaymentHistoryResponse> AddChildrenPaymentHistory(int accountId, Guid childId, DateTime childDob)
        {
            List<ChildrenPaymentHistoryResponse> childrenPaymentHistoryResponses = new List<ChildrenPaymentHistoryResponse>();
            //Extract Year from Child Date of Birth
            int birthYear = childDob.Year;

            for (int i = 0; i <= 10; i++)
            {
                if (i == 0)
                {
                    for (int j = 1; j <= 12; j++)
                    {
                        ChildPaymentHistory childrenPaymentHistory = new ChildPaymentHistory();
                        childrenPaymentHistory.ChildPaymentId = Guid.NewGuid();
                        childrenPaymentHistory.AccountId = accountId;
                        childrenPaymentHistory.isBirthYear = true;
                        childrenPaymentHistory.Month = j.ToString();
                        childrenPaymentHistory.PaymentYear = Convert.ToString(birthYear);
                        childrenPaymentHistory.ChildId = childId;
                        childrenPaymentHistory.CreateDate = DateTime.Now;
                        _context.ChildPaymentHistory.Add(childrenPaymentHistory);
                        _context.SaveChanges();
                        var childPaymentResponse = _mapper.Map<ChildrenPaymentHistoryResponse>(childrenPaymentHistory);
                        childrenPaymentHistoryResponses.Add(childPaymentResponse);
                    }
                }
                else
                {
                    ChildPaymentHistory childrenPaymentHistory = new ChildPaymentHistory();
                    childrenPaymentHistory.ChildPaymentId = Guid.NewGuid();
                    childrenPaymentHistory.AccountId = accountId;
                    childrenPaymentHistory.PaymentYear = Convert.ToString(birthYear + i);
                    childrenPaymentHistory.ChildId = childId;
                    childrenPaymentHistory.CreateDate = DateTime.Now;
                    _context.ChildPaymentHistory.Add(childrenPaymentHistory);
                    _context.SaveChanges();
                    var childPaymentResponse = _mapper.Map<ChildrenPaymentHistoryResponse>(childrenPaymentHistory);
                    childrenPaymentHistoryResponses.Add(childPaymentResponse);
                }
            }

            return childrenPaymentHistoryResponses;
        }

        /// <summary>
        /// Update child Payment History Details
        /// </summary>
        /// <param name="accountId">account Id</param>
        /// <param name="childId">child Id</param>
        /// <param name="paymentYear">Payment Id</param>
        /// <returns></returns>
        public bool UpdateChildPaymentHistory(int accountId, Guid childId, string paymentYear)
        {
            bool isUpdatePayment = false;
            //Extract Year from Child Date of Birth


            var childDetails = GetChildrenDetails(childId);

            if (childDetails != null && childDetails.ChildDOB.Year.ToString() == paymentYear)
            {
                var firstYearchilldPayment = _context.ChildPaymentHistory
                                            .Where(x => x.AccountId == accountId && x.ChildId == childId && x.PaymentYear == paymentYear).ToList();
                int success = 0;
                foreach (var childPayment in firstYearchilldPayment)
                {
                    childPayment.PaymentYear = paymentYear;
                    childPayment.IsUserOnTrial = false;
                    childPayment.IsUserPaid = true;
                    _context.ChildPaymentHistory.Update(childPayment);
                    success = _context.SaveChanges();
                }

                if (success > 0) isUpdatePayment = true;
            }
            else
            {
                var childPaymenthistorydetails = _context.ChildPaymentHistory
              .Where(x => x.AccountId == accountId && x.ChildId == childId && x.PaymentYear == paymentYear).FirstOrDefault();

                childPaymenthistorydetails.PaymentYear = paymentYear;
                childPaymenthistorydetails.IsUserOnTrial = false;
                childPaymenthistorydetails.IsUserPaid = true;
                _context.ChildPaymentHistory.Update(childPaymenthistorydetails);
                int success = _context.SaveChanges();

                if (success > 0) isUpdatePayment = true;
            }

            return isUpdatePayment;

        }

        /// <summary>
        /// Get Children payment History
        /// </summary>
        /// <param name="childId"></param>
        /// <returns></returns>
        public List<ChildrenPaymentHistoryResponse> GetChildrenPaymentHistory(Guid childId)
        {
            List<ChildrenPaymentHistoryResponse> childrenPaymentHistoryResponses = new List<ChildrenPaymentHistoryResponse>();
            var childpaymentHistory = _context.ChildPaymentHistory.Where(x => x.ChildId == childId).ToList();
            return _mapper.Map<List<ChildrenPaymentHistoryResponse>>(childpaymentHistory);
        }

        /// <summary>
        /// Process Multiple Image Files here.
        /// </summary>
        /// <param name="childId"></param>
        /// <param name="memoryId"></param>
        /// <param name="formFiles"></param>
        /// <returns></returns>
        private bool UploadMemoryFiles(Guid childId, Guid memoryId, string memoryName, string memoryYear, List<IFormFile> formFiles)
        {
            int addUpdatePhoto = 0;
            bool fileuploaded = false;
            foreach (var file in formFiles)
            {
                var childrenDetails = _context.ChildMemory
                                                 .Where(x => x.ChildId == childId && x.MemoryId == memoryId).FirstOrDefault();


                if (childrenDetails == null)
                {
                    ChildMemory childMemory = new ChildMemory();
                    childMemory.ChildId = childId;
                    childMemory.MemoryId = Guid.NewGuid();
                    childMemory.MemoryYear = memoryYear;
                    childMemory.MemoryName = memoryName;
                    childMemory.CreatedDate = DateTime.Now;
                    childMemory.MemoryPhoto = UploadImageFileToServer(file, childId);
                    childMemory.ContentType = getContentType(childMemory.MemoryPhoto);
                    _context.ChildMemory.Add(childMemory);
                    addUpdatePhoto = _context.SaveChanges();
                }

                else
                {
                    childrenDetails.MemoryPhoto = UploadImageFileToServer(file, childId);
                    childrenDetails.ContentType = getContentType(childrenDetails.MemoryPhoto);
                    childrenDetails.UpdateDate = DateTime.Now;
                    _context.ChildMemory.Update(childrenDetails);
                    addUpdatePhoto = _context.SaveChanges();
                }

                if (addUpdatePhoto > 0)
                {
                    fileuploaded = true;
                }
            }

            return fileuploaded;
        }

        private string getContentType(string filePath)
        {
            var provider = new FileExtensionContentTypeProvider();
            string contentType = string.Empty;
            if (!provider.TryGetContentType(filePath, out contentType))
            {
                contentType = "application/octet-stream";
            }

            return contentType;
        }

        private string UploadImageFileToServer(IFormFile file, Guid childId)
        {
            var fileNameWithPath = string.Empty;
            if (file.Length > 0)
            {
                // full path to file in temp location
                var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "PhotoMemories", childId.ToString());
                fileNameWithPath = string.Concat(filePath, "\\", file.FileName);

                FileInfo fileInfo = new FileInfo(filePath);
                if (!fileInfo.Exists)
                    Directory.CreateDirectory(filePath);

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }

            return fileNameWithPath;
        }


    }
}
