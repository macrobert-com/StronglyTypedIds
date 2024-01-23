set NUGET_PACKAGE_SOURCE=local

dotnet build src\MacRobert.StronglyTypedIds --configuration Release
dotnet build src\MacRobert.AspNetCore.StronglyTypedIds --configuration Release
dotnet build src\MacRobert.EntityFrameworkCore.StronglyTypedIds --configuration Release
dotnet build src\MacRobert.EntityFrameworkCore.StronglyTypedIds.Ulid --configuration Release
pause
