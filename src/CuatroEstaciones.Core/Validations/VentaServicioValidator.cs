using CuatroEstaciones.Core.Models;
using FluentValidation;

namespace CuatroEstaciones.Core.Validations {

    public class VentaServicioValidator : AbstractValidator<VentaServicio> {

        public VentaServicioValidator() {
            RuleFor(x => x.Servicio).NotEmpty();
        }
    }
}