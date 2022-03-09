using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Bikemvc.Models
{
    public class BikeInfo
    {
        public int BikeInfoId { get; set; }
        [StringLength(8, MinimumLength = 2)]
        [Display(Name = "Bike Name")]
        [Required(ErrorMessage = "This Field can not be blank")]
        public string BikeName { get; set; }
        public virtual ICollection<ModelInfo> ModelInfos { get; set; }
    }
    public class ModelInfo
    {
        [Key]
        public int ModelInfoId { get; set; }
        [StringLength(8, MinimumLength = 2)]
        [Display(Name = "Model")]
        [Required(ErrorMessage = "ModelName can not be blank")]
        public string ModelName { get; set; }
        public bool IsAvailable { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? ReleaseDate { get; set; }

        [Required(ErrorMessage = "Add a Value")]
        [Range(minimum: 200, maximum: 600)]
        public int ModelPrice { get; set; }
        public string Image { get; set; }

        [NotMapped]
        public HttpPostedFileBase PicturePFB { get; set; }

        public int BikeInfoId { get; set; }
        public BikeInfo BikeInfo { get; set; }
    }
    public class SignUp
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}