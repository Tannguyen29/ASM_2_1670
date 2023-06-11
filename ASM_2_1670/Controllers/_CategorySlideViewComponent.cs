using ASM_2_1670.Areas.Admin.Models;
using ASM_2_1670.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ASM_2_1670.Controllers
{
    [ViewComponent(Name = "_CategorySlide")]
    public class _CategorySlideViewComponent:ViewComponent
    {
        private readonly ASM_2_1670Context _context;

        public _CategorySlideViewComponent(ASM_2_1670Context context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var _categorySlide = _context.Category.ToList();
            return View("_CategorySlide", _categorySlide);
        }

    }
}
