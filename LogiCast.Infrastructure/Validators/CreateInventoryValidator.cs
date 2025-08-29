using FluentValidation;
using LogiCast.Domain.DTOs;

public class CreateInventoryValidator : AbstractValidator<CreateInventoryDto>
{
    public CreateInventoryValidator()
    {
        RuleFor(x => x.WarehouseId)
            .NotEmpty().WithMessage("WarehouseId is required.");

        RuleFor(x => x.ItemId)
            .NotEmpty().WithMessage("ItemId is required.");

        RuleFor(x => x.maxValue)
            .GreaterThan(0).WithMessage("Max value must be greater than 0.");

        RuleFor(x => x.minValue)
            .GreaterThan(0).WithMessage("Min value must be greater than 0.")
            .LessThanOrEqualTo(x => x.maxValue)
            .WithMessage("Min value cannot be greater than max value.");

        RuleFor(x => x.Quantity)
            .GreaterThanOrEqualTo(0).WithMessage("Quantity cannot be negative.")
            .LessThanOrEqualTo(x => x.maxValue)
            .WithMessage("Quantity cannot exceed max value.");
    }
}