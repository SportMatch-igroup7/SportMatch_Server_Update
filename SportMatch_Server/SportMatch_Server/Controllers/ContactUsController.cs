using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SportMatch_1.Models;
using static SportMatch_1.Models.MailHelper;

namespace SportMatch_1.Controllers
{
    public class ContactUsController : ApiController
    {
        public int Post([FromBody]string email)
        {
            User u = new User();
            string password = u.getUsers(email).Password;
            if (password != "")
            {
                string subject = "שחזור סיסמה - SportMatch";
                string body = @"סיסמתך היא: " + password + "<br/>" +
                                    " הינך מוזמן להיכנס למערכת:http://proj.ruppin.ac.il/igroup7/proj/client/#/ <br/>" +
                                    "בברכה ,<br/> צוות SportMatch";
                // SendEMail(u.Email, subject, body);
                SendEMail("sportmatch8@gmail.com", subject, body);
                return 1;
            }
            else
                return 0;
        }
    }

}
