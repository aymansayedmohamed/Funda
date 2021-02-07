#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["src/Funda/Funda.csproj", "src/Funda/"]
RUN dotnet restore "src/Funda/Funda.csproj"
COPY . .
WORKDIR "/src/src/Funda"
RUN dotnet build "Funda.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Funda.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Funda.dll"]
