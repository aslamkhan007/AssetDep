using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssetDeprciation.Models
{
    public class AuthenticateModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { set; get; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { set; get; }
        public List<SelectListItem> Book { set; get; }
        //[Required(ErrorMessage = "Book needs to Be Selected")]
        public string BookName { set; get; }
    }
}