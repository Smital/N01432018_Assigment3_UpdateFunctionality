using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using N01432018_Assignment3_MVC.Models;
using System.Diagnostics;

namespace N01432018_Assignment3_MVC.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }

        //GET : Teacher(name of Controller)/List
        public ActionResult List(string SearchKey, string Number, string Date)
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.ListTeachers(SearchKey, Number, Date);

            return View(Teachers);
        }
        //GET : Teacher/Show/{id}
        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.findTeacher(id);
            return View(SelectedTeacher);
        }

        //GET :Teacher/DeleteTeacherConfirm/{id}
        public ActionResult DeleteTeacherConfirm(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher newTeacher = controller.findTeacher(id);
            return View(newTeacher);
        }

        //POST:Teacher/Delete/{id}
        public ActionResult Delete(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);
            return RedirectToAction("List");
        }

        //GET: Teacher/New
        public ActionResult New()
        {
            //We are not providing any parameter in this because we dont need to know the information of id
            return View();
        }

        //POST: Teacher/Create
        [HttpPost]
        public ActionResult Create(string TeacherFname, string TeacherLname, string EmployeeNumber, DateTime HireDate, Decimal Salary)
        {
            //Idetify that this method is running
            //Identify the inputs provided from the form

            Debug.WriteLine("I have access the create method");
            Debug.WriteLine(TeacherFname);
            Debug.WriteLine(TeacherLname);
            Debug.WriteLine(EmployeeNumber);
            Debug.WriteLine(HireDate);
            Debug.WriteLine(Salary);

            //Server side validation
            if (TeacherFname == "" && TeacherLname == "")
            {
                Debug.WriteLine("Invalid Name");
                return RedirectToAction("New");
            }

            Teacher NewTeacher = new Teacher();
            NewTeacher.TeacherFname = TeacherFname;
            NewTeacher.TeacherLname = TeacherLname;
            NewTeacher.EmployeeNumber = EmployeeNumber;
            NewTeacher.HireDate = HireDate;
            NewTeacher.Salary = Salary;

            TeacherDataController controller = new TeacherDataController();
            controller.AddTeacher(NewTeacher);

            return RedirectToAction("List");
        }
        /// <summary>
        /// Routes to a dynamically generated "Teacher Update" Page.
        /// Gathers info from the database
        /// 
        ///</summary>
        ///<param name="id">Id of the Teacher </param>
        ///<returns> A dynamic "Update Teacher" webpage which provides the current info
        ///of the teacher and asks the user fot new information as a pasrt of a form
        ///</returns>
        ///<example>GET: /Teacher/Update/{id}</example>
        public ActionResult Update(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.findTeacher(id);
            return View(SelectedTeacher);

        }
        /// <summary>
        /// POST : /Author/Update/{id}
        /// /// Recieve a post request containing information about an existing teacher in the
        /// system with new values.
        /// It conveys this information to the API and redirects to the "Teacher SHow"
        /// page of our update teacher.
        /// </summary>
        /// <param name="id">Id of the Teacher to update</param>
        /// <param name="TeacherFname">The updated first name of teacher</param>
        /// <param name="TeacherLname">The updated lastname of teacher</param>
        /// <param name="EmployeeNumber">The update employeenumber of teacher</param>
        /// <param name="HireDate">The update hiredat of teracher</param>
        /// <param name="Salary">The update salary of the teacher</param>
        /// <returns> A dyamic webpage which provides the current teacher
        /// information</returns>
        /// <example>
        /// POST :/Teacher/Update/{id}
        /// POST:/Teacher/Update/10
        /// Forma Data/POST data/Request Body
        /// {
        /// "TeacherFname"="Smital",
        /// "TeacherLname"="Christian",
        /// "EmployeeNumber"="T23401",
        /// "Hiredate"="2021=04-12"
        /// "Salary"="23.5"
        /// }
        /// </example>
        [HttpPost]
        public ActionResult Update(int id, string TeacherFname, string TeacherLname, string EmployeeNumber, DateTime HireDate, Decimal Salary)
        {
            Teacher TeacherInfo = new Teacher();
            TeacherInfo.TeacherFname = TeacherFname;
            TeacherInfo.TeacherLname = TeacherLname;
            TeacherInfo.EmployeeNumber = EmployeeNumber;
            TeacherInfo.HireDate = HireDate;
            TeacherInfo.Salary = Salary;
            TeacherDataController controller = new TeacherDataController();
            controller.UpdateTeacher(id, TeacherInfo);


            return RedirectToAction("Show/" + id);

        }

        //Server Side Validation
        [HttpPost]
        public ActionResult Validation(ServerValidation.Models.TeacherValidate  model)
        {
            if (string.IsNullOrEmpty(model.Fname))
            {
                ModelState.AddModelError("Fname", "Fname is required");
            }
            else if (string.IsNullOrEmpty(model.Lname))
            {
                ModelState.AddModelError("Lname", "Lname is required");
            }
            else if (string.IsNullOrEmpty(model.EmployeeNumber))
            {
                ModelState.AddModelError("EmployeeNumber", "EmployeeNumber is required");
            }
            else if (string.IsNullOrEmpty(model.HireDate))
            {
                ModelState.AddModelError("HireDate", "HireDate is required");
            }
            else
            {
                ModelState.AddModelError("Salary", "Salary is required");
            }

            if (ModelState.IsValid)
            {
                ViewBag.Fname = model.Fname;
                ViewBag.Lname = model.Lname;
                ViewBag.EmployeeNumber = model.EmployeeNumber;
                ViewBag.HireDate = model.HireDate;
                ViewBag.Salary = model.Salary;
            }
            return View(model);
        }
    }
}
