using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Farmers_Training.Models
{
    public class Training
    {
        [Key]
        public int TrainingId { get; set; }

        [Required]
        [StringLength(200)]
        [DisplayName(" Trainer Name: ")]
        public string TrainerName { get; set; }

        [Required]
        [StringLength(100)]
        [DisplayName(" Event: ")]
        public string EventName { get; set; }

        [Required]
        [StringLength(10000)]
        public string Description { get; set; }

        [Column(TypeName = "smalldatetime")]
       // [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm:ss tt}")]
        [DisplayName(" Training Time: ")]
        public DateTime TrainingTime { get; set; }
        [Column(TypeName = "decimal")]
        public decimal? Cost { get; set; }
    }
}
