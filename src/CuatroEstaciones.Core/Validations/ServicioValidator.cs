using CuatroEstaciones.Core.Models;
using FluentValidation;

namespace CuatroEstaciones.Core.Validations {

    public class ServicioValidator : AbstractValidator<Servicio> {

        public ServicioValidator() {
            RuleFor(x => x.Nombre).NotEmpty();
            RuleFor(x => x.TipoServicio).NotEmpty();
            RuleFor(x => x.Precio).NotEmpty();
        }
    }
}