#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["SearchMicroservice.WebAPI/SearchMicroservice.WebAPI.csproj", "SearchMicroservice.WebAPI/"]
COPY ["SearchMicroservice.Domain/SearchMicroservice.Domain.csproj", "SearchMicroservice.Domain/"]
COPY ["RabbitMQConfig/RabbitMQConfig.csproj", "RabbitMQConfig/"]
RUN dotnet restore "SearchMicroservice.WebAPI/SearchMicroservice.WebAPI.csproj"
COPY . .
WORKDIR "/src/SearchMicroservice.WebAPI"
RUN dotnet build "SearchMicroservice.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SearchMicroservice.WebAPI.csproj" -c Release -o /app/publish
COPY [".docker-config/appsettings.json", "/app/publish/"]

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SearchMicroservice.WebAPI.dll"]