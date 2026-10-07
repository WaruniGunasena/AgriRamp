# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project from nested path and restore dependencies
COPY ["AgriState_MiniTracker/AgriState_MiniTracker/AgriState_MiniTracker.csproj", "AgriState_MiniTracker/AgriState_MiniTracker/"]
RUN dotnet restore "AgriState_MiniTracker/AgriState_MiniTracker/AgriState_MiniTracker.csproj"

# Copy remaining source code
COPY . .
WORKDIR "/src/AgriState_MiniTracker/AgriState_MiniTracker"
RUN dotnet build "AgriState_MiniTracker.csproj" -c Release -o /app/build

# Publish Stage
FROM build AS publish
RUN dotnet publish "AgriState_MiniTracker.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AgriState_MiniTracker.dll"]