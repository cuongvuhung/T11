using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T11
{
    internal class Employee
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Username { get; set; }
        public string Position { get; set; }
        public Employee() { }
        public Employee(int? id, string name, string password, string role, string username, string position)
        {
            this.Id = id;
            this.Name = name;
            this.Password = password;
            this.Role = role;
            this.Username = username;
            this.Position = position;
        }
        public void GetData()
        {
            Console.Write("Enter ID:"); this.Id = Convert.ToInt32(Console.ReadLine() + "");
            Console.Write("Enter Name:"); this.Name = Console.ReadLine() + "";
            Console.Write("Enter Password:"); this.Password = Console.ReadLine() + "";
            Console.Write("Enter Role:"); this.Role = Console.ReadLine() + "";
            Console.Write("Enter Username:"); this.Username = Console.ReadLine() + "";
            Console.Write("Enter Position:"); this.Position = Console.ReadLine() + "";
        }
        public void GetDataForSearch()
        {
            Console.Write("Enter Name:"); this.Name = Console.ReadLine() + "";            
            Console.Write("Enter Role:"); this.Role = Console.ReadLine() + "";
            Console.Write("Enter Username:"); this.Username = Console.ReadLine() + "";
            Console.Write("Enter Position:"); this.Position = Console.ReadLine() + "";
        }
        public void GetDataForNew()
        {
            Console.Write("Enter Name:"); this.Name = Console.ReadLine() + "";
            Console.Write("Enter Password:"); this.Password = Console.ReadLine() + "";
            Console.Write("Enter Role:"); this.Role = Console.ReadLine() + "";
            Console.Write("Enter Username:"); this.Username = Console.ReadLine() + "";
            Console.Write("Enter Position:"); this.Position = Console.ReadLine() + "";
        }
        public void GetDataForDelete()
        {
            Console.Write("Enter ID:"); this.Name = Console.ReadLine() + "";            
        }
        public string ToStringExport()
        {
            return Id + "," + Name + "," + Password + "," + Role + "," + Username + "," + Position;
        }

        public bool EqualId(Employee other)
        {
            if ((this.Id == other.Id) && (other.Id != null)) return true;
            return false;
        }
        public bool EqualName(Employee other)
        {
            if ((this.Name == other.Name) && (other.Name != "")) return true;
            return false;
        }
        public bool EqualUsername(Employee other)
        {
            if ((this.Username == other.Username) && (other.Username != "")) return true;
            return false;
        }
        public bool EqualRole(Employee other)
        {
            if ((this.Role == other.Role) && (other.Role != "")) return true;
            return false;
        }
        public bool EqualPosition(Employee other)
        {
            if ((this.Position == other.Position) && (other.Position != "")) return true;
            return false;
        }

    }
}


