using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UMS.DAL.Models;

namespace UMS.PL.ViewModels
{
    public class EmployeeVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name Is Required")]
        [MaxLength(50, ErrorMessage = "Max Length Is 50 Chars")]
        [MinLength(5, ErrorMessage = "Min Length Is 5 Chars")]
        public string Name { get; set; }

        [Range(22, 35, ErrorMessage = "Age Must Be In Range 22-35")]
        public int? Age { get; set; }
        [RegularExpression("^(\\d+)\\s+([A-Za-z0-9\\s,.-]+),\\s*([A-Za-z\\s]+),\\s*([A-Z]{2})\\s+(\\d{5}(?:-\\d{4})?)$",
            ErrorMessage = "Addres Must Be Like: 789 Oak Blvd, Chicago, IL 60601")]
        public string Address { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }


        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }
        //Navigation Property
        [InverseProperty("Employees")]
        public Department Department { get; set; }
    }
}
