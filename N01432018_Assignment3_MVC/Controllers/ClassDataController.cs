using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using N01432018_Assignment3_MVC.Models;
using MySql.Data.MySqlClient;
using System.Numerics;

namespace N01432018_Assignment3_MVC.Controllers
{
    public class ClassDataController : ApiController
    {
        //create an instance of class which we create in Model folder ScoolDbContext.cs
        //School is an object of class SchoolDbContext
        private SchoolDbContext School = new SchoolDbContext();


        //httpget is used to get the data from the database
        [HttpGet]

        //IEnumerable method is used to get the list of Teachers from the teachers table
        public IEnumerable<Class> ListClasses()
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
            cmd.CommandText = "Select * from classes";

            //Gather the result set of query into a variable
            MySqlDataReader Resultset = cmd.ExecuteReader();

            //Create ab Empty list of teacher names
            List<Class> Classes = new List<Class> { };

            //Loop for each row would be accessed by the resultset
            while (Resultset.Read())
            //Read method will read series of rows
            {
                //Access Colum infromation be the Db column name as an index
                int ClassId = (int)Resultset["classid"];
                string ClassCode = (string)Resultset["classcode"];
                int TeacherId = Convert.ToInt32(Resultset["teacherid"]);
                DateTime StartDate = (DateTime)Resultset["startdate"];
                DateTime FinishDate = (DateTime)Resultset["finishdate"];
                string ClassName = (string)Resultset["classname"];

                //To make an object of an Class class
                Class newClass = new Class();
                newClass.ClassId = ClassId;
                newClass.ClassCode= ClassCode;
                newClass.TeacherId = TeacherId;
                newClass.StartDate = StartDate;
                newClass.FinishDate = FinishDate;
                newClass.ClassName = ClassName;

                //Add the TechserName to the list
                Classes.Add(newClass);
            }

            //For closing the connection between Mysql dataabse and web server

            Conn.Close();

            //Return the final list of Techer Names
            return Classes;

        }
        [HttpGet]
        public Class findClass(int id)
        {
            Class newClass= new Class();

            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the webserver and the database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //Sql Query
            cmd.CommandText = "Select * from classes where classid = " + id;

            //Gather the result set of query into a variable
            MySqlDataReader Resultset = cmd.ExecuteReader();

            while (Resultset.Read())
            //Read method will read series of rows
            {
                int ClassId = (int)Resultset["classid"];
                string ClassCode = (string)Resultset["classcode"];
                int TeacherId = Convert.ToInt32(Resultset["teacherid"]);
                DateTime StartDate = (DateTime)Resultset["startdate"];
                DateTime FinishDate = (DateTime)Resultset["finishdate"];
                string ClassName = (string)Resultset["classname"];
         
                newClass.ClassId = ClassId;
                newClass.ClassCode = ClassCode;
                newClass.TeacherId = TeacherId;
                newClass.StartDate = StartDate;
                newClass.FinishDate = FinishDate;
                newClass.ClassName = ClassName;

            }

            return newClass;
        }

    }
}
