using System.Collections.Generic;
using FluentValidation;
using Neppo.Models;

namespace Neppo.Validators
{
    public class PessoaValidator : AbstractValidator<Pessoa>
    {
        public PessoaValidator() {
            RuleFor(p => p.nome)
                .NotEmpty().WithMessage("O campo [nome] não pode ser vazio.")
                .MaximumLength(50).WithMessage("O campo [nome] só pode conter até 50 carácteres.");

            RuleFor(p => p.documento)
                .NotEmpty().WithMessage("O campo [documento] não pode ser vazio.")
                .MaximumLength(20).WithMessage("O campo [documento] só pode conter até 50 carácteres.");

            RuleFor(p => p.dataNascimento)
                .NotEmpty().WithMessage("O campo [dataNascimento] não pode ser vazio e deve possuir a formato <yyyy-mm-dd>.");

            List<string> sexos = new List<string>{"m", "f"};

            RuleFor(p => p.sexo)
                .NotEmpty().WithMessage("O campo [sexo] não pode ser vazio.")
                .Must(p => sexos.Contains(p)).WithMessage("O campo [sexo] só pode conter os valores 'm' ou 'f'.");

            RuleFor(p => p.endereco)
                .NotEmpty().WithMessage("O campo [endereco] não pode ser vazio.")
                .MaximumLength(150).WithMessage("O campo [endereco] só pode conter até 150 carácteres.");
        }
    }
}
