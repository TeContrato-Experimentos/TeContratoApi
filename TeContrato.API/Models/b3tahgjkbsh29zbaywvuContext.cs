using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Supermarket.API.Models
{
    public partial class b3tahgjkbsh29zbaywvuContext : DbContext
    {
        public b3tahgjkbsh29zbaywvuContext()
        {
        }

        public b3tahgjkbsh29zbaywvuContext(DbContextOptions<b3tahgjkbsh29zbaywvuContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=b3tahgjkbsh29zbaywvu-mysql.services.clever-cloud.com;port=3306;database=b3tahgjkbsh29zbaywvu;uid=ujmkif873jqih5w7;password=WU1ShfGZ7s2zvfH2huqT", Microsoft.EntityFrameworkCore.ServerVersion.FromString("8.0.22-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
