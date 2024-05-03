using LMS.WebFrontend.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;


namespace LMS.WebFrontend.Controllers
{
    public class BooksController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public BooksController(IHttpClientFactory httpClientFactory)
        {

            _httpClientFactory = httpClientFactory;

        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var books = await GetBooksAsync();
                return View(books.ToArray());
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }
        public async Task<List<BookDetailVM>> GetBooksAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var apiUrl = "https://localhost:44382/Books/GetBooks";

            var response = await client.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<BookDetailVM>>(jsonString);
            }
            else
            {
                throw new InvalidOperationException("Could not retrieve books: " + response.StatusCode);
            }
        }


        [HttpPost]
        public async Task<IActionResult> BorrowBookAsync(int id)
        {
            BorrowBookVM bb = new BorrowBookVM()
            {
                MemberId = Convert.ToInt32(HttpContext.Session.GetString("UserId")),
                BookId = id,
            };

            var client = _httpClientFactory.CreateClient();
            var apiUrl = "https://localhost:44382/BorrowBooks/InsertBorrow";

            try
            {
                var response = await client.PostAsJsonAsync(apiUrl, bb);

                if (response.IsSuccessStatusCode)
                {
                    var borrowResponse = await response.Content.ReadFromJsonAsync<int>();

                    if (borrowResponse == 0)
                    {
                        return Json(new { res = borrowResponse });
                    }
                    else
                    {
                        return Json(new { res = 1 });
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Something went wrong.");
            }

            return Json(new { res = 1 });
        }

        [HttpPost]
        public async Task<IActionResult> ReturnBookAsync(int id)
        {
            BorrowBookVM bb = new BorrowBookVM()
            {
                MemberId = Convert.ToInt32(HttpContext.Session.GetString("UserId")),
                BookId = id,
            };

            var client = _httpClientFactory.CreateClient();
            var apiUrl = "https://localhost:44382/BorrowBooks/BorrowReturn";

            try
            {
                var response = await client.PostAsJsonAsync(apiUrl, bb);

                if (response.IsSuccessStatusCode)
                {
                    var returnResponse = await response.Content.ReadFromJsonAsync<int>();

                    if (returnResponse == 0)
                    {
                        return Json(new { res = returnResponse });
                    }
                    else
                    {
                        return Json(new { res = 1 });
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Something went wrong.");
            }

            return Json(new { res = 1 });
        }
    }
}
