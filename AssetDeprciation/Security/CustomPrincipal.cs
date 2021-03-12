using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace AssetDeprciation.Security
{

        public class CustomPrincipal : IPrincipal
        {
            public IIdentity Identity { get; private set; }
            public bool IsInRole(string role)
            {
                if (roles.Any(r => role.Contains(r)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public CustomPrincipal(string Username)
            {
                this.Identity = new GenericIdentity(Username);
            }

            public string UserId { get; set; }
            public string dept { get; set; }
            public string book { get; set; }
            public string[] roles { get; set; }
        }

        public class CustomPrincipalSerializeModel
        {
            public string UserId { get; set; }
            public string dept { get; set; }
            public string book { get; set; }
            public string[] roles { get; set; }
        }


    }
