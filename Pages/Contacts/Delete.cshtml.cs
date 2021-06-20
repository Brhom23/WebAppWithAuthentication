using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebAppWithAuthenticationEFCJet.Authorization;
using WebAppWithAuthenticationEFCJet.Data;
using WebAppWithAuthenticationEFCJet.Models;

namespace WebAppWithAuthenticationEFCJet.Pages.Contacts
{
    public class DeleteModel : DI_BasePageModel
    {
        public DeleteModel(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<AppUser> userManager)
            : base(context, authorizationService, userManager)
        {
        }

        [BindProperty]
        public Contact Contact { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Contact = await Context.Contacts.FirstOrDefaultAsync(
                                                 m => m.ContactId == id);

            if (Contact == null)
            {
                return NotFound();
            }

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                     User, Contact,
                                                     ContactOperations.Delete);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var contact = await Context
                .Contacts.AsNoTracking()
                .FirstOrDefaultAsync(m => m.ContactId == id);

            if (contact == null)
            {
                return NotFound();
            }

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                     User, contact,
                                                     ContactOperations.Delete);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            Context.Contacts.Remove(contact);
            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
    //public class DeleteModel : PageModel
    //{
    //    private readonly WebAppWithAuthenticationEFCJet.Data.ApplicationDbContext _context;

    //    public DeleteModel(WebAppWithAuthenticationEFCJet.Data.ApplicationDbContext context)
    //    {
    //        _context = context;
    //    }

    //    [BindProperty]
    //    public Contact Contact { get; set; }

    //    public async Task<IActionResult> OnGetAsync(int? id)
    //    {
    //        if (id == null)
    //        {
    //            return NotFound();
    //        }

    //        Contact = await _context.Contacts.FirstOrDefaultAsync(m => m.ContactId == id);

    //        if (Contact == null)
    //        {
    //            return NotFound();
    //        }
    //        return Page();
    //    }

    //    public async Task<IActionResult> OnPostAsync(int? id)
    //    {
    //        if (id == null)
    //        {
    //            return NotFound();
    //        }

    //        Contact = await _context.Contacts.FindAsync(id);

    //        if (Contact != null)
    //        {
    //            _context.Contacts.Remove(Contact);
    //            await _context.SaveChangesAsync();
    //        }

    //        return RedirectToPage("./Index");
    //    }
    //}
}
