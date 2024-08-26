using fakestrore_Net.DTOs;

namespace fakestrore_Net.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime OrderUpdatedDate {  get; set; } 
        public int UserId { get; set; }
        public User User { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; } 
        public string Note { get; set; }     
        public string IsActive { get; set; }
        public string Status { get; set; }
        public ICollection<OrderProduct> OrderProducts { get; set; }

    }
}
