@echo off
echo Cleaning, a moment please...

attrib *.suo -s -r -h
del /f /s /q *.suo
del /f /s /q *.user
del /f /s /q *.ncb

rd /s /q obj

del /f /s /q bin\Debug\*.config
del /f /s /q bin\Debug\*.pdb
del /f /s /q bin\Debug\*.vshost.*
del /f /s /q bin\Debug\*.plist
del /f /s /q bin\Release\*.config
del /f /s /q bin\Release\*.pdb
del /f /s /q bin\Release\*.vshost.*
del /f /s /q bin\Release\*.plist
del /f /s /q bin\ReleaseDemo\*.config
del /f /s /q bin\ReleaseDemo\*.pdb
del /f /s /q bin\ReleaseDemo\*.vshost.*
del /f /s /q bin\ReleaseDemo\*.plist

echo Cleaning done!
