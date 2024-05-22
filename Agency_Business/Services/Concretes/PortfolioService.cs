using Agency_Business.Exceptions;
using Agency_Business.Services.Abstracts;
using Agency_Core.Models;
using Agency_Core.RepositoryAbstracts;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency_Business.Services.Concretes
{
    public class PortfolioService : IPortfolioService
    {
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PortfolioService(IPortfolioRepository portfolioRepository, IWebHostEnvironment webHostEnvironment)
        {
            _portfolioRepository = portfolioRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public void AddPortfolio(Portfolio portfolio)
        {
            if (portfolio == null) 
                throw new NullReferenceException("Portfolio not found");

            if (portfolio.ImgFile == null)
                throw new ImageRequiredException("ImgFile", "Image is required");

            if (!portfolio.ImgFile.ContentType.Contains("image/"))
                throw new FileContentTypeError("ImgFile", "File content type error");

            if (portfolio.ImgFile.Length > 2097152)
                throw new FileSizeException("ImgFile", "File size error");

            string fileName = portfolio.ImgFile.FileName;
            string path = _webHostEnvironment.WebRootPath + @"\upload\portfolio\" + fileName;
            using(FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                portfolio.ImgFile.CopyTo(fileStream);
            }
            portfolio.ImgUrl = fileName;

            _portfolioRepository.Add(portfolio);
            _portfolioRepository.Commit();
        }

        public void DeletePortfolio(int id)
        {
            var existPortfolio = _portfolioRepository.Get(x => x.Id == id);
            if (existPortfolio == null)
                throw new EntityNotFoundException("", "Entity not found");

            string path = _webHostEnvironment.WebRootPath + @"\upload\portfolio\" + existPortfolio.ImgUrl;
            if (!File.Exists(path))
                throw new Exceptions.FileNotFoundException("ImgFile", "File not found error");

            File.Delete(path);

            _portfolioRepository.Delete(existPortfolio);
            _portfolioRepository.Commit();
        }

        public List<Portfolio> GetAllPortfolios(Func<Portfolio, bool>? func = null)
        {
            return _portfolioRepository.GetAll(func);
        }

        public Portfolio GetPortfolio(Func<Portfolio, bool>? func = null)
        {
            return _portfolioRepository.Get(func);
        }

        public void UpdatePortfolio(int id, Portfolio portfolio)
        {
            var existPortfolio = _portfolioRepository.Get(x => x.Id == id);
            if (existPortfolio == null)
                throw new EntityNotFoundException("", "Entity not found");

            if(portfolio.ImgFile != null)
            {
                if (!portfolio.ImgFile.ContentType.Contains("image/"))
                    throw new FileContentTypeError("ImgFile", "File content type error");

                if (portfolio.ImgFile.Length > 2097152)
                    throw new FileSizeException("ImgFile", "File size error");

                string fileName = portfolio.ImgFile.FileName;
                string path = _webHostEnvironment.WebRootPath + @"\upload\portfolio\" + fileName;
                using (FileStream fileStream = new FileStream(path, FileMode.Create))
                {
                    portfolio.ImgFile.CopyTo(fileStream);
                }
                portfolio.ImgUrl = fileName;

                existPortfolio.ImgUrl = portfolio.ImgUrl;

            }

            existPortfolio.Title = portfolio.Title;
            existPortfolio.Subtitle = portfolio.Subtitle;

            _portfolioRepository.Commit();

        }
    }
}
