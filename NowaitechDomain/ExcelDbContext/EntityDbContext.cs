using Microsoft.EntityFrameworkCore;
using NowaitechShared.AuthenticateOperations;
using NowaitechShared.Models.ExcelEntities.Aktuels;
using NowaitechShared.Models.ExcelEntities.Lager;
using NowaitechShared.Models.ExcelEntities.PO;
using NowaitechShared.Models.ExcelEntities.POC;
using NowaitechShared.Models.ExcelEntities.Zugang;
using NowaitechShared.Models.User;

namespace NowaitechDomain.ExcelDbContext
{
    public class EntityDbContext : DbContext
    {
        public EntityDbContext(DbContextOptions<EntityDbContext> options)   //https://stackoverflow.com/questions/8073806/warning-the-type-x-in-y-cs-conflicts-with-the-imported-type-x-in-z-dll
           : base(options)
        {
        }

        #region DbSets
        public virtual DbSet<RouterAktuell>? RouterAktuell { get; set; }
        public virtual DbSet<RouterSwapAktuell>? RouterSwapAktuells { get; set; }
        public virtual DbSet<MultiProject>? MultiProjects { get; set; }
        public virtual DbSet<JSLMultiProject>? JSLMultiProjects { get; set; }
        public virtual DbSet<ZTE_PO>? ZTE_POs { get; set; }
        public virtual DbSet<Deltatel_PO>? Deltatel_POs { get; set; }
        public virtual DbSet<Cisco_PO>? Cisco_POs { get; set; }
        public virtual DbSet<LagerCentral>? LagerCentrals { get; set; }
        public virtual DbSet<ZugangsdatenAktuell>? ZugangsdatenAktuells { get; set; }
        public virtual DbSet<XWDMAktuell>? XWDMAktuells { get; set; }
        public virtual DbSet<XWDMAktuellOrderList>? XWDMAktuelOrderLists { get; set; }
        public virtual DbSet<RouterAktuellOrderList>? RouterAktuellOrderLists { get; set; }
        public virtual DbSet<User>? UserInputs { get; set; }
        public virtual DbSet<UserType>? UserTypes { get; set; }
        #endregion DbSets

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EntityDbContext).Assembly);

            modelBuilder.Entity<UserType>().HasData(
                new UserType
                {
                    id = 1,
                    role = UserTypeEnums.Admin
                },
                new UserType
                {
                    id = 2,
                    role = UserTypeEnums.RegularUser
                }); ;

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Username = "regular",
                    UserTypeId = 1,
                    PasswordHash = HashPassword.CreateHash("regular")
                }
                );

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Username = "admin",
                    UserTypeId = 2,
                    PasswordHash = HashPassword.CreateHash("admin")
                }
                );

            //modelBuilder.Entity<RouterAktuell>(entity =>
            //{
            //    entityw
            //    .HasMany<XWDMAktuell>(ra => ra.XWDMAktuells)
            //    .WithMany(xwdm => xwdm.RouterAktuells);
            //});

            #region PrimaryKey

            modelBuilder.Entity<RouterAktuell>(entity =>
            {
                entity.HasKey(key => key.id);
            });

            modelBuilder.Entity<RouterAktuellOrderList>(entity =>
            {
                entity.HasKey(key => key.id);
            });

            modelBuilder.Entity<XWDMAktuell>(entity =>
            {
                entity.HasKey(key => key.id);
            });

            modelBuilder.Entity<XWDMAktuellOrderList>(entity =>
            {
                entity.HasKey(key => key.id);
            });

            modelBuilder.Entity<LagerCentral>(entity =>
            {
                entity.HasKey(key => key.id);
            });

            modelBuilder.Entity<Cisco_PO>(entity =>
            {
                entity.HasKey(key => key.id);
            });

            modelBuilder.Entity<Deltatel_PO>(entity =>
            {
                entity.HasKey(key => key.id);
            });

            modelBuilder.Entity<ZTE_PO>(entity =>
            {
                entity.HasKey(key => key.id);
            });

            modelBuilder.Entity<JSLMultiProject>(entity =>
            {
                entity.HasKey(key => key.id);
            });

            modelBuilder.Entity<MultiProject>(entity =>
            {
                entity.HasKey(key => key.id);
            });

            modelBuilder.Entity<RouterSwapAktuell>(entity =>
            {
                entity.HasKey(key => key.id);
            });

            modelBuilder.Entity<ZugangsdatenAktuell>(entity =>
            {
                entity.HasKey(key => key.id);
            });

            #endregion PrimaryKey


            modelBuilder.Entity<ZTE_PO>().HasQueryFilter(filter => filter.SO_Nr != null);
            //querylerde so_nr null olmayanları getiriyor. Diğer tabloların proje ve saha numaraları boş olanları da ekle
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //optionsBuilder.UseLazyLoadingProxies();
        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}
