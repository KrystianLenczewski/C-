using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MemeGenerator.Models
{
    public class MemeReports
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_report { get; set; }
        public string login { get; set; }
        public int Id_meme { get; set; }
        public string Description { get; set; }
    }
}
