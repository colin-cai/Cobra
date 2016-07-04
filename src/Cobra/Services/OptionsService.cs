using Cobra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cobra.Services
{
    public class OptionsService
    {
        public string[] ListOrderStates()
        {
            return Enum.GetNames(typeof(PreOrderState));
        }

        public string[] ListProductTypes()
        {
            return Enum.GetNames(typeof(ProductType));
        }

        public string[] ListUnits()
        {
            return Enum.GetNames(typeof(Unit));
        }

        public string[] ListPaperWeights()
        {
            return Enum.GetNames(typeof(PaperWeight));
        }

        public string[] ListPrintStyles()
        {
            return Enum.GetNames(typeof(PrintStyle));
        }
    }
}
