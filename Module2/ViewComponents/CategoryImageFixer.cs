using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Module2.ViewComponents
{
    public class CategoryImageFixerViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(byte[] picture)
        {
            //it will not work if we try to change image. Because it is not time to hande both image formats which contains OLE db header and without this header.
            TempData["image"] = Convert.ToBase64String(picture, 78, picture.Length - 78);
            return View();
        }
    }
}
