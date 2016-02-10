namespace VendingMachine.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Web;

    public class VendingDBInitializer:CreateDatabaseIfNotExists<VendingContext>
    {
        protected override void Seed(VendingContext context)
        {
            base.Seed(context);

            var captital = AddCapitial();
            captital.ForEach(x => context.VendingCapital.Add(x));

            var snacks = AddSnacks();
            snacks.ForEach(x => context.VendingSnacks.Add(x));

            var slots = AddSlots(snacks);
            slots.ForEach(x => context.VendingSlots.Add(x));

            context.SaveChanges();
        }

        private List<VendingCapital> AddCapitial()
        {
            var captital = new List<VendingCapital>();

            foreach(Denomination d in Enum.GetValues(typeof(Denomination)))
            {
                VendingCapital c = new VendingCapital{
                    Denomination = d,
                    InMachine = true
                };

                captital.AddRange(Enumerable.Repeat(c, 50));
            }

            return captital;
        }

        private List<VendingSnack> AddSnacks()
        {
            var snacks = new List<VendingSnack>();

            #region Drinks
            snacks.AddRange(Enumerable.Repeat(new VendingSnack
            {
                Title = "Coke",
                Calories = 240,
                FoodGroup = FoodGroup.Drink
            },20));

            snacks.AddRange(Enumerable.Repeat(new VendingSnack
            {
                Title = "Dr. Pepper",
                Calories = 250,
                FoodGroup = FoodGroup.Drink
            }, 20));

            snacks.AddRange(Enumerable.Repeat(new VendingSnack
            {
                Title = "Sprite",
                Calories = 194,
                FoodGroup = FoodGroup.Drink
            }, 20));

            snacks.AddRange(Enumerable.Repeat(new VendingSnack
            {
                Title = "Mt. Dew",
                Calories = 275,
                FoodGroup = FoodGroup.Drink
            }, 20));

            snacks.AddRange(Enumerable.Repeat(new VendingSnack
            {
                Title = "Diet Coke",
                Calories = 0,
                FoodGroup = FoodGroup.Drink
            }, 20));
            #endregion

            #region Chips
            snacks.AddRange(Enumerable.Repeat(new VendingSnack
            {
                Title = "Doritos",
                Calories = 140,
                FoodGroup = FoodGroup.Chips
            }, 20));

            snacks.AddRange(Enumerable.Repeat(new VendingSnack
            {
                Title = "Salt & Vineger",
                Calories = 160,
                FoodGroup = FoodGroup.Chips
            }, 20));

            snacks.AddRange(Enumerable.Repeat(new VendingSnack
            {
                Title = "Cheetos",
                Calories = 150,
                FoodGroup = FoodGroup.Chips
            }, 20));

            snacks.AddRange(Enumerable.Repeat(new VendingSnack
            {
                Title = "Sour Cream & Onion",
                Calories = 160,
                FoodGroup = FoodGroup.Chips
            }, 20));

            snacks.AddRange(Enumerable.Repeat(new VendingSnack
            {
                Title = "Fritos",
                Calories = 160,
                FoodGroup = FoodGroup.Chips
            }, 20));

            #endregion

            #region Candy

            snacks.AddRange(Enumerable.Repeat(new VendingSnack
            {
                Title = "Milky Way",
                Calories = 264,
                FoodGroup = FoodGroup.Candy
            }, 20));

            snacks.AddRange(Enumerable.Repeat(new VendingSnack
            {
                Title = "Snickers",
                Calories = 215,
                FoodGroup = FoodGroup.Candy
            }, 20));

            snacks.AddRange(Enumerable.Repeat(new VendingSnack
            {
                Title = "Three Musketeers",
                Calories = 262,
                FoodGroup = FoodGroup.Candy
            }, 20));

            snacks.AddRange(Enumerable.Repeat(new VendingSnack
            {
                Title = "Almond Joy",
                Calories = 234,
                FoodGroup = FoodGroup.Candy
            }, 20));

            snacks.AddRange(Enumerable.Repeat(new VendingSnack
            {
                Title = "Kit Kat",
                Calories = 218,
                FoodGroup = FoodGroup.Candy
            }, 20));

            #endregion

            #region Crackers

            snacks.AddRange(Enumerable.Repeat(new VendingSnack
            {
                Title = "Toast Chee",
                Calories = 220,
                FoodGroup = FoodGroup.Crackers
            }, 20));

            snacks.AddRange(Enumerable.Repeat(new VendingSnack
            {
                Title = "Peanut Butter",
                Calories = 190,
                FoodGroup = FoodGroup.Crackers
            }, 20));

            snacks.AddRange(Enumerable.Repeat(new VendingSnack
            {
                Title = "Whole Grain",
                Calories = 180,
                FoodGroup = FoodGroup.Crackers
            }, 20));

            snacks.AddRange(Enumerable.Repeat(new VendingSnack
            {
                Title = "Chedder Cheese",
                Calories = 264,
                FoodGroup = FoodGroup.Crackers
            }, 20));

            snacks.AddRange(Enumerable.Repeat(new VendingSnack
            {
                Title = "Cream Cheese",
                Calories = 264,
                FoodGroup = FoodGroup.Crackers
            }, 20));

            #endregion

            #region Gum

            snacks.AddRange(Enumerable.Repeat(new VendingSnack
            {
                Title = "Winterfresh",
                Calories = 120,
                FoodGroup = FoodGroup.Gum
            }, 20));

            snacks.AddRange(Enumerable.Repeat(new VendingSnack
            {
                Title = "Juicy Fruit",
                Calories = 130,
                FoodGroup = FoodGroup.Gum
            }, 20));

            snacks.AddRange(Enumerable.Repeat(new VendingSnack
            {
                Title = "Doublemint",
                Calories = 110,
                FoodGroup = FoodGroup.Gum
            }, 20));

            snacks.AddRange(Enumerable.Repeat(new VendingSnack
            {
                Title = "Hubba Bubba",
                Calories = 120,
                FoodGroup = FoodGroup.Gum
            }, 20));

            snacks.AddRange(Enumerable.Repeat(new VendingSnack
            {
                Title = "Big Red",
                Calories = 130,
                FoodGroup = FoodGroup.Gum
            }, 20));

            #endregion

            return snacks;
        }

        private List<VendingSlot> AddSlots(List<VendingSnack> snacks)
        {
            var slots = new List<VendingSlot>();

            char alpha = 'A';
            int numeric = 10;
            decimal price = (decimal)1.5;
            foreach (FoodGroup fg in Enum.GetValues(typeof(FoodGroup)))
            {
                foreach (var snack in snacks.Select(x=> new {FoodGroup = x.FoodGroup, Title = x.Title }).Distinct())
                {
                    if (snack.FoodGroup == fg)
                    {
                        slots.Add(new VendingSlot
                        {
                            Alpha = alpha.ToString(),
                            Numeric = numeric,
                            Price = price,
                            Snacks = snacks.Where(x=> x.Title == snack.Title).ToList()
                        });
                        numeric++;
                    }
                }
                alpha++;
                numeric += 5;
                price -= (decimal).25;
            }

            return slots;
        }
    }
}
