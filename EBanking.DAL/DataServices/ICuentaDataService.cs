using EBanking.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBanking.DAL.DataServices
{
    public interface ICuentaDataService
    {
        List<Entities.Cuenta> GetCuentasByUsuario(int usuarioID);
        Entities.Cuenta GetCuentaByCuentaID(int cuentaID);
    }
}
