using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TODO.DataAccess.Models
{
    public sealed class Todo
    {
        [Key]
        public int TodoId { get; set; }

        public string Title { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DueDate { get; set; }

        public bool IsMarked { get; set; }
    }
}
