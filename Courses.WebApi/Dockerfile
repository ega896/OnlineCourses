FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app

EXPOSE 5000/tcp
ENV ASPNETCORE_URLS http://*:5000
ENV ASPNETCORE_ENVIRONMENT docker

FROM microsoft/dotnet:2.2-sdk AS build
 
# Restore dotnet before build to allow for caching
WORKDIR /
COPY Courses.Application/Courses.Application.csproj /src/Courses.Application/
COPY Courses.Domain/Courses.Domain.csproj /src/Courses.Domain/
COPY Courses.Infrastructure/Courses.Infrastructure.csproj /src/Courses.Infrastructure/
COPY Courses.Persistence/Courses.Persistence.csproj /src/Courses.Persistence/
COPY Courses.WebApi/Courses.WebApi.csproj /src/Courses.WebApi/

RUN dotnet restore /src/Courses.WebApi/Courses.WebApi.csproj

# Copy source files and build
COPY . /src

RUN dotnet publish /src/Courses.WebApi/Courses.WebApi.csproj --no-restore -c Release -o /app

# Copy compiled app to runtime container
FROM base AS final
COPY --from=build /app .

CMD ["dotnet", "Courses.WebApi.dll"]