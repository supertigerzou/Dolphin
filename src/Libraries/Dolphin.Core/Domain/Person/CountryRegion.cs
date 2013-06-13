using Dolphin.Core.Domain.Sales;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dolphin.Core.Domain.Person
{
    public class CountryRegion
    {
        private ICollection<Territory> _territories;

        [Required]
        [Alias("CountryRegionCode")]
        [StringLength(3)]
        public virtual string Code { get; set; }

        [Required]
        [StringLength(50)]
        public virtual string Name { get; set; }

        [Required]
        [Default(typeof(DateTime), "getdate()")]
        public virtual DateTime ModifiedDate { get; set; }

        public virtual ICollection<Territory> Territories
        {
            get { return _territories ?? (_territories = new List<Territory>()); }
            set { _territories = value; }
        }
    }
}
