FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src
EXPOSE 80
EXPOSE 443

#COPY ["Portal.API.csproj", "Portal.API/"]
COPY ["Portal.API.csproj", "."]
#RUN dotnet restore "Portal.API/Portal.API.csproj"
RUN dotnet restore "./Portal.API.csproj"

COPY . .
#WORKDIR "/src/Portal.API"
WORKDIR "/src/."
#RUN dotnet build "Portal.API.csproj" -c Release -o /app
RUN dotnet build "Portal.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Portal.API.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Portal.API.dll"]