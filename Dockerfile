#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SWP.FUGoodsExchangeManagement.API/SWP.FUGoodsExchangeManagement.API.csproj", "SWP.FUGoodsExchangeManagement.API/"]
COPY ["SWP.FUGoodsExchangeManagement.Business/SWP.FUGoodsExchangeManagement.Business.csproj", "SWP.FUGoodsExchangeManagement.Business/"]
COPY ["SWP.FUGoodsExchangeManagement.Repository/SWP.FUGoodsExchangeManagement.Repository.csproj", "SWP.FUGoodsExchangeManagement.Repository/"]
RUN dotnet restore "./SWP.FUGoodsExchangeManagement.API/./SWP.FUGoodsExchangeManagement.API.csproj"
COPY . .
WORKDIR "/src/SWP.FUGoodsExchangeManagement.API"
RUN dotnet build "./SWP.FUGoodsExchangeManagement.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SWP.FUGoodsExchangeManagement.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SWP.FUGoodsExchangeManagement.API.dll"]