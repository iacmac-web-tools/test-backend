FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /src
COPY ["src/Theses.Api/Theses.Api.csproj", "src/Theses.Api/"]
COPY ["src/Theses.Application/Theses.Application.csproj", "src/Theses.Application/"]
COPY ["src/Theses.Domain/Theses.Domain.csproj", "src/Theses.Domain/"]
COPY ["src/Theses.Infrastructure/Theses.Infrastructure.csproj", "src/Theses.Infrastructure/"]
RUN dotnet restore "src/Theses.Api/Theses.Api.csproj"
COPY . .
WORKDIR "/src/src/Theses.Api"
RUN dotnet build "Theses.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Theses.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Theses.Api.dll"]
