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
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
                    
!pr0cC0d3!
     
END;