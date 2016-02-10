namespace VendingMachine.Models
{
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    //To track revenue, not implemented yet
    public class VendingCapital
    {
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required]
        public Denomination Denomination { get; set; }

        [Column(Order = 1), ForeignKey("VendingSnack")]
        public int? SnackId { get; set; }

        [JsonIgnore]
        public virtual VendingSnack VendingSnack { get; set; }

        public bool InMachine { get; set; }
    }

    public enum Denomination
    {
        Nickel,
        Dime,
        Quarter,
        Dollar
    }
}