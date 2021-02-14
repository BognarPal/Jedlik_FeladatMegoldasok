update munkalap_works
    set id = @id
--#if @RequesterName
       ,RequesterName = @RequesterName
--#endif
--#if @Description
       ,Description = @Description
--#endif
--#if @RequestDate
       ,RequestDate = @RequestDate
--#endif      
--#if @ProfessionId
       ,ProfessionId = @ProfessionId
--#endif      
--#if @EmployeeId
       ,EmployeeId = @EmployeeId
--#endif   
--#if @DeadLine
       ,DeadLine = @DeadLine
--#endif   
--#if @AssignDate
       ,AssignDate = @AssignDate
--#endif   
--#if @AssignDetails
       ,AssignDetails = @AssignDetails
--#endif   
--#if @AssignerName
       ,AssignerName = @AssignerName
--#endif   
--#if @FinishDate
       ,FinishDate = @FinishDate
--#endif  
--#if @FinishComment
       ,FinishComment = @FinishComment
--#endif 
--#if @CheckDate
       ,CheckDate = @CheckDate
--#endif 
--#if @CheckerUser
       ,CheckerUser = @CheckerUser
--#endif 
--#if @CheckComment
       ,CheckComment = @CheckComment
--#endif        
where id = @id