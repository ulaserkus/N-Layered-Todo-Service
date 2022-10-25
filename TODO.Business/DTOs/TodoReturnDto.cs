using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODO.Business.DTOs
{
    public sealed class TodoReturnDto
    {
        /// <summary>
        /// Todo Unique ID
        /// </summary>
        public int TodoId { get; set; }

        /// <summary>
        /// Todo Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Todo Due Date
        /// </summary>
        public string DueDate { get; set; }

        /// <summary>
        /// Todo Complete Status
        /// </summary>
        public bool IsMarked { get; set; }
    }
}
