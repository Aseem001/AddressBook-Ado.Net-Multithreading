﻿// --------------------------------------------------------------------------------------------------------------------
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
        }
    }
}
