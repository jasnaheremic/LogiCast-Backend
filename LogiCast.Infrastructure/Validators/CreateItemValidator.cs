using FluentValidation;
using LogiCast.Domain.DTOs;
using LogiCast.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class CreateItemValidator : AbstractValidator<CreateItemDto>
{
    private readonly AppDbContext _dbContext;

    public CreateItemValidator(AppDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Item name is required.")
            .MustAsync(BeUniqueName).WithMessage("An item with this name already exists.");

        RuleFor(x => x.Barcode)
            .NotEmpty().WithMessage("Barcode is required.")
            .Matches(@"^\d{10}$").WithMessage("Barcode must be exactly 10 digits.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0.");
    }

    private async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
    {
        return !await _dbContext.Item.AnyAsync(i => i.Name == name, cancellationToken);
    }
}