using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using IntacoWebDemoForm.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System.Collections;
using IntacoWebDemoForm.Mailing;

namespace IntacoWebDemoForm.Pages.Persons
{
    public class CaptchaVerification
    {
        public CaptchaVerification()
        {
        }

        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("error-codes")]
        public IList Errors { get; set; }
    }

    public class CreateModel : PageModel
    {
        public bool Disbale1 { get; set; } = false;

        private MailHelper mail;

        private readonly IntacoWebDemoForm.Models.IntacoWebDemoFormContext _context;

        public CreateModel(IntacoWebDemoForm.Models.IntacoWebDemoFormContext context)
        {
            _context = context;
            mail = new MailHelper();
        }

        public string GenerateToken()
        {
            byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            byte[] key = Guid.NewGuid().ToByteArray();
            string token = Convert.ToBase64String(time.Concat(key).ToArray());
            if(token.Contains('+'))
            {
               token = token.Replace('+', 'x');
            }
            return token;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Person view { get; set; }

        private async Task<CaptchaVerification> VerifyCaptchaAsync()
        {
            string userIP = string.Empty;
            var ipAddress = Request.HttpContext.Connection.RemoteIpAddress;
            if (ipAddress != null) userIP = ipAddress.MapToIPv4().ToString();

            var captchaResponse = Request.Form["g-recaptcha-response"];
            var payload = string.Format("&secret={0}&remoteip={1}&response={2}",
                "6Le242UUAAAAAMJ0BxUe-JxhfHw2q8TIkvyH_6Kf",
                userIP,
                captchaResponse
                );

            var client = new HttpClient();
            client.BaseAddress = new Uri("https://www.google.com");
            var request = new HttpRequestMessage(HttpMethod.Post, "/recaptcha/api/siteverify");
            request.Content = new StringContent(payload, Encoding.UTF8, "application/x-www-form-urlencoded");
            var response = await client.SendAsync(request);
            return JsonConvert.DeserializeObject<CaptchaVerification>(response.Content.ReadAsStringAsync().Result);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var resultCaptcha = await VerifyCaptchaAsync();
            if (!resultCaptcha.Success)
            {
                ModelState.AddModelError("", "Wymagana jest poprawna weryfikacja captcha");
                return Page();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    view.Token=GenerateToken();
                    _context.Person.Add(view);
                    await _context.SaveChangesAsync();
                    mail.SendMail(view.E_Mail, view.Token);
                    return RedirectToPage("./Validation");

                }
                return Page();
            }

        }
    }
}