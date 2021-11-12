#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["FileMicroservice.WebAPI/FileMicroservice.WebAPI.csproj", "FileMicroservice.WebAPI/"]
COPY ["FileMicroservice.Data/FileMicroservice.Data.csproj", "FileMicroservice.Data/"]
COPY ["FileMicroservice.Domain/FileMicroservice.Domain.csproj", "FileMicroservice.Domain/"]
COPY [".docker-config/appsettings.json", "FileMicroservice.WebApi/appsettings.json"]
RUN dotnet restore "FileMicroservice.WebAPI/FileMicroservice.WebAPI.csproj"
COPY . .
WORKDIR "/src/FileMicroservice.WebAPI"
RUN dotnet build "FileMicroservice.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FileMicroservice.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FileMicroservice.WebAPI.dll"]