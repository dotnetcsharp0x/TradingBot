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

В проекте TradingBotService в файле app.config указать путь для размещения файла сделок 

```config
<add key="Path" value="Ваш путь"/>
```

Запустить TradingBotSerivce

В проекте TradingBot в файле app.config указать путь для размещения файла сделок 

```config
<add key="Path" value="Ваш путь"/>
```


Запустить TradingBot


Также в проекте имеется .bat файл, для удобства можно запускать проект им
```json
*/TradingBot/bot.bat
```
