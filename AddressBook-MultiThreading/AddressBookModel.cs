// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AddressBookModel.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator Name="Aseem Anand"/>
// --------------------------------------------------------------------------------------------------------------------
namespace AddressBook_MultiThreading
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AddressBookModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zip { get; set; }
        public double PhoneNumber { get; set; }
        public string Email { get; set; }
        public string ContactType { get; set; }
        public string AddressBookName { get; set; }
        public DateTime DateAdded { get; set; }
        public string TypeCode { get; set; }
    }
}
