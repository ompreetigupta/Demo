﻿using EmployeeData.DAL;
using EmployeeData.Models;
using EmployeeData.Models.DBEntities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics.Eventing.Reader;

namespace EmployeeData.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeDbContext _context;
        public EmployeeController(EmployeeDbContext context)
        {
           this._context = context;

        }
        [HttpGet]
        public IActionResult Index()
        {
            var employees = _context.Employees.ToList();
            List<EmployeeViewModel> employeeList = new List<EmployeeViewModel>();
            if (employees != null)
            {
                foreach (var employee in employees)
                {
                    var EmployeeViewModel = new EmployeeViewModel()
                    {
                        Id = employee.Id,
                        FirstName = employee.FirstName,
                        LastName = employee.LastName,
                        DateOfBirth = employee.DateOfBirth,
                        Email = employee.Email,
                        Salary = employee.Salary
                    };
                    employeeList.Add(EmployeeViewModel);
                }
                return View(employeeList);
            }

            return View(employeeList);

        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();

        }
        [HttpPost]

        public IActionResult Create(EmployeeViewModel employeeData)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var employee = new Employee()
                    {
                        FirstName = employeeData.FirstName,
                        LastName = employeeData.LastName,
                        DateOfBirth = employeeData.DateOfBirth,
                        Email = employeeData.Email,
                        Salary = employeeData.Salary

                    };
                    _context.Employees.Add(employee);
                    _context.SaveChanges();
                    TempData["sucessMessage"] = "Employee created sucessfully!";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["errorMessage"] = "Model data is not valid";
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();

                
            }


        }
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            try
            {
                var employee = _context.Employees.SingleOrDefault(x=>x.Id==Id);
                {
                    if (employee != null)
                    {
                        var employeeView = new EmployeeViewModel()
                        {
                            Id = employee.Id,
                            FirstName = employee.FirstName,
                            LastName = employee.LastName,
                            DateOfBirth = employee.DateOfBirth,
                            Email = employee.Email,
                            Salary = employee.Salary
                        };

                        return View(employeeView);

                    }
                    else
                    {
                        TempData["errorMessage"] = $"Employee Details not available with the Id:{Id}";
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction("Index");
                
            }


        }

        [HttpPost]

        public IActionResult Edit(EmployeeViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var employee = new Employee()
                    {
                        Id = model.Id,  
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        DateOfBirth = model.DateOfBirth,
                        Email = model.Email,
                        Salary = model.Salary

                    };
                    _context.Employees.Update(employee);
                    _context.SaveChanges();
                    TempData["sucessMessage"] = "Employee Detail updated sucessfully!";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["errorMessage"] = "Model data is not valid";
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();


            }


        }

        [HttpGet]
        public IActionResult Delete(int Id)
        {
            try
            {
                var employee = _context.Employees.SingleOrDefault(x => x.Id == Id);
                
                    if (employee!=null)
                    {
                        var employeeView = new EmployeeViewModel()
                        {
                            Id = employee.Id,
                            FirstName = employee.FirstName,
                            LastName = employee.LastName,
                            DateOfBirth = employee.DateOfBirth,
                            Email = employee.Email,
                            Salary = employee.Salary
                        };

                        return View(employeeView);

                    }
                    else
                    {
                        TempData["errorMessage"] = $"Employee Details not available with the Id:{Id}";
                        return RedirectToAction("Index");
                    }
                
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction("Index");

            }


        }


        [HttpPost]

        public IActionResult Delete(EmployeeViewModel model)
        {
            try
            {
                var employee = _context.Employees.SingleOrDefault(x=>x.Id==model.Id);
                
                    if (employee != null)
                    {
                        _context.Employees.Remove(employee);
                        _context.SaveChanges();
                        TempData["sucessMessage"] = "Employee deleted sucessfully!";
                        return RedirectToAction("Index");

                    }
                    else
                    {
                        TempData["errorMessage"] = $"Employee Details not available with the Id:{model.Id}";
                        return RedirectToAction("Index");
                    }
                
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();

            }
        }
    }
}
