using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.DAL.Models
{
    public class Department
    {
        public int Id { get; set; }
        [MaxLength(50)]
        [Required(ErrorMessage = "Name is mandatory.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Code name is required.")]
        public string Code { get; set; }
        public DateTime DateOfCreation { get; set; }

        [InverseProperty("Department")]
        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}
