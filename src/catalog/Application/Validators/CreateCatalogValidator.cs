using FluentValidation;
namespace catalog.Application.Validators;
public class CreateCatalogValidator : AbstractValidator<catalog.Domain.Models.CreateCatalogDto> { public CreateCatalogValidator(){ RuleFor(x=>x.Name).NotEmpty(); } }