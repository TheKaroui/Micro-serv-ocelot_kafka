using FluentValidation;
namespace customers.Application.Validators;
public class CreateCustomersValidator : AbstractValidator<customers.Domain.Models.CreateCustomersDto> { public CreateCustomersValidator(){ RuleFor(x=>x.Name).NotEmpty(); } }