using FluentValidation;
namespace orders.Application.Validators;
public class CreateOrdersValidator : AbstractValidator<orders.Domain.Models.CreateOrdersDto> { public CreateOrdersValidator(){ RuleFor(x=>x.Name).NotEmpty(); } }