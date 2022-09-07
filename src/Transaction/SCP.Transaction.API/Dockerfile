#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Transaction/SCP.Transaction.API/SCP.Transaction.API.csproj", "src/Transaction/SCP.Transaction.API/"]
COPY ["src/Transaction/SCP.Transaction.Application/SCP.Transaction.Application.csproj", "src/Transaction/SCP.Transaction.Application/"]
COPY ["src/Transaction/SCP.Transaction.Infrastructure/SCP.Transaction.Infrastructure.csproj", "src/Transaction/SCP.Transaction.Infrastructure/"]
COPY ["src/Transaction/SCP.Transaction.Domain/SCP.Transaction.Domain.csproj", "src/Transaction/SCP.Transaction.Domain/"]
RUN dotnet restore "src/Transaction/SCP.Transaction.API/SCP.Transaction.API.csproj"
COPY . .
WORKDIR "/src/src/Transaction/SCP.Transaction.API"
RUN dotnet build "SCP.Transaction.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SCP.Transaction.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
LABEL org.opencontainers.image.source https://github.com/krdrag/simple-checkout-platform
ENTRYPOINT ["dotnet", "SCP.Transaction.API.dll"]
