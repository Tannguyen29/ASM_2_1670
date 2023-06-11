using ASM_2_1670.Areas.Admin.Models;
using ASM_2_1670.Data;
using Microsoft.AspNetCore.Mvc;

namespace ASM_2_1670.Controllers
{
	[ViewComponent(Name = "_Category")]
	public class _CategoryViewComponent:ViewComponent
	{
		private readonly ASM_2_1670Context _context;

		public _CategoryViewComponent(ASM_2_1670Context context)
		{
			_context = context;
		}
		public IViewComponentResult Invoke()
		{
			var _category = _context.Category.ToList();
			return View("_Category", _category);
		}
	}
}
