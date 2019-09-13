@Echo Off
cd %systemroot%\system32



::************************************************************************************************
::
::             WE MUST BE ADMIN FOR THIS TO WORK AT ALL.
::
::************************************************************************************************
:IsAdmin
Reg.exe query "HKU\S-1-5-19\Environment"
If Not %ERRORLEVEL% EQU 0 (
 Cls & Echo You must have administrator rights to continue.
 Pause & Exit
)

::This is included for local users who accidentally run it locally
:: and don't want to be bothered by error messages.
Reg.exe DELETE "HKCR\*\shell\~Copy to Clipboard - Filename" /f
Reg.exe DELETE "HKCR\*\shell\~Copy to Clipboard - Path" /f
Reg.exe DELETE "HKCR\*\shell\~Copy to Clipboard - Path And Filename" /f
Reg.exe DELETE "HKCR\Folder\shell\~Copy to Clipboard - Folder Name" /f
Reg.exe DELETE "HKCR\Folder\shell\~Copy to Clipboard - Folder Path" /f

cls





::************************************************************************************************
::
::             COPY THE FILE TO A SPECIFIED LOCATION
::
::************************************************************************************************
:COPY
echo To install, we will copy a file from here to there:
echo   %~d0%~p0Copy Full File Name.exe
echo   %ProgramFiles%\Copy Full File Name\Copy Full File Name.exe
if not exist "%ProgramFiles%\Copy Full File Name\" md "%ProgramFiles%\Copy Full File Name\"
copy "%~d0%~p0Copy Full File Name.exe" "%ProgramFiles%\Copy Full File Name\Copy Full File Name.exe"
echo.





::************************************************************************************************
::
::             CHANGE AROUND OUR REGISTRY, PLEASE.
::
::************************************************************************************************
:REG

echo Lastly, we need to set a bunch of registry keys.
Reg.exe add "HKCR\*\shell\Copy to Clipboard - Filename" /ve /t REG_SZ /d "Copy to Clipboard - Filename" /f
Reg.exe add "HKCR\*\shell\Copy to Clipboard - Filename\command" /ve /t REG_SZ /d "\"%ProgramFiles%\Copy Full File Name\Copy Full File Name.exe\" 1 %%L" /f
Reg.exe add "HKCR\*\shell\Copy to Clipboard - Path" /ve /t REG_SZ /d "Copy to Clipboard - Path" /f
Reg.exe add "HKCR\*\shell\Copy to Clipboard - Path\command" /ve /t REG_SZ /d "\"%ProgramFiles%\Copy Full File Name\Copy Full File Name.exe\" 2 %%L" /f
Reg.exe add "HKCR\*\shell\Copy to Clipboard - Path And Filename" /ve /t REG_SZ /d "Copy to Clipboard - Path And Filename" /f
Reg.exe add "HKCR\*\shell\Copy to Clipboard - Path And Filename\command" /ve /t REG_SZ /d "\"%ProgramFiles%\Copy Full File Name\Copy Full File Name.exe\" 3 %%L" /f
Reg.exe add "HKCR\Folder\shell\Copy to Clipboard - Folder Name" /ve /t REG_SZ /d "Copy to Clipboard - Folder Name" /f
Reg.exe add "HKCR\Folder\shell\Copy to Clipboard - Folder Name\command" /ve /t REG_SZ /d "\"%ProgramFiles%\Copy Full File Name\Copy Full File Name.exe\" 1 %%L" /f
Reg.exe add "HKCR\Folder\shell\Copy to Clipboard - Folder Path" /ve /t REG_SZ /d "Copy to Clipboard - Folder Path" /f
Reg.exe add "HKCR\Folder\shell\Copy to Clipboard - Folder Path\command" /ve /t REG_SZ /d "\"%ProgramFiles%\Copy Full File Name\Copy Full File Name.exe\" 3 %%L" /f





::************************************************************************************************
::
::             DONE!
::
::************************************************************************************************

echo.
echo Done!  You should now be able to right click
echo  and copy the names and paths of any file.
echo.
echo Cheers!
echo --Scott Myers
echo.
pause

