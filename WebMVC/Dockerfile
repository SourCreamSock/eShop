FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS build
WORKDIR /src
COPY *.csproj .
RUN dotnet restore ".WebMVC/WebMVC.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build  ".WebMVC/WebMVC.csproj" -c Release -o /app/build
RUN dotnet publish ".WebMVC/WebMVC.csproj" -c Release -o /app/build

From mcr.microsoft.com/dotnet/aspnet:7.0 As final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet","WebMVC.dll"]


