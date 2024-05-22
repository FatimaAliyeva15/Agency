using Agency_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency_Business.Services.Abstracts
{
    public interface IPortfolioService
    {
        void AddPortfolio(Portfolio portfolio);
        void DeletePortfolio(int id);
        void UpdatePortfolio(int id, Portfolio portfolio);
        Portfolio GetPortfolio(Func<Portfolio, bool>? func = null);
        List<Portfolio> GetAllPortfolios(Func<Portfolio, bool>? func = null);

    }
}
