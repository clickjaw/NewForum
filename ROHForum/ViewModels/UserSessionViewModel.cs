using ROHForum.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class UserSessionViewModel
    {
        public string Username { get; set; }
        public int UserId { get; set; }

        public int TotalUpvotes { get; set; }
        public int TotalDownvotes { get; set; }
        



    }
}
