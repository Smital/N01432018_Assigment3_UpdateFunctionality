using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using N01432018_Assignment3_MVC.Models;

namespace N01432018_Assignment3_MVC.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }
        //GET : Teacher(name of Controller)/List
        public ActionResult List()
        {
            StudentDataController controller = new StudentDataController();
            IEnumerable<Student> Students = controller.ListStudents();

            return View(Students);
        }
        //GET : Teacher/Show/{id}
        public ActionResult Show(int id)
        {
            StudentDataController controller = new StudentDataController();
            Student newStudents = controller.findStudent(id);
            return View(newStudents);
        }
    }
}