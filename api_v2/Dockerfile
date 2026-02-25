# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj files and restore dependencies
COPY ["MooMoo.Api/MooMoo.Api.csproj", "MooMoo.Api/"]
COPY ["MooMoo.Application/MooMoo.Application.csproj", "MooMoo.Application/"]
COPY ["MooMoo.Domain/MooMoo.Domain.csproj", "MooMoo.Domain/"]
COPY ["MooMoo.Infrastructure/MooMoo.Infrastructure.csproj", "MooMoo.Infrastructure/"]

RUN dotnet restore "MooMoo.Api/MooMoo.Api.csproj"

# Copy everything and build
COPY . .
WORKDIR "/src/MooMoo.Api"
RUN dotnet build "MooMoo.Api.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "MooMoo.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MooMoo.Api.dll"]
