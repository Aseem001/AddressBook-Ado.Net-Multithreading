// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator Name="Aseem Anand"/>
// --------------------------------------------------------------------------------------------------------------------
namespace AddressBook_MultiThreading
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            AddressBookRepository repository = new AddressBookRepository();
            //UC 16
            repository.RetrieveAllContactDetails();
            //UC 17
            Console.WriteLine(repository.UpdateExistingContactUsingName("Rahul", "Kumar", "Email", "rahul12345@gmail.com") ? "Update done" : "Update Failed");
            //UC 18
            repository.GetContactsAddedInPeriod("2018-01-01", "2020-01-01");
            //UC 19
            repository.GetNumberOfContactsByCityOrState();
            //UC 20
            AddressBookModel contactDetails = new AddressBookModel();
            contactDetails.FirstName = "Virat";
            contactDetails.LastName = "Kohli";
            contactDetails.Address = "Chinnaswamy";
            contactDetails.City = "Bangalore";
            contactDetails.State = "Karnataka";
            contactDetails.Zip = 432565;
            contactDetails.PhoneNumber = 6787665678;
            contactDetails.Email = "viratkohli@gmail.com";
            contactDetails.DateAdded = Convert.ToDateTime("2019-06-10");
            contactDetails.AddressBookName = "BCCI";
            contactDetails.ContactType = "CRICKETER";
            contactDetails.TypeCode = "CRI";
            Console.WriteLine(repository.AddContactDetailsIntoDataBase(contactDetails) ? "Contact added successfully" : "Contact was not added");
        }
    }
}
