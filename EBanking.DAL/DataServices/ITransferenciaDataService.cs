using EBanking.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBanking.DAL.DataServices
{
    public interface ITransferenciaDataService
    {
        void SaveTransferencia(EBanking.Entities.Transferencia transferencia);
        List<Entities.Transferencia> GetTransferenciasByUsuario(int usuarioID);
        Entities.Transferencia GetTransferenciaByTransferenciaID(int transferenciaID);
        List<Entities.Transferencia> GetTransferenciasByCuentaIdOrigen(int cuentaIdOrigen);
    }
}
