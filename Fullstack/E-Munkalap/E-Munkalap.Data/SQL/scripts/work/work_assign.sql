update munkalap_works
    set Id = @Id,
        ProfessionId = @ProfessionId,
        EmployeeId = @EmployeeId,
        DeadLine = @DeadLine,
        AssignDate = CURTIME(),
        AssignDetails = @AssignDetails,
        AssignerName = @AssignerName
where id = @id
    