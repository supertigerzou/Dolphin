using Dolphin.Core.Domain.Person;
using ServiceStack.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace Dolphin.Core.Domain.Sales
{
    [Alias("SalesTerritory")]
    public class Territory
    {
        [AutoIncrement]
        public virtual int Id { get; set; }

        [Required]
        [StringLength(50)]
        public virtual string Name { get; set; }

        [References(typeof(CountryRegion))]
        public virtual string CountryRegionCode { get; set; }

        public virtual string Group { get; set; }
        public virtual decimal SalesYTD { get; set; }
        public virtual decimal SalesLastYear { get; set; }
        public virtual decimal CostYTD { get; set; }
        public virtual decimal CostLastYear { get; set; }
        public virtual Guid RowGuid { get; set; }
        public virtual DateTime ModifiedDate { get; set; }

        public virtual CountryRegion CountryRegion { get; set; }
    }
}
