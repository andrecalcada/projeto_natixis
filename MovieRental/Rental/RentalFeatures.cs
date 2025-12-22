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
            await using var transaction = await _movieRentalDb.Database.BeginTransactionAsync();
            try
            {
                if (rental.PaymentMethod is null)
                    throw new InvalidOperationException("Payment method is required.");

                var MbWayProvider = new PaymentProviders.MbWayProvider();
                var PayPalProvider = new PaymentProviders.PayPalProvider();
                var Price = rental.DaysRented * 2/*Price per day*/;
                bool Success = false;
                switch (rental.PaymentMethod)
                {
                    case "MbWay":
                        Success = await MbWayProvider.Pay(Price);
                        break;
                    case "PayPal":
                        Success = await PayPalProvider.Pay(Price);
                        break;
                    default:
                        throw new InvalidOperationException($"Unsupported payment method: {rental.PaymentMethod}");
                }
                if(!Success)
                    throw new InvalidOperationException("Payment failed.");
                await _movieRentalDb.Rentals.AddAsync(rental);
                await _movieRentalDb.SaveChangesAsync();

                await transaction.CommitAsync();

                return rental;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

		public IEnumerable<Rental> GetRentalsByCustomerName(string customerName)
		{
            return _movieRentalDb.Rentals.Where(x=>x.CustomerName == customerName).ToList();
		}

    }
}
