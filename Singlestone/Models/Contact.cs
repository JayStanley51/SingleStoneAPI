using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Singlestone.Models
{
    public class Name
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string NameId { get; set; }
        public string First { get; set; }
        public string Middle { get; set; }
        public string Last { get; set; }
    }
    public class Address
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string AddressId { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }
    public class Phone
    {
       [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string PhoneId { get; set; }
        public string Number { get; set; }
        public string Type { get; set; }
    }
    public class Contact
    {
        public int ContactId { get; set; }
        public Name Name { get; set; }
        public Address Address { get; set; }
        public List<Phone> Phone { get; set; }
        public string Email { get; set; }
    }
   
    public class CallList
    {
        public int Id { get; set; }
        public Name Name { get; set; }
        public string Number { get; set; }

    }
}
