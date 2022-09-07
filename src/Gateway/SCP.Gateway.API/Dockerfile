#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Gateway/SCP.Gateway.API/SCP.Gateway.API.csproj", "src/Gateway/SCP.Gateway.API/"]
RUN dotnet restore "src/Gateway/SCP.Gateway.API/SCP.Gateway.API.csproj"
COPY . .
WORKDIR "/src/src/Gateway/SCP.Gateway.API"
RUN dotnet build "SCP.Gateway.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SCP.Gateway.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
LABEL org.opencontainers.image.source https://github.com/krdrag/simple-checkout-platform
ENTRYPOINT ["dotnet", "SCP.Gateway.API.dll"]