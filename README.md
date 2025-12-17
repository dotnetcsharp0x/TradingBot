# TradingBot

Создать appsettings.json внутри проекта TradingBotSerivice с настройками ниже:

{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "Urls": "https://localhost:5001",
  "AccessToken": "ваш токен"

}


Запустить TradingBotSerivce
Запустить TradingBot


В проекте TradingBot в файле app.config указать путь для размещения файла сделок <add key="Path" value="Ваш путь"/>
