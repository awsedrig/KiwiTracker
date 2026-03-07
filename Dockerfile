FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY *.sln .
COPY KiwiTracker.API/*.csproj ./KiwiTracker.API/
RUN dotnet restore

COPY . .
WORKDIR /app/KiwiTracker.API
RUN dotnet publish -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /out .

ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

ENTRYPOINT ["dotnet", "KiwiTracker.API.dll"]