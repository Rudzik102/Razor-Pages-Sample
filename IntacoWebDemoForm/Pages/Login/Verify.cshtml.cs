using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntacoWebDemoForm.Helpers;
using IntacoWebDemoForm.Mailing;
using IntacoWebDemoForm.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace IntacoWebDemoForm.Pages.Persons
{
    [Authorize]
    public class VerifyModel : PageModel
    {
        private readonly IntacoWebDemoForm.Models.IntacoWebDemoFormContext _context;
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _env;
        private readonly IGraphSdkHelper _graphSdkHelper;
        private MailHelper mail;

        public VerifyModel(IntacoWebDemoForm.Models.IntacoWebDemoFormContext context, IConfiguration configuration, IHostingEnvironment hostingEnvironment, IGraphSdkHelper graphSdkHelper)
        {
            _context = context;
            _configuration = configuration;
            _env = hostingEnvironment;
            _graphSdkHelper = graphSdkHelper;
            mail = new MailHelper();

        }

        [BindProperty]
        public Person Personcs { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Personcs = await _context.Person.FirstOrDefaultAsync(m => m.ID == id);

            if (Personcs == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostRemoveAsync()
        {
         _context.Person.Remove(Personcs);
          await _context.SaveChangesAsync();
         return RedirectToPage("./Index");
        }

        [HttpPost]
        public async Task<IActionResult> OnPostAcceptAsync()
        {
         var result = _context.Person.SingleOrDefault(v => v.ID == Personcs.ID);
         // Get user's id for token cache.
         var identifier = User.FindFirst(Startup.ObjectIdentifierType)?.Value;

         // Initialize the GraphServiceClient.
         var graphClient = _graphSdkHelper.GetAuthenticatedClient(identifier);

         // Create User in AAD.
         List<string> resp = new List<string>(await GraphService.CreateUserAsync(graphClient, _env, HttpContext));
         if(resp[0].Contains("Created"))
         {
                
                mail.SendMailAAD(result.E_Mail, resp[1], resp[2]);
                result.verified = true;
                result.verified_date = DateTime.Now.Date;
                result.AddName = resp[1];
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
         }
            return RedirectToPage("../Error");
        }


    }
}