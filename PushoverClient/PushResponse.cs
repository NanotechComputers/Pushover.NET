using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PushoverClient
{
    /// <summary>
    /// Represents a response message from the Pushover API
    /// </summary>
    [DataContract]
    public class PushResponse
    {
        [DataMember(Name = "status")] 
        public int Status { get; set; }

        [DataMember(Name = "request")] 
        public string Request { get; set; }

        [DataMember(Name = "errors")] 
        public List<string> Errors { get; set; }
        
        [IgnoreDataMember] 
        public Limitations Limits { get; set; }
    }

    public class Limitations
    {
        public int Limit { get; set; }
        public int Remaining { get; set; }
        public string Reset { get; set; }
    }
}