﻿using Jewelry.Models;
using Jewelry.Respository;
using Microsoft.AspNetCore.Razor.Language.Intermediate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Jewelry.Services
{
    public class LineLogin 
    {
        public GetTokenFromCodeResult GetTokenFromCodeResult(string code, string ClientId, string ClientSecret, string redirect_uri)
        {
            try
            {
                WebClient wc = new WebClient();
                wc.Encoding = System.Text.Encoding.UTF8;
                wc.Headers.Clear();
                //wc.Headers.Add("Content-Type", "application/json");

                var data = new System.Collections.Specialized.NameValueCollection();
                data["grant_type"] = "authorization_code";
                data["code"] = code;
                data["redirect_uri"] = redirect_uri;
                data["client_id"] = ClientId;
                data["client_secret"] = ClientSecret;
                //post
                byte[] bResult = wc.UploadValues("https://api.line.me/oauth2/v2.1/token", data);
                //get result
                string JSON = System.Text.Encoding.UTF8.GetString(bResult);
                //parsing JSON
                var GetTokenFromCodeResult = Newtonsoft.Json.JsonConvert.DeserializeObject<GetTokenFromCodeResult>(JSON);
                return GetTokenFromCodeResult;
            }
            catch (WebException ex)
            {
                using (var reader = new System.IO.StreamReader(ex.Response.GetResponseStream()))
                {
                    var responseText = reader.ReadToEnd();
                    throw new Exception("GetTokenFromCode: " + responseText, ex);
                }
            }
        }

        public UserInfoResult GetUserInfo(string token,string idtoken)
        {
            try
            {
                WebClient wc = new WebClient();
                wc.Encoding = System.Text.Encoding.UTF8;
                wc.Headers.Clear();
                wc.Headers.Add("Authorization", "Bearer " + token);

                //get
                string JSON = wc.DownloadString("https://api.line.me/v2/profile");
                var JwtSecurityToken = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(token);
                var email = "";
                //如果有email
                if (JwtSecurityToken.Claims.ToList().Find(c => c.Type == "email") != null)
                    email = JwtSecurityToken.Claims.First(c => c.Type == "email").Value;

                //parsing JSON
                var Result = Newtonsoft.Json.JsonConvert.DeserializeObject<UserInfoResult>(JSON);
                Result.email = email;
                return Result;
            }
            catch (WebException ex)
            {
                using (var reader = new System.IO.StreamReader(ex.Response.GetResponseStream()))
                {
                    var responseText = reader.ReadToEnd();
                    throw new Exception("GetUserInfo: " + responseText, ex);
                }
            }
        }
    }
}
