using CuatroEstaciones.Core.Models;
using FluentValidation;

namespace CuatroEstaciones.Core.Validations {

    public class TipoServicioValidator : AbstractValidator<TipoServicio> {

        public TipoServicioValidator() {
            RuleFor(x => x.Nombre).NotEmpty();
        }
    }
}