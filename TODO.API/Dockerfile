#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["TODO.API/TODO.API.csproj", "TODO.API/"]
COPY ["TODO.DataAccess/TODO.DataAccess.csproj", "TODO.DataAccess/"]
COPY ["TODO.Shared/TODO.Shared.csproj", "TODO.Shared/"]
COPY ["TODO.Business/TODO.Business.csproj", "TODO.Business/"]
RUN dotnet restore "TODO.API/TODO.API.csproj"
COPY . .
WORKDIR "/src/TODO.API"
RUN dotnet build "TODO.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TODO.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TODO.API.dll"]