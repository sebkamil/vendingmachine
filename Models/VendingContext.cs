namespace VendingMachine.Models
{
    using System.Data.Entity;

    public class VendingContext:DbContext
    {
        public VendingContext():base("name=DefaultConnection")
        {

        }

        public DbSet<VendingSnack> VendingSnacks { get; set; }
        public DbSet<VendingSlot> VendingSlots { get; set; }
        //Not implemented yet
        public DbSet<VendingCapital> VendingCapital { get; set; }
    }
}