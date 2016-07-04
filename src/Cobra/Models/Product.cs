using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Cobra.Models
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int Quantity { get; set; }

        public string Description { get; set; }

        public int PreOrderId { get; set; }

        public PreOrder PreOrder { get; set; }

        public static Product Create(ProductType type)
        {
            Product product = null;
            switch (type)
            {
                case ProductType.PaperBag:
                    product = new PaperBag() { HasJHooks = true };
                    
                    break;
                case ProductType.PaperBox:
                    product = new PaperBox();
                    break;
            }

            return product;
        }
    }

    public class PaperProduct : Product
    {
        public float Length { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }

        public Unit Unit { get; set; }

        public string Brand { get; set; }



        public int NumberOfDesign { get; set; }

        public PrintStyle PrintingStyle { get; set; }
    }

    public enum Unit
    {
        inch,
        cm
    }

    public enum PaperWeight
    {
        G157,
        G127
    }

    public enum PrintStyle
    {
        SixColor,
        FourColor
    }
}
