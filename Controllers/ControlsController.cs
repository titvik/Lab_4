using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using lab4.Models;

namespace lab4.Controllers
{
    public class ControlsController : Controller
    {
        

        public IActionResult Index()
        {

            return View();
        }

        [HttpGet]
        public IActionResult TextBox()
        {
            return View();
        }


        [HttpPost]
        public IActionResult TextBox(string TextBoxString)
        {
            ViewData["Title"] = "Text Box";
            ViewData["Content"] = "   <h6> <b>Text</b>    " + TextBoxString + "</h6>";
            return View("ControlResult");
        }


        [HttpGet]
        public IActionResult TextArea()
        {

            return View();
        }

        [HttpPost]
        public IActionResult TextArea(string TextAreaString)
        {
            ViewData["Title"] = "Text Area";
            ViewData["Content"] = "   <h6> <b>Text</b>    " + TextAreaString + "</h6>";
            return View("ControlResult");
        }

        [HttpGet]
        public IActionResult CheckBox()
        {

            return View();
        }

        [HttpPost]
        public IActionResult CheckBox(bool CheckBoxState)
        {
            ViewData["Title"] = "Check Box";
            ViewData["Content"] = "   <h6> <b>IsSelected</b>    " + CheckBoxState + "</h6>";
            return View("ControlResult");
        }

        [HttpGet]
        public IActionResult Radio()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Radio(string RadioSelected)
        {
            ViewData["Title"] = "Radio";
            ViewData["Content"] = "   <h6> <b>Month</b>    " + RadioSelected + "</h6>";
            return View("ControlResult");
        }

        [HttpGet]
        public IActionResult DropList()
        {

            return View();
        }

        [HttpPost]
        public IActionResult DropList(string DropListString)
        {
            ViewData["Title"] = "Radio";
            ViewData["Content"] = "   <h6> <b>Month</b>    " + DropListString + "</h6>";
            return View("ControlResult");
        }

        [HttpGet]
        public IActionResult ListBox()
        {

            return View();
        }

        [HttpPost]
        public IActionResult ListBox(List<string> ListBoxString)
        {
            ViewData["Title"] = "List Box";
            ViewData["Content"] = "   <h6> <b>Month</b>    ";
            for (int i = 0; i<ListBoxString.Count; i++)
                ViewData["Content"] += ListBoxString[i] + " ";
            ViewData["Content"] += "</h6>";
            return View("ControlResult");
        }
    }
}