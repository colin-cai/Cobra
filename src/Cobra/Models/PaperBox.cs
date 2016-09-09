using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cobra.Models
{
    public class PaperBox : Product
    {
        public float Length { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }

        public Unit Unit { get; set; }

        public string Brand { get; set; }



        public int NumberOfDesign { get; set; }

        public PrintStyle PrintingStyle { get; set; }

        public override float EvaluatePrice(ICalculateFactor factor)
        {
            throw new NotImplementedException();
        }
    }
}
