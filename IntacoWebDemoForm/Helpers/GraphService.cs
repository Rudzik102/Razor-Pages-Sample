/* 
*  Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license. 
*  See LICENSE in the source repository root for complete license information. 
*/

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Graph;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Resources;
using System.Web;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace IntacoWebDemoForm.Helpers
{
    public static class GraphService
    {
        /*
        public static async Task GetProfileAsync(GraphServiceClient graphClient, IHostingEnvironment hostingEnvironment, HttpContext httpContext)
        {
            var user = await graphClient.Users["b.ozurkiewicz@execoz.pl"].Request().GetAsync();
            var obj = JsonConvert.SerializeObject(user, Formatting.Indented);
            System.IO.File.WriteAllText("path.txt", obj.ToString());
  
        }
        */
        public static string GenerateRandomPassword(PasswordOptions opts = null)
        {
            if (opts == null) opts = new PasswordOptions()
            {
                RequiredLength = 10,
                RequiredUniqueChars = 4,
                RequireDigit = true,
                RequireLowercase = true,
                RequireNonAlphanumeric = true,
                RequireUppercase = true
            };

            string[] randomChars = new[] {
        "ABCDEFGHJKLMNOPQRSTUVWXYZ",    // uppercase 
        "abcdefghijkmnopqrstuvwxyz",    // lowercase
        "0123456789",                   // digits
        "!@$?_-"                        // non-alphanumeric
    };
            Random rand = new Random(Environment.TickCount);
            List<char> chars = new List<char>();

            if (opts.RequireUppercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[0][rand.Next(0, randomChars[0].Length)]);

            if (opts.RequireLowercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[1][rand.Next(0, randomChars[1].Length)]);

            if (opts.RequireDigit)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[2][rand.Next(0, randomChars[2].Length)]);

            if (opts.RequireNonAlphanumeric)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[3][rand.Next(0, randomChars[3].Length)]);

            for (int i = chars.Count; i < opts.RequiredLength
                || chars.Distinct().Count() < opts.RequiredUniqueChars; i++)
            {
                string rcs = randomChars[rand.Next(0, randomChars.Length)];
                chars.Insert(rand.Next(0, chars.Count),
                    rcs[rand.Next(0, rcs.Length)]);
            }

            return new string(chars.ToArray());
        }

        public static async Task<IList<string>> CreateUserAsync(GraphServiceClient graphClient, IHostingEnvironment hostingEnvironment, HttpContext httpContext)
        {
            string guid = Guid.NewGuid().ToString();
            string password = GenerateRandomPassword();
            string subalias = "demo";
            // This snippet gets the tenant domain from the Organization object to construct the user's email address.
            IGraphServiceOrganizationCollectionPage organization = await graphClient.Organization.Request().GetAsync();
            string alias = subalias.ToLower() + guid.Substring(0, 8);
            string domain = organization.CurrentPage[0].VerifiedDomains.ElementAt(0).Name;

            // Add the user.
            User user = await graphClient.Users.Request().AddAsync(new User
            {
                AccountEnabled = true,
                DisplayName = subalias+guid.Substring(0, 8),
                MailNickname = alias,
                PasswordProfile = new PasswordProfile
                {
                    ForceChangePasswordNextSignIn = true,
                    Password = password
                },
                UserPrincipalName = alias + "@" + domain
            });
            var response = JsonConvert.SerializeObject(user, Formatting.Indented);
            List<string> list = new List<string>();
            list.Add(response);
            list.Add(alias + "@" + domain);
            list.Add(password);
            return list;
        }


    }
}
