using EBanking.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBanking.Business
{
    public class TransferenciaBusinessRules : AbstractValidator<Transferencia>
    {
        public TransferenciaBusinessRules(Transferencia transferencia)
        {
            RuleFor(t => t.CuentaIdOrigen).NotEmpty().WithMessage("El campo Cuenta Origen es requerido.");
            RuleFor(t => t.CuentaIdDestino).NotEmpty().WithMessage("El campo Cuenta Destino es requerido.");
            RuleFor(t => t.CuentaIdOrigen).NotEqual(transferencia.CuentaIdDestino).WithMessage("Las cuentas origen y destino deben ser distintas.");
            RuleFor(t => t.Monto).NotEmpty().WithMessage("El campo Monto es requerido y debe ser mayor a 0.");
        }
    }
}
