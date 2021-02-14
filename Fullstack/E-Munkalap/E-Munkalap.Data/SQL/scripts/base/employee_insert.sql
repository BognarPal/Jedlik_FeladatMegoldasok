insert into munkalap_employees (name, adLoginName)
values (@name, @adLoginName);

Select id, name, adLoginName
from munkalap_employees
where id = LAST_INSERT_ID();