using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Cobra.Models
{
    public abstract class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [NotMapped]
        public string ObscureId { get; set; }

        public int Quantity { get; set; }

        public string Description { get; set; }

        public float? EvaluatedPrice { get; set; }

        public float? EvaluatedAmount { get { return EvaluatedPrice * Quantity; } }

        public float? ConfirmedPrice { get; set; }

        public float? ConfirmedAmount { get { return ConfirmedPrice * Quantity; } }

        public int PreOrderId { get; set; }

        [NotMapped]
        public string ObscurePreOrderId { get; set; }

        public PreOrder PreOrder { get; set; }

        public static Product Create(ProductType type)
        {
            Product product = null;
            switch (type)
            {
                case ProductType.PaperBag:
                    product = new PaperBag() {  };
                    
                    break;
                case ProductType.PaperBox:
                    product = new PaperBox();
                    break;
            }

            return product;
        }

        public abstract float EvaluatePrice(ICalculateFactor factor);
    }

}
