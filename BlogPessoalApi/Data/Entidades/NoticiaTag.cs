using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entidades
{
    [Table("NoticiaTags")]
    public class NoticiaTag
    {
        [Column("Id")]
        public int Id { get; set; }
        
        [Column("NoticiaId")]
        public int NoticiaId { get; set; }
        public Noticia Noticia { get; set; }
        
        [Column("TagId")]
        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
