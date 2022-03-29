using System.Runtime.Serialization;

namespace MainTestService.Models
{
    [DataContract]
    public class ReturnMessage
    {
        [DataMember(Name = "Message")]
        public string? Message { get; set; }
    }

}
