#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS publish
WORKDIR /src
COPY . .
RUN dotnet restore "Services/Data/SmartHome.Data.Api/SmartHome.Data.Api.csproj"
WORKDIR "/src/Services/Data/SmartHome.Data.Api"
RUN dotnet publish "SmartHome.Data.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SmartHome.Data.Api.dll"]