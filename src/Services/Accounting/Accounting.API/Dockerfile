#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Accounting/Accounting.API/Accounting.API.csproj", "Services/Accounting/Accounting.API/"]
COPY ["Common/EventBus.Messages/EventBus.Messages.csproj", "Common/EventBus.Messages/"]
RUN dotnet restore "Services/Accounting/Accounting.API/Accounting.API.csproj"
COPY . .
WORKDIR "/src/Services/Accounting/Accounting.API"
RUN dotnet build "Accounting.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Accounting.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Accounting.API.dll"]