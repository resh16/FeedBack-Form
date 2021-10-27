using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DataAccessLayer.Model
{
    [Keyless]
    [Table("FeedbackDetail")]
    public partial class FeedbackDetail
    {
        public int FeedbackId { get; set; }
        public int FdtId { get; set; }

        [ForeignKey(nameof(FdtId))]
        public virtual FeedbackDetailType Fdt { get; set; }
        [ForeignKey(nameof(FeedbackId))]
        public virtual Feedback Feedback { get; set; }
    }
}
