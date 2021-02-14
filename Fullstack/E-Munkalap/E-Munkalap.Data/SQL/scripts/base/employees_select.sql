Select id, name, adLoginName
from munkalap_employees
--#if @id
where id = @id
--#endif
order by name