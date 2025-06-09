using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private static int _count = 1; // Initialised to 1 due to start value being 1
        private static bool _shouldResetCount = true; //Should we reset the value? initialised to true 
        private static string _charReversedOutput = ""; // Output for character reversal
        private static string _wordReversedOutput = ""; // Output for word reversal
        private static bool _shouldResetUserInput = true; // Should we reset the user input? initialised to true
        private static string _stringReversalError = "";// Error message for string reversal

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (_shouldResetCount)
                _count = 1; //Reset the count on page load or refresh
            _shouldResetCount = true;
            if (_shouldResetUserInput)
            {
                _charReversedOutput = "";
                _wordReversedOutput = "";
            }
            _shouldResetUserInput = true;
            ViewBag.CharReversed = _charReversedOutput;
            ViewBag.WordReversed = _wordReversedOutput;
            ViewBag.Count = _count;
            ViewBag.StringReversalError = _stringReversalError; // Pass the error message to the view
            return View();
        }
        [HttpPost]
        public IActionResult Increment()
        {
            _shouldResetCount = false; // Don't reset on immediate redirect
            _count++;
            return RedirectToAction("Index");
        }
        public IActionResult ResetCount()
        {
            _shouldResetCount = true;
            return RedirectToAction("Index");
        }
        public IActionResult ClickSummary()
        {
            ViewBag.Count = _count;
            return View();
        }
        [HttpPost]
        public IActionResult ReverseString(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                _stringReversalError = "String reversal is a required field!"; // Set error message if input is null or empty
                return RedirectToAction("Index");
            }
            _shouldResetUserInput = false;// Don't reset on immediate redirect
            _stringReversalError = ""; // Clear any previous errors
            _charReversedOutput = new string(input?.Reverse().ToArray()); // Reverse the characters in the string
            _wordReversedOutput = input == null ? "" : string.Join(" ", input.Split(' ', StringSplitOptions.RemoveEmptyEntries).Reverse()); // Reverse the words in the string
            return RedirectToAction("Index"); // Redirect to Index to show the results
        }
        public IActionResult ResetReverse()
        {
            _shouldResetUserInput = true;
            return RedirectToAction("Index");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
