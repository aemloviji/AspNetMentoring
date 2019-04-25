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

    public partial class CustomerDemographics
    {
        /// <summary>
        /// Initializes a new instance of the CustomerDemographics class.
        /// </summary>
        public CustomerDemographics()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the CustomerDemographics class.
        /// </summary>
        public CustomerDemographics(string customerTypeId = default(string), string customerDesc = default(string), IList<CustomerCustomerDemo> customerCustomerDemo = default(IList<CustomerCustomerDemo>))
        {
            CustomerTypeId = customerTypeId;
            CustomerDesc = customerDesc;
            CustomerCustomerDemo = customerCustomerDemo;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "customerTypeId")]
        public string CustomerTypeId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "customerDesc")]
        public string CustomerDesc { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "customerCustomerDemo")]
        public IList<CustomerCustomerDemo> CustomerCustomerDemo { get; set; }

    }
}
