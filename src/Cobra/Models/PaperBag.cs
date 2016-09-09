using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cobra.Models
{
    public class PaperBag : Product
    {
        public Unit Unit { get; set; }

        public float Height { get; set; }

        public float Width { get; set; }

        public float Gusset { get; set; }

        public PaperMaterialType Material { get; set; }

        [Display(Name = "Paper Weight")]
        public PaperMaterialWeight MaterialWeight { get; set; }

        public PrintStyle Printing { get; set; }

        [Display(Name = "Inside Printing")]
        public PrintStyle InsidePrinting { get; set; }

        public LaminationType Lamination { get; set; }

        [Display(Name = "Embellishment")]
        public SufaceEmbellishment SurfaceEmbellishment { get; set; }

        public HandleType Handle { get; set; }

        [Display(Name = "Has Tag")]
        public bool HasTag { get; set; }

        [Display(Name = "Has JHook")]
        public bool HasJHook { get; set; }

        [Display(Name = "Has Label")]
        public bool HasLabel { get; set; }

        [Display(Name = "Outer Packing Way")]
        public PackingWay OuterPackingWay { get; set; }

        [Display(Name = "Inner Packing Way")]
        public PackingWay InnerPackingWay { get; set; }

        [Display(Name = "Shipping Way")]
        public ShippingWay ShippingWay { get; set; }

        private float PaperLength
        {
            get
            {
                return Width * 2 + Gusset * 2 + 3;
            }
        }

        private float PaperWidth
        {
            get
            {
                return Height + Gusset * 2 / 3 + 3;
            }
        }

        private float PaperArea
        {
            get
            {
                return PaperLength * PaperWidth;
            }
        }

        private float LengthOfOuterBox
        {
            get
            {
                return Height + 5;
            }
        }

        private float WidthOfOuterBox
        {
            get
            {
                return Width + 2;
            }
        }

        private float HeightOfOuterBox
        {
            get
            {
                return (int)OuterPackingWay * 0.3F + 3;
            }
        }

        private float AreaOfOuterBox
        {
            get
            {
                return ((LengthOfOuterBox + WidthOfOuterBox) * 2 + 3) * (WidthOfOuterBox + HeightOfOuterBox) / 10000;
            }
        }

        public int CountOf20GP
        {
            get
            {
                return (int)(26 / (LengthOfOuterBox * WidthOfOuterBox * HeightOfOuterBox / 1000000));
            }
        }

        public int CountOf40GP
        {
            get
            {
                return (int)(56 / (LengthOfOuterBox * WidthOfOuterBox * HeightOfOuterBox / 1000000));
            }
        }

        public int CountOf40HQ
        {
            get
            {
                return (int)(66 / (LengthOfOuterBox * WidthOfOuterBox * HeightOfOuterBox / 1000000));
            }
        }

        public float CostOfShipping
        {
            get
            {
                return LengthOfOuterBox * WidthOfOuterBox * HeightOfOuterBox / 1000000 * 50 / (int)OuterPackingWay;
            }
        }

        public override float EvaluatePrice(ICalculateFactor factor)
        {
            FactorForPaperBag bagFactor = factor as FactorForPaperBag;
            if (bagFactor == null)
            {
                throw new InvalidCastException();
            }

            var materialPrice = 0F;
            switch(Material)
            {
                case PaperMaterialType.CoatedArtPaper:
                    materialPrice = bagFactor.CoatedArtPaper;
                    break;
                case PaperMaterialType.FoilPaper:
                    materialPrice = bagFactor.FoilPaper;
                    break;
                case PaperMaterialType.MetalisedPaper:
                    materialPrice = bagFactor.MetalisedPaper;
                    break;
                case PaperMaterialType.NaturalKraftPaper:
                    materialPrice = bagFactor.NaturalKraftPaper;
                    break;
                case PaperMaterialType.WhiteCardboard:
                    materialPrice = bagFactor.WhiteCardboard;
                    break;
                case PaperMaterialType.WhiteKraftPaper:
                    materialPrice = bagFactor.WhiteKraftPaper;
                    break;
                case PaperMaterialType.Other:
                    materialPrice = 10000;
                    break;
            }

            var paperCost = 1.02 * PaperArea / 10000 * (int)MaterialWeight * materialPrice / 1000000;

            var printCost = bagFactor.LaborOfPrintOnePaper * (PaperWidth >= 70 ? 2 : 1) * (PaperLength >= 70 ? 2 : 1);

            var laminationPrice = 0F;
            switch(Lamination)
            {
                case LaminationType.Matt:
                    laminationPrice = bagFactor.Matt;
                    break;
                case LaminationType.Glossy:
                    laminationPrice = bagFactor.Glossy;
                    break;
                case LaminationType.None:
                    break;
            }
            var laminationCost = PaperArea * laminationPrice / 10000;

            var embellishmentPrice = 0F;
            switch(SurfaceEmbellishment)
            {
                case SufaceEmbellishment.Glitter:
                    embellishmentPrice = bagFactor.Glitter;
                    break;
                case SufaceEmbellishment.HotStamping:
                    embellishmentPrice = bagFactor.HotStamping;
                    break;
                case SufaceEmbellishment.SpotUV:
                    embellishmentPrice = bagFactor.SpotUV;
                    break;
                case SufaceEmbellishment.TipOn:
                    embellishmentPrice = bagFactor.TipOn;
                    break;
                case SufaceEmbellishment.Other:
                    embellishmentPrice = bagFactor.OtherEmbellishment;
                    break;
                case SufaceEmbellishment.None:
                    break;
            }
            var embellishmentCost = Height * Width * embellishmentPrice / 10000;

            var cir = Height + Width + Gusset;
            var laborPrice = 0F;
            if (cir <= 30)
                laborPrice = bagFactor.LaborOfSmallSize;
            else if (cir >= 50)
                laborPrice = bagFactor.LaborOfLargeSize;
            else
                laborPrice = bagFactor.LaborOfMediumSize;

            var laborCost = laborPrice * ((PaperWidth > PaperLength ? PaperWidth : PaperLength) > 110 ? 2 : 1);

            var handlePrice = 0F;
            switch(Handle)
            {
                case HandleType.GrosgrainRibbon:
                    handlePrice = bagFactor.GrosgrainRibbon;
                    break;
                case HandleType.OrganzaRibbon:
                    handlePrice = bagFactor.OrganzaRibbon;
                    break;
                case HandleType.PaperTwist:
                    handlePrice = bagFactor.PaperTwist;
                    break;
                case HandleType.PP:
                    handlePrice = bagFactor.PP;
                    break;
                case HandleType.SantinRibbon:
                    handlePrice = bagFactor.SantinRibbon;
                    break;
                case HandleType.Other:
                    handlePrice = bagFactor.OtherHandle;
                    break;
            }

            var packingCost = AreaOfOuterBox * 3.5 / (int)OuterPackingWay;
            var shippingCost = CostOfShipping;

            return (float)(paperCost + printCost + laminationCost + embellishmentCost + laborCost + handlePrice +
                bagFactor.LaborOfFolding + bagFactor.PriceOfLabel + bagFactor.PriceOfJHook + bagFactor.PriceOfTag
                + packingCost + shippingCost);
        }
    }
}
