﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeData.Models.DBEntities
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string FirstName { get; set; }

        public string LastName { get; set; }    

        public DateTime DateOfBirth { get; set; }

        public string Email { get; set; }   
        public double Salary { get; set; }
    }
}
