using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cobra.Models
{
    public class PaperBag : PaperProduct
    { 
        public string Finish { get; set; }

        public string Embellishments { get; set; }

        public string Handle { get; set; }

        public string Tag { get; set; }

        public bool HasLabel { get; set; }

        public bool HasJHooks{ get; set; }

        public string Barbel { get; set; }

        public string InnerPacking { get; set; }

        public string PackingWay { get; set; }
    }
}
