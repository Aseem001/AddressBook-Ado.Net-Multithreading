// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator Name="Aseem Anand"/>
// --------------------------------------------------------------------------------------------------------------------
namespace AddressBook_MultiThreading
{
    using System;
    using System.Collections.Generic;

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
            //UC 21
            AddressBookModel contactDetails1 = new AddressBookModel();
            contactDetails1.FirstName = "Rohit";
            contactDetails1.LastName = "Sharma";
            contactDetails1.Address = "Wankhede";
            contactDetails1.City = "Mumbai";
            contactDetails1.State = "Maharashtra";
            contactDetails1.Zip = 654567;
            contactDetails1.PhoneNumber = 3456453345;
            contactDetails1.Email = "rs@gmail.com";
            contactDetails1.DateAdded = Convert.ToDateTime("2019-01-10");
            contactDetails1.AddressBookName = "B";
            contactDetails1.ContactType = "B";
            contactDetails1.TypeCode = "B";
            AddressBookModel contactDetails2 = new AddressBookModel();
            contactDetails2.FirstName = "MS";
            contactDetails2.LastName = "Dhoni";
            contactDetails2.Address = "Chidambaram";
            contactDetails2.City = "Chennai";
            contactDetails2.State = "Tamil Nadu";
            contactDetails2.Zip = 546765;
            contactDetails2.PhoneNumber = 2345432345;
            contactDetails2.Email = "msd@gmail.com";
            contactDetails2.DateAdded = Convert.ToDateTime("2018-06-10");
            contactDetails2.AddressBookName = "A";
            contactDetails2.ContactType = "A";
            contactDetails2.TypeCode = "A";
            List<AddressBookModel> contactList = new List<AddressBookModel>();
            contactList.Add(contactDetails1);
            contactList.Add(contactDetails2);
            repository.AddMultipleContactsUsingThread(contactList);
        }
    }
}
