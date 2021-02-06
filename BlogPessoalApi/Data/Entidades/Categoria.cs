using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entidades
{
    [Table("Categorias")]
    public class Categoria
    {
        [Column("Id")]
        public int Id { get; set; }

        [Column("Nome")]
        public string Nome { get; set; }

        public ICollection<Noticia> Noticias { get; set; }
    }
}
