## API settings

+ Web.Api/appsetings.json - configuration DB
+ Web.Models/JWTconfig.cs - configuration JWT

## How to run API
```
dotnet restore
cd Web.Models
dotnet ef --startup-project ../Web.Api database update
cd ../
cd Web.Api
dotnet run
```
## How to run ClientApp
```
cd ClientApp
npm i
npm start
```
