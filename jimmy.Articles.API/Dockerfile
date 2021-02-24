FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src

# It's important to keep lines from here down to "COPY . ." identical in all Dockerfiles
# to take advantage of Docker's build cache, to speed up local container builds
COPY "jimmy.Articles.API.sln" "jimmy.Articles.API.sln"
COPY "jimmy.Articles.API/jimmy.Articles.API.csproj" "jimmy.Articles.API/jimmy.Articles.API.csproj"
COPY "jimmy.Articles.API.Tests/jimmy.Articles.API.Tests.csproj" "jimmy.Articles.API.Tests/jimmy.Articles.API.Tests.csproj"

COPY "docker-compose.dcproj" "docker-compose.dcproj"
COPY "NuGet.config" "NuGet.config"

# check network connection 
RUN ping -c 4 google.com

# https://stackoverflow.com/questions/59752005/docker-nuget-connection-timeout
RUN dotnet restore --disable-parallel "jimmy.Articles.API.sln"

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
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "jimmy.Articles.API.dll"]