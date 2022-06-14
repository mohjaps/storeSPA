#nullable disable
namespace storeSPA.Controllers
{
    public class DataVM
    {
        public String Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public IFormFile img { get; set; }
    }
    [Route("api/Data")]
    [ApiController]
    public class PerfumeController : ControllerBase
    {
        private readonly IRepository<Perfume> _repo;
        private readonly IWebHostEnvironment env;

        public PerfumeController(IRepository<Perfume> repo, IWebHostEnvironment env)
        {
            _repo = repo;
            this.env = env;
        }

        [Authorize("All")]
        [HttpPost("NewPerfume")]
        public async Task<IActionResult> AddNew([FromForm] DataVM model)
        {
            if (!ModelState.IsValid)
                return Ok(new ApiResult { Result = false, Errors = new() { "Fill Required Fields" } });

            Perfume perfume = new Perfume();
            perfume.Name = model.Name;
            perfume.Description = model.Description;
            perfume.Quantity = model.Quantity;
            perfume.Price = model.Price;

            string imgName = Guid.NewGuid().ToString() + model.img.FileName;
            string path = Path.Combine(env.WebRootPath, "Images", imgName);
            using(var stream = new FileStream(path, FileMode.Create))
            {
                string fileName =  Path.Combine("Images", imgName);
                await model.img.CopyToAsync(stream);
                perfume.Image = fileName;

            }
            var userId = HttpContext.User.FindFirstValue("UserId");
            perfume.Saler_Id = userId;
            return Ok(await _repo.Add(perfume));
        }

        [Authorize("All")]
        [HttpPut("UpdatePerfume")]
        public async Task<IActionResult> Update([FromForm] DataVM model)
        {
            if (!ModelState.IsValid)
                return Ok(new ApiResult { Result = false, Errors = new() { "Fill Required Fields" } });

            Perfume perfume = (await _repo.GetById(model.Id)).Data[0];
            if (perfume is null)
                return Ok(new ApiResult { Result = false, Errors = new() { "Un Expected Error" } });

            if (model.img is not null)
            {
                string imgName = Guid.NewGuid().ToString() + model.img.FileName;
                string path = Path.Combine(env.WebRootPath, "Images", imgName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    string fileName = Path.Combine("Images", imgName);
                    await model.img.CopyToAsync(stream);
                    perfume.Image = fileName;
                }
            }
            perfume.Name = model.Name;
            perfume.Description = model.Description;
            perfume.Quantity = model.Quantity;
            perfume.Price = model.Price;

            return Ok(await _repo.Update(perfume));
        }

        [Authorize("Admin")]
        [HttpDelete("DeletePerfume")]
        public async Task<IActionResult> Delete(String id)
        {
            if (ModelState.IsValid) { }
                return Ok(await _repo.Delete(id));

            return Ok(new ApiResult { Result = false });
        }

        [HttpGet("GetPerfume")]
        public async Task<IActionResult> GetOneId([FromBody] String id)
        {
            if (ModelState.IsValid)
                return Ok(await _repo.GetById(id));

            return Ok(new ApiResult { Result = false });
        }
        
        [HttpGet("GetByNamePerfume")]
        public async Task<IActionResult> GetOneName([FromBody] String Name)
        {
            if (ModelState.IsValid)
                return Ok(await _repo.GetByName(Name));

            return Ok(new ApiResult { Result = false });
        }

        [HttpGet("GetByUserPerfume")]
        [Authorize(Policy = "All")]
        public async Task<IActionResult> GetAllForUser()
        {
            var userId = HttpContext.User.FindFirstValue("UserId");
            return Ok(await _repo.GetAllForUser(userId));
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _repo.GetAll());
        }

    }
}
