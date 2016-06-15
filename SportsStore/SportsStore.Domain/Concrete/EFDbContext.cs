namespace SportsStore.Domain.Concrete
{
    using Entites;
    using System.Data.Entity;

    public class EFDbContext : DbContext
    {
        public EFDbContext()
         : base("EFDbContext")
        {
        }

        public EFDbContext(string conectionStringName)
            : base(conectionStringName)
        {
        }

        public IDbSet<Product> Products { get; set; }
    }
}
