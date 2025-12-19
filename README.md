# MovieRental Exercise

This is a dummy representation of a movie rental system.
Can you help us fix some issues and implement missing features?

 * The app is throwing an error when we start, please help us. Also, tell us what caused the issue.
 * The rental class has a method to save, but it is not async, can you make it async and explain to us what is the difference?
 * Please finish the method to filter rentals by customer name, and add the new endpoint.
 * We noticed we do not have a table for customers, it is not good to have just the customer name in the rental.
   Can you help us add a new entity for this? Don't forget to change the customer name field to a foreign key, and fix your previous method!
 * In the MovieFeatures class, there is a method to list all movies, tell us your opinion about it.
 * No exceptions are being caught in this api, how would you deal with these exceptions?


	## Challenge (Nice to have)
We need to implement a new feature in the system that supports automatic payment processing. Given the advancements in technology, it is essential to integrate multiple payment providers into our system.

Here are the specific instructions for this implementation:

* Payment Provider Classes:
    * In the "PaymentProvider" folder, you will find two classes that contain basic (dummy) implementations of payment providers. These can be used as a starting point for your work.
* RentalFeatures Class:
    * Within the RentalFeatures class, you are required to implement the payment processing functionality.
* Payment Provider Designation:
    * The specific payment provider to be used in a rental is specified in the Rental model under the attribute named "PaymentMethod".
* Extensibility:
    * The system should be designed to allow the addition of more payment providers in the future, ensuring flexibility and scalability.
* Payment Failure Handling:
    * If the payment method fails during the transaction, the system should prevent the creation of the rental record. In such cases, no rental should be saved to the database.


# Implementation
* The app is throwing an error when we start, please help us. Also, tell us what caused the issue.
    * So the problem is that the classes, Movie and Rental, in DbContext are declared as scope and not singleton so in the program.cs that needs to be fixed.
* The rental class has a method to save, but it is not async, can you make it async and explain to us what is the difference?
    * First what is the diferrence between an async method and non sync method. By default a method is sync, this means it runs all its code sequencially ignoring if the line of code has terminated what it should do or not, it just executes; an async method doesn't just run the code it runs the code but when a line of code as an await it awaits for this line of code to finish its execution and only after this it executes the next line of code.
    * Secondly yes i can make it async to ensure that when the function returns that data is saved in the database, to do this the function must be declared as async, this must also be reflected in the interface, and add an await on the lines where the data is added to the database.
* Please finish the method to filter rentals by customer name, and add the new endpoint.
    * To do this all that is needed is to replace the return []; by return _movieRentalDb.Rentals.Where(x=>x.CustomerName == customerName).ToList();
 * We noticed we do not have a table for customers, it is not good to have just the customer name in the rental.
   * To do this first we need create a table for Customers, and add a foreign key between Customer.CustomerName and Rentals.CustomerName, this includes adding customer class, update rental class and updating DbContext
* In the MovieFeatures class, there is a method to list all movies, tell us your opinion about it.
    * First think that comes to my mind is why do you need all the movies if the objective is to get a list of all movies this is not correct, pagination is missing at least because if you have more than 10 movies in the database this is only making it dificult for frontend besides if this was sensitive data we would be giving all the data which is a huge security problem besides the memory consumption
 * No exceptions are being caught in this api, how would you deal with these exceptions?
     * First of not dealing with execptions is bad because there can always be unexpected errors. 
     Secondly each method should be surrounded by a try catch, there can be multiple in some more complex methods. the handling of execptions should always be handled like this log the error, in a log file, and return something consedering each method but making sure that the error can be found in the frontend, returning json is always good of an object that contains a success bolean {Sucess:false, Data: data (null or default value)}