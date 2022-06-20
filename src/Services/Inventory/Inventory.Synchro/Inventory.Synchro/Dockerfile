#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Inventory/Inventory.Synchro/Inventory.Synchro/Inventory.Synchro.csproj", "Services/Inventory/Inventory.Synchro/Inventory.Synchro/"]
COPY ["Services/Inventory/Inventory.Infrastructure/Inventory.Infrastructure.csproj", "Services/Inventory/Inventory.Infrastructure/"]
COPY ["Common/EventBus.Messages/EventBus.Messages.csproj", "Common/EventBus.Messages/"]
COPY ["Services/Inventory/Inventory.Application/Inventory.Application.csproj", "Services/Inventory/Inventory.Application/"]
COPY ["Common/Application/Application.csproj", "Common/Application/"]
COPY ["Services/Inventory/Inventory.Domain/Inventory.Domain.csproj", "Services/Inventory/Inventory.Domain/"]
RUN dotnet restore "Services/Inventory/Inventory.Synchro/Inventory.Synchro/Inventory.Synchro.csproj"
COPY . .
WORKDIR "/src/Services/Inventory/Inventory.Synchro/Inventory.Synchro"
RUN dotnet build "Inventory.Synchro.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Inventory.Synchro.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Inventory.Synchro.dll"]