select * from Person.Person;
select * from HumanResources.Employee;

select count(*) from HumanResources.Employee
where YEAR(HireDate) > 2010;
