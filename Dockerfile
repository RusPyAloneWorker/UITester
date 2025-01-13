# Используем .NET SDK, поддерживающий .NET 8.0
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Устанавливаем Node.js и Playwright CLI
RUN apt-get update && \
    apt-get install -y curl && \
    curl -fsSL https://deb.nodesource.com/setup_18.x | bash - && \
    apt-get install -y nodejs && \
    npm install -g playwright && \
    npx playwright install-deps && \
    npx playwright install

# Указываем рабочую директорию
WORKDIR /app

# Копируем файлы проектов для восстановления зависимостей
COPY TestProjectWithUITest/TestProjectWithUITest.csproj TestProjectWithUITest/
COPY UITester/UITester.csproj UITester/

# Восстанавливаем зависимости
RUN dotnet restore TestProjectWithUITest/TestProjectWithUITest.csproj

# Копируем оставшиеся файлы проекта
COPY TestProjectWithUITest/ TestProjectWithUITest/
COPY UITester/ UITester/

# Сборка тестового проекта
RUN dotnet build TestProjectWithUITest/TestProjectWithUITest.csproj --configuration Release

# Создаем директорию TestRecords в каталоге решения
RUN mkdir /app/TestRecords

# Запуск тестов
CMD ["dotnet", "test", "TestProjectWithUITest/TestProjectWithUITest.csproj", "--configuration", "Release", "--logger:trx"]
