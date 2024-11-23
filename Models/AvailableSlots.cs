using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ScheduleApp.Models;

namespace ScheduleApp.ViewModels
{
    public class AvailableSlot
    {
        [Key]
        public int SlotId { get; set; } 

        [Required]
        public string LecturerId { get; set; } 

        [ForeignKey("LecturerId")]
        public required Lecturer Lecturer { get; set; }

        [ForeignKey("StudentId")]
        public required Student Student { get; set; }

        [Required]
        public required string StudentId { get; set; }

        [ForeignKey("ScheduleId")]
        public required Schedule Schedule { get; set; }

        [Required]
        public int ScheduleId { get; set; }

        [Required]
        public DateTime SlotDate { get; set; } 

        [Required]
        public TimeSpan StartTime { get; set; } 

        [Required]
        public TimeSpan EndTime { get; set; } 
        public bool IsBooked { get; set; } = false;
        
    }
}
