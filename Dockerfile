FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["KiwiTracker.API.csproj", "./"]
RUN dotnet restore "KiwiTracker.API.csproj"
COPY . .
RUN dotnet publish "KiwiTracker.API.csproj" -c Release -o /app/publish
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_HTTP_PORTS=80
EXPOSE 80
ENTRYPOINT ["dotnet", "KiwiTracker.API.dll"]