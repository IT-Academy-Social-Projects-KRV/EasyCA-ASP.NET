#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["CrudMicroservice.WebApi/CrudMicroservice.WebApi.csproj", "CrudMicroservice.WebApi/"]
COPY ["RabbitMQConfig/RabbitMQConfig.csproj", "RabbitMQConfig/"]
COPY ["CrudMicroservice.Domain/CrudMicroservice.Domain.csproj", "CrudMicroservice.Domain/"]
COPY ["CrudMicroservice.Data/CrudMicroservice.Data.csproj", "CrudMicroservice.Data/"]
RUN dotnet restore "CrudMicroservice.WebApi/CrudMicroservice.WebApi.csproj"
COPY . .
WORKDIR "/src/CrudMicroservice.WebApi"
RUN dotnet build "CrudMicroservice.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CrudMicroservice.WebApi.csproj" -c Release -o /app/publish
COPY [".docker-config/appsettings.json", "/app/publish/"]


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CrudMicroservice.WebApi.dll"]