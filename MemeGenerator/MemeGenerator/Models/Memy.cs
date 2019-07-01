using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MemeGenerator.Models
{
    public partial class Memy
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [HiddenInput]
        public int Id_mema { get; set; }
        public string Autor { get; set; }
        [Required]
        public string Title { get; set; }
        [HiddenInput]
        public string coverImg { get; set; }
     
        public string Description { get; set; }
        [Required]
        public string Category { get; set; }
        [HiddenInput]
        public DateTime? releaseDate { get; set; }
        public DateTime? modifyDate { get; set; }
        public int? Like { get; set; }
        public int? Dislike { get; set; }
    }
}
