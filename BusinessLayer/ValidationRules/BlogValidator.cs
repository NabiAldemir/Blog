using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Concrete;
using FluentValidation;

namespace BusinessLayer.ValidationRules
{
    public class BlogValidator:AbstractValidator<Blog>
    {
        public BlogValidator() 
        { 
        RuleFor(x=>x.BlogTitle).MaximumLength(150).WithMessage("Lütfen 150 Karakterden daha az karakter kullanın!!");
        RuleFor(x=>x.BlogTitle).MinimumLength(5).WithMessage("Lütfen 5 Karakterden daha fazla karakter kullanın!!");
        }
    }
}
