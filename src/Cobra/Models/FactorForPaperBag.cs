using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Cobra.Models
{
    public class FactorForPaperBag : ICalculateFactor
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime CreateTime { get; set; }


        public float CoatedArtPaper { get; set; }
        public float FoilPaper { get; set; }
        public float MetalisedPaper { get; set; }
        public float NaturalKraftPaper { get; set; }
        public float WhiteKraftPaper { get; set; }
        public float WhiteCardboard { get; set; }

        [Display(Name = "Paper Printing")]
        public float LaborOfPrintOnePaper { get; set; }

        public float LaborOfSmallSize { get; set; }
        public float LaborOfMediumSize { get; set; }
        public float LaborOfLargeSize { get; set; }

        public float LaborOfFolding { get; set; }


        public float Matt { get; set; }
        public float Glossy { get; set; }

        public float Glitter { get; set; }
        public float HotStamping { get; set; }
        public float SpotUV { get; set; }
        public float TipOn { get; set; }
        public float OtherEmbellishment { get; set; }

        public float PP { get; set; }
        public float PaperTwist { get; set; }
        public float SantinRibbon { get; set; }
        public float GrosgrainRibbon { get; set; }
        public float OrganzaRibbon { get; set; }
        public float OtherHandle { get; set; }

        public float PriceOfTag { get; set; }
        public float PriceOfLabel { get; set; }
        public float PriceOfJHook { get; set; }
    }
}
