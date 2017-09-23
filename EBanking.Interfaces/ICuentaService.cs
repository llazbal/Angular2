using EBanking.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBanking.Interfaces
{
    public interface ICuentaService
    {
        List<Cuenta> GetCuentasByUsuario(int usuarioID);
        List<Cuenta> GetCuentasByUsuario(int usuarioID, int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out TransactionalInformation transaction);
        Cuenta GetCuentaByCuentaID(int cuentaID);
        Cuenta GetCuentaByCuentaID(int cuentaID, out TransactionalInformation transaction);
    }
}
