#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MediatrExample.API/MediatrExample.API.csproj", "MediatrExample.API/"]
COPY ["MediatrExample.CQRS/MediatrExample.CQRS.csproj", "MediatrExample.CQRS/"]
COPY ["MediatrExample.Service/MediatrExample.Service.csproj", "MediatrExample.Service/"]
COPY ["MediatrExample.Data/MediatrExample.Data.csproj", "MediatrExample.Data/"]
COPY ["MediatrExample.Core/MediatrExample.Core.csproj", "MediatrExample.Core/"]
COPY ["MediatrExample.Shared/MediatrExample.Shared.csproj", "MediatrExample.Shared/"]
RUN dotnet restore "MediatrExample.API/MediatrExample.API.csproj"
COPY . .
WORKDIR "/src/MediatrExample.API"
RUN dotnet build "MediatrExample.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MediatrExample.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MediatrExample.API.dll"]