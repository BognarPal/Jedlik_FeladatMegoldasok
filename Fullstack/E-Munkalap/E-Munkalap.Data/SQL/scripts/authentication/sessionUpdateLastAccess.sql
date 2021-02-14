update munkalap_sessions
	set lastAccess = NOW()
where sessionID = @sessionId