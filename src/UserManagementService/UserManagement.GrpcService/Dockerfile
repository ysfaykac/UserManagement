#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/UserManagementService/UserManagement.GrpcService/UserManagement.GrpcService.csproj", "src/UserManagementService/UserManagement.GrpcService/"]
COPY ["src/BuildingBlocks/EventBus/EventBus.Factory/EventBus.Factory.csproj", "src/BuildingBlocks/EventBus/EventBus.Factory/"]
COPY ["src/BuildingBlocks/EventBus/EventBus.Base/EventBus.Base.csproj", "src/BuildingBlocks/EventBus/EventBus.Base/"]
COPY ["src/BuildingBlocks/EventBus/EventBus.RabbitMQ/EventBus.RabbitMQ.csproj", "src/BuildingBlocks/EventBus/EventBus.RabbitMQ/"]
COPY ["src/UserManagementService/UserManagement.Infrastructure/UserManagement.Infrastructure.csproj", "src/UserManagementService/UserManagement.Infrastructure/"]
COPY ["src/UserManagementService/UserManagement.Domain/UserManagement.Domain.csproj", "src/UserManagementService/UserManagement.Domain/"]
COPY ["src/UserManagementService/UserManagement.Application/UserManagement.Application.csproj", "src/UserManagementService/UserManagement.Application/"]
RUN dotnet restore "src/UserManagementService/UserManagement.GrpcService/UserManagement.GrpcService.csproj"
COPY . .
WORKDIR "/src/src/UserManagementService/UserManagement.GrpcService"
RUN dotnet build "UserManagement.GrpcService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UserManagement.GrpcService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserManagement.GrpcService.dll"]