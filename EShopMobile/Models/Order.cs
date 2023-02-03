using System;

namespace EShopMobile.Models
{
    /// <summary>
    /// An instance of an order
    /// </summary>
    public class Order
    {
        /// <summary>
        /// The id of the order
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The customer's name of the order
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// The customer instance it self
        /// </summary>
        public Customer Customer { get; set; }

        /// <summary>
        /// The customer's id that the order is connected to 
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// The final price of the order
        /// </summary>
        public float FinalPrice { get; set; }

        /// <summary>
        /// The date that the order was placed
        /// </summary>
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// The state of the order
        /// </summary>
        public bool Completed { get; set; }
    }
}