using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebAppWithAuthenticationEFCJet.Models;

namespace WebAppWithAuthenticationEFCJet.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser> //IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Contact> Contacts { get; set; }
    }
}
