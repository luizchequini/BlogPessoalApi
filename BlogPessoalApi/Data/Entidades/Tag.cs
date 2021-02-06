using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entidades
{
    [Table("Tags")]
    public class Tag
    {
        [Column("Id")]
        public int Id { get; set; }

        [Column("Nome")]
        public string Nome { get; set; }

        public ICollection<NoticiaTag> NoticiaTags { get; set; }
    }
}
