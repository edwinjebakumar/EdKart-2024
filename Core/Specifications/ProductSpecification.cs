using Core.Entities;

namespace Core.Specifications
{
    public class ProductSpecification : BaseSpecification<Product>
    {
        public ProductSpecification(ProductSpecparams productSpecparams)
                    : base(x =>
                    (string.IsNullOrEmpty(productSpecparams.Search) || x.Name.ToLower().Contains(productSpecparams.Search)) &&
                    (!productSpecparams.Brands.Any() || productSpecparams.Brands.Contains(x.Brand)) &&
                    (!productSpecparams.Types.Any() || productSpecparams.Types.Contains(x.Type)))
        {
            ApplyPaging((productSpecparams.PageIndex - 1) * productSpecparams.PageSize, productSpecparams.PageSize);
            switch (productSpecparams.Sort)
            {
                case "priceAsc":
                    AddOrderBy(x => x.Price);
                    break;
                case "priceDesc":
                    AddOrderByDesc(x => x.Price);
                    break;
                default:
                    AddOrderBy(x => x.Name);
                    break;
            }
        }
    }
}