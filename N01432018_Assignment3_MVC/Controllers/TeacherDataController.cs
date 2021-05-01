using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using N01432018_Assignment3_MVC.Models;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace N01432018_Assignment3_MVC.Controllers
{
    public class TeacherDataController : ApiController
    {
        //create an instance of class which we create in Model folder ScoolDbContext.cs
        //School is an object of class SchoolDbContext
        private SchoolDbContext School = new SchoolDbContext();


        //httpget is used to get the data from the database
        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{Searchkey}/{Number}/{Date}")]

        //IEnumerable method is used to get the list of Teachers from the teachers table
        public IEnumerable<Teacher> ListTeachers(string SearchKey,string Number,string Date)
        {
            //Accessdatabse is the method of ScoolDbContext class,so use the method of the class we need 
            // to create an object of connection method.
            //So here Conn is the object of a connection MySqlConnection to access the method AccessDatabase()

            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the webserver and the database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //Sql Query
            //cmd.CommandText = "Select * from teachers where lower(teacherfname) like lower('%@key%') or lower(teacherlname) like lower('%@Key%') or lower(concat(teacherfname,' ',teacherlname)) like lower('%@key%') or salary like '%"+Number + "%'";
            cmd.CommandText = "Select * from teachers where lower(teacherfname) like lower('%"+SearchKey+ "%') or lower(teacherlname) like lower('%" +SearchKey+ "%') or lower(concat(teacherfname,' ',teacherlname)) like lower('" +SearchKey+ "') or salary like '"+Number+ "' or hiredate like '"+Date+"'";
            //which parameter is to insert in a particular query.So here we are inserting searchkey
            //cmd.Parameters.AddWithValue("key", "%" + SearchKey + "%");
            //cmd.Prepare();
           
            //Gather the result set of query into a variable
            MySqlDataReader Resultset = cmd.ExecuteReader();

            //Create ab Empty list of teacher names
            List<Teacher> Teachers = new List<Teacher> { };

            //Loop for each row would be accessed by the resultset
            while(Resultset.Read())
            //Read method will read series of rows
            {
                //Access Colum infromation be the Db column name as an index
                int TeacherId = (int)Resultset["teacherid"];
                string TeacherFname = (string)Resultset["teacherfname"];
                string TeacherLName = (string)Resultset["teacherlname"];
                string EmployeeNumber = (string)Resultset["employeenumber"];
                DateTime  HireDate = (DateTime)Resultset["hiredate"];
                decimal Salary = (decimal)Resultset["salary"];

                //To make an object of an teacher class
                Teacher newTeacher = new Teacher();
                newTeacher.TeacherId = TeacherId;
                newTeacher.TeacherFname = TeacherFname;
                newTeacher.TeacherLname = TeacherLName;
                newTeacher.EmployeeNumber = EmployeeNumber;
                newTeacher.HireDate = HireDate;
                newTeacher.Salary = Salary;

                //Add the TechserName to the list
                Teachers.Add(newTeacher);
            }

            //For closing the connection between Mysql dataabse and web server

            Conn.Close();

            //Return the final list of Techer Names
            return Teachers;

        }

        [HttpGet]
        public Teacher findTeacher(int id)
        {


            Teacher newTeacher = new Teacher();

            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the webserver and the database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //Sql Query
            //cmd.CommandText = "Select t.teacherfname,t.teacherlname ,c.classname from teachers t JOIN classes c ON t.teacherid = c.teacherid where .teacherid = " + id;
            cmd.CommandText = "Select * from teachers where teacherid = " + id;

            //Gather the result set of query into a variable
            MySqlDataReader Resultset = cmd.ExecuteReader();

            while (Resultset.Read())
            //Read method will read series of rows
            {
                int TeacherId = (int)Resultset["teacherid"];
                string TeacherFname = (string)Resultset["teacherfname"];
                string TeacherLName = (string)Resultset["teacherlname"];
                string EmployeeNumber = (string)Resultset["employeenumber"];
                DateTime HireDate = (DateTime)Resultset["hiredate"];
                decimal Salary = (decimal)Resultset["salary"];


                newTeacher.TeacherId = TeacherId;
                newTeacher.TeacherFname = TeacherFname;
                newTeacher.TeacherLname = TeacherLName;
                newTeacher.EmployeeNumber = EmployeeNumber;
                newTeacher.HireDate = HireDate;
                newTeacher.Salary = Salary;

            }

                return newTeacher;
        }
        //POST : /api/TeacherData/DeleteTeacher/3
        [HttpPost]
        //Here we donot need route attribute because its already defined in App_start>webapi.congfig file
        public void DeleteTeacher(int id)
        {
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the webserver and the database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //Sql Query
            //cmd.CommandText = "Delete teacher based on their id from the database;
            cmd.CommandText = "Delete from teachers where teacherid =@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            //THis method is allow us to execute not select statement which returns nothing
            cmd.ExecuteNonQuery();


            Conn.Close();
            //As we are not returning anything because the method is void
        }

        [HttpPost]
        public void AddTeacher(Teacher NewTeacher)
        {
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the webserver and the database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //Sql Query
            //cmd.commandTeaxt : insert data in to teachers tables
            cmd.CommandText = "Insert into teachers (teacherfname,teacherlname,employeenumber,hiredate,salary) values (@TeacherFname,@TeacherLname,@EmployeeNumber,@HireDate,@Salary)";
            cmd.Parameters.AddWithValue("@TeacherFname", NewTeacher.TeacherFname);
            cmd.Parameters.AddWithValue("@TeacherLname", NewTeacher.TeacherLname);
            cmd.Parameters.AddWithValue("@EmployeeNumber", NewTeacher.EmployeeNumber);
            cmd.Parameters.AddWithValue("@HireDate", NewTeacher.HireDate);
            cmd.Parameters.AddWithValue("@Salary", NewTeacher.Salary);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();

        }  
        /// <summary>
        /// To provide id as paramaeter to easily identify which Teacher we are updating
        /// </summary>
        /// <param name="id">Primary Parameter</param>
        /// <param name="TeacherInfo">Secondary Paramater</param>
        /// 
        public void UpdateTeacher(int id,[FromBody]Teacher TeacherInfo)
        {
            MySqlConnection Conn = School.AccessDatabase();

            Debug.WriteLine(TeacherInfo.TeacherFname);

            //Open the connection between the webserver and the database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //Sql Query
            //cmd.commandTeaxt : Update data in to teachers tables
            cmd.CommandText = "update teachers  set teacherfname=@TeacherFname,teacherlname=@TeacherLname,employeenumber=@EmployeeNumber,hiredate=@HireDate,salary=@Salary where teacherid =@TeacherId";
            cmd.Parameters.AddWithValue("@TeacherFname", TeacherInfo.TeacherFname);
            cmd.Parameters.AddWithValue("@TeacherLname", TeacherInfo.TeacherLname);
            cmd.Parameters.AddWithValue("@EmployeeNumber", TeacherInfo.EmployeeNumber);
            cmd.Parameters.AddWithValue("@HireDate", TeacherInfo.HireDate);
            cmd.Parameters.AddWithValue("@Salary", TeacherInfo.Salary);
            cmd.Parameters.AddWithValue("@TeacherId",id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();
        }
    }

}
