using Future.Core.Domain.Sales;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Future.Core.Domain.Person
{
    public partial class CountryRegion
    {
        private ICollection<Territory> _territories;

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

        public virtual ICollection<Territory> Territories
        {
            get { return _territories ?? (_territories = new List<Territory>()); }
            set { _territories = value; }
        }
    }
}
