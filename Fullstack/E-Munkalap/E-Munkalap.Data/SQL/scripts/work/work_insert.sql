insert into munkalap_works (RequesterName, Description, RequestDate)
values (@RequesterName, @Description, CURTIME());

Select id, RequesterName, Description, RequestDate
from munkalap_works
where id = LAST_INSERT_ID();
