dotnet tool install -g dotnet-roundhouse
exec { & rh /d=ContosoUniversityBlazor-Server /f=..\ContosoUniversity\ContosoUniversity\App_Data /s="(LocalDb)\mssqllocaldb" /silent }
exec { & rh /d=ContosoUniversityBlazor-Server-Test /f=..\ContosoUniversity\ContosoUniversity\App_Data /s="(LocalDb)\mssqllocaldb" /silent /drop }
exec { & rh /d=ContosoUniversityBlazor-Server-Test /f=..\ContosoUniversity\ContosoUniversity\App_Data /s="(LocalDb)\mssqllocaldb" /silent /simple }