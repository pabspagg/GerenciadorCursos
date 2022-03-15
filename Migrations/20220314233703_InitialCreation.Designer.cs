﻿// <auto-generated />
using GerenciadorCursos.DataAcessRepo.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GerenciadorCursos.Migrations
{
    [DbContext(typeof(RepositoryContext))]
    [Migration("20220314233703_InitialCreation")]
    partial class InitialCreation
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.15")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GerenciadorCursos.DomainCore.Models.CursoModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Duracao")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("varchar(30)");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("varchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Cursos");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Duracao = 2,
                            Status = "Previsto",
                            Titulo = "Curso programação backend"
                        },
                        new
                        {
                            Id = 2,
                            Duracao = 2,
                            Status = "EmAndamento",
                            Titulo = "Curso programação frontend"
                        },
                        new
                        {
                            Id = 3,
                            Duracao = 1,
                            Status = "Concluido",
                            Titulo = "Curso devops"
                        },
                        new
                        {
                            Id = 4,
                            Duracao = 5,
                            Status = "Previsto",
                            Titulo = "Curso mobile"
                        },
                        new
                        {
                            Id = 5,
                            Duracao = 10,
                            Status = "Previsto",
                            Titulo = "Curso inovação"
                        });
                });

            modelBuilder.Entity("GerenciadorCursos.DomainCore.Models.UserModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("varchar(15)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("varchar(15)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("varchar(15)");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Password = "123456",
                            Role = "Aluno",
                            Username = "pablo"
                        },
                        new
                        {
                            Id = 2,
                            Password = "123456",
                            Role = "Aluno",
                            Username = "joao"
                        },
                        new
                        {
                            Id = 3,
                            Password = "123456",
                            Role = "Aluno",
                            Username = "lucas"
                        },
                        new
                        {
                            Id = 4,
                            Password = "123456",
                            Role = "Aluno",
                            Username = "diego"
                        },
                        new
                        {
                            Id = 5,
                            Password = "123456",
                            Role = "Aluno",
                            Username = "victor"
                        },
                        new
                        {
                            Id = 6,
                            Password = "123456",
                            Role = "Aluno",
                            Username = "joao"
                        },
                        new
                        {
                            Id = 7,
                            Password = "123456",
                            Role = "Secretaria",
                            Username = "maria"
                        },
                        new
                        {
                            Id = 8,
                            Password = "123456",
                            Role = "Gerencia",
                            Username = "jose"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}