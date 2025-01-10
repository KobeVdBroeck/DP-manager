echo off
echo Generate backup file name

for /f "tokens=2 delims==" %%I in ('"wmic os get localdatetime /value"') do set datetime=%%I
set CUR_YYYY=%datetime:~0,4%
set CUR_MM=%datetime:~4,2%
set CUR_DD=%datetime:~6,2%
set CUR_HH=%datetime:~8,2%
set CUR_NN=%datetime:~10,2%
set CUR_SS=%datetime:~12,2%

set BACKUP_FILE=%CUR_YYYY%-%CUR_MM%-%CUR_DD%_%CUR_HH%-%CUR_NN%-%CUR_SS%.custom.backup

echo Backup path: %BACKUP_FILE%

echo Getting variables from .env

set PGDUMP_PATH=""
set PG_USER=""
set PG_PASS=""
set PG_DBNAME=""
for /f "usebackq tokens=1,* delims==" %%A in (.env) do (
    if /i "%%A"=="PG_DUMP" (
        set "PGDUMP_PATH=%%B"
    )
    if /i "%%A"=="USER" (
        set "PG_USER=%%B"
    )
    if /i "%%A"=="PASS" (
        set "TARGET_VALUE=%%B"
    )
    if /i "%%A"=="DB" (
        set "PG_DBNAME=%%B"
    )
)

if "%PGDUMP_PATH%"=="" (
    GOTO error
)
if %PG_USER%=="" (
    GOTO error
)
if %PG_PASS%=="" (
    GOTO error
)
if %PG_DBNAME%=="" (
    GOTO error
)

echo Creating a backup for database %PG_DBNAME%

set PGPASSWORD=%TARGET_VALUE%
"%PGDUMP_PATH%" --username="%PG_USER%" -d "%PG_DBNAME%" --format=custom -f "%BACKUP_FILE%"

echo Backup successfully created: %BACKUP_FILE%
exit /b 1

:error
echo Could not create a backup. The following values must be set in a .env file: PG_DUMP, USER, PASS, DB.
exit /b 1