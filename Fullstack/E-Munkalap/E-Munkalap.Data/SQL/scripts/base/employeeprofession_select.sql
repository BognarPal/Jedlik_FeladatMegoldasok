Select munkalap_employeeProfessions.Id, 
       munkalap_employeeProfessions.EmployeeId, 
       munkalap_employeeProfessions.ProfessionId,
       munkalap_professions.name as ProfessionName,
       munkalap_employees.name as EmployeeName
from munkalap_employeeProfessions left join
     munkalap_professions on munkalap_employeeProfessions.ProfessionId = munkalap_professions.id left join
     munkalap_employees on munkalap_employeeProfessions.EmployeeId = munkalap_employees.Id
--#if @employeeId
where munkalap_employeeProfessions.employeeId = @employeeId
--#endif
--#if @professionId
where munkalap_employeeProfessions.professionId = @professionId
--#endif
order by ProfessionName