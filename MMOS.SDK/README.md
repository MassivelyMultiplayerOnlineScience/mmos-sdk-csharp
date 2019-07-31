
[Site](http://mmos.ch/) |
[Docs](https://github.com/MassivelyMultiplayerOnlineScience/mmos-sdk-csharp/tree/master/doc/) |
[Developer Portal](https://devportal.mmos.ch/) |
[Twitter](https://twitter.com/the_mmos) |

# MMOS SDK - C# edition

The MMOS SDK gives easy access to the MMOS API by providing an abstraction layer and encapsulating the authentication modules.

## Installation

Install the package 'MMOS.SDK' from the Nuget package manager in Visual Studio.

The NuGet package doesn't reference the Microsoft.CSharp NuGet package as it causes conflicts when using in Unity. If you use the SDK in a standard C# project you'll have to add this package manually.

## Usage example

In a standard Visual Studio C# project you can use the following syntax:

```csharp
Api.ApiConfig config = new Api.ApiConfig(
    new Api.ApiKey("key", "secret"),
    "game",
    null,
    "host",
    null,
    null
);
Api api = new Api(config);

dynamic body = new ExpandoObject();
body.projects = new string[] { "unige-exoplanet" };
body.player = new ExpandoObject();
body.player.accountCode = "accountcode";

dynamic response = await api.V2.Players.CreateTask("playercode", body);
```

## Using the MMOS SDK inside unity

In Unity we suggest to do the following steps:

* install the [NuGet for Unity](https://assetstore.unity.com/packages/tools/utilities/nuget-for-unity-104640) package from the Asset Store.
* set the Scripting Runtime Version to .NET  4.x as described here: https://docs.microsoft.com/en-us/visualstudio/cross-platform/unity-scripting-upgrade?view=vs-2019
* install the MMOS.SDK package with NuGet for Unity 

In Unity the members of the returned response object can't be accessed with the dot notation, so the following syntax is needed.

Please note the the MMOS.SDK is designed to be a server side component as the API Key it uses is a per game API Key, therefore it needs to be managed on the server side.

```csharp
Api.ApiConfig config = new Api.ApiConfig(
    new Api.ApiKey("key", "secret"),
    "game",
    null,
    "host",
    null,
    null
);
Api api = new Api(config);

dynamic body = new ExpandoObject();
body.projects = new string[] { "unige-exoplanet" };
body.player = new ExpandoObject();
body.player.accountCode = "accountcode";

dynamic response = null;

try {
    response = await api.V2.Players.CreateTask("playercode", body);
    var responseBody = response["body"];
    Debug.Log((string)responseBody["uid"]);    
} catch (Exception ex) {
    Debug.LogError(ex);
}       
```


## Documentation


API blueprint is available in compiled [html](doc/blueprint/mmos-api-public.html) and [apib](doc/blueprint/mmos-api-public.apib) format.


## Running automated tests

Presently the MMOS SDK automated tests use the MMOS Developer Portal. The MMOS Developer Portal helps developers understand how the MMOS API works through a set of publicly available test projects. Registration is publicly available at (https://devportal.mmos.ch/).

In order to run the tests, first you'll need to creare an account on the MMOS Developer Portal. Please note that the test rely on specific projects to be avaliable for the game, which is presently the Exoplanet research project by the University of Geneva. So first you'll need to add the Unige Exoplanet project to your available projects on the Developer Portal.

Please note that this may change in the future and thus you may need to update to the latest version of the SDK and follow the up-to-date instructions to be able to run the automated tests.

Once the account is created, there are three environment variables that need to be set in order to be able to run the tests:
* MMOS_SDK_TEST_API_KEY - The MMOS API Key
* MMOS_SDK_TEST_API_SECRET - The MMOS API Secret
* MMOS_SDK_TEST_GAME - The game id that is generated from your email address

Windows example:
```bat
$ SET MMOS_SDK_TEST_API_KEY=key
$ SET MMOS_SDK_TEST_API_SECRET=secret
$ SET MMOS_SDK_TEST_GAME=game
```

Linux example:
```shell
$ export MMOS_SDK_TEST_API_KEY=key
$ export MMOS_SDK_TEST_API_SECRET=secret
$ export MMOS_SDK_TEST_GAME=game
```

After setting the following environment variables you can run the the unit test inside Visual Studio, by switching to View -> Test. 

Please note that under MacOSX using Visual Studio you'll have to call the following commands (replacing with your personal values) in order for Visual Studio to see your environment variables (VS restart is needed):
```
launchctl setenv MMOS_SDK_TEST_API_KEY key
launchctl setenv MMOS_SDK_TEST_API_SECRET secret
launchctl setenv MMOS_SDK_TEST_GAME game
```
When you are done with testing you can remove these environment variables with the following commands:
```
launchctl unsetenv MMOS_SDK_TEST_API_KEY
launchctl unsetenv MMOS_SDK_TEST_API_SECRET
launchctl unsetenv MMOS_SDK_TEST_GAME
```

## Authentication

Please see the [authentication docs](api-hmac-authentication.md) for details.

## Acknowledgments

The GAPARS project has received funding from the European Union’s Horizon 2020 research and innovation programme under grant agreement Nr 732703

![EU flag](https://github.com/MassivelyMultiplayerOnlineScience/mmos-sdk-csharp/raw/master/doc/logo/eu.jpg)