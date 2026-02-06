FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["KiwiTracker.API/KiwiTracker.API.csproj", "KiwiTracker.API/"]
RUN dotnet restore "KiwiTracker.API/KiwiTracker.API.csproj"
COPY . .
WORKDIR "/src/KiwiTracker.API"
RUN dotnet build "KiwiTracker.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "KiwiTracker.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "KiwiTracker.API.dll"]