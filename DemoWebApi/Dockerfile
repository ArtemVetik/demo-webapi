#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base

USER root
WORKDIR /app

EXPOSE 8080
EXPOSE 8081

COPY . .

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["DemoWebApi/DemoWebApi.csproj", "DemoWebApi/"]
RUN dotnet restore "./DemoWebApi/./DemoWebApi.csproj"
COPY . .
WORKDIR "/src/DemoWebApi"
RUN dotnet build "./DemoWebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./DemoWebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

RUN dotnet new tool-manifest
RUN dotnet tool install dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

ENTRYPOINT ["dotnet", "DemoWebApi.dll"]