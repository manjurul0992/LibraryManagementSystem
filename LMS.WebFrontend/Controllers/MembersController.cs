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
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _httpClient.Dispose();
            }
            base.Dispose(disposing);
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
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            HttpResponseMessage response = await _httpClient.GetAsync($"Members/GetMemberById/{id}");
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
        public async Task<IActionResult> Edit(MemberVM member)
        {
            if (ModelState.IsValid)
            {
                // Send HTTP PUT request to update member data
                HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"Members/EditMember/{member.MemberId}", member);
                if (response.IsSuccessStatusCode)
                {
                    // Handle success
                    return RedirectToAction("Index"); // Redirect to list page after successful edit
                }
                else
                {
                    // Handle error
                    return View("Error");
                }
            }
            // If model state is not valid, redisplay the form with validation errors
            return View(member);
        }





        public async Task<IActionResult> Delete(int? id)
        {

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
