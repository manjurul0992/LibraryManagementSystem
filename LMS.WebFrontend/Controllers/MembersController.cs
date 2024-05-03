using LMS.WebFrontend.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;

namespace LMS.WebFrontend.Controllers
{
    public class MembersController : Controller
    {
        // private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _httpClient;
        public MembersController(IHttpClientFactory httpClientFactory)
        {

            this._httpClient = httpClientFactory.CreateClient(); ;
            _httpClient.BaseAddress = new Uri("https://localhost:44382/");

        }
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("Members/GetMembers");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var members = JsonConvert.DeserializeObject<List<MemberVM>>(content);
                return View(members);
            }
            else
            {
                // Handle error if API request fails
                return View(new List<MemberVM>()); // or return an error view
            }
        }
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MemberId,FirstName,LastName,Email,PhoneNumber,Password,RegistrationDate")] MemberVM memberVM)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(memberVM);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("Members/InsertMembers", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View("Error");
                }
            }
            return View(memberVM);
        }
        [HttpGet]
        [Route("Edit/{id}")]

        public async Task<IActionResult> Edit(int? id)
        {
            id = 1;
            if (id == null)
            {
                return NotFound();
            }

            var response = await _httpClient.GetAsync($"Members/EditMember/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var memberVM = JsonConvert.DeserializeObject<MemberVM>(content);
                if (memberVM == null)
                {
                    return NotFound();
                }
                return View(memberVM);
            }
            else
            {
                // Handle error if API request fails
                return View("Error");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MemberId,FirstName,LastName,Email,PhoneNumber,Password,RegistrationDate")] MemberVM memberVM)
        {
            if (id != memberVM.MemberId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var json = JsonConvert.SerializeObject(memberVM);
                    var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                    var response = await _httpClient.PostAsync($"Members/EditMember/{id}", content);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        return View("Error");
                    }
                }
                catch (HttpRequestException)
                {
                    return View("Error");
                }
            }
            return View(memberVM);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            id = 1;
            if (id == null)
            {
                return NotFound();
            }

            var response = await _httpClient.DeleteAsync($"Members/DeleteMember/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                // Handle error if API request fails
                return View("Error");
            }

        }
    }
}
