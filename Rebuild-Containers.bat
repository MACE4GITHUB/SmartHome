@echo off

call Stop-Containers.bat
call Remove-Images.bat

echo Rebuild Docker containers
docker-compose up --build

pause