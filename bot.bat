@echo off
echo Starting TradingBotService...
cd /d "C:\Users\dmitry\source\repos\TradingBot\TradingBotService"
start dotnet run

echo Waiting for Service to initialize...
timeout /t 5

echo Starting TradingBot...
cd /d "C:\Users\dmitry\source\repos\TradingBot\TradingBot"
start dotnet run

echo Both systems are launching!
pause     