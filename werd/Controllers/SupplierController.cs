using Microsoft.AspNetCore.Mvc;
using werd.Model;
using werd.Repository;

namespace werd.Controllers
{
    [Route("[controller]")]
    public class SupplierController : Controller
    {
        private readonly SupplierRepository _supplierRepository;
        public SupplierController(SupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        [HttpPost("Add")]
        public IActionResult Add([FromBody] Supplier supplier)
        {
            string id = _supplierRepository.Add(supplier);
            return Ok(id);
        }

        [HttpPost("GetPagination")]
        public IActionResult GetPagination([FromBody] SearchCondition searchCondition)
        {
            var result = _supplierRepository.GetPagination(searchCondition);
            return Ok(new
            {
                suppliers = result.suppliers,
                total = result.total
            });
        }

        [HttpGet("GetTest")]
        public IActionResult GetTest()
        {
            List<TestData> testDatas = new List<TestData>();
            return Ok(testDatas);
        }


        [HttpPost("Update")]
        public IActionResult Update([FromBody] Supplier supplier)
        {
            int count = _supplierRepository.Update(supplier);
            return Ok(count);
        }

        [HttpPost("DeleteIds")]
        public IActionResult DeleteIds([FromBody] string[] ids)
        {
            bool isDelete = _supplierRepository.Delete(ids);
            return Ok(isDelete);
        }
    }
}
