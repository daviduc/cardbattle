-- Step 1: Kill all active connections to the database
USE master;
GO

DECLARE @kill varchar(8000) = '';
SELECT @kill = @kill + 'KILL ' + CONVERT(varchar(5), session_id) + '; '
FROM sys.dm_exec_sessions
WHERE database_id = DB_ID('BattleSimulatorDB')

EXEC(@kill);
GO

-- Step 2: Set the database to SINGLE_USER mode with immediate rollback
ALTER DATABASE [BattleSimulatorDB] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
GO

-- Step 3: Drop the database
DROP DATABASE [BattleSimulatorDB];
GO
