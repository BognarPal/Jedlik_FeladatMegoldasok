update munkalap_works
    set Id = @Id,
        CheckDate = CURTIME(),
        CheckerUser = @CheckerUser,
        CheckComment = @CheckComment
where id = @id