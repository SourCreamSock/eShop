using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.DTOs.Catalog;

namespace Web.Application.Validatiors
{
    public class GetItemsFilterDtoValidator : AbstractValidator<GetItemsFilterDto>
    {
        public GetItemsFilterDtoValidator()
        {
            RuleFor(r=>r.CategoryId).GreaterThan(0).LessThan(long.MaxValue);
            RuleFor(r=>r.BrandId).GreaterThan(0).LessThan(long.MaxValue);
            RuleFor(r=>r.PageSize).GreaterThan(0).LessThan(int.MaxValue);
            RuleFor(r=>r.PageIndex).GreaterThan(-1).LessThan(int.MaxValue);
        }
    }
}
