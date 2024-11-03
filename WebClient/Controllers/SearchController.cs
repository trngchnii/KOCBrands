﻿using api.Data;
using api.DTOs;
using api.Models;
using api.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebClient.Controllers
{
    public class SearchController : Controller
    {
        private readonly HttpClient _httpClient;

		public SearchController(HttpClient httpClient)
        {
            _httpClient = httpClient;
		}

        public async Task<ActionResult> Index()
        {
            string apiUrl = "https://localhost:7290/odata/SeachKOL/";

            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var kocs = JsonConvert.DeserializeObject<IEnumerable<InfluencerDto>>(jsonResponse);

                return View(kocs);
            }

            return View(new List<InfluencerDto>());
        }

        [HttpPost]
        public async Task<IActionResult> Search(string name, string? gender, DateTime? dateOfBirth, decimal? bookingPrice = null, int? personalIdentificationNumber = null)
        {
            string apiUrl = $"https://localhost:7290/odata/SeachKOL/search?name={name}&gender={gender}&dateOfBirth={dateOfBirth}&bookingPrice={bookingPrice}&personalIdentificationNumber={personalIdentificationNumber}";

            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            IEnumerable<InfluencerDto> influencers = Enumerable.Empty<InfluencerDto>();

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                influencers = JsonConvert.DeserializeObject<IEnumerable<InfluencerDto>>(jsonResponse);
            }

            return View("Index", influencers);
        }
    }
}