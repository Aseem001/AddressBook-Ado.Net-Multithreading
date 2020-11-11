// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AddressBookRepository.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator Name="Aseem Anand"/>
// --------------------------------------------------------------------------------------------------------------------
namespace AddressBook_MultiThreading
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.Text;
    using System.Threading;

    public class AddressBookRepository
    {
        public static SqlConnection connection { get; set; }

        /// <summary>
        /// UC 16 : Retrieves all contact details.
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void RetrieveAllContactDetails()
        {
            //Creates a new connection for every method to avoid "ConnectionString property not initialized" exception
            DBConnection dbc = new DBConnection();
            connection = dbc.GetConnection();
            AddressBookModel model = new AddressBookModel();
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("dbo.spGetAllContacts", connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            model.FirstName = reader.GetString(0);
                            model.LastName = reader.GetString(1);
                            model.Address = reader.GetString(2);
                            model.PhoneNumber = reader.GetInt64(3);
                            model.Email = reader.GetString(4);
                            model.DateAdded = reader.GetDateTime(5);
                            model.City = reader.GetString(6);
                            model.State = reader.GetString(7);
                            model.Zip = reader.GetInt32(8);
                            model.ContactType = reader.GetString(9);
                            model.AddressBookName = reader.GetString(10);
                            Console.WriteLine($"First Name: {model.FirstName}\nLast Name: {model.LastName}\nAddress: {model.Address}\nCity: {model.City}\nState: {model.State}\nZip: {model.Zip}\nPhone Number: {model.PhoneNumber}\nEmail: {model.Email}\nContact Type: {model.ContactType}\nAddress Book Name : {model.AddressBookName}");
                            Console.WriteLine("\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No data found");
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (connection.State.Equals("Open"))
                    connection.Close();
            }
        }

        /// <summary>
        /// UC 17 : Updates the column specified of the existing contact using name.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="column">The column.</param>
        /// <param name="newValue">The new value.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool UpdateExistingContactUsingName(string firstName, string lastName, string column, string newValue)
        {
            DBConnection dbc = new DBConnection();
            connection = dbc.GetConnection();
            try
            {
                using (connection)
                {
                    connection.Open();
                    string query = $@"update dbo.contact set {column}='{newValue}' where FirstName='{firstName}' and LastName='{lastName}'";
                    SqlCommand command = new SqlCommand(query, connection);
                    var result = command.ExecuteNonQuery();
                    connection.Close();
                    if (result > 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (connection.State.Equals("Open"))
                    connection.Close();
            }
        }

        /// <summary>
        /// UC 18 : Gets the contacts added in period.
        /// </summary>
        /// <param name="startdate">The startdate.</param>
        /// <param name="endDate">The end date.</param>
        public void GetContactsAddedInPeriod(string startdate, string endDate)
        {
            string query = $@"select c.*,ca.city,ca.state,ca.zip,t.ContactType,am.AddressBookName from contact c,contact_address ca,type t,addressbookmap am,contact_type ct where c.dateAdded between cast('{startdate}' as date)  and cast('{endDate}' as date) and c.Firstname=ca.FirstName and c.LastName=ca.lastName and c.Firstname=ct.FirstName and c.LastName=ct.lastName and t.TypeCode=ct.TypeCode and c.Firstname=am.FirstName and c.LastName=am.lastName";
            GetData(query);
        }

        /// <summary>
        /// UC 19 : Gets the state of the number of contacts by city or.
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void GetNumberOfContactsByCityOrState()
        {
            Console.WriteLine("Enter:\n1.For city\n2.For state");
            int option = Convert.ToInt32(Console.ReadLine());
            string query = "";
            switch (option)
            {
                case 1:
                    query = $@"select City,count(City) as PeopleInCity from address_book group by City";
                    break;
                case 2:
                    query = $@"select State,count(State) as PeopleInCity from address_book group by State";
                    break;
            }
            DBConnection dbc = new DBConnection();
            connection = dbc.GetConnection();
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string location = reader[0].ToString();
                            int count = reader.GetInt32(1);
                            Console.WriteLine($"City/State:{location}\nPeopleCount:{count}\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No Data found");
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (connection.State.Equals("Open"))
                    connection.Close();
            }
        }

        /// <summary>
        /// UC 20 : Adds the contact details in database in all corresponding tables.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public bool AddContactDetailsIntoDataBase(AddressBookModel model)
        {
            DBConnection dbc = new DBConnection();
            connection = dbc.GetConnection();
            try
            {             
                using (connection)
                {
                    SqlCommand command = new SqlCommand();
                    command.CommandText = "dbo.spAddContactIntoMultipleTables";
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@firstname", model.FirstName);
                    command.Parameters.AddWithValue("@lastname", model.LastName);
                    command.Parameters.AddWithValue("@address", model.Address);
                    command.Parameters.AddWithValue("@city", model.City);
                    command.Parameters.AddWithValue("@state", model.State);
                    command.Parameters.AddWithValue("@zip", model.Zip);
                    command.Parameters.AddWithValue("@phonenumber", model.PhoneNumber);
                    command.Parameters.AddWithValue("@email", model.Email);
                    command.Parameters.AddWithValue("@dateadded", model.DateAdded);
                    command.Parameters.AddWithValue("@contacttype", model.ContactType);
                    command.Parameters.AddWithValue("@typecode", model.TypeCode);
                    command.Parameters.AddWithValue("@addressbookname", model.AddressBookName);
                    connection.Open();
                    int result = command.ExecuteNonQuery();                 
                    if (result > 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                if (connection.State.Equals("Open"))
                    connection.Close();
            }
        }

        /// <summary>
        /// Gets and displays the data based on the query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <exception cref="Exception"></exception>
        public void GetData(string query)
        {
            //Creates a new connection for every method to avoid "ConnectionString property not initialized" exception
            DBConnection dbc = new DBConnection();
            connection = dbc.GetConnection();
            AddressBookModel model = new AddressBookModel();
            try
            {                
                using (connection)
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            model.FirstName = reader.GetString(0);
                            model.LastName = reader.GetString(1);
                            model.Address = reader.GetString(2);
                            model.PhoneNumber = reader.GetInt64(3);
                            model.Email = reader.GetString(4);
                            model.DateAdded = reader.GetDateTime(5);
                            model.City = reader.GetString(6);
                            model.State = reader.GetString(7);
                            model.Zip = reader.GetInt32(8);
                            model.ContactType = reader.GetString(9);
                            model.AddressBookName = reader.GetString(10);
                            Console.WriteLine($"First Name: {model.FirstName}\nLast Name: {model.LastName}\nAddress: {model.Address}\nCity: {model.City}\nState: {model.State}\nZip: {model.Zip}\nPhone Number: {model.PhoneNumber}\nDateAdded:{model.DateAdded}\nEmail: {model.Email}\nContact Type: {model.ContactType}\nAddress Book Name : {model.AddressBookName}");
                            Console.WriteLine("\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No data found");
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (connection.State.Equals("Open"))
                    connection.Close();
            }
        }

        /// <summary>
        /// UC 21 : Adds the multiple contacts int the List using thread.
        /// </summary>
        /// <param name="contactList">The contact list.</param>
        public void AddMultipleContactsUsingThread(List<AddressBookModel> contactList)
        {            
            Stopwatch s = new Stopwatch();
            s.Start();
            foreach (var contact in contactList)
            {
                // Using thread to add each contact present in the list
                Thread th = new Thread(() =>
                  {
                      Console.WriteLine("Thread id: " + Thread.CurrentThread.ManagedThreadId);
                      Console.WriteLine($"Contact being added:{contact.FirstName} {contact.LastName}");
                      AddContactDetailsIntoDataBase(contact);
                      Console.WriteLine(AddContactDetailsIntoDataBase(contact)?$"Contact added:{contact.FirstName} {contact.LastName}":"Addition Failed");
                  });
                th.Start();               
                th.Join();
            }
            s.Stop();
            Console.WriteLine("Elapsed time to add contacts:" + s.ElapsedMilliseconds);
        }
    }
}
