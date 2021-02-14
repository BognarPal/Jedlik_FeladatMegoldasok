using E_Munkalap.DTO.Base;
using E_Munkalap.SQL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Munkalap.Controllers
{
    [ApiController]
    public class BaseDataController : ControllerBase
    {
        private readonly DatabaseProvider databaseProvider;

        public BaseDataController(IOptions<DatabaseProvider> dbProvider)
        {
            databaseProvider = dbProvider.Value;
        }

#region professions
        [HttpGet]
        [Route("base/professions")]
        public IActionResult Professions()
        {
            return this.RunWithErrorHandling(() =>
            {
                return Ok
                (
                    databaseProvider.Query<Profession>("base.professions_select")
                );
            });
        }

        [HttpGet]
        [Route("base/profession/{id}")]
        public IActionResult GetProfession(int id)
        {
            return this.RunWithErrorHandling(() =>
            {
                return Ok
                (
                    databaseProvider.Query<Profession>("base.professions_select", new { id }).FirstOrDefault()
                );
            });
        }


        [HttpPost]
        [Route("base/professions")]
        public IActionResult InsertProfession(Profession profession)
        {
            return this.RunWithErrorHandling(() =>
            {
                return Ok
                (
                    databaseProvider.Query<Profession>("base.profession_insert", profession).FirstOrDefault()
                );
            });
        }

        [HttpPut]
        [Route("base/professions")]
        public IActionResult UpdateProfession(Profession profession)
        {
            return this.RunWithErrorHandling(() =>
            {
                return Ok
                (
                    databaseProvider.Query<Profession>("base.profession_update", profession).FirstOrDefault()
                );
            });
        }

        [HttpDelete]
        [Route("base/professions")]
        public IActionResult DeleteProfession(Profession profession)
        {
            return this.RunWithErrorHandling(() =>
            {
                databaseProvider.Execute("base.profession_delete", profession);
                return Ok(true);
            });
        }
        #endregion professions

#region employee
        [HttpGet]
        [Route("base/employees")]
        public IActionResult Employees()
        {
            return this.RunWithErrorHandling(() =>
            {
                return Ok
                (
                    databaseProvider.Query<Employee>("base.employees_select")
                );
            });
        }

        [HttpGet]
        [Route("base/employee/{id}")]
        public IActionResult GetEmployee(int id)
        {
            return this.RunWithErrorHandling(() =>
            {
                return Ok
                (
                    databaseProvider.Query<Employee>("base.professions_select", new { id }).FirstOrDefault()
                );
            });
        }

        [HttpPost]
        [Route("base/employees")]
        public IActionResult InsertEmployee(Employee employee)
        {
            return this.RunWithErrorHandling(() =>
            {
                return Ok
                (
                    databaseProvider.Query<Employee>("base.employee_insert", employee).FirstOrDefault()
                );
            });
        }

        [HttpPut]
        [Route("base/employees")]
        public IActionResult UpdateEmployee(Employee employee)
        {
            return this.RunWithErrorHandling(() =>
            {
                return Ok
                (
                    databaseProvider.Query<Employee>("base.employee_update", employee).FirstOrDefault()
                );
            });
        }

        [HttpDelete]
        [Route("base/employees")]
        public IActionResult DeleteEmployee(Employee employee)
        {
            return this.RunWithErrorHandling(() =>
            {
                databaseProvider.Execute("base.employee_delete", employee);
                return Ok(true);
            });
        }
        #endregion employee

#region employee-profession
        [HttpGet]
        [Route("base/employeeprofession/{id}")]
        public IActionResult GetEmployeeProfession(int id)
        {
            return this.RunWithErrorHandling(() =>
            {
                return Ok
                (
                    databaseProvider.Query<EmployeeProfession>("base.employeeprofession_select", new { employeeId = id})
                );
            });
        }

        [HttpPost]
        [Route("base/employeeprofession")]
        public IActionResult InsertEmployeeProfession(EmployeeProfession employeeProfession)
        {
            return this.RunWithErrorHandling(() =>
            {
                return Ok
                (
                    databaseProvider.Query<EmployeeProfession>("base.employeeprofession_insert", employeeProfession).FirstOrDefault()
                );
            });
        }

        [HttpDelete]
        [Route("base/employeeprofession")]
        public IActionResult DeleteEmployeeProfession(EmployeeProfession employeeProfession)
        {
            return this.RunWithErrorHandling(() =>
            {
                databaseProvider.Execute("base.employeeprofession_delete", employeeProfession);
                return Ok(true);
            });
        }
#endregion

    }
}
