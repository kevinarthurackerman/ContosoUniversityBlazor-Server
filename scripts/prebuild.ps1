dotnet tool install -g dotnet-roundhouse
rh /d=ContosoUniversityBlazor-Server /f=..\ContosoUniversity\ContosoUniversity\App_Data /s="(LocalDb)\mssqllocaldb" /silent
rh /d=ContosoUniversityBlazor-Server-Test /f=..\ContosoUniversity\ContosoUniversity\App_Data /s="(LocalDb)\mssqllocaldb" /silent /drop
rh /d=ContosoUniversityBlazor-Server-Test /f=..\ContosoUniversity\ContosoUniversity\App_Data /s="(LocalDb)\mssqllocaldb" /silent /simple