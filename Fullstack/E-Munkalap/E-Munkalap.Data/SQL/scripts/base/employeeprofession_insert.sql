insert into munkalap_employeeProfessions (EmployeeId, ProfessionId)
select @EmployeeId, @ProfessionId
WHERE NOT EXISTS(SELECT * from munkalap_employeeProfessions WHERE ProfessionId = @ProfessionId AND EmployeeId = @EmployeeId);

Select munkalap_employeeProfessions.Id, 
       munkalap_employeeProfessions.EmployeeId, 
       munkalap_employeeProfessions.ProfessionId,
       munkalap_professions.name as ProfessionName,
       munkalap_employees.name as EmployeeName
from munkalap_employeeProfessions left join
     munkalap_professions on munkalap_employeeProfessions.ProfessionId = munkalap_professions.id left join
     munkalap_employees on munkalap_employeeProfessions.EmployeeId = munkalap_employees.Id
where munkalap_employeeProfessions.ProfessionId = @ProfessionId
and munkalap_employeeProfessions.EmployeeId = @EmployeeId;