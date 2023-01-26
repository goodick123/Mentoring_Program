using System.Runtime.Serialization;

namespace DAL.Models
{
    [DataContract]
    public class LocalizedBook : Book
    {
        public Publisher LocalPublisher { get; set; }

        public Country CountryOfLocalization { get; set; }
    }
}
