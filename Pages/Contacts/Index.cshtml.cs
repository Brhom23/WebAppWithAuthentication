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

    public class IndexModel : DI_BasePageModel
    {
        public IndexModel(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<AppUser> userManager)
            : base(context, authorizationService, userManager)
        {
        }

        public IList<Contact> Contact { get; set; }

        public async Task OnGetAsync()
        {
            var contacts = from c in Context.Contacts
                           select c;

            var isAuthorized = User.IsInRole(Constants.ContactManagersRole) ||
                               User.IsInRole(Constants.ContactAdministratorsRole);

            var currentUserId = UserManager.GetUserId(User);

            // Only approved contacts are shown UNLESS you're authorized to see them
            // or you are the owner.
            if (!isAuthorized)
            {
                contacts = contacts.Where(c => c.Status == ContactStatus.Approved
                                            || c.OwnerID == currentUserId);
            }

            Contact = await contacts.ToListAsync();
        }
    }

    //[AllowAnonymous]
    //public class IndexModel : PageModel
    //{
    //    private readonly WebAppWithAuthenticationEFCJet.Data.ApplicationDbContext _context;

    //    public IndexModel(WebAppWithAuthenticationEFCJet.Data.ApplicationDbContext context)
    //    {
    //        _context = context;
    //    }

    //    public IList<Contact> Contact { get;set; }

    //    public async Task OnGetAsync()
    //    {
    //        Contact = await _context.Contacts.ToListAsync();
    //    }
    //}
}
