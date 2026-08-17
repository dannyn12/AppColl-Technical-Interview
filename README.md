**Author:** Danny Nguyen

This applications imports the data found in "https://data.cityofnewyork.us/resource/qz5f-yx82.csv" and displays a "table of the raw data", the total number of zip codes, and the zip codes where the percentage of homes with no internet access is below 10%. It also allows the user to import data into XML, JSON, and "raw" (CSV) format. 

## Tech Stack
* **Language:** C#
* **Framework:** .NET Core / ASP.NET Core
* **Web UI:** Blazor Server

## How to build/deploy/run (in VS Code):
1. Open the project folder
2. Navigate to the application directory in terminal
3. Restore project's dependencies (dotnet restore)
4. Build application (dotnet build)
5. Run application (dotnet run)
6. Open the URL shown in terminal

## Testing:
The application was manually tested using this process:
1. Started the application using the Load Data button.
2. Verified the data was successfully imported.
3. Confirmed the raw data was displayed in a table.
4. Verified the total number of unique ZIP codes.
5. Verified that ZIP codes with less than 10% of homes without internet access were correctly filtered.
6. Tested JSON export and verified the exported file contained valid JSON.
7. Tested XML export and verified the exported file contained valid XML.
8. Tested raw/CSV export and verified the exported file contained the imported records.
9. Tested the Start Over functionality.

Screenshots/output files of testing is located in the Testing folder.
