using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemeGenerator.Models
{
    public class MemyCategoryViewModel
    {
        public List<Memy> Memys { get; set; }
        public SelectList Categorys { get; set; }
        public string MemyCategory { get; set; }
        public string SearchString { get; set; }
        public DateTime? SearchDate { get; set; }
       public int? like { get; set; }
       //public int? dislike { get; set; }
    }
}
