# TradingBot

Создать appsettings.json внутри проекта TradingBotSerivice с настройками ниже:
```json
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
```

Запустить TradingBotSerivce

В проекте TradingBot в файле app.config указать путь для размещения файла сделок 

```config
<add key="Path" value="Ваш путь"/>
```


Запустить TradingBot


