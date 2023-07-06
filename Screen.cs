using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T11
{

    internal class Screen
    {
        // User and dataexecute
        private Employee user = new();
        private EmployeeManager manager = new EmployeeManager();
        //private SQLUtils data = new();

        // Start
        public void Start()
        {

            do
            {
                Login();
                switch (user.Role)
                {
                    case "manager":
                        ManagerScreen();
                        break;
                    case "user":
                        UserScreen();
                        break;
                    default:
                        Console.Write("No role for ur user" + user.Role); Console.ReadLine();
                        break;
                }
            } while (user.Role == "");

        }

        // Login Screen
        private void Login()
        {
            do
            {
                Console.WriteLine("===== EMPLOYEE MANAGE =====");
                Console.WriteLine("=====       LOGIN     =====");
                Console.WriteLine("Press 0 for sign up a user");
                Console.WriteLine("Any else for login");
                string choise = Console.ReadLine() + "";
                switch (choise)
                {
                    case "0":
                        AddNewScreen();
                        user.Role = "";
                        break;
                    default:
                        Console.Write("User name:"); user.Username = Console.ReadLine() + "";
                        Console.Write("Password:"); user.Password = Console.ReadLine() + "";
                        user.Password = Utils.Hash(user.Password);
                        
                        try
                        {
                            manager.GetData();
                            foreach (Employee emp in manager.employees)
                            { 
                            if ((emp.Username == user.Username) && (emp.Password == user.Password))  user.Role = emp.Role;
                            }
                        }
                        catch
                        {
                            Console.WriteLine("Cant login!");
                        }
                        break;
                }
            } while (user.Role == "");
        }


        // Module Manager Screen
        private void ManagerScreen()
        {
            int selected = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("***EMPLOYEE MANAGER***");
                Console.WriteLine("*** MANAGER SCREEN ***");
                Console.WriteLine("----------------------");
                Console.WriteLine("Username: {0}", user.Username);
                Console.WriteLine("----------------------");
                Console.WriteLine("1. Search Employee by Name or EmpNo");
                Console.WriteLine("2. Add New Employee");
                Console.WriteLine("3. Update Employee");
                Console.WriteLine("4. Delete Employee");
                Console.WriteLine("5. Show a List of Employee Sorted");
                Console.WriteLine("6. Import a list of Employee");
                Console.WriteLine("7. Export a list of Employee");
                Console.WriteLine("8. Logout");
                Console.WriteLine("9. Exit");
                Console.Write("   Select (1-9): ");

                // Try get a select with right numeric
                try
                {
                    selected = Convert.ToInt16(Console.ReadLine());
                }
                catch { }

                // Route program
                switch (selected)
                {
                    case 1:
                        FindScreen();
                        break;
                    case 2:
                        AddNewScreen();
                        break;
                    case 3:
                        UpdateScreen();
                        break;
                    case 4:
                        DeleteScreen();
                        break;
                    case 5:
                        SortScreen();
                        break;
                    case 6:
                        ImportScreen();
                        break;
                    case 7:
                        ExportScreen();
                        break;
                    case 8:
                        Console.WriteLine("Logging out");
                        user.Role = "";
                        break;
                    case 9:
                        Console.WriteLine("-------- END ---------");
                        break;
                    default:
                        Console.Write("Wrong format!"); Console.ReadLine();
                        break;
                }
            } while (selected != 9 && user.Role != "");
        }

        // Module User Screen
        private void UserScreen()
        {
            int selected = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("***EMPLOYEE MANAGER***");
                Console.WriteLine("***  USER SCREEN   ***");
                Console.WriteLine("----------------------");
                Console.WriteLine("Username: {0}", user.Username);
                Console.WriteLine("----------------------");
                Console.WriteLine("1. Search Employee by Name or EmpNo");
                Console.WriteLine("2. Show a List of Employee Sorted");
                Console.WriteLine("3. Log out");
                Console.WriteLine("4. Exit");
                Console.Write("   Select (1-4): ");

                // Try get a select with right numeric
                try
                {
                    selected = Convert.ToInt16(Console.ReadLine());
                }
                catch { }

                // Route program
                switch (selected)
                {
                    case 1:
                        FindScreen();
                        break;
                    case 2:
                        SortScreen();
                        break;
                    case 3:
                        Console.WriteLine("Logging out");
                        user.Role = "";
                        break;
                    case 4:
                        break;
                    default:
                        Console.Write("Wrong format!"); Console.ReadLine();
                        break;
                }
            } while (selected != 4 && user.Role != "");
        }

        // Find Screen
        private void FindScreen()
        {
            try
            {
                Console.Clear();
                Employee emp = new();
                emp.GetDataForSearch();

                // Name search
                Console.WriteLine($"Name: {emp.Name}");
                List<Employee> list = new();
                foreach (Employee e in manager.employees)
                    if (e.EqualName(emp)) list.Add(e);
                if (list.Count > 0)
                {
                    foreach (Employee ei in list)
                        Console.WriteLine(ei.ToString());
                }
                else
                {
                    Console.WriteLine("No result!");
                }
                Console.ReadLine();

                // Role search
                Console.WriteLine($"Role: {emp.Role}");
                list = new();
                foreach (Employee e in manager.employees)
                    if (e.EqualRole(emp)) list.Add(e);
                if (list.Count > 0)
                {
                    foreach (Employee ei in list)
                        Console.WriteLine(ei.ToString());
                }
                else
                {
                    Console.WriteLine("No result!");
                }
                Console.ReadLine();

                // Username search
                Console.WriteLine($"Username: {emp.Username}");
                list = new();
                foreach (Employee e in manager.employees)
                    if (e.EqualUsername(emp)) list.Add(e);
                if (list.Count > 0)
                {
                    foreach (Employee ei in list)
                        Console.WriteLine(ei.ToString());
                }
                else
                {
                    Console.WriteLine("No result!");
                }
                Console.ReadLine();

                // Position search
                Console.WriteLine($"Position: {emp.Position}");
                list = new();
                foreach (Employee e in manager.employees)
                    if (e.EqualPosition(emp)) list.Add(e);
                if (list.Count > 0)
                {
                    foreach (Employee ei in list)
                        Console.WriteLine(ei.ToString());
                }
                else
                {
                    Console.WriteLine("No result!");
                }
                Console.ReadLine();
            }
            catch
            {
                Console.Write("Something wrong!"); Console.ReadLine();
            }
        }

        // AddNew Screen
        private void AddNewScreen()
        {
            try
            {
                Employee emp = new Employee();
                emp.GetDataForNew();
                manager.Add(emp);
            }
            catch
            {
                Console.Write("Something wrong!"); Console.ReadLine();
            }

        }

        // Update Screen
        private void UpdateScreen()
        {
            try
            {
                Employee emp = new Employee();
                emp.GetData();
                manager.Update(emp);
            }
            catch
            {
                Console.Write("Something wrong!"); Console.ReadLine();
            }
        }

        // Delete Screen
        private void DeleteScreen()
        {
            try
            {
                Employee emp = new Employee();
                emp.GetDataForDelete();
                manager.Delete(emp);
            }
            catch
            {
                Console.Write("Something wrong!"); Console.ReadLine();
            }
        }

        // Import Screen
        private void ImportScreen()
        {
            try
            {
                Console.Clear();
                List<string> list = new();
                Console.Write("File name:");
                string filename = @"" + Console.ReadLine() + "";
                FileStream f = new(filename, FileMode.OpenOrCreate);
                StreamReader s = new(f);
                string? line;
                while ((line = s.ReadLine()) != null)
                {
                    list.Add(line + "");
                }
                s.Close();

                foreach (string str in list)
                {
                    try
                    {
                        // import logic 
                        Employee emp = new();
                        emp.Name = str.ToArrayString()[1];
                        emp.Password = str.ToArrayString()[2];
                        emp.Role = str.ToArrayString()[3];
                        emp.Username = str.ToArrayString()[4];
                        emp.Position = str.ToArrayString()[5];
                        manager.Add(emp);
                        // import to database
                    }
                    catch { }
                }
            }
            catch
            {
                Console.Write("Something wrong!"); Console.ReadLine();
            }

        }

        // Export Screen
        private void ExportScreen()
        {
            try
            {
                Console.Clear();
                Console.Write("File name:");
                string filename = @"" + Console.ReadLine() + "";
                FileStream f = new(filename, FileMode.OpenOrCreate);
                StreamWriter w = new(f);
                foreach (Employee emp in manager.employees)
                {
                    w.WriteLine(emp.ToStringExport() + "");
                }
                w.Close();
            }
            catch
            {
                Console.Write("Something wrong!"); Console.ReadLine();
            }

        }

        // Sort Sreen
        private void SortScreen()
        {
            Console.Clear();
            foreach (Employee line in manager.SortedList())
            {
                Console.WriteLine(line.ToStringExport());
            }
            Console.ReadLine();
        }
    }
        
}
