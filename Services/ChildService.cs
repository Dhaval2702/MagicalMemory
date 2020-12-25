using AutoMapper;
using Microsoft.Extensions.Options;
using System;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.Children;

namespace WebApi.Services
{
    public interface IChildService
    {
        bool AddNewChildMemory(ChildrenRequest childrenRequest);
        void UpdateChildMemory(ChildrenRequest childrenRequest);
        void DeleteChildMemory(ChildrenRequest childrenRequest);



    }
    public class ChildService : IChildService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;
        private readonly IEmailService _emailService;

        public ChildService(
         DataContext context,
         IMapper mapper,
         IOptions<AppSettings> appSettings,
         IEmailService emailService)
        {
            _context = context;
            _mapper = mapper;
            _appSettings = appSettings.Value;
            _emailService = emailService;
        }


        public bool AddNewChildMemory(ChildrenRequest childrenRequest)
        {
            bool isChildAdded = false;
            // map model to new account object
            var adnewChildren = _mapper.Map<Children>(childrenRequest);
            adnewChildren.ChildId = Guid.NewGuid();

            foreach (var childMemory in adnewChildren.ChildMemory)
            {
                childMemory.MemoryId = Guid.NewGuid();
                childMemory.ChildId = adnewChildren.ChildId;
            }

            foreach (var childSkill in adnewChildren.ChildSkill)
            {
                childSkill.ChildSkillId = Guid.NewGuid();
                childSkill.ChildId = adnewChildren.ChildId;
            }

            _context.Children.Add(adnewChildren);
          int success =  _context.SaveChanges();
            if(success != 0)
            {
                isChildAdded = true;
            }

           return isChildAdded;
        }

        public void DeleteChildMemory(ChildrenRequest childrenRequest)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateChildMemory(ChildrenRequest childrenRequest)
        {
            throw new System.NotImplementedException();
        }
    }
}
