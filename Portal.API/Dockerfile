FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore "./Portal.API.csproj"
#RUN dotnet restore "./koereskoleportalen/Portal.API/Portal.API.csproj"
# Build and publish a release
RUN dotnet publish "./Portal.API.csproj" -c Release -o out
#RUN dotnet publish "./koereskoleportalen/Portal.API/Portal.API.csproj" -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
COPY --from=build-env /App/out .
ENTRYPOINT ["dotnet", "Portal.API.dll"]
