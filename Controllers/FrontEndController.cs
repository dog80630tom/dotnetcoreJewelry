using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jewelry.Models;
using Jewelry.Respository;
using Jewelry.Respository.MemberResp;

using Microsoft.AspNetCore.Mvc;

namespace Jewelry.Controllers
{
    public class FrontEndController : Controller
    {
        private readonly ICRUD<Member> _cRUDMember;
        public FrontEndController(ICRUD<Member> cRUD) {

            _cRUDMember = cRUD;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MemberRegist()
        {
            return View();
        }

        [HttpPost]
        public IActionResult FrontCreate(Member Member)
        {
            
            Member.MemberPassword = Encryption.EncryptionMethod(Member.MemberPassword, Member.MemberName);
            _cRUDMember.Crate(Member);
            _cRUDMember.SaveChange();
            return Redirect("/FrontEnd/Index");
        }
    }
}