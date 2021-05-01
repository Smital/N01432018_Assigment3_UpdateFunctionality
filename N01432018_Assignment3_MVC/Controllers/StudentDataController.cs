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
    public class StudentDataController : ApiController
    {
        private SchoolDbContext School = new SchoolDbContext();

        [HttpGet]
        public IEnumerable<Student> ListStudents()
        {
            MySqlConnection Conn = School.AccessDatabase();

            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            //Create a query
            cmd.CommandText = "Select * from students";

            //Get the result set of query into data variable
            MySqlDataReader Resultset = cmd.ExecuteReader();

            List<Student> Students = new List<Student> { };

            while (Resultset.Read())
            {
                int StudentId = Convert.ToInt32(Resultset["studentid"]);
                string StudentFname = (string)Resultset["studentfname"];
                string StudentLName = (string)Resultset["studentlname"];
                string StudentNumber = (string)Resultset["studentnumber"];
                DateTime EnrolDate = (DateTime)Resultset["enroldate"];

                Student newStudent = new Student();
                newStudent.StudentId = StudentId;
                newStudent.StudentFname = StudentFname;
                newStudent.StudentLname = StudentLName;
                newStudent.StudentNumber = StudentNumber;
                newStudent.EnrolDate = EnrolDate;


                Students.Add(newStudent);

            }

            Conn.Close();

            return Students;
        }
            [HttpGet]
            public Student findStudent(int id)
            {


                Student newStudent = new Student();

                MySqlConnection Conn = School.AccessDatabase();

                //Open the connection between the webserver and the database
                Conn.Open();

                //Establish a new command (query) for our database
                MySqlCommand cmd = Conn.CreateCommand();

                //Sql Query
                cmd.CommandText = "Select * from students where studentid = " + id;

                //Gather the result set of query into a variable
                MySqlDataReader Resultset = cmd.ExecuteReader();

                while (Resultset.Read())
                //Read method will read series of rows
                {
                    int StudentId = Convert.ToInt32(Resultset["studentid"]);
                    string StudentFname = (string)Resultset["studentfname"];
                    string StudentLName = (string)Resultset["studentlname"];
                    string StudentNumber = (string)Resultset["studentnumber"];
                    DateTime EnrolDate = (DateTime)Resultset["enroldate"];

                    newStudent.StudentId = StudentId;
                    newStudent.StudentFname = StudentFname;
                    newStudent.StudentLname = StudentLName;
                    newStudent.StudentNumber = StudentNumber;
                    newStudent.EnrolDate = EnrolDate;

                 }

                return newStudent;
            }
        }
    }
