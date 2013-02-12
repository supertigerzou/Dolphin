using Future.Core.Domain.Person;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;
using System.ComponentModel.DataAnnotations;

namespace Future.Core.Domain.Sales
{
    [Alias("SalesTerritory")]
    public partial class Territory
    {
        [AutoIncrement]
        public virtual int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [References(typeof(CountryRegion))]
        public string CountryRegionCode { get; set; }

        public string Group { get; set; }
        public decimal SalesYTD { get; set; }
        public decimal SalesLastYear { get; set; }
        public decimal CostYTD { get; set; }
        public decimal CostLastYear { get; set; }
        public System.Guid RowGuid { get; set; }
        public System.DateTime ModifiedDate { get; set; }

        public virtual CountryRegion CountryRegion { get; set; }
    }
}
