using Microsoft.EntityFrameworkCore;
using MovieRental.Data;

namespace MovieRental.Rental
{
	public class RentalFeatures : IRentalFeatures
	{
		private readonly MovieRentalDbContext _movieRentalDb;
		public RentalFeatures(MovieRentalDbContext movieRentalDb)
		{
			_movieRentalDb = movieRentalDb;
		}

		public async Task<Rental> Save(Rental rental)
		{
			await _movieRentalDb.Rentals.AddAsync(rental);
			await _movieRentalDb.SaveChangesAsync();
			return rental;
		}

		public IEnumerable<Rental> GetRentalsByCustomerName(string customerName)
		{
            return _movieRentalDb.Rentals.Where(x=>x.CustomerName == customerName).ToList();
		}

        /*public string Payment(Rental rental)
        {
            switch(rental.PaymentMethod)
			{
				case "MbWay":
					return PaymentProviders.MbWayProvider.;
				case "PayPal":
					return PaymentProviders.PayPalProvider;
				default:
					return null;
			}
        }*/

    }
}
