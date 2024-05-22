using Agency_Core.Models;
using Agency_Core.RepositoryAbstracts;
using Agency_Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency_Data.RepositoryConcretes
{
    public class PortfolioRepository : GenericRepository<Portfolio>, IPortfolioRepository
    {
        public PortfolioRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
