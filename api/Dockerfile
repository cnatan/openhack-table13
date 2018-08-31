
FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 888

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY api.csproj api/
RUN dotnet restore api/api.csproj
WORKDIR /src/api
COPY . .
RUN dotnet build api.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish api.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "api.dll"]
