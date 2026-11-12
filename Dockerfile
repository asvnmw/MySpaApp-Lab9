# ЭТАП 1: Сборка приложения в SDK
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env
WORKDIR /app

# Копируем csproj и восстанавливаем зависимости
COPY *.csproj ./
RUN dotnet restore

# Копируем оставшиеся файлы и публикуем
COPY . ./
RUN dotnet publish -c Release -o out

# ЭТАП 2: Запуск приложения в легковесном Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build-env /app/out .

EXPOSE 8080

ENTRYPOINT ["dotnet", "MySpaApp.dll"]