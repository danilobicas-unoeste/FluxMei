using FluxMei.Models;
using LivroCaixa.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace LivroCaixa.Map
{
    public class MeiMap : EntityTypeConfiguration<Mei>
    {
        public MeiMap()
        {
            ToTable("MEI");
            HasKey(x => x.IdMei)
                .Property(x => x.IdMei)
                .HasColumnName("idMei")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.NomeEmpresa)
                .HasColumnName("nomeempresa")
                .IsRequired()
                .HasMaxLength(50);

            Property(x => x.NomeProprietario)
                .HasColumnName("nomeproprietario")
                .IsRequired();
        }

        public static MeiMap Create() => new MeiMap();
        
    }
}