using ExtremeSports2.Data;
using ExtremeSports2.Data.Entities;
using ExtremeSports2.Helpers;
using ExtremeSports2.Models;
using ExtremeSports2.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ExtremeSports2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public HomeController(ILogger<HomeController> logger, DataContext context, IUserHelper userHelper)
        {
            _logger = logger;
            _context = context;
            _userHelper = userHelper;
        }

        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "NameDesc" : "";
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "PriceDesc" : "Price";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;


            IQueryable<Product> query = _context.Products
                .Include(p => p.ProductImages)
                .Include(p => p.ProductCategories)
                .ThenInclude(pc => pc.Category);

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(p => (p.Name.ToLower().Contains(searchString.ToLower()) ||
                                            p.ProductCategories.Any(pc => pc.Category.Name.ToLower().Contains(searchString.ToLower()))) &&
                                            p.Stock > 0);
            }
            else
            {
                query = query.Where(p => p.Stock > 0);
            }


            switch (sortOrder)
            {
                case "NameDesc":
                    query = query.OrderByDescending(p => p.Name);
                    break;
                case "Price":
                    query = query.OrderBy(p => p.Price);
                    break;
                case "PriceDesc":
                    query = query.OrderByDescending(p => p.Price);
                    break;
                default:
                    query = query.OrderBy(p => p.Name);
                    break;
            }
            int pageSize = 8;

            HomeViewModel model = new()
            {
                Products = await PaginatedList<Product>.CreateAsync(query, pageNumber ?? 1, pageSize),
                Categories = await _context.Categories.ToListAsync()
            };
            User user = await _userHelper.GetUserAsync(User.Identity.Name);
            if (user != null)
            {
                model.Quantity = await _context.TemporalSales
                    .Where(ts => ts.User.Id == user.Id)
                    .SumAsync(ts => ts.Quantity);
            }

            return View(model);
        }
        public async Task<IActionResult> Add(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            Product product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            User user = await _userHelper.GetUserAsync(User.Identity.Name);
            if (user == null)
            {
                return NotFound();
            }

            TemporalSale temporalSale = new()
            {
                Product = product,
                Quantity = 1,
                User = user
            };

            _context.TemporalSales.Add(temporalSale);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

		[Route("error/404")]
		public IActionResult Error404()
		{
			return View();
		}
	}
}