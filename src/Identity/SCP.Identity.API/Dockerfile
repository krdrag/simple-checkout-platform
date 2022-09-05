#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Identity/SCP.Identity.API/SCP.Identity.API.csproj", "src/Identity/SCP.Identity.API/"]
COPY ["src/Common/SCP.Common/SCP.Common.csproj", "src/Common/SCP.Common/"]
RUN dotnet restore "src/Identity/SCP.Identity.API/SCP.Identity.API.csproj"
COPY . .
WORKDIR "/src/src/Identity/SCP.Identity.API"
RUN dotnet build "SCP.Identity.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SCP.Identity.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SCP.Identity.API.dll"]