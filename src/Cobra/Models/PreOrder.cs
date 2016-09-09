using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Cobra.Models
{
    public class PreOrder
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }


        public string Title { get; set; }

        public string Description { get; set; }

        public string Comment { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Upload your Desing File(s) in a Package")]
        [NotMapped]
        public ICollection<IFormFile> DesignFiles { get; set; }

        public string MappedFiles { get; set; }

        public ApplicationUser User { get; set; }

        public ApplicationUser UpdateUser { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }

        public PreOrderState Status { get; set; }

        public List<PaperBag> PaperBags { get; set; }

        public List<PaperBox> PaperBoxs { get; set; }
    }
     
    public enum ProductType
    {
        PaperBag,
        PaperBox
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
