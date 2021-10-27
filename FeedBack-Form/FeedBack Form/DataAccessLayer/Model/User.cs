using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DataAccessLayer.Model
{
    public partial class User
    {
        public User()
        {
            SessionConductors = new HashSet<Session>();
            SessionSpeakers = new HashSet<Session>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }

        [InverseProperty(nameof(Session.Conductor))]
        public virtual ICollection<Session> SessionConductors { get; set; }
        [InverseProperty(nameof(Session.Speaker))]
        public virtual ICollection<Session> SessionSpeakers { get; set; }
    }
}
