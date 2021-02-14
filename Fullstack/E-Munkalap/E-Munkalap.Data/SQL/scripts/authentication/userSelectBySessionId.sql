Select munkalap_sessions.id,       
       munkalap_sessions.AdLoginName,
       munkalap_sessions.UserName
from munkalap_sessions
where munkalap_sessions.sessionId = @sessionId

--#include authentication.roles_select