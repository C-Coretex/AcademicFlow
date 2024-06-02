# AcademicFlow
Academic management system, created as a university project in a team.

Features:
- Authentication/authorization from scratch
- Role system with many roles to one user relation
- Admin/Professor/Student roles
- Course and assignment system
- Assignment submitting and grading
- Management of all the data and relations

----

### How to run the application:
- Install the latest version of Visual Studio
- Install node.js (https://nodejs.org/en/download/package-manager)
- Install ef core tools (https://learn.microsoft.com/en-us/ef/core/cli/dotnet)
- Install SSMS (optionally https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16)
- Download this repository, open the AcademicFlow.sln file with VisualStudio
- Open AcademicFlow/AcademicFlow.Migrations/ folder with cmd (could also be done directly in VisualStudio)
- Run this command in cmd and close the window after success: dotnet ef database update
- (optional) In case you can not to connect to your database, check ...\AcademicFlow\AcademicFlow\appsettings.json file. Look for "ConnectionStrings" -> "AcademicFlowConnectionString" string. 
Change value to "Server=localhost;Database=AcademicFLow;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True" record. Or change "Server" value in case your database connection is different.
Check the same string in ...\AcademicFlow\AcademicFlow.Migrations\appsettings.json.
- If everything is fine - press green arrow button on the top of the screen (in the middle of the toolbar) ![image](https://github.com/C-Coretex/AcademicFlow/assets/145047860/d7009ef3-d010-4a82-9923-8c9c5db5e479)



The application shoud run successfully.

On login enter:
- Username: admin
- For Password enter: BadPassword01

### Sistēmas dokumentācija ir pieejama šeit: 

### Ja jums rodas jautājumi vai neskaidrības, lūdzu rakstiet uz af21043@edu.lu.lv.


----

![image](https://github.com/C-Coretex/AcademicFlow/assets/44605873/5a918857-c054-4487-9ba6-2d3b86b2bc37)
