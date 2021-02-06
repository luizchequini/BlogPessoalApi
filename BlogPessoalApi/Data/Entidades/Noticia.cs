using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entidades
{
    [Table("Noticias")]
    public class Noticia
    {
        [Column("Id")]
        public int Id { get; set; }
        
        [Column("Titulo")]
        public string Titulo { get; set; }
        
        [Column("Data")]
        public DateTime Data { get; set; }
        
        [Column("Mensagem")]
        public string Mensagem { get; set; }

        [Column("CategoriaId")]
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
        
        public ICollection<NoticiaTag> NoticiaTags { get; set; }
    }
}
