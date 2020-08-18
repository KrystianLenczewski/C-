using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MemeGenerator.Models
{
    public class Marking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [HiddenInput]
        public int IdMakring { get; set; }
        [ForeignKey("Id_mema")]
        public int IdMema { get; set; }
        public string Authorr { get; set; }
        public string Author { get; set; }
        public int? CountLike { get; set; }
        public int? CountDislike { get; set; }
    }
}
