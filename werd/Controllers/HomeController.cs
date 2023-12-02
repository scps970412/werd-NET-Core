using Microsoft.AspNetCore.Mvc;
using werd.Model;
using werd.Repository;

namespace werd.Controllers
{
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly HomeRepository _homeRepository;
        public HomeController(HomeRepository homeRepository)
        {
            _homeRepository = homeRepository;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            List<TestData> testDatas = new List<TestData>();
            _homeRepository.test();
            //testDatas = _homeRepository.GetAll().ToList();
            return Ok(testDatas);
        }
    }
}
