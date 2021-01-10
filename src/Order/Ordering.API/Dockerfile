#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/Order/Ordering.API/Ordering.API.csproj", "src/Order/Ordering.API/"]
COPY ["src/Order/Ordering.Application/Ordering.Application.csproj", "src/Order/Ordering.Application/"]
COPY ["src/Order/Ordering.Core/Ordering.Core.csproj", "src/Order/Ordering.Core/"]
COPY ["src/Order/Ordring.Infrastructure/Ordering.Infrastructure.csproj", "src/Order/Ordring.Infrastructure/"]
COPY ["src/Common/EventBusRabbitMQ/EventBusRabbitMQ.csproj", "src/Common/EventBusRabbitMQ/"]
COPY ["src/Basket/Basket.API/Basket.API.csproj", "src/Basket/Basket.API/"]
RUN dotnet restore "src/Order/Ordering.API/Ordering.API.csproj"
COPY . .
WORKDIR "/src/src/Order/Ordering.API"
RUN dotnet build "Ordering.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ordering.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ordering.API.dll"]
