// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Moldule4.Tests.Models
{
    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public partial class Region
    {
        /// <summary>
        /// Initializes a new instance of the Region class.
        /// </summary>
        public Region()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the Region class.
        /// </summary>
        public Region(int? regionId = default(int?), string regionDescription = default(string), IList<Territories> territories = default(IList<Territories>))
        {
            RegionId = regionId;
            RegionDescription = regionDescription;
            Territories = territories;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "regionId")]
        public int? RegionId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "regionDescription")]
        public string RegionDescription { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "territories")]
        public IList<Territories> Territories { get; set; }

    }
}
