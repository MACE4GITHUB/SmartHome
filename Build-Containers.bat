@echo off

call Stop-Containers.bat
echo Build Docker containers
docker-compose up --build
pause