update munkalap_professions
	set name = @name
where id = @id;

Select id, name
from munkalap_professions
where id = @id;


