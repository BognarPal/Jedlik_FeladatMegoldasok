update munkalap_employees
	set name = @name,
	    adLoginName = @adLoginName
where id = @id;

Select id, name, adLoginName
from munkalap_employees
where id = @id;


