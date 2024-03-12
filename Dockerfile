# FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
# WORKDIR /app
# EXPOSE 8080
# EXPOSE 8081

# FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
# ARG BUILD_CONFIGURATION=Release
# WORKDIR /src

# COPY ["api/api.csproj", "api/"]

# RUN dotnet restore "api/api.csproj"
# COPY . .
# WORKDIR "/src/api"
# RUN dotnet build "api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# FROM build AS publish
# ARG BUILD_CONFIGURATION=Release
# RUN dotnet publish "api.csproj" -c $BUILD_CONFIGURATION -o /app/publish

# FROM base AS final
# WORKDIR /app
# COPY --from=publish /app/publish .
# ENTRYPOINT ["dotnet", "api.dll"]






# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app
EXPOSE 80
EXPOSE 443

COPY ./api/*.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish ./api/api.csproj -c Release -o out

# Final stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final-env
WORKDIR /app
COPY --from=build-env /app/out .
EXPOSE 8080
EXPOSE 8081

ENTRYPOINT [ "dotnet", "api.dll" ]


