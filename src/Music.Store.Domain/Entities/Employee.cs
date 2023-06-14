using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Music.Store.Domain.Entities
{
    public class Employee : EntityBase
    {
        public int UserId { get; set; }

        public User User { get; set; }

        [MaxLength(30)]
        public string Title { get; set; }

        public int? ReportsTo { get; set; }

        [ForeignKey("ReportsTo")]
        public User ReportsToManager { get; set; }

        [MaxLength(70)]
        public string Address { get; set; }

        public DateTime? BirthDate { get; set; }

        public DateTime? HireDate { get; set; }
    }
}
