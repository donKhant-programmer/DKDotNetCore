using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DKDotNetCore.RestApiWithNLayer.Features.IncompatibleFood
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncompatibleFoodController : ControllerBase
    {
        private async Task<IncompatibleFood> getDataAsync()
        {
            String jsonStr = await System.IO.File.ReadAllTextAsync("IncompatibleFood.json");
            var model = JsonConvert.DeserializeObject<IncompatibleFood>(jsonStr);

            return model;
        }

        [HttpGet]
        public async Task<IActionResult> IncompatibleFood()
        {
            var model = await getDataAsync();

            return Ok(model.Tbl_IncompatibleFood);
        }

        [HttpGet("categories")]
        public async Task<IActionResult> Categories()
        {
            var model = await getDataAsync();

            var lst = model.Tbl_IncompatibleFood.Select(x => x.Description).Distinct().ToList();

            return Ok(lst);
        }

        [HttpGet("{description}")]
        public async Task<IActionResult> ListByCategory(String description)
        {
            var model = await getDataAsync();

            var lst = model.Tbl_IncompatibleFood.Where(x => x.Description.Equals(description)).ToList();

            return Ok(lst);
        }
    }

0
    public class IncompatibleFood
    {
        public Tbl_Incompatiblefood[] Tbl_IncompatibleFood { get; set; }
    }

    public class Tbl_Incompatiblefood
    {
        public int Id { get; set; }
        public string FoodA { get; set; }
        public string FoodB { get; set; }
        public string Description { get; set; }
    }

}
