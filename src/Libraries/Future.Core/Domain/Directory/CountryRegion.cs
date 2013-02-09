using ServiceStack.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace Future.Core.Domain.Directory
{
    public class CountryRegion
    {
        [Required]
        [Alias("CountryRegionCode")]
        [StringLength(3)]
        public string Code { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [Default(typeof(DateTime), "getdate()")]
        public DateTime ModifiedDate { get; set; }
    }
}
