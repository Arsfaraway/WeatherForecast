# Устанавливаем версию SDK .NET 8.0 для сборки проекта
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Копируем файлы проекта и восстанавливаем зависимости
WORKDIR /src
COPY ["WeatherReport/WeatherReport.csproj", "WeatherReport/"]
RUN dotnet restore "./WeatherReport/WeatherReport.csproj"
COPY . .

# Собираем проект
WORKDIR "/src/WeatherReport"
RUN dotnet build "./WeatherReport.csproj" -c Release -o /app/build

# Публикуем приложение
FROM build AS publish
RUN dotnet publish "./WeatherReport.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Создаем конечный образ
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WeatherReport.dll"]
