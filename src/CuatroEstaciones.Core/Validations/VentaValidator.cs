using CuatroEstaciones.Core.Models;
using FluentValidation;

namespace CuatroEstaciones.Core.Validations {

    public class VentaValidator : AbstractValidator<Venta> {

        public VentaValidator() {
            RuleFor(x => x.Fecha).NotEmpty();
            RuleFor(x => x.Cliente).NotEmpty();
        }
    }
}