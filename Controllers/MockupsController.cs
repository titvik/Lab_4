using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lab4.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using lab4.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net.Mail;
using System.Net;
using System.Linq.Expressions;

namespace lab4.Controllers
{
    public class MockupsController : Controller
    {
        private MvcUserContext db;

        public MockupsController(MvcUserContext context)
        {
            db = context;
        }
        public async Task<ActionResult> Index()
        {
            return View(await db.Users.ToListAsync());
        }


        [HttpGet]
        public IActionResult SignUp()
        {

            return View();
        }

        [HttpPost]
        public IActionResult SignUp([Bind("FirstName,LastName,Birthday,Gender")] User _user, int day, int month, int year)
        {
            DateTime date = new DateTime();
            try {
                date = new DateTime(year, month, day);
            }
            catch
            {
                ModelState.AddModelError("Birthday", "Invalid Date");
            }
            
            _user.Birthday = date;

            
            if (String.IsNullOrEmpty(_user.FirstName))
            {
                ModelState.AddModelError("FirstName", "Invalid First name");
            }
            if (String.IsNullOrEmpty(_user.LastName))
            {
                ModelState.AddModelError("LastName", "Invalid Last name");
            }
            if (String.IsNullOrEmpty(_user.Gender))
            {
                ModelState.AddModelError("Gender", "Invalid Gender");
            }
            if (ModelState.IsValid)
            {
                TempData["temp"] = JsonConvert.SerializeObject(_user);
                return View("SignUp2");
            }
            else
                return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp2([Bind("Email,Password")] User _user, string ConfirmPass)
        {
            if (String.IsNullOrEmpty(_user.Email))
            {
                ModelState.AddModelError("Email", "Invalid email");
            }
            if (String.IsNullOrEmpty(_user.Password))
            {
                ModelState.AddModelError("Password", "Invalid password");
            }
            if (_user.Password != ConfirmPass)
            {
                ModelState.AddModelError("Password", "Password must be match");
            }
            if (ModelState.IsValid)
            {
                User user = JsonConvert.DeserializeObject<User>(TempData["temp"].ToString());
                user.Email = _user.Email;
                user.Password = _user.Password;

                db.Users.Add(user);
                await db.SaveChangesAsync();

                return View("SignUpCredentials", user);
            }
            else
                return View();
        }


        [HttpGet]
        public IActionResult Reset()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Reset(User _user, string btn)
        {
            if (String.IsNullOrEmpty(_user.Email))
            {
                ModelState.AddModelError("Email", "Invalid email");
            }
            if (ModelState.IsValid)
            {
                if (btn == "send")
                {
                    User user = await db.Users.FirstOrDefaultAsync(u => u.Email == _user.Email);

                    if (user != null)
                    {
                        Random rnd = new Random();
                        string code = rnd.Next(9).ToString() + rnd.Next(9).ToString() + rnd.Next(9).ToString() + rnd.Next(9).ToString();
                        TempData["ResetCode"] = code;
                        ModelState.AddModelError("Email", "Code: " + code);
                        return View();
                    }
                    else
                    {
                        ModelState.AddModelError("Email", "Email not found");
                    }


                }
                else if (btn == "havecode")
                {
                    return View("ResetCode");
                }
            }
            return View();
        }



        [HttpPost]
        public IActionResult ResetCode(User _user)
        {
            if (_user.ResetCode == TempData.Peek("ResetCode").ToString())
            {
                return Content("Code verified");
            }
            else
            {
                ModelState.AddModelError("ResetCode", "Invalid code");
            }
            return View();
        }
        public IActionResult SignUpCredentials()
        {

            return View();
        }
    }
}