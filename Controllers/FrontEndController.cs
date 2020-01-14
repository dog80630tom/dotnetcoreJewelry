using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Jewelry.Models;
using Jewelry.Respository;
using Jewelry.Respository.MemberResp;
using Jewelry.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Nancy.Authentication.Forms;
using Newtonsoft.Json;
using RestSharp;

namespace Jewelry.Controllers
{
    public class FrontEndController : Controller
    {
        private readonly ICRUD<Member> _cRUDMember;
        private readonly MemberServices _services;
        public FrontEndController(ICRUD<Member> cRUD)
        {

            _cRUDMember = cRUD;
            _services = new MemberServices(cRUD);
        }
        public IActionResult Index()
        {
            if (HttpContext.User.Claims.Count()>0)
            {
                //这里通过 HttpContext.User.Claims 可以将我们在Login这个Action中存储到cookie中的所有
                //claims键值对都读出来，比如我们刚才定义的UserName的值Wangdacui就在这里读取出来了
                ViewBag.UserName = HttpContext.User.Claims.First().Value;
            }
            return View();
        }

        public IActionResult MemberRegist()
        {
            return View();
        }
        public IActionResult MemberPageAddresschange()
        {
          
           
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult FrontCreate(Member Member)
        {

            Member.MemberPassword = Encryption.EncryptionMethod(Member.MemberPassword, Member.MemberName);
            //_cRUDMember.Crate(Member);
            //_cRUDMember.SaveChange();
            _services.CreateMember(Member);
            return Redirect("/FrontEnd/Index");
        }

        public IActionResult Login(string uname, string psw)
        {
            var Googlecode = Request.Query["googlecode"].ToString();
         
            var Linecode = Request.Query["Linecode"].ToString();
           
            string sql = "select * from Members";
            var membership=new Member();
            var password="";
            var email = "";
            var password2 = "";
            var name = "";
            var token = new GetTokenFromCodeResult();
            switch (LoginCompare.Compare(Googlecode, Linecode))
            {
                case 0:

                    var temp = _cRUDMember.GetData(sql).Any(x => x.MemberEmail == uname);
                    if (temp)
                    {
                        membership = (from m in _cRUDMember.GetData(sql) where m.MemberEmail == uname select m).FirstOrDefault();
                        password = Encryption.EncryptionMethod(psw, membership.MemberName);
                        if (membership.MemberEmail == uname && password == membership.MemberPassword)
                        {
                            LoginProcess("Client", membership.MemberName, true, membership);
                            TempData["username"] = membership.MemberName;
                            TempData["roles"] = "Client";
                            return RedirectToAction("MemberPageAddresschange");
                        }
                        return RedirectToAction("MemberPageAddresschange");

                    }
                    return RedirectToAction("MemberPageAddresschange");
                case 1:
                    GoogleLogin google = new GoogleLogin();
                    token = google.GetTokenFromCodeResult(Googlecode,
                "145015126077-5afcqbo9rc629k3ilceajnbfrlrdamlj.apps.googleusercontent.com",
                "At2kDe1L5weKB4Xf7dpf6rmx",
                "https://vegetable20191216120019.azurewebsites.net/FrontEnd/GoogleLogin");

                    var UserInfoResult = google.GetUserInfo(token.access_token);
                    // 這邊不建議直接把 Token 當做參數傳給 CallAPI 可以避免 Token 洩漏

                     email = UserInfoResult.email;
                    name = UserInfoResult.name;
                    password2 = UserInfoResult.id;
                    if (!_cRUDMember.GetData(sql).Any(x => x.MemberEmail == email))
                    {
                        Member member = new Member();

                        member.MemberPassword = Encryption.EncryptionMethod(password2, email);
                        member.MemberName = name;
                        member.MemberEmail = email;
                        member.MemberGender = "Google";
                        member.MemberPhone = "google";

                        _services.CreateMember(member);

                    }
                     membership = (from m in _cRUDMember.GetData(sql) where m.MemberEmail == email select m).FirstOrDefault();
                    password = Encryption.EncryptionMethod(password2, membership.MemberName);
                    LoginProcess("Client", membership.MemberName, true, membership);
                    TempData["username"] = membership.MemberName;
                    return RedirectToAction("MemberPageAddresschange");
                case 2:
                    LineLogin line = new LineLogin();
                   token = line.GetTokenFromCodeResult(Linecode,
               "1653659088",
               "27d426186987ed6e5d69cb7601129805",
               "https://vegetable20191216120019.azurewebsites.net/FrontEnd/LineLogin");

                     UserInfoResult = line.GetUserInfo(token.access_token, token.id_token);
                    // 這邊不建議直接把 Token 當做參數傳給 CallAPI 可以避免 Token 洩漏

                    int i = 0;
                     email = UserInfoResult.email;
                     name = UserInfoResult.displayName;
                     password2 = UserInfoResult.userId;
                    if (!_cRUDMember.GetData(sql).Any(x => x.MemberEmail == email))
                    {
                        Member member = new Member();
                     
                        member.MemberPassword = Encryption.EncryptionMethod(password2, email);
                        member.MemberName = name;
                        member.MemberEmail = email;
                        member.MemberGender = "Line";
                        member.MemberPhone = "Line";

                        _services.CreateMember(member);

                    }
                     membership = (from m in _cRUDMember.GetData(sql) where m.MemberEmail == email select m).FirstOrDefault();
                     password = Encryption.EncryptionMethod(password2, membership.MemberName);
                    LoginProcess("Client", membership.MemberName, true, membership);
                    TempData["username"] = membership.MemberName;
                    return RedirectToAction("MemberPageAddresschange");
            }
            return RedirectToAction("MemberPageAddresschange");
        }
        private async void LoginProcess(string level, string Name, bool isRemeber, Member user)
        {
            var now = DateTime.Now;
            string roles = level;
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.MemberEmail),
                new Claim("FullName", user.MemberName),
                new Claim(ClaimTypes.Role, "Administrator"),
                };
         
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                //AllowRefresh = <bool>,
                // Refreshing the authentication session should be allowed.

                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                // The time at which the authentication ticket expires. A
                // value set here overrides the ExpireTimeSpan option of
                // CookieAuthenticationOptions set with AddCookie.

                IsPersistent = true,
                // Whether the authentication session is persisted across
                // multiple requests. When used with cookies, controls
                // whether the cookie's lifetime is absolute (matching the
                // lifetime of the authentication ticket) or session-based.

                //IssuedUtc = <DateTimeOffset>,
                // The time at which the authentication ticket was issued.

                //RedirectUri = <string>
                // The full path or absolute URI to be used as an http
                // redirect response value.
            };
            await HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity), authProperties);



        }


    }
}