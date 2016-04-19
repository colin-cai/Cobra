using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cobra.Models
{
    public class PreOrder
    {
        public int ID { get; set; }

        public ApplicationUser User { get; set; }

        public DateTime CreateTime { get; set; }
        
        public ApplicationUser UpdateUser { get;set; }
        
        public DateTime UpdateTime { get; set; }

        public PreOrderState Status { get; set; }
        
        public string Description { get;set; }

        public string Comment { get;set; }
        
        public List<PreOrderItem> Items { get; set; }
    }

    public class PreOrderItem
    {
        public int ID { get; set; }

        public Product Product { get; set; }

        public int Quantity { get; set; }
    }

    public enum PreOrderState
    {
        None,
        New,
        Saved,
        Submited,
        Reviewed,
        Sourced,
        Confirmed
    }

}
