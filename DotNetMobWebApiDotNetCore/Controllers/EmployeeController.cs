using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DotnetMobDomain.Abstract;
using DotnetMobDomain.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace DotnetMobAPI.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private IDotnetMobRepo dotnetMobRepo;
        
        public EmployeeController(IDotnetMobRepo dotnetMob)
        {
            dotnetMobRepo = dotnetMob;
        }

        [HttpGet]
        [Route("SakreeyaOkkomaSewakayoEwanna")]
        public IEnumerable<Employee> GetAllTheEmployees()
        {
            return dotnetMobRepo.GetAllEmployees();
        }

        [HttpGet]
        [Route("MemaAnkayataAdalaSewakayawaEwanna/{Id}")]
        public IQueryable<Employee> GetSpecificEmp(int Id)
        {
          return  dotnetMobRepo.GetEmployeeById(Id);
        }

        [HttpGet]
        [Route("AkreeyaSiyalumaSewakayanwaEwanna")]
        public IQueryable<Employee> GetCancelledEmployees()
        {
            return dotnetMobRepo.GetCancelledEmployeesRepo();
        }

        [HttpPost]
        [Route("MemaSewakayawaEthulathKranna")]
        public HttpResponseMessage SaveSelectedEmployee(Employee em)
        {
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
            if (em.EmpId == 0)
            {
                dotnetMobRepo.SaveEmployee(em);
                httpResponseMessage.ReasonPhrase = "Insert success";
                httpResponseMessage.StatusCode = HttpStatusCode.OK;
            }
            else
            {            
                httpResponseMessage.ReasonPhrase = "Employee Id should not be there";
                httpResponseMessage.StatusCode = HttpStatusCode.NotAcceptable;
            }
            return httpResponseMessage;
            
        }

        [HttpPut]
        [Route("SewakayawaYawathkaleenaKaranna/{id}")]
        public IActionResult UpdateSelectedEmployee(int id, Employee em)
        {
            try
            {
                dotnetMobRepo.UpdateEmployee(em);
            }
            catch(Exception)
            {                
                if(dotnetMobRepo.GetEmployeeById(id).Count() == 0)
                {
                    return NotFound();
                }
                else
                { 
                    throw;
                }
            }
            return NoContent();          
        }

        [HttpDelete]
        [Route("MemaSewakayawaAkreeyaKaranna/{emp}")]
        public void DeleteSelectedEmp(int emp)
        {
            //dotnetMobRepo.DeleteEmployee(emp);
            dotnetMobRepo.MarkEmployeeAsDelete(emp);
        }

    }
}
