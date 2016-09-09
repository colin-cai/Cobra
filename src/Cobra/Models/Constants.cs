using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Cobra.Models
{
    public class EnumItem
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public object Value { get; set; }
    }

    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }

        public static string GetEnumDescription(Type enumType, object value)
        {
            FieldInfo fi = enumType.GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }

        public static List<EnumItem> GetEnumItems(Type enumType)
        {
            List<EnumItem> list = new List<EnumItem>();
            foreach(var value in Enum.GetValues(enumType))
            {
                list.Add(new EnumItem
                {
                    Name = Enum.GetName(enumType, value),
                    Description = GetEnumDescription(enumType, value),
                    Value = value
                });
            }

            return list;
        } 
    }

    public enum Unit
    {
        inch = 0,
        cm = 1
    }

    public enum PrintStyle
    {
        None = 0,
        [Description("1 Color")]
        SingleColor = 1,
        [Description("2 Colors")]
        DoubleColors = 2,
        [Description("4 Colors")]
        FourColors = 4,
        [Description("5 Colors")]
        FiveColors = 5,
        [Description("6 Colors")]
        SixColors = 6,
        Other = 7
    }

    public enum PaperMaterialType
    {
        [Description("Coated Art Paper")]
        CoatedArtPaper = 0,
        [Description("Foil Paper")]
        FoilPaper = 1,
        [Description("Metalised Paper")]
        MetalisedPaper = 2,
        [Description("Natural Kraft Paper")]
        NaturalKraftPaper = 3,
        [Description("White Kraft Paper")]
        WhiteKraftPaper = 4,
        [Description("White Cardboard")]
        WhiteCardboard = 5,
        Other = 6
    }

    public enum PaperMaterialWeight
    {
        [Description("80 g")]
        G80 = 80,
        [Description("100 g")]
        G100 = 100,
        [Description("120 g")]
        G120 = 120,
        [Description("128 g")]
        G128 = 128,
        [Description("140 g")]
        G140 = 140,
        [Description("157 g")]
        G157 = 157,
        [Description("180 g")]
        G180 = 180,
        [Description("210 g")]
        G210 = 210,
        [Description("230 g")]
        G230 = 230,
        [Description("250 g")]
        G250 = 250,
        [Description("300 g")]
        G350 = 350
    }

    public enum LaminationType
    {
        None = 0,
        Matt = 1,
        Glossy = 2
    }

    public enum SufaceEmbellishment
    {
        None = 0,
        Glitter = 1,
        [Description("Hot Stamping")]
        HotStamping = 2,
        [Description("Spot UV")]
        SpotUV = 3,
        [Description("Tip On")]
        TipOn = 4,
        Other = 5
    }

    public enum HandleType
    {
        PP = 0,
        [Description("Paper Twist")]
        PaperTwist = 1,
        [Description("Santin Ribbon")]
        SantinRibbon = 2,
        [Description("Grosgrain Ribbon")]
        GrosgrainRibbon = 3,
        [Description("Organza Ribbon")]
        OrganzaRibbon = 4,
        Other = 5
    }

    public enum PackingWay
    {
        [Description("4 pieces")]
        P4 = 4,
        [Description("6 pieces")]
        P6 = 6,
        [Description("12 pieces")]
        P12 = 12,
        [Description("24 pieces")]
        P24 = 24,
        [Description("36 pieces")]
        P36 = 36,
        [Description("48 pieces")]
        P48 = 48,
        [Description("72 pieces")]
        P72 = 72,
        [Description("96 pieces")]
        P96 = 96,
        [Description("144 pieces")]
        P144 = 144,
        [Description("200 pieces")]
        Other = 200
    }

    public enum ContainerType
    {
        [Description("20GP")]
        GP20 = 26,
        [Description("40GP")]
        GP40 = 56,
        [Description("40HQ")]
        HQ40 = 66,
    }

    public enum ShippingWay
    {
        LCL = 0,
        FCL = 1
    }
}
