FROM mcr.microsoft.com/dotnet/sdk:7.0 as build

WORKDIR /source

COPY . . 

RUN dotnet restore "./TODOApp.Api/TODOApp.Api.csproj" --disable-parallel
RUN dotnet publish "./TODOApp.Api/TODOApp.Api.csproj" -c release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/sdk:7.0
WORKDIR /app
COPY --from=build /app ./
EXPOSE 5000

ENTRYPOINT [ "dotnet", "TODOApp.Api.dll" ]