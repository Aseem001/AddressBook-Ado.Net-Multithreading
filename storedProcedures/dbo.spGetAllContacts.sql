CREATE PROCEDURE dbo.spGetAllContacts
AS
begin
select c.*,ca.city,ca.state,ca.zip,t.ContactType,am.AddressBookName
from contact c,contact_address ca,type t,addressbookmap am,contact_type ct
where c.Firstname=ca.FirstName and c.LastName=ca.lastName
and c.Firstname=ct.FirstName and c.LastName=ct.lastName
and t.TypeCode=ct.TypeCode
and c.Firstname=am.FirstName and c.LastName=am.lastName
end