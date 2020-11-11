using CuatroEstaciones.Core.Models;
using FluentValidation;

namespace CuatroEstaciones.Core.Validations {

    public class ClienteValidator : AbstractValidator<Cliente> {

        public ClienteValidator() {
            RuleFor(x => x.Nombre).NotEmpty();
            RuleFor(x => x.Telefono).NotNull();
        }
    }
}