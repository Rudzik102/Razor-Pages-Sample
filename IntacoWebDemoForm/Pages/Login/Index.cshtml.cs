using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IntacoWebDemoForm.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace IntacoWebDemoForm.Pages.Persons
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IntacoWebDemoForm.Models.IntacoWebDemoFormContext _context;

        public IndexModel(IntacoWebDemoForm.Models.IntacoWebDemoFormContext context)
        {
            _context = context;
        }
        public IList<Person> Person { get;set; }

        public async Task OnGetAsync()
        {
            IList<Person> temp = await _context.Person.Where(n => n.email_Verified==true).ToListAsync();
            Person = temp;
        }

    }
}
