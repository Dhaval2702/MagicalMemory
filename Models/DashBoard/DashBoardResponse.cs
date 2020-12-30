using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models.Accounts;
using WebApi.Models.Children;

namespace WebApi.Models.DashBoard
{
    public class DashBoardResponse
    {
        public AccountResponse AccountResponse { get; set; }
        public  List<ChildrenResponse> ChildrenResponse { get; set; }
    }
}
