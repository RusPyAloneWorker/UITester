using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
	public class CalculatorController : Controller
	{
		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Result(double number1, double number2, string operation)
		{
			double result = operation switch
			{
				"Add" => number1 + number2,
				"Subtract" => number1 + number2,
				"Multiply" => number1 * number2,
				"Divide" => number2 != 0 ? number1 / number2 : double.NaN,
				_ => 0
			};

			ViewBag.Result = result;
			return View();
		}
	}
}