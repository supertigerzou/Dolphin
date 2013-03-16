using EF.Troop;
using EF.Troop.Query;
using Future.Web_Troop_.DataContracts;
using System.Collections.Generic;

namespace Future.Web_Troop_.Query
{
    public class CountryResourceLoader : IResourceLoader
    {
        public IEnumerable<IResource> Load(IResourceServiceContext context, IEnumerable<string> codes)
        {
            var results = new List<Country>();

            foreach (var code in codes)
            {
                results.Add(new Country(code) { Name = "Country_" + code });
            }

            return results;
        }
    }
}