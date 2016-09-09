using Cobra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cobra.Services
{
    public class OptionsService
    {

        public List<EnumItem> ListOrderStates()
        {
            return EnumExtensions.GetEnumItems(typeof(PreOrderState));
        }

        public List<EnumItem> ListProductTypes()
        {
            return EnumExtensions.GetEnumItems(typeof(ProductType));
        }

        public List<EnumItem> ListUnits()
        {
            return EnumExtensions.GetEnumItems(typeof(Unit));
        }

        public List<EnumItem> ListPaperWeights()
        {
            return EnumExtensions.GetEnumItems(typeof(PaperMaterialWeight));
        }

        public List<EnumItem> ListPrintStyles()
        {
            return EnumExtensions.GetEnumItems(typeof(PrintStyle));
        }

        public List<EnumItem> ListLaminationTypes()
        {
            return EnumExtensions.GetEnumItems(typeof(LaminationType));
        }

        public List<EnumItem> ListSufaceEmbellishments()
        {
            return EnumExtensions.GetEnumItems(typeof(SufaceEmbellishment));
        }

        public List<EnumItem> ListHandleTypes()
        {
            return EnumExtensions.GetEnumItems(typeof(HandleType));
        }

        public List<EnumItem> ListPackingWays()
        {
            return EnumExtensions.GetEnumItems(typeof(PackingWay));
        }

        public List<EnumItem> ListShippingWays()
        {
            return EnumExtensions.GetEnumItems(typeof(ShippingWay));
        }

        public List<EnumItem> ListPaperMaterialTypes()
        {
            return EnumExtensions.GetEnumItems(typeof(PaperMaterialType));
        }
    }
}
