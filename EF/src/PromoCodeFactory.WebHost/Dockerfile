# Используем базовый образ Ubuntu версии 22.04.
FROM ubuntu:22.04

# Устанавливаем рабочую директорию внутри контейнера. 
# Все команды и файлы далее будут относиться к этой директории.
WORKDIR /app

# Устанавливаем переменную окружения для ASP.NET Core. 
# Она указывает приложению слушать HTTP-запросы на всех интерфейсах на порту 5002.
ENV ASPNETCORE_URLS http://*:5002

# Обновляем список пакетов и устанавливаем wget для загрузки файлов.
RUN apt update; \
    apt install wget -y;

# Загружаем конфигурацию репозитория Microsoft и устанавливаем её.
# Это добавляет поддержку пакетов Microsoft, таких как .NET Runtime.
RUN wget https://packages.microsoft.com/config/ubuntu/22.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb; \
    dpkg -i packages-microsoft-prod.deb

# Обновляем список пакетов снова, добавляем поддержку HTTPS для apt,
# и устанавливаем ASP.NET Core Runtime версии 8.0, необходимый для выполнения приложения.
RUN apt-get update; \
    apt-get install -y apt-transport-https && \
    apt-get update && \
    apt-get install -y aspnetcore-runtime-8.0

# Копируем опубликованное приложение из локальной директории в контейнер.
# Предполагается, что приложение уже собрано и опубликовано через dotnet publish.
COPY ../PromoCodeFactory.WebHost/bin/Release/net8.0/publish ./

# Указываем команду по умолчанию, которая будет запускаться при старте контейнера.
# dotnet PromoCodeFactory.WebHost.dll запускает приложение ASP.NET Core.
CMD dotnet PromoCodeFactory.WebHost.dll