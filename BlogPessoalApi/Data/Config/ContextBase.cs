using Data.Entidades;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.Config
{
    public class ContextBase : IdentityDbContext<ApplicationUser>
    {

        public ContextBase(DbContextOptions<ContextBase> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>().ToTable("AspNetUsers").HasKey(t => t.Id);
            base.OnModelCreating(builder);
        }

        private string GetStringConectionConfig()
        {
            string strCon = "Server=LAPTOP-CV9KCCO0\\SQLEXPRESS;Database=BlogPessoalDb;Trusted_Connection=True;MultipleActiveResultSets=true";

            return strCon;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(GetStringConectionConfig());
                base.OnConfiguring(optionsBuilder);
            }
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Noticia> Noticias { get; set; }
        public DbSet<NoticiaTag> NoticiaTags { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}
