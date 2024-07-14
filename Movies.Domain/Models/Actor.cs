using Movies.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Domain.Models
{
    public class Actor : BaseEntity
    {
        public string Name { get; set; }
        public string? Biography { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Picture { get; set;}
        public string? PictureUri { get; set; }
        public List<string>? Awards { get; set; }
    }
}
