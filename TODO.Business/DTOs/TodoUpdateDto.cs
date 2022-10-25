using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TODO.Business.DTOs
{
    public sealed class TodoUpdateDto
    {
        /// <summary>
        /// Todo Title
        /// </summary>
        [Required(ErrorMessage = "Title is required")]
        [MaxLength(100, ErrorMessage = "Max length is 100 for title")]
        [MinLength(5, ErrorMessage = "Min length is 5 for title")]
        public string Title { get; set; }


        /// <summary>
        /// Todo Due Date
        /// </summary>
        [Column(TypeName = "date")]
        public DateTime? DueDate { get; set; }


        /// <summary>
        /// Todo Complete Status
        /// </summary>
        [Required(ErrorMessage = "Marked info is required")]
        public bool IsMarked { get; set; }
    }
}
