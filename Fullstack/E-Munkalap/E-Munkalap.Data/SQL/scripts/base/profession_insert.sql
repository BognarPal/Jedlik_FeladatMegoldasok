insert into munkalap_professions (name)
values (@name);

Select id, name
from munkalap_professions
where id = LAST_INSERT_ID();