using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using CloudNoteV1.AWS.DynamoDb;
using Amazon.DynamoDBv2.Model;
using System.Collections.Generic;

namespace CloudNoteV1.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        DynamoService ds;

        public ApplicationDbContext() /*: base("DefaultConnection", throwIfV1Schema: false)*/
        {
            try
            {
                ds = new DynamoService();
                /* deneme kodu
                Dictionary<string, Amazon.DynamoDBv2.Model.AttributeValue> dc = new Dictionary<string, AttributeValue>();
                dc.Add("user_id", new AttributeValue("ss"));
                ds.DynamoClient.PutItem("UserDb", dc);
                */
            }
            catch(System.Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            
            
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}