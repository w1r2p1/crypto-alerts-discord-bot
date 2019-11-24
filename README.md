
# crypto-alerts-discord-bot

A Discord bot that allows users to create & manage cryptocurrency price alerts and be notified when an alert is set off.

#### Prerequisites

.NET Core 2.1 

#### Installing + running

1. Clone or download the repository 
2. Build the project using the .NET Core CLI or through Visual Studio and navigate to the output directory
3. To run the program, in a CLI, run the following command ```dotnet CryptoDiscordBot.dll <arguments>```

#### Required arguments/parameters
There are three parameters that the program needs to run. These can either be supplied as command line arguments when running the program, or if no command line arguments are passed, the program will ask for the user to input them.
The three paramaters, in order, are:
```
Discord bot token - The token generated for the Discord bot
Server ID - The ID of the server for the bot to interact with
Channel ID - The ID of the channel that the bot will send alert messages to
```
#### Discord commands
```
!add <exchange> <ticker> <price> <comment> - Adds an alert for the specified parameters. E.g. "!add bittrex btc-eth 0.5 yay"
!remove <id> - Removes the alert specified by its ID
!list - Lists all of the current untriggered alerts that have been added
!help - Shows all available commands
```
