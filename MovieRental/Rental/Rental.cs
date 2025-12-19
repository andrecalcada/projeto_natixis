using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieRental.Rental
{
	public class Rental
	{
		[Key]
		public int Id { get; set; }
		public int DaysRented { get; set; }
		public Movie.Movie? Movie { get; set; }

		[ForeignKey("Movie")]
		public int MovieId { get; set; }

		public string PaymentMethod { get; set; }

        public string CustomerName { get; set; }

        // Navigation property. The attribute points to the FK property name.
        [ForeignKey(nameof(CustomerName))]
        public Customer.Customer.Customer? Customer { get; set; }
    }
}
