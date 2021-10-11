FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["TestePerformanceRegex/TestePerformanceRegex.csproj", "TestePerformanceRegex/"]
RUN dotnet restore "TestePerformanceRegex/TestePerformanceRegex.csproj"
COPY . .
WORKDIR "/src/TestePerformanceRegex"
RUN dotnet build "TestePerformanceRegex.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TestePerformanceRegex.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestePerformanceRegex.dll"]
