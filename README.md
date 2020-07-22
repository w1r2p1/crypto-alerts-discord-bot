
# crypto-alerts-discord-bot

A Discord bot that allows users to create & manage cryptocurrency price alerts and be notified when an alert is set off. The bot also allows you to query prices and supports multiple exchanges.

#### Installing + running

To clone, build and run this project:
```
git clone https://github.com/toastercoder/crypto-alerts-discord-bot.git
cd crypto-alerts-discord-bot/CryptoDiscordBot/CryptoDiscordBot
dotnet build
dotnet run <args>
```

#### Required arguments/parameters
There are three parameters that the program needs to run. These can either be supplied as command line arguments when running the program, or if no command line arguments are passed, the program will ask for the user to input them.

When supplying them as command line arguments, simply enter the values of each in order with a space between each argument. The three paramaters, in order, are:

```
Discord bot token - The token generated for the Discord bot
Server ID - The ID of the server for the bot to interact with
Channel ID - The ID of the channel that the bot will send alert messages to
```
The `Discord bot token` can be generated from the [Discord Developer Portal](https://discord.com/developers/applications).

#### Discord commands
```
!add <exchange> <ticker> <price> <comment> - Adds an alert for the specified parameters. E.g. "!add bittrex btc-eth 0.5 yay"
!remove <id> - Removes the alert specified by its ID
!list - Lists all of the current untriggered alerts that have been added
!getprice exchange ticker - Gets the current price of the market specified
!help - Shows all available commands
```

### Supported exchanges
* Binance
* Bitfinex
* Bitmex
* Bittrex
* Kucoin
