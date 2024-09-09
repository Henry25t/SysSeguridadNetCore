using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SysSeguridadG05.EN;

namespace SysSeguridadG05.DAL
{
    public class DBContexto : DbContext
    {
        public DbSet<Rol> Rol { get; set; }
        public DbSet<Usuario> Usuario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseMySql("Server=localhost;Database=DBSeguridadG05;User=root;Password=password;",
            //new MySqlServerVersion(new Version(8, 0, 0)));
            optionsBuilder.UseSqlServer(@"workstation id=DBSeguridadPractica.mssql.somee.com;packet size=4096;user id=HACP_SQLLogin_1;pwd=73j7hfo2wt;data source=DBSeguridadPractica.mssql.somee.com;persist security info=False;initial catalog=DBSeguridadPractica;TrustServerCertificate=True");
        }
    }
}