Select munkalap_works.id, 
       munkalap_works.RequesterName, 
       munkalap_works.Description, 
       munkalap_works.RequestDate, 
       munkalap_works.ProfessionId, 
       munkalap_works.EmployeeId, 
       munkalap_works.DeadLine, 
       munkalap_works.AssignDate, 
       munkalap_works.AssignDetails, 
       munkalap_works.AssignerName, 
       munkalap_works.FinishDate, 
       munkalap_works.FinishComment, 
       munkalap_works.CheckDate, 
       munkalap_works.CheckerUser, 
       munkalap_works.CheckComment,
       munkalap_professions.Name as ProfessionName,
       munkalap_employees.Name as EmployeeName
from munkalap_works left join
     munkalap_professions on munkalap_works.ProfessionId = munkalap_professions.Id left join
     munkalap_employees on munkalap_works.EmployeeId = munkalap_employees.Id
--#if @id
where munkalap_works.id = @id
--#endif
order by munkalap_works.id desc
