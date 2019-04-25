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

    public partial class Employees
    {
        /// <summary>
        /// Initializes a new instance of the Employees class.
        /// </summary>
        public Employees()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the Employees class.
        /// </summary>
        public Employees(int? employeeId = default(int?), string lastName = default(string), string firstName = default(string), string title = default(string), string titleOfCourtesy = default(string), System.DateTime? birthDate = default(System.DateTime?), System.DateTime? hireDate = default(System.DateTime?), string address = default(string), string city = default(string), string region = default(string), string postalCode = default(string), string country = default(string), string homePhone = default(string), string extension = default(string), byte[] photo = default(byte[]), string notes = default(string), int? reportsTo = default(int?), string photoPath = default(string), Employees reportsToNavigation = default(Employees), IList<EmployeeTerritories> employeeTerritories = default(IList<EmployeeTerritories>), IList<Employees> inverseReportsToNavigation = default(IList<Employees>), IList<Orders> orders = default(IList<Orders>))
        {
            EmployeeId = employeeId;
            LastName = lastName;
            FirstName = firstName;
            Title = title;
            TitleOfCourtesy = titleOfCourtesy;
            BirthDate = birthDate;
            HireDate = hireDate;
            Address = address;
            City = city;
            Region = region;
            PostalCode = postalCode;
            Country = country;
            HomePhone = homePhone;
            Extension = extension;
            Photo = photo;
            Notes = notes;
            ReportsTo = reportsTo;
            PhotoPath = photoPath;
            ReportsToNavigation = reportsToNavigation;
            EmployeeTerritories = employeeTerritories;
            InverseReportsToNavigation = inverseReportsToNavigation;
            Orders = orders;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "employeeId")]
        public int? EmployeeId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "lastName")]
        public string LastName { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "firstName")]
        public string FirstName { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "titleOfCourtesy")]
        public string TitleOfCourtesy { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "birthDate")]
        public System.DateTime? BirthDate { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "hireDate")]
        public System.DateTime? HireDate { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "address")]
        public string Address { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "region")]
        public string Region { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "postalCode")]
        public string PostalCode { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "homePhone")]
        public string HomePhone { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "extension")]
        public string Extension { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "photo")]
        public byte[] Photo { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "notes")]
        public string Notes { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "reportsTo")]
        public int? ReportsTo { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "photoPath")]
        public string PhotoPath { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "reportsToNavigation")]
        public Employees ReportsToNavigation { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "employeeTerritories")]
        public IList<EmployeeTerritories> EmployeeTerritories { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "inverseReportsToNavigation")]
        public IList<Employees> InverseReportsToNavigation { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "orders")]
        public IList<Orders> Orders { get; set; }

    }
}