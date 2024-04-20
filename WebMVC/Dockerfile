FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app
COPY ["WebMVC/WebMVC.csproj", "WebMVC/"]
RUN dotnet restore "WebMVC/WebMVC.csproj"
COPY . .
WORKDIR "/app/WebMVC/"
RUN dotnet build "WebMVC.csproj" -c Release -o /app/build
RUN dotnet publish "WebMVC.csproj" -c Release -o /app/build

FROM base As final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet","WebMVC.dll"]


