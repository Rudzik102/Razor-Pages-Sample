using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntacoWebDemoForm.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace IntacoWebDemoForm.Pages.Persons
{
    public class AfterValidationModel : PageModel
    {

        private readonly IntacoWebDemoForm.Models.IntacoWebDemoFormContext _context;

        private Person Personcs1 { get; set; }
        public string Text { get; set; } = "Poprawnie zweryfikowano adres email.";

        public AfterValidationModel(IntacoWebDemoForm.Models.IntacoWebDemoFormContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> OnGetAsync(string token_ID)
        {
            try
            {
                Personcs1 = await _context.Person.FirstOrDefaultAsync(m => m.Token == token_ID);
                Personcs1.email_Verified = true;
                _context.Person.Update(Personcs1);
                await _context.SaveChangesAsync();
                return Page();
            }
            catch
            {
                Text = "Niepoprawny token weryfikacyjny.";
                return Page();
            }
        }

    }
}