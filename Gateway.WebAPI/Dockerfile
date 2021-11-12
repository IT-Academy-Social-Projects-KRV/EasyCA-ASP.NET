#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Gateway.WebAPI/Gateway.WebAPI.csproj", "Gateway.WebAPI/"]
RUN dotnet restore "Gateway.WebAPI/Gateway.WebAPI.csproj"
COPY . .
WORKDIR "/src/Gateway.WebAPI"
RUN dotnet build "Gateway.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Gateway.WebAPI.csproj" -c Release -o /app/publish
COPY [".docker-config/appsettings.json", "/app/publish/"]
COPY [".docker-config/ocelot.json", "/app/publish/"]

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gateway.WebAPI.dll"]