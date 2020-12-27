using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
        bool AddNewChildDetails(ChildrenRequest childrenRequest);
        ChildrenResponse UpdateChildrenDetails(Guid ChildId, ChildrenRequest childrenUpdateRequest);
        int DeleteChildrenDetails(Guid childId);
        ChildrenResponse GetChildrenDetails(Guid ChildId);
        List<ChildrenResponse> GetAllChildrenDetailsByAccountId(int accountId);

    }

    /// <summary>
    /// This is Children Service Class 
    /// </summary>
    public class ChildService : IChildService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ChildService(
         DataContext context,
         IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Add New Child Details
        /// </summary>
        /// <param name="childrenRequest">childrenRequest</param>
        /// <returns></returns>
        public bool AddNewChildDetails(ChildrenRequest childrenRequest)
        {
            bool isChildAdded = false;
            // map model to new account object
            var adnewChildren = _mapper.Map<Children>(childrenRequest);
            adnewChildren.ChildId = Guid.NewGuid();
            adnewChildren.CreatedDate = DateTime.Now;
            adnewChildren.UpdatedDate = null;
            foreach (var childMemory in adnewChildren.ChildMemory)
            {
                childMemory.MemoryId = Guid.NewGuid();
                childMemory.CreatedDate = DateTime.Now;
                childMemory.UpdateDate = null;
                childMemory.ChildId = adnewChildren.ChildId;
            }

            foreach (var childSkill in adnewChildren.ChildSkill)
            {
                childSkill.ChildSkillId = Guid.NewGuid();
                childSkill.SkillCreatedDate = DateTime.Now;
                childSkill.SkillUpdatedDate = null;
                childSkill.CreatedDate = DateTime.Now;
                childSkill.UpdatedDate = null;
                childSkill.ChildId = adnewChildren.ChildId;
            }

            _context.Children.Add(adnewChildren);
            int success = _context.SaveChanges();
            if (success != 0)
            {
                isChildAdded = true;
            }

            return isChildAdded;
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
                _context.ChildMemory.Remove(childMemory);

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
            var childrenDetails = _context.Children
                           .Include(c => c.ChildMemory)
                           .Include(c => c.ChildSkill).Where(x => x.ChildId == ChildId).FirstOrDefault();

            if (childrenDetails == null) throw new KeyNotFoundException("Children Not found!!!");

            return _mapper.Map<ChildrenResponse>(childrenDetails);
        }

        /// <summary>
        /// Get All Children Details By Account Id
        /// </summary>
        /// <param name="accountId">accountId</param>
        /// <returns></returns>
        public List<ChildrenResponse> GetAllChildrenDetailsByAccountId(int accountId)
        {
            var childrenDetails = _context.Children
                                   .Include(x => x.ChildMemory)
                                   .Include(x => x.ChildSkill).Where(x => x.AccountId == accountId).ToList();

            if (childrenDetails == null) throw new KeyNotFoundException("Children Not found!!!");

            return _mapper.Map<List<ChildrenResponse>>(childrenDetails);
        }


    }
}
