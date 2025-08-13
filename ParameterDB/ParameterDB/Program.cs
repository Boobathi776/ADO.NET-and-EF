using System.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;
using Parameter;
using Parameter.Utility;

public class Program
{
    public static void Main(string[] args)
    {
        Console.Clear();
        Console.WriteLine("*****************************************************************************************************");
        Console.WriteLine("********************************************       CRUD       ***************************************");
        Console.WriteLine("*****************************************************************************************************");
        CRUD crud = new CRUD();
        //crud.SpReturnMultipleResultSetUsingDataTable();
        //crud.SPReturnMultipleResultSet();
        //crud.CallStoredProcedure();
        //crud.CallStoredProcedureWithParameter(1);

        int option = ShowOptions();

        do
        {
            switch (option)
            {
                case 1:
                    crud.InsertWithParameter();
                    crud.ShowAllRecord();
                    option = ShowOptions();
                    break;
                case 2:
                    Console.WriteLine("\nAvailable Student list :");
                    crud.ShowAllRecord();
                    crud.UpdateWithParameter(InputValidation.GetStudentID());
                    Console.WriteLine("\nAfter the Student data updated :");
                    crud.ShowAllRecord();
                    option = ShowOptions();
                    break;
                case 3:
                    Console.WriteLine("\nAvailable Student list :");
                    crud.ShowAllRecord();
                    crud.DeleteWithParameter();
                    Console.WriteLine("\nAfter the Record Deletion :");
                    crud.ShowAllRecord();
                    option = ShowOptions();
                    break;
                case 4:
                    crud.ShowAllRecord();
                    option = ShowOptions();
                    break;
                case 5:
                    crud.CallStoredProcedure();
                    option = ShowOptions();
                    break;
                case 6:
                    Console.WriteLine("\nAvailable Student list :");
                    crud.ShowAllRecord();
                    crud.CallStoredProcedureWithParameter(InputValidation.GetStudentID());
                    option = ShowOptions();
                    break;
                case 7:
                    crud.SPReturnMultipleResultSet();
                    option = ShowOptions();
                    break;
                case 8:
                    crud.SpReturnMultipleResultSetUsingDataTable();
                    option = ShowOptions();
                    break;
                case 9:
                    Console.WriteLine("Exiting.......");
                    break;

                default:
                    Console.WriteLine("Invalid choice......");
                    break;
            }
        } while (option!=9);

    }

    public static int ShowOptions()
    {
        Console.Write("1.INSERT\n2.UPDATE\n3.DELETE\n4.VIEW ALL\n5.CALL STORED PROCEDURE(All student data)\n6.CALL STORED PROCEDURE WITH PARAMETER (One student data will return)\n" +
            "7.GET STUDENT AND COURSE (SP RETURN MULTIPLE RESULT SET)\n8.SP MULTIPLE RESULT SET USING DATASET\n9.EXIT\n");
        int numberOfOptions = 9;
        int option = InputValidation.GetValueForSwtich(numberOfOptions);
        return option;
    }
}


//using Microsoft.Data.SqlClient;

//string connString = "Server=BSD-BOOBATHIA01\\SQLEXPRESS;Database=Student;Integrated Security=SSPI;TrustServerCertificate=True;";
//using (SqlConnection conn = new SqlConnection(connString))
//{
//    conn.Open();
//    Console.Write("connected successfully");
//}