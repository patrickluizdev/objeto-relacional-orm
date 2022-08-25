﻿/* 
EXEMPLO DE CÓDIGO UTILIZANDO O ORM ENTITY FRAMEWORK

1 - INSTALE O PACOTE EntityFrameworkCore

Install-Package Microsoft.EntityFrameworkCore.SqlServer
Install-Package Microsoft.EntityFrameworkCore.Tools

2 - IMPLEMENTE AS CLASSE DE DADOS E DE CONTEXTO

3 - ADICIONE O MIGRATION

add-migration ormDB


4 - ATUALIZE O BANCO DE DADOS

update-database

5 - USE O BANCO DE DADOS NA SUA APLICAÇÃO =D
*/

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ORM_Exemplo
{
    public class Genero
    {
        [Key]
        public int Id { get; set; }
        public string Descricao { get; set; }

        public ICollection<Filme> Filme { get; set; }
    }

    public class Filme
    {
        [Key]
        public int Id { get; set; }
        public string Titulo { get; set; }
        public int GeneroId { get; set; }

        [ForeignKey("GeneroId")]
        public Genero Genero { get; set; }
    }

    public class ApplicationContext: DbContext
    {
        public DbSet<Genero> Genero { get; set; }
        public DbSet<Filme> Filme { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=orm;Trusted_Connection=True;");
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new ApplicationContext())
            {
                var genero = new Genero()
                {
                    Descricao = "Fantasia"
                };

                context.Genero.Add(genero);
                context.SaveChanges();

                var filme = new Filme()
                {
                    Titulo = "De volta para o futuro",
                    GeneroId = genero.Id
                };

                context.Filme.Add(filme);
                context.SaveChanges();

            }
        }
    }
}
