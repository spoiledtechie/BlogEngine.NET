using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogEngine.Core.Data.Context
{
    [Table("Contact")]
    public class ContactDb
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        
        public string RecordId { get; set; }
            public string UserId { get; set; }
        public string Information { get; set; }
        public string Title { get; set; }
        public string DisplayName { get; set; }

       public string  UserName { get; set; }

       public string  FullName { get; set; }
       public string  OtherName { get; set; }


       public string  FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

       public string  Spouse { get; set; }
       public string  AboutMe { get; set; }

       public string  PhotoUrl { get; set; }
       public string  Birthday { get; set; }
       public string  EmailAddress { get; set; }
       public string  Address { get; set; }

        public string AddressAlt { get; set; }
       public string  CityTown { get; set; }


        public string RegionState { get; set; }
       public string  Zip { get; set; }
       public string  Country { get; set; }

        public string WorkPhone { get; set; }

       public string  PhoneFax { get; set; }

        public string PhoneMobile { get; set; }
        public string  PhoneMain { get; set; }
        public string Company { get; set; }

        public string Organization { get; set; }

        public string Website { get; set; }
       public string  NewsLetter { get; set; }

        public string Code { get; set; }

        public string Private { get; set; }

    }
}