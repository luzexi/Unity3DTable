IF NOT EXIST tables/output GOTO :MAKEOUTPUTDIR
cd tables/output
del *.bytes
del *.cs
cd ..
cd ..
GOTO EXPORTSTUFF

:MAKEOUTPUTDIR
cd tables
md output
cd ..

:EXPORTSTUFF
cd tables
call exportData.bat
call exportText.bat
cd ..

:: copy .\tables\output\*.cs .\project\Assets\scripts\dataTable\ /y
:: copy .\tables\output\*.bytes .\project\Assets\Resources\dat\ /y

:: .\tools\ClientDataTableHashExporter.exe .\project\Assets\Resources\dat\
.\tools\ClientDataTableHashExporter.exe .\tables\output\

pause