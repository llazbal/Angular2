using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBanking.DAL
{
    static class GenericDataService
    {
        private static EBankingDataDataContext _dataContext;

        /// <summary>
        /// Database Context
        /// </summary>
        public static EBankingDataDataContext DataContext
        {
            get
            {
                return _dataContext = new EBankingDataDataContext(EBanking.Common.Utilities.GetConnectionString());
            }
        }
    }
}
