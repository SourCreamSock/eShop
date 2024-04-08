#FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
#WORKDIR /app
#EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY *.csproj .
RUN dotnet restore  ".Catalog.API/Catalog.API.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet ".Catalog.API/Catalog.API.csproj" build -c Release -o /app/build 
RUN dotnet ".Catalog.API/Catalog.API.csproj" publish  -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Catalog.API.dll"]