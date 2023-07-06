using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Runtime.Intrinsics.Arm;
using Microsoft.VisualBasic.FileIO;
//THIS CLASS FOR EXECUTE SQL
//2 OPTION: SQLExecute for non query and SQLQuery for query
////SQLExecute 
// (Insert, tblname, value1, value 2, value 3,......)
//                  = Insert into tblname values (value1,value2,....)
// (Update, tblname, id, field1, value1, field2, value2,....)
//                  = Update tblname set field1 = value1, field2 = value2,.... where id = id
// (Delete, tblname, field1, value1, field 2, value2,....) 
//                  = Delete from tblname where field1 = value 1 and field2 = value 2
////SQLQuery
//
namespace T11
{
    internal class SQLUtils
    {
        // MS SQL Worker

        // Excute SQL non querry
        private readonly Config config = new();
        public int SQLExecute(string str)
        {
            SqlConnection cnn = new(config.conStr);
            cnn.Open();
            // get sql template
            string sql;
            SqlCommand cmd = cnn.CreateCommand();
            if (str.ToArrayString()[0] == "Insert")
            {
                // sql = Insert into tablename values (@0,@1,.....)
                sql = $"Insert into {str.ToArrayString()[1]} values ("; 
                for (int i = 2; i < str.Count()-1;i++)
                {
                    sql += "@" + i +"," ;
                }
                sql += "@"+ (str.Count()-1) + ")";
                //Console.WriteLine(sql); Console.ReadLine();
                // @0 = str.ToArrayString()[2])
                cmd = new(sql, cnn);
                for (int i = 2; i < str.Count(); i++)
                {
                    cmd.Parameters.AddWithValue("" + i + "", str.ToArrayString()[i]);                    
                }                
            }

            if (str.ToArrayString()[0] == "Update")
            {
                // sql = Update tablename set field1 = @0, field2= @2,..... where id =@id
                sql = $"Update {str.ToArrayString()[1]} Set ";
                for (int i = 3; i < str.Count() - 2; i++)
                {
                    sql += str.ToArrayString()[i] + "=@" + (i+1) + ",";
                    i++;
                }
                sql += (str.ToArrayString()[str.Count() - 2]) + "=@" + (str.Count()-1);
                sql += " where id = @id";
                //Console.WriteLine(sql); Console.ReadLine();
                // @0 = str.ToArrayString()[3] @id=str.ToArrayString()[2]
                cmd = new(sql, cnn);
                for (int i = 3; i < str.Count(); i++) 
                {
                    cmd.Parameters.AddWithValue("" + (i + 1) + "", str.ToArrayString()[i+1]);
                    i++;
                }
                cmd.Parameters.AddWithValue("id", str.ToArrayString()[2]);
            }
            if (str.ToArrayString()[0] == "Delete")
            {
                // sql = Delete tablename where field1 = @0 and field2= @2
                sql = $"Delete from {str.ToArrayString()[1]} where ";
                for (int i = 2; i < str.Count() - 2; i++)
                {
                    sql += str.ToArrayString()[i] + "=@" + (i+1) + "and";
                    i++;
                }
                sql += (str.ToArrayString()[str.Count() - 2]) + "=@" + (str.Count()-1);
                // @0=str.ToArrayString()[2],@2=str.ToArrayString()[4]
                //Console.WriteLine(sql);Console.ReadLine();
                cmd = new(sql, cnn);
                for (int i = 2; i < str.Count(); i++)
                {
                    cmd.Parameters.AddWithValue("" + (i + 1) + "", str.ToArrayString()[i+1]);
                    i++;
                }                
            }            
            int result = cmd.ExecuteNonQuery();
            //Console.Write("Execute successful!"); Console.ReadLine();
            cnn.Close();
            return result;
        }

        // Excute SQL SELECT 
        public List<Employee> SQLQuery(string str)
        // ('Select', 'Table name', Condition Field1, Val1, Condition Field2, Val2,.....)        
        {
            SqlConnection cnn = new(config.conStr);
            cnn.Open();
            string sql;
            SqlCommand cmd;
            List<Employee> result = new();

            //sql= Select * from tablename where field1 = @3 and field2 = @5 and ......
            sql = $"Select * from {str.ToArrayString()[1]}";
            if (str.Count() > 3)
            {
                sql += " where ";
                for (int i = 2; i < str.Count() - 2; i++)
                {
                    sql += str.ToArrayString()[i] + "=@" + (i + 1) + " and ";
                    i++;
                }
                sql += str.ToArrayString()[str.Count() - 2] + "=@" + (str.Count() - 1);
                //@3=str.ToArrayString()[3],@5=str.ToArrayString()[5]
                cmd = new(sql, cnn);
                for (int i = 2; i < str.Count(); i++)
                {
                    cmd.Parameters.AddWithValue("" + (i + 1) + "", str.ToArrayString()[i + 1]);
                    i++;
                }
            }
            else
            {
                cmd = new(sql, cnn);
            }
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read()) 
            { 
                Employee emp = new Employee();
                emp.Id = (int)rdr["Id"];
                emp.Name = (string)rdr["Name"];
                emp.Password = (string)rdr["Password"];
                emp.Role = (string)rdr["Role"];
                emp.Username = (string)rdr["Username"];
                emp.Position = (string)rdr["Position"];
                result.Add(emp);
            }
            cnn.Close();
            return result;
        }

    }
}
