using Domain.Entities.Security;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("EntryBy")]
        public Guid EntryById { get; set; }
        public User EntryBy { get; set; }
        public DateTime EntryDate { get; set; } = DateTime.Now;
        [ForeignKey("UpdatedBy")]
        public Guid? UpdatedById { get; set; }
        public User? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; } = DateTime.Now;
        public EnStatus Status { get; set; }
    }
}
