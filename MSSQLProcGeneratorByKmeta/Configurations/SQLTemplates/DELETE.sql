IF OBJECT_ID('dbo.!pr0cNam3!') IS NULL                             -- Check if SP Exists
        EXEC('CREATE PROCEDURE dbo.!pr0cNam3! AS SET NOCOUNT ON;') -- Create dummy/empty SP
 GO
ALTER PROCEDURE !pr0cNam3!
!param3t3rs!
AS
/*====================================================================
DESCRIPTION:
!Operati0n!     !tblNAM3! 
**********************************************************************
TESTING STRINGS:

!T3sTStrings!

CHANGE CONTROL:
When        Who         What 
---------- -----------------------------------------------------------
!cr3ationDAT3! !cr3ator!        Created
=====================================================================*/
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
 
    BEGIN TRY           
                
!pr0cC0d3!           
      
    END TRY
    BEGIN CATCH
  
       DECLARE @ErrorMessage    NVARCHAR(4000)
              ,@ErrorNumber     INT
              ,@ErrorSeverity   INT
              ,@ErrorState      INT
              ,@ErrorLine       INT
              ,@ErrorProcedure  NVARCHAR(200);
  
        -- Assign variables to error-handling functions that
        -- capture information for RAISERROR.
        SELECT @ErrorNumber     = ERROR_NUMBER()
              ,@ErrorSeverity   = ERROR_SEVERITY()
              ,@ErrorState      = ERROR_STATE()
              ,@ErrorLine       = ERROR_LINE()
              ,@ErrorProcedure  = ISNULL(ERROR_PROCEDURE(), '-')
              ,@ErrorMessage    = ERROR_MESSAGE();
        -- Build the message string that will contain original
        -- error information.
            IF Charindex('~|', @errormessage) = 0     
                SELECT @ErrorMessage = N'~| Error %d, Level %d, State %d, occurred in Procedure %s, Line %d, ' + 'Message: '+ @ErrorMessage
            ELSE
                   SET @ErrorMessage = OBJECT_NAME(@@PRocID) + '<--' + @ErrorMessage
  
        -- Raise an error: msg_str parameter of RAISERROR will contain
        -- the original error information.
        RAISERROR
                 (
                    @ErrorMessage,
                    @ErrorSeverity,
                    1,              
                    @ErrorNumber,    -- parameter: original error number.
                    @ErrorSeverity,  -- parameter: original error severity.
                    @ErrorState,     -- parameter: original error state.
                    @ErrorProcedure, -- parameter: original error procedure name.
                    @ErrorLine       -- parameter: original error line number.
                 );   
    END CATCH   
END;