using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebAppWithAuthenticationEFCJet.Data;

[assembly: HostingStartup(typeof(WebAppWithAuthenticationEFCJet.Areas.Identity.IdentityHostingStartup))]
namespace WebAppWithAuthenticationEFCJet.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}