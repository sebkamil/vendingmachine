using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Description;
using VendingMachine.Models;
using System.Data.Entity.Validation;

namespace VendingMachine.Controllers
{
    public class VendingController : ApiController
    {
        private VendingContext db = new VendingContext();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.db.Dispose();
            }

            base.Dispose(disposing);
        }

        private async Task<List<Slot>> GetAllVendingSlots()
        {
            try
            {
                return await Task.Run(() => db.VendingSlots.Select(x=> new Slot
                {
                    Id = x.Id,
                    Alpha = x.Alpha,
                    Numeric = x.Numeric,
                    Price = x.Price,
                    Snack = x.Snacks.FirstOrDefault()
                }).ToList());
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }

        //GET api/Vending
        [ResponseType(typeof(List<VendingSlot>))]
        public async Task<IHttpActionResult> Get()
        {
            List<Slot> slots = await GetAllVendingSlots();

            if(slots == null)
            {
                return this.NotFound();
            }

            return this.Ok(slots);
        }

        private async Task<VendingSnack> StoreAsync(VendingSnack snack)
        {
            List<VendingSnack> snacks = new List<VendingSnack>(); 
            foreach(VendingSnack s in db.VendingSnacks)
            {
                snacks.Add(s);
            }

            var selectedSnack = await db.VendingSnacks.FindAsync(snack.Id+1);
            selectedSnack.Purchased = true;
            return await db.VendingSnacks.FirstOrDefaultAsync(x => x.Purchased != true && x.SlotId == selectedSnack.SlotId);
        }

        // POST api/Vending
        [ResponseType(typeof(VendingSnack))]
        public async Task<IHttpActionResult> Post(VendingSnack snack)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var nextSnack = await this.StoreAsync(snack);
            return this.Ok<VendingSnack>(nextSnack);
        }
    }
    public class Slot
    {
        public Slot() { }

        public int Id { get; set; }
        public string Alpha { get; set; }
        public int Numeric { get; set; }
        public decimal Price { get; set; }
        public VendingSnack Snack { get; set; }
    }
}
