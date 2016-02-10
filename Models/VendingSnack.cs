namespace VendingMachine.Models
{
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Collections.Generic;

    public class VendingSnack
    {
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public int Calories { get; set; }

        public FoodGroup FoodGroup { get; set; }

        public bool Purchased { get; set; }

        [Column(Order = 1), ForeignKey("VendingSlot")]
        public int? SlotId { get; set; }

        [JsonIgnore]
        public virtual VendingSlot VendingSlot { get; set; }

        //Track revenue, not implemented yet
        public List<VendingCapital> Payments { get; set; }

    }

    public enum FoodGroup
    {
        Drink,
        Chips,
        Candy,
        Crackers,
        Gum
    }
}