namespace ImageServe.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using ImageServe.Services.Contracts;
    using ImageServe.WebModels.Dtos;

    using Microsoft.AspNetCore.Mvc;

    using PagedList.Core;

    public class SearchController : Controller
    {
        private readonly IImageService imageService;

        public SearchController(IImageService imageService)
        {
            this.imageService = imageService;
        }

        public IActionResult Index(string searchString, int? page)
        {

            var pageSize = 9;
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;

            var result = this.imageService.Search(searchString, pageNumber, pageSize);

            var currentPage = result.AsQueryable().ToPagedList<ImageDto>(pageNumber, pageSize);


            return View(currentPage);
        }

    }
}