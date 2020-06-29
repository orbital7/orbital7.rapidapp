using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Orbital7.RapidApp.Models;

namespace SampleWebApp.Pages.ModalDialogs
{
    public class DialogActionModel
        : PageModel
    {
        public class InputModel
        {
            [Display(Name = "Sample Value")]
            public string SampleValue { get; set; }
        }

        [BindProperty]
        public string Message { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public void OnGet(
            string message)
        {
            this.Message = message;
            this.Input = new InputModel();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (this.ModelState.IsValid)
            {
                try
                {
                    System.Threading.Thread.Sleep(2000);
                    await Task.CompletedTask;   // Do real work here.
                    return this.RASuccess();
                }
                catch (Exception ex)
                {
                    this.HandleException(nameof(Input), ex);
                }
            }

            return this.RAFailurePage();
        }
    }
}
