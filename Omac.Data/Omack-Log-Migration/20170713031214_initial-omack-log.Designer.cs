using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Omack.Data.DAL;

namespace Omack.Data.OmackLogMigration
{
    [DbContext(typeof(OmackLogContext))]
    [Migration("20170713031214_initial-omack-log")]
    partial class initialomacklog
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Omack.Data.LogModels.ApiLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Application")
                        .HasMaxLength(50);

                    b.Property<string>("CallSite")
                        .HasMaxLength(500);

                    b.Property<string>("Exception")
                        .HasMaxLength(500);

                    b.Property<string>("Level")
                        .HasMaxLength(50);

                    b.Property<DateTime>("Logged");

                    b.Property<string>("Logger")
                        .HasMaxLength(500);

                    b.Property<string>("MachineName")
                        .HasMaxLength(50);

                    b.Property<string>("Message")
                        .HasMaxLength(500);

                    b.Property<string>("ThreadId")
                        .HasMaxLength(10);

                    b.HasKey("Id");

                    b.ToTable("ApiLog");
                });

            modelBuilder.Entity("Omack.Data.LogModels.WebLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Application")
                        .HasMaxLength(50);

                    b.Property<string>("CallSite")
                        .HasMaxLength(500);

                    b.Property<string>("Exception")
                        .HasMaxLength(500);

                    b.Property<string>("Level")
                        .HasMaxLength(50);

                    b.Property<DateTime>("Logged");

                    b.Property<string>("Logger")
                        .HasMaxLength(500);

                    b.Property<string>("MachineName")
                        .HasMaxLength(50);

                    b.Property<string>("Message")
                        .HasMaxLength(500);

                    b.Property<string>("ThreadId")
                        .HasMaxLength(10);

                    b.HasKey("Id");

                    b.ToTable("WebLog");
                });
        }
    }
}
