using Catalog.API.Models;
using System.Xml.Linq;

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name, string Description, Guid CategoryId) : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);

    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("CategoryId is required");
        }
    }

    internal class CreateProductHandler
        (IProductRepository repository)
        : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = command.Name,
                Description = command.Description,
                CategoryId = command.CategoryId
            };

            var res = await repository.AddAsync(product, cancellationToken);

            return new CreateProductResult(res.Id);
        }
    }
}
