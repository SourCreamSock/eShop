FROM mcr.microsoft.com/dotnet/aspnet:8.0  AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY ["Catalog.API/Catalog.API.csproj", "Catalog.API/"]
RUN dotnet restore "Catalog.API/Catalog.API.csproj"
COPY . .
WORKDIR "/app/Catalog.API/"
RUN dotnet build "Catalog.API.csproj"  -c Release -o /app/build 
RUN dotnet publish "Catalog.API.csproj"   -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "watch","Catalog.API.dll"]
