using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MessagingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagingBaseController : ControllerBase
    {
        private int? userId;
        protected int? LoggedInUserId
        {
            get
            {
                if (userId != null)
                {
                    return userId;
                }

                // userId == null
                var sid = GetClaim(Constants.UserId);
                if (sid == null)
                {
                    return null;
                }
                if (int.TryParse(sid, out int id))
                {
                    userId = id;
                }

                return userId;
            }
        }

        private string emailAddress;
        protected string LoggedInEmailAddress
        {
            get
            {
                if (emailAddress != null)
                {
                    return emailAddress;
                }

                var emailAddressFromClaims = GetClaim(Constants.EmailAddress);
                if (emailAddressFromClaims == null)
                {
                    return null;
                }
                emailAddress = emailAddressFromClaims;

                return emailAddress;
            }
        }

        private string Name;
        protected string LoggedInName
        {
            get
            {
                if (Name != null)
                {
                    return Name;
                }

                var NameFromClaims = GetClaim(Constants.Name);
                if (NameFromClaims == null)
                {
                    return null;
                }
                Name = NameFromClaims;

                return Name;
            }
        }


        private string GetClaim(string claimType)
        {
            return User?.Claims?.Where(c => c.Type == claimType)?.FirstOrDefault()?.Value;
        }
    }
}