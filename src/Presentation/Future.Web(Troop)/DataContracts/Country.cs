using EF.Troop;
using System.Runtime.Serialization;

namespace Future.Web_Troop_.DataContracts
{
    [DataContract]
    public class Country : ResourceBase
    {
        public const string RESOURCE_TYPE = "country";

        public Country(string code)
        {
            Id = new ResourceIdentity(RESOURCE_TYPE, code);
        }

        [DataMember]
        public string Name { get; set; }
    }
}