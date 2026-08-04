namespace CRS.Database
{
    #region Usings

    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    #endregion

    internal class ucsdbEntities : DbContext
    {
        public virtual DbSet<clan> clan { get; set; }

        public virtual DbSet<player> player { get; set; }

        public ucsdbEntities()
            : base("name=ucsdbEntities") // + connectionString
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    }
}