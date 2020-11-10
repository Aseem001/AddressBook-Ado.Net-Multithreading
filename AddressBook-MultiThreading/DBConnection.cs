// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DBConnection.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator Name="Aseem Anand"/>
// --------------------------------------------------------------------------------------------------------------------
namespace AddressBook_MultiThreading
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Text;

    class DBConnection
    {
        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <returns></returns>
        public SqlConnection GetConnection()
        {
            //For windows authentication
            //public static string ConnectionString = "Data Source=ASEEMANAND\SQLEXPRESS;Initial Catalog=payroll_service;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            //For sql authentication
            string connectionString = @"Server=ASEEMANAND\SQLEXPRESS; Initial Catalog =addressbook_service; User ID = aseemanand1; Password=12345";
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }
    }
}
