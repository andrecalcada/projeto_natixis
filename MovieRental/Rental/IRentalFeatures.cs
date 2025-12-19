namespace MovieRental.Rental;

public interface IRentalFeatures
{
	Task<Rental> Save(Rental rental);
	IEnumerable<Rental> GetRentalsByCustomerName(string customerName);
}