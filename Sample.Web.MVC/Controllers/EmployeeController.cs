using Sample.Domain;
using Sample.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sample.Web.MVC.Controllers
{
    public class EmployeeController : Controller
    {
        private IEmployeeRepository _repository;

        public EmployeeController(IEmployeeRepository r)
        {
            this._repository = r;
        }

        // GET: Employee
        public ActionResult Index()
        {
            var employees = this._repository.GetAll();
            return View(employees);
        }

        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
            var employee = this._repository.Get(id);
            return View(employee);
        }

        // GET: Employee/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee instance)
        {
            try
            {
                // TODO: Add insert logic here

                if (ModelState.IsValid)
                {
                    this._repository.Create(instance);
                    return RedirectToAction("Index");
                }

                return View(instance);
            }
            catch
            {
                return View(instance);
            }
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            var employee = this._repository.Get(id);
            return View(employee);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Employee instance)
        {
            try
            {
                // TODO: Add update logic here

                if (ModelState.IsValid)
                {
                    this._repository.Update(instance);
                    return RedirectToAction("Index");
                }
                return View(instance);
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            var employee = this._repository.Get(id);
            return View(employee);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                // TODO: Add delete logic here
                this._repository.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
