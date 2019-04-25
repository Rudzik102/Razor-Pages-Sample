using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IntacoWebDemoForm.Extensions
{
    public class ValidationMethods
    {
        public static ValidationResult ValidateString(string stream, ValidationContext context)
        {
            if(!string.IsNullOrWhiteSpace(stream))
            {
                if (context.MemberName == "Name")
                {
                    if (!Regex.IsMatch(stream, @"^[a-zA-Z]+$"))
                    {
                        return new ValidationResult("Niepoprawne imię.", new[] {
                        context.MemberName });
                    }
                }
                else
                {
                    if (!Regex.IsMatch(stream, @"^[a-zA-Z-]+$"))
                    {
                        return new ValidationResult("Niepoprawne nazwisko.", new[] {
                        context.MemberName });
                    }
                }
            }
            return ValidationResult.Success;
        }

    }
}
