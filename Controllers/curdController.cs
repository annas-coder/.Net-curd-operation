using Record.Db;
using Microsoft.AspNetCore.Mvc;
using Record.Model;

namespace Record.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class curdController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        
        public curdController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        [Route("api/create")]
        //IActionResult return multi datatype ex; int,float,string
        public IActionResult creating([FromQuery]Employees emp)
        {
            List<Employees> emply = new List<Employees>();
            _db.Employees.Add(emp);
            _db.SaveChanges();

            emply = _db.Employees.ToList();

            return Ok(emply);
        }

        [HttpGet]
        [Route("api/getAll")]
        public IActionResult GetAllDat()
        {
            List<Employees> Employee = new List<Employees>();
            Employee = _db.Employees.ToList();
            if(Employee.Count > 0)
            {
                 return Ok(Employee); 
            }
            return NotFound();
        }

        [HttpGet]
        [Route("api/GetById")]

        public IActionResult GetById([FromQuery]String name)
        {
            //Employees employees = _db.Employees.where(c=>c.id == id).FirstOrDefault();
            var employees = (from emp in _db.Employees
                                  where emp.name == name
                                  orderby emp.id descending
                                  select new   
                                  {
                                      custom_id = emp.id,
                                      custom_name = emp.name,
                                  }).ToList();
            return employees.Count > 0 ? Ok(employees): Ok("NO Matched Data in db");   
            //return NotFound();
        }


        [HttpPut]
        [Route("api/update")]
        public IActionResult update([FromQuery]Employees emp)
        {
            _db.Employees.Update(emp);
            _db.SaveChanges();
            return Ok(emp);
        }

        [HttpDelete]
        [Route("api/delete")]

        public IActionResult DeleteRow([FromQuery]int id)
        {
            Employees emp = _db.Employees.Where(c => c.id == id).FirstOrDefault();
            _db.Employees.Remove(emp);
            _db.SaveChanges();
            return Ok(emp);

        }

    }
}
