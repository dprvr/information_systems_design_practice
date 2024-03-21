using System.ComponentModel.DataAnnotations;
namespace Entities
{
    public class Client
    {
        
        public int ID { get; set; }
        [Required]
        [StringLength(20)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(20)]
        public string LastName { get; set; }
        [Required]
        [StringLength(50)]
        public string Address { get; set; }
        [StringLength(10)]
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        [Required]
        [StringLength(20)]
        public string City { get; set; }

        public Client()
        {

        }

        public Client(int iD, string firstName, string lastName, string address, string phone, string city)
        {
            ID = iD;
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            Phone = phone;
            City = city;
        }

        public override string ToString()
        {
            return $"[{ID}] -> {LastName} {FirstName} City:{City}";
        }

    }
}
