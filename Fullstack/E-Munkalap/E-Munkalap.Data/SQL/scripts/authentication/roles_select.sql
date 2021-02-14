select munkalap_roles.Name
from munkalap_sessions inner join
     munkalap_userRoles on munkalap_sessions.AdLoginName = munkalap_userRoles.AdLoginName inner join
     munkalap_roles on munkalap_userRoles.roleId = munkalap_roles.id
where munkalap_sessions.sessionId = @sessionId
