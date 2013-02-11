using Future.Core.Domain.Person;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;
using System.ComponentModel.DataAnnotations;

namespace Future.Core.Domain.Sales
{
    [Alias("SalesTerritory")]
    public partial class Territory : IHasIntId
    {
        [AutoIncrement]
        public virtual int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        //[References(typeof(CountryRegion))]
        public string CountryRegionCode { get; set; }

        public virtual CountryRegion CountryRegion { get; set; }
    }
}
