namespace VendingMachine.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Collections.Generic;

    using Newtonsoft.Json;

    public class VendingSlot
    {
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //string because json encodes char
        [Required]
        public string Alpha { get; set; }

        [Required]
        public int Numeric { get; set; }

        public decimal Price { get; set; }

        public virtual List<VendingSnack> Snacks { get; set; }
    }
}