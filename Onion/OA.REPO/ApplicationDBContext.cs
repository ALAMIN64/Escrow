using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OA.DATA.Entities;
using OA.DATA.Mapping;
using System;

namespace OA.REPO
{
    public class ApplicationDBContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> option) : base(option)
        {

        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new DeliveryTypeMap(modelBuilder.Entity<DeliveryType>());
            new TransactionMap(modelBuilder.Entity<Transactions>());
            new TransactionLinkMap(modelBuilder.Entity<TransactionLink>());
            new PaymentMap(modelBuilder.Entity<Payment>());
            new UserTypeMap(modelBuilder.Entity<UserType>());
            new AboutMap(modelBuilder.Entity<About>());
            new PolicyMap(modelBuilder.Entity<Policy>());
            new TermsMap(modelBuilder.Entity<Terms>());
            new FeePerAmountMap(modelBuilder.Entity<FeePerAmount>());
            new ContactMap(modelBuilder.Entity<Contact>());
            new PinfoMap(modelBuilder.Entity<Pinfo>());
            new HomeContentMap(modelBuilder.Entity<HomeContent>());
        }
    }
}
