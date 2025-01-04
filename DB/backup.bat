echo off
echo 'Generate backup file name'

for /f "tokens=2 delims==" %%I in ('"wmic os get localdatetime /value"') do set datetime=%%I
set CUR_YYYY=%datetime:~0,4%
set CUR_MM=%datetime:~4,2%
set CUR_DD=%datetime:~6,2%
set CUR_HH=%datetime:~8,2%
set CUR_NN=%datetime:~10,2%
set CUR_SS=%datetime:~12,2%

set BACKUP_FILE=%CUR_YYYY%-%CUR_MM%-%CUR_DD%_%CUR_HH%-%CUR_NN%-%CUR_SS%.custom.backup

echo 'Backup path: %BACKUP_FILE%'
echo 'Creating a backup ...'

set TARGET_VALUE=""
for /f "usebackq tokens=1,* delims==" %%A in (.env) do (
    if /i "%%A"=="PG_PASS" (
        set "TARGET_VALUE=%%B"
        goto :done
    )
)
echo %TARGET_VALUE%

:done
set PGPASSWORD=%TARGET_VALUE%
"C:\Program Files\PostgreSQL\16\bin\pg_dump.exe" --username="postgres" -d DP_Stock --format=custom -f "%BACKUP_FILE%"

echo 'Backup successfully created: %BACKUP_FILE%'

