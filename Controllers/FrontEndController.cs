using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jewelry.Models;
using Jewelry.Respository.MemberResp;
using Jewelry.Services;
using Microsoft.AspNetCore.Mvc;

namespace Jewelry.Controllers
{
    public class FrontEndController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult FrontCreate(Member Member,IEncryption encryption)
        {
            MemberServices services = new MemberServices();
            Member.MemberPassword = encryption.EncryptionMethod(Member.MemberPassword, Member.MemberName);
            services.CreateMember(Member);
            return Redirect("/FrontEnd/Index");
        }
    }
}