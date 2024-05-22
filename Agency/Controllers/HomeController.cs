
using Agency_Business.Services.Abstracts;
using Agency_Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Agency.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPortfolioService _portfolioService;

        public HomeController(IPortfolioService portfolioService)
        {
            _portfolioService = portfolioService;
        }

        public IActionResult Index()
        {
            List<Portfolio> portfolios = _portfolioService.GetAllPortfolios();
            return View(portfolios);
        }

    }
}