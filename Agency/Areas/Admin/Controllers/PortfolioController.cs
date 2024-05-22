using Agency_Business.Exceptions;
using Agency_Business.Services.Abstracts;
using Agency_Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FileNotFoundException = Agency_Business.Exceptions.FileNotFoundException;

namespace Agency.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class PortfolioController : Controller
    {
        private readonly IPortfolioService _portfolioService;

        public PortfolioController(IPortfolioService portfolioService)
        {
            _portfolioService = portfolioService;
        }

        public IActionResult Index()
        {
            List<Portfolio> portfolios = _portfolioService.GetAllPortfolios();
            return View(portfolios);
        }

        public IActionResult Create()
        {
            return View();  
        }

        [HttpPost]
        public IActionResult Create(Portfolio portfolio)
        {
            if(!ModelState.IsValid)
                return View();

            try
            {
                _portfolioService.AddPortfolio(portfolio);
            }
            catch(NullReferenceException ex)
            {
                return NotFound();
            }
            catch(ImageRequiredException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch(FileContentTypeError ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch(FileSizeException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var existPortfolio = _portfolioService.GetPortfolio(x => x.Id == id);
            if (existPortfolio == null)
                return NotFound();

            try
            {
                _portfolioService.DeletePortfolio(id);
            }
            catch(EntityNotFoundException ex)
            {
                return NotFound();
            }
            catch(FileNotFoundException ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int id)
        {
            var existPortfolio = _portfolioService.GetPortfolio(x => x.Id == id);
            if (existPortfolio == null) return NotFound();
                return View(existPortfolio);
        }

        [HttpPost]
        public IActionResult Update(int id, Portfolio portfolio)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                _portfolioService.UpdatePortfolio(id, portfolio);
            }
            catch (NullReferenceException ex)
            {
                return NotFound();
            }
            catch (ImageRequiredException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (FileContentTypeError ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (FileSizeException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (EntityNotFoundException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (FileNotFoundException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
