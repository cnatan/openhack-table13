FROM microsoft/dotnet:sdk AS build-env
WORKDIR .

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM microsoft/dotnet:aspnetcore-runtime
WORKDIR .
COPY --from=build-env ./out .
ENTRYPOINT ["dotnet", "aspnetapp.dll"]
