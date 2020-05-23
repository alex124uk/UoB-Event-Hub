﻿using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace ExtensionMethods
{

        public static class IdentityExtensions
        {
            public static string GetUserCourse(this IIdentity identity)
            {
                if (identity == null)
                {
                    throw new ArgumentNullException("identity");
                }
                var ci = identity as ClaimsIdentity;
                if (ci != null)
                {
                    return ci.FindFirstValue("UserCourse");
                }
                return null;
            }
        }
    }
