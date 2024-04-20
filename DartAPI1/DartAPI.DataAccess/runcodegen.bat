dotnet tool restore
dotnet tool run dbcodegen -c DbCodeGenConfig.yml
Rem dotnet tool run migrateef -e CRHEntities.edmx -o TestOutput