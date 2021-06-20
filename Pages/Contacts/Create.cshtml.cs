using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppWithAuthenticationEFCJet.Authorization;
using WebAppWithAuthenticationEFCJet.Data;
using WebAppWithAuthenticationEFCJet.Models;

namespace WebAppWithAuthenticationEFCJet.Pages.Contacts
{
    public class CreateModel : DI_BasePageModel //PageModel
    {
        private readonly WebAppWithAuthenticationEFCJet.Data.ApplicationDbContext _context;

        //public CreateModel(WebAppWithAuthenticationEFCJet.Data.ApplicationDbContext context)
        //{
        //    _context = context;
        //}

        public CreateModel(
        ApplicationDbContext context,
        IAuthorizationService authorizationService,
        UserManager<AppUser> userManager)
        : base(context, authorizationService, userManager)
        {
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Contact Contact { get; set; }

        //// To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        //public async Task<IActionResult> OnPostAsync()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Page();
        //    }

        //    _context.Contacts.Add(Contact);
        //    await _context.SaveChangesAsync();

        //    return RedirectToPage("./Index");
        //}
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Contact.OwnerID = UserManager.GetUserId(User);

            // requires using ContactManager.Authorization;
            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                        User, Contact,
                                                        ContactOperations.Create);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            Context.Contacts.Add(Contact);
            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
