#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Session/SCP.Session.API/SCP.Session.API.csproj", "src/Session/SCP.Session.API/"]
COPY ["src/Session/SCP.Session.Application/SCP.Session.Application.csproj", "src/Session/SCP.Session.Application/"]
COPY ["src/Common/SCP.MessageBus/SCP.MessageBus.csproj", "src/Common/SCP.MessageBus/"]
COPY ["src/Common/SCP.Common/SCP.Common.csproj", "src/Common/SCP.Common/"]
COPY ["src/Session/SCP.Session.Domain/SCP.Session.Domain.csproj", "src/Session/SCP.Session.Domain/"]
RUN dotnet restore "src/Session/SCP.Session.API/SCP.Session.API.csproj"
COPY . .
WORKDIR "/src/src/Session/SCP.Session.API"
RUN dotnet build "SCP.Session.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SCP.Session.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
LABEL org.opencontainers.image.source https://github.com/krdrag/simple-checkout-platform
ENTRYPOINT ["dotnet", "SCP.Session.API.dll"]