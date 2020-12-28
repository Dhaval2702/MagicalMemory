﻿using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using WebApi.Helpers;
using WebApi.Models.Children;
using WebApi.Models.DashBoard;

namespace WebApi.Services
{
    /// <summary>
    /// DashBoard Service for Loggedin user.
    /// </summary>
    public interface IDashBoardService
    {
        DashBoardResponse PrepareUserDashboard(int accountId, Guid childId);
    }

    /// <summary>
    /// 
    /// </summary>
    public class DashBoardService : IDashBoardService
    {
        private readonly IChildService _childService;
        private readonly AppSettings _appSettings;
        private readonly IAccountService _accountService;
        private readonly IEmailService _emailService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="childService"></param>
        /// <param name="accountService"></param>
        /// <param name="appSettings"></param>
        /// <param name="emailService"></param>
        public DashBoardService(IChildService childService, IAccountService accountService, IOptions<AppSettings> appSettings, IEmailService emailService)
        {
            _childService = childService;
            _accountService = accountService;
            _appSettings = appSettings.Value;
            _emailService = emailService;
        }



        public DashBoardResponse PrepareUserDashboard(int accountId, Guid childId)
        {

            DashBoardResponse dashBoardResponse = new DashBoardResponse();
           
            // Retrive User Account Deteails
            var accountDetails = _accountService.GetById(accountId);
            var childrenpaymentHistory = new List<ChildrenPaymentHistoryResponse>();
            // Retrive Children Details By Account Id
            var childrenDetails = _childService.GetChildrenDetails(childId);

            // Retrive Children Payment History Details.
            var childrenPaymentHistoryDetails = _childService.GetChildrenPaymentHistory(childrenDetails.ChildId);
            dashBoardResponse.AccountResponse = accountDetails;
            dashBoardResponse.ChildrenResponse = childrenDetails;
            dashBoardResponse.childrenPaymentHistoryResponses = childrenPaymentHistoryDetails;
            return dashBoardResponse;

        }
    }
}