using AutoMapper;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.Children;

namespace WebApi.Services
{
    public interface IChildrenService
    {
        void AddDependentChildren(int accountId, DependentChildrenRequest dependentChildrenRequest);


    }

    public class ChildrenService : IChildrenService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;
        private readonly IEmailService _emailService;

        public ChildrenService(
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

        public void AddDependentChildren(int accountId, DependentChildrenRequest dependentChildrenModel)
        {
            var account = _context.Accounts.Where(x => x.Id == accountId).FirstOrDefault();
            // map model to new dependentChildren object
            var dependentChildrenRequest = _mapper.Map<DependentChildren>(dependentChildrenModel);
            dependentChildrenRequest.ChildId = Guid.NewGuid();
            dependentChildrenRequest.CreatedDate = DateTime.Now;

            if (dependentChildrenModel.ChildWeightDetail.Count > 0)
            {
                foreach (var weight in dependentChildrenRequest.ChildWeightDetail)
                {
                    weight.ChildId = dependentChildrenRequest.ChildId;
                }
            }

            if (dependentChildrenModel.ChildPhotoMemory.Count > 0)
            {
                foreach (var photoMemory in dependentChildrenRequest.ChildPhotoMemory)
                {
                    photoMemory.ChildId = dependentChildrenRequest.ChildId;
                }
            }

            // save  dependentChildren
            _context.DependentChildren.Add(dependentChildrenRequest);
            _context.SaveChanges();
        }
    }
}
