FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /

# It's important to keep lines from here down to "COPY . ." identical in all Dockerfiles
# to take advantage of Docker's build cache, to speed up local container builds
COPY "jimmy.Articles.API.sln" "jimmy.Articles.API.sln"
COPY "jimmy.Articles.API/jimmy.Articles.API.csproj" "jimmy.Articles.API/jimmy.Articles.API.csproj"
COPY "jimmy.Articles.API.Tests/jimmy.Articles.API.Tests.csproj" "jimmy.Articles.API.Tests/jimmy.Articles.API.Tests.csproj"

COPY "docker-compose.dcproj" "docker-compose.dcproj"
COPY "NuGet.config" "NuGet.config"

RUN dotnet restore "jimmy.Articles.API.sln"

COPY . .
WORKDIR /jimmy.Articles.API
RUN dotnet publish --no-restore -c Release -o /app

FROM build as test
WORKDIR /jimmy.Articles.API.Tests

#FROM build as unittest
#WORKDIR /jimmy.Articles.API.UnitTests

#FROM build as functionaltest
#WORKDIR /jimmy.Articles.API.Tests.FunctionalTests

FROM build AS publish

FROM base AS final

# MSSQL connection healthcheck script
COPY wait-for-mssql.sh /wait-for-mssql.sh
RUN chmod +x /wait-for-mssql.sh

WORKDIR /app    
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "jimmy.Articles.API.dll"]