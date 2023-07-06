using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T11
{
    internal class EmployeeManager
    {
        public List<Employee> employees =new();
        public EmployeeManager() { }

        public void GetData() 
        { 
            Config config = new Config();
            SqlDataReader rdr = new SQLUtils().SQLQuery("Select,Employees");
            int i = 0;
            while (rdr.Read()) 
            {
                employees[i].Id = (int) rdr["id"];
                employees[i].Name = (string) rdr["name"];
                employees[i].Password = (string) rdr["Password"];
                employees[i].Role = (string) rdr["Role"];
                employees[i].Username = (string) rdr["Username"];
                employees[i].Position = (string) rdr["Position"];
                i++;
            }
        }

        public void Add(Employee emp) 
        {
            //logic
            emp.Password = Utils.Hash(emp.Password);
            employees.Add(emp);            
            string str = $"Insert,Employees,{emp.Name},{emp.Password},{emp.Role},{emp.Username},{emp.Position}";
            
            //database
            Console.WriteLine(new SQLUtils().SQLExecute(str) + " employees added!");
        }
        public void Update(Employee emp) 
        {
            //logic
            foreach (Employee e in employees) 
            { 
                if (e.Id == emp.Id) 
                { 
                    if (emp.Name!= "") e.Name = emp.Name ;
                    if (emp.Password != "") e.Password = emp.Password;
                    if (emp.Role != "") e.Role = emp.Role;
                    if (emp.Username != "") e.Username = emp.Username;
                    if (emp.Position != "") e.Position = emp.Position;

                }
            }

            //database
            string str = $"Update,Employees,{emp.Id},";
            if (emp.Name != "") str += "Name,"+ emp.Name;
            if (emp.Password != "") str += ",Password," + emp.Password;
            if (emp.Role != "") str += ",Role," + emp.Role;
            if (emp.Username != "") str += ",Username," + emp.Username;
            if (emp.Position != "") str += ",Position," + emp.Position;
            
            Console.WriteLine(new SQLUtils().SQLExecute(str) + " employees updated!");
        }
        public void Delete(Employee emp)
        {
            //logic
            for (int i=0;i<employees.Count();i++)                             
                if (employees[i].Id == emp.Id) employees.RemoveAt(i);

            //database
            string str = $"Delete,Employees,Id,{emp.Id}";
            Console.WriteLine(new SQLUtils().SQLExecute(str) + " employees updated!");
        }
        public List<Employee> SortedList() 
        { 
            List<Employee> result = employees.OrderByDescending(x => x.Position).ToList();
            return result;
        }
    }
}
