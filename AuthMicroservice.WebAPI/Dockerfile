#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["AuthMicroservice.WebAPI/AuthMicroservice.WebAPI.csproj", "AuthMicroservice.WebAPI/"]
COPY ["AuthMicroservice.Domain/AuthMicroservice.Domain.csproj", "AuthMicroservice.Domain/"]
COPY ["AuthMicroservice.Data/AuthMicroservice.Data.csproj", "AuthMicroservice.Data/"]
RUN dotnet restore "AuthMicroservice.WebAPI/AuthMicroservice.WebAPI.csproj"
COPY . .
WORKDIR "/src/AuthMicroservice.WebAPI"
RUN dotnet build "AuthMicroservice.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AuthMicroservice.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AuthMicroservice.WebAPI.dll"]