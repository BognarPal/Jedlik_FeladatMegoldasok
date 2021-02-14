Select id, name
from munkalap_professions
--#if @id
where id = @id
--#endif
order by name