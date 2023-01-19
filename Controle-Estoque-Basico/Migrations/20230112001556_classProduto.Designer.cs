﻿// <auto-generated />
using System;
using Controle_Estoque_Basico.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Controle_Estoque_Basico.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230112001556_classProduto")]
    partial class classProduto
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.32")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Controle_Estoque_Basico.Models.Produto", b =>
                {
                    b.Property<int>("PRO_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("PRO_DATAENTRADA")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("PRO_DESCRICAO")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("PRO_NOME")
                        .IsRequired()
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4")
                        .HasMaxLength(50);

                    b.Property<decimal>("PRO_QUANTIDADE")
                        .HasColumnType("decimal(18, 3)");

                    b.Property<DateTime>("PRO_VALIDADE")
                        .HasColumnType("datetime(6)");

                    b.HasKey("PRO_ID");

                    b.ToTable("Produto");
                });
#pragma warning restore 612, 618
        }
    }
}
