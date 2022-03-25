using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace MainTestService.Models
{
    [DataContract]
    public class Product
    {
        [DataMember(Name = "Id", Order = 1), Key, Range(0.0, 50.0)]
        public int Id { get; set; }

        [DataMember(Name = "Name", Order = 2)]
        public string? Name { get; set; }

       
        [DataMember(Name = "Category", Order = 3)]
        public string? Category { get; set; }


        [DataMember(Name = "Price", Order = 4)]
        public decimal Price { get; set; }
    }
}