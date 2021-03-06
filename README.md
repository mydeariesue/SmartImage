# SmartImage

*Find the source image in one click!*

SmartImage is a reverse image search tool for Windows with context menu integration. SmartImage will open the best match found returned from various image search engines (see the supported sites) right in your web browser. This behavior can be configured to the user's preferences.

Supported sites:

- [SauceNao](https://saucenao.com/) (`SauceNao`)
- ImgOps (`ImgOps`)
- Google Images (`GoogleImages`)
- TinEye (`TinEye`)
- IQDB (`Iqdb`)
- trace.moe (`TraceMoe`)
- KarmaDecay (`KarmaDecay`)
- Yandex (`Yandex`)
- Bing (`Bing`)

Search engine names and configuration:

- `SauceNao`
- `ImgOps`
- `GoogleImages`
- `TinEye`
- `Iqdb`
- `TraceMoe`
- `KarmaDecay`
- `Yandex`
- `Bing`
- `All`
- `None`

# Download

**[See the latest releases](https://github.com/Decimation/SmartImage/releases)**

# Example

![Demo](https://github.com/Decimation/SmartImage/raw/master/Demo.gif)

![Context menu image](https://github.com/Decimation/SmartImage/blob/master/Context%20menu%20integration.png)


# Usage

Command line syntax:

`smartimage <command> [options...]`

- Angle brackets (`<>`) specify required arguments.

- Square brackets (`[]`) specify optional arguments. 

- Ellipses (`...`) specify one or more arguments.

**Behavior note**: *Any options not specified via the command line are automatically read from the configuration file.*

# Options

`--engines <engines>`

Sets the search engines to use when searching, delimited by commas. See the above list for possible arguments. 
*Default: `All`*

`--priority-engines <engines>`

Sets the priority search engines, delimited by commands. See the above list for possible arguments. Priority search engines are engines whose results will be automatically opened in your browser when searching is complete. For example, if you designate `SauceNao` as a priority engine, then results returned by
`SauceNao` will be automatically opened in your browser. *Default: `SauceNao`*

`--saucenao-auth <api key>`

Configures the SauceNao API key. Register an application [here](https://saucenao.com/user.php), then get your key [here](https://saucenao.com/user.php?page=search-api). If this is configured, SmartImage will be able to return more specific results. SmartImage will be able to function as seen in the demo (opening the direct source image in your browser). You can also use the
`create-sn [auto]` verb to create an account automatically. 

`--imgur-auth <consumer id>`

Configures Imgur API keys. Register an application [here](https://api.imgur.com/oauth2/addclient), then get your ID [here](https://imgur.com/account/settings/apps). If this is configured, SmartImage will use Imgur to upload temporary images instead of ImgOps.

`--auto-exit`

Automatically exits the program once searching is complete.

`--update-cfg`

Updates the configuration file with the supplied command line arguments.



# Commands

`search <image path> [options...]`

This is the default functionality. Explicitly specifying this is not needed.

`ctx-menu <add/remove>`

Adds or removes context menu integration.

`path <add/remove>`

Adds or removes executable path to path environment variable.

`reset [all]`

Removes integrations. Specify `all` to additionally reset configuration.

`create-sn [true/false]`

Register a SauceNao account (for API keys). Specify `true` to autofill registration fields.

`info`

Displays information about the program and its configuration.

`help`

Display available commands.

`version`

Display program version.

# Usage examples

`smartimage --engines All --priority-engines None "image.jpg"`

Runs the program using all search engines and no results will be in the browser.

`smartimage --engines SauceNao,ImgOps,GoogleImages --priority-engines SauceNao "image.jpg"`

Runs the program using SauceNao, ImgOps, and Google Images. The best result from SauceNao will be opened in the browser.

`smartimage --engines SauceNao,ImgOps,KarmaDecay --priority-engines SauceNao --update-cfg "image.jpg"`

Runs the program using SauceNao, ImgOps, and Karma Decay. The best result from SauceNao will be opened in the browser.
The specified options will be saved to the configuration file.

`smartimage create-sn true`

Automatically creates a SauceNao account for usage. Account information is stored in a text file in the current directory and the
API key is saved to the configuration file.

`smartimage reset all`

Fully resets configuration and removes all integrations.

# Notes

- Ensure that the executable is placed in the system PATH (*`%PATH%`*) environment variable, otherwise the context menu integration will not work. You can read about how to do this [here](https://superuser.com/questions/949560/how-do-i-set-system-environment-variables-in-windows-10). You can also use the `--add-to-path` command to add the current directory to the path.

- SmartImage uploads temporary images using ImgOps (the uploaded images are automatically deleted after 2 hours). Imgur can also be used, but you must register an Imgur application client.

- SmartImage is a single-file, trimmed .NET executable. This means that dynamic libraries must be extracted to a temporary directory
(usually `%temp%\.net\SmartImage`).

- Some functions use hacky solutions (like dynamically creating a registry key file to install context menu integration). This is temporary until I can find better approaches, but it should work in the meantime.

# to-do

- Add an icon

- Rewrite & refactor codebase; find better, less hacky approaches to various functions

# Inspiration

- [SauceNao-Windows](https://github.com/RoxasShadow/SauceNao-Windows)
- [SharpNao](https://github.com/Lazrius/SharpNao)
