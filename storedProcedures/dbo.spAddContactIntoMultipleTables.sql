create procedure dbo.spAddContactIntoMultipleTables
(
@firstname varchar(100),
@lastname varchar(100),
@address varchar(500),
@phonenumber bigint,
@email varchar(250),
@dateadded varchar(100),
@city varchar(50),
@state varchar(50),
@zip int,
@contacttype varchar(50),
@typecode varchar(3),
@addressbookname varchar(50)
)
as
Begin
     --set nocount on added to prevent extra result sets from
	 --interfering with select statements
Set nocount on;
begin try
Begin transaction
declare @firstnameexists varchar(100)
declare @lastnameexists varchar(100)
declare @typecodeexists varchar(3)
declare @addressbooknameexists varchar(50)

		     --insert into contact  table
			 select @firstnameexists=FirstName ,@lastnameexists=LastName
			 from contact
			 where FirstName=@firstname and LastName=@lastname
			 if(@firstnameexists is null and @lastnameexists is null)
			 begin
				insert into contact(FirstName,LastName,Address,PhoneNo,Email,dateAdded)
				values(@firstname,@lastname,@address,@phonenumber,@email,convert(date,@dateadded));				
			 end
			 --Check if type code exists if not insert into type table
			 select @typecodeexists=TypeCode
			 from type
			 where TypeCode=@typecode
			 if(@typecodeexists is null)
			 begin
			     insert into type(TypeCode,ContactType)			 
			     values(@typecode,@contacttype);
			 end
			 --Check if addressbook name exists
			 select @addressbooknameexists =AddressBookName
			 from addressbookList
			 where AddressBookName=@addressbookname
			 if(@addressbooknameexists is null)
			 begin
			     insert into addressbookList(AddressBookName)
			     values(@addressbookname);	
			 end
			 --inserting into contact_address
			 insert into contact_address(FirstName,LastName,Address,City,State,Zip) 
			 values(@firstname,@lastname,@address,@city,@state,@zip);
			 --inserting into contact_type
			 insert into contact_type(FirstName,LastName,TypeCode)
			 values(@firstname,@lastname,@typecode);
			 --inserting into addressbookmap table
			 insert into addressbookmap(FirstName,LastName,AddressBookName)
			 values(@firstname,@lastname,@addressbookname);
			 			 						
		--if not error, commit transaction 
		commit transaction
		End Try
		Begin catch
		   --if error, roll back changes done by any of the sql queries
		   PRINT 'Rollin back changes'; 
		   Rollback transaction
		End catch
End