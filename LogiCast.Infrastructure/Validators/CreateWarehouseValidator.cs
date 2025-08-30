using FluentValidation;
using LogiCast.Domain.DTOs;
using LogiCast.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LogiCast.Infrastructure.Validators;

public class CreateWarehouseValidator : AbstractValidator<CreateWarehouseDto>
{
    private readonly AppDbContext appDbContext;

   /* public CreateWarehouseValidator(AppDbContext appDbContext)
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Warehouse name is required.")
            .MaximumLength(100).WithMessage("Warehouse name cannot exceed 100 characters.")
            .MustAsync(BeUniqueName).WithMessage("A warehouse with this name already exists.");

        RuleFor(x => x.Location)
            .NotEmpty().WithMessage("Location is required.")
            .MaximumLength(200).WithMessage("Location cannot exceed 200 characters.");

        RuleFor(x => x.MaxCapacity)
            .GreaterThan(0).WithMessage("Max capacity must be greater than 0.");
    }
    private async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
    {
        return !await appDbContext.Warehouse.AnyAsync(w => w.Name == name, cancellationToken);
    }*/
}