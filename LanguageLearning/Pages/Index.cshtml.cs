using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LanguageLearning.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace LanguageLearning.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _config;

        public IndexModel(IConfiguration config)
        {
            _config = config;
        }       

        public JsonResult OnGetClaimsIndex()
        {
            JwtValidation jwtValidation = new JwtValidation(_config);
            string userToken = ReadCookie("Token");

            if (String.IsNullOrEmpty(userToken))
            {
                return new JsonResult("Invalid");
            }
            else
            {
                JObject json = jwtValidation.ParseTokenToJSON(userToken);
                string username = jwtValidation.JSONGetValue(json, "sub");
                return new JsonResult(json);
            }
        }

        public string ReadCookie(string cookieName)
        {
            string cookieValue = Request.Cookies[cookieName];

            if (cookieValue != null)            
                return cookieValue;
            else
                return null;
        }
    }
}
