using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

/*******************************************************************************************
 * 
 * Author:  Larry Kotman
 * Date:    21 October 2019
 * 
 * Description
 * -----------
 * 
 * Bank2020 is an ASP.NET Core MVC application for a banking system that supports the
 * following actions:
 * 
 * 1) Create and list customers
 * 2) Create and list accounts for each customer
 * 3) For any account, logging the following types of tranactions:
 *       A) Deposit
 *       B) Withdrawal
 *       C) Add interest
 *       D) Transfer money to another account
 * 
 * References
 * ----------
 * https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/?view=aspnetcore-2.2
 * https://www.learnrazorpages.com/razor-pages/tutorial/bakery/
 * 
 * NuGet Package Manager => Package manager Console
 * ------------------------------------------------
 * Add-Migration Initial
 * Update-Database
 * 
 * Development steps
 * -----------------
 * 1. Create application with ASP.NET CORE MVC
 * 2. Define data models for a 
 *      Customer, 
 *      Account, 
 *      Transaction, 
 *          Credit, 
 *          Debit, 
 *          Interest
 *      Transfer (Should this extend Transaction?)
 * 3. Scaffolding to create:
 *      DbContext, 
 *      Controllers, 
 *      Views
 * 4. Add-Migration and Update-Database
 * 
 * Incrementally do the following, and repeat:
 * 
 * 5. Redefine selected actions and corresponding Views
 * 6. Delete unused actions
 * 7. Test the application
 * *****************************************************************************************/

namespace ShopF2019
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
