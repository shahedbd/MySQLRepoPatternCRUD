using Microsoft.AspNetCore.Mvc;
using MySQLRepository;
using MySQLRepository.Model;
using MySQLRepository.UnitOfWork;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Web.MySQLCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasicInfoController : ControllerBase
    {
        protected IMySqlUnitOfWork Uow { get; set; }
        public BasicInfoController(IMySqlUnitOfWork uow)
        {
            Uow = uow;
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> GetAll()
        {
            string MySQLQuery = "select * from basicinfo;";
            IEnumerable<BasicInfo> list = await Uow.TblBasicInfo.GetAll(MySQLQuery);

            DataTable list2 = await Uow.TblBasicInfo.GetByIDUsingSP(3);

            List<BasicInfo> listPersonalInfo3 = list2.DataTableToList<BasicInfo>();

            var json = JsonConvert.SerializeObject(list);
            return new string[] { json };
        }


        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<string>> Get(int id)
        {
            string MySQLQuery = "select * from basicinfo where basicinfoid=3;";
            BasicInfo _BasicInfo = await Uow.TblBasicInfo.GetByID(MySQLQuery);

            var json = JsonConvert.SerializeObject(_BasicInfo);
            return json;
        }

        // POST api/values
        [HttpPost]
        //public async void Post([FromBody] string value)
        public async void Post()
        {
            string MySQLQuery = @"INSERT INTO `devtest`.`basicinfo` (`basicinfoid`, `firstname`, `lastname`, `dateofbirth`, `city`, `country`, 
                                `mobileno`, `nid`, `email`, `status`) 
                                VALUES('4', 'Jamal 3', 'Islam', '1987-04-26', 'Dhaka', 'BD', '132465798', '798465132132465798', 'dev@gmail.com', '1'); ";
            bool result = await Uow.TblBasicInfo.Execute(MySQLQuery);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
