# Chartboost Core - Usercentrics Adapter

The Chartboost Core - Usercentrics Adapter mediates Usercentrics via the Chartboost Core SDK.

## Minimum Requirements

| Plugin | Version |
| ------ | ------ |
| Cocoapods | 1.11.3+ |
| iOS | 11.0+ |
| Xcode | 14.1+ |
| Android API | 21+ |
| Unity | 2022.3.+ |

## Integration

Chartboost Core Usercentrics Adapter is distributed using the public [npm registry](https://www.npmjs.com/search?q=com.chartboost.core.consent.usercentrics) as such it is compatible with the Unity Package Manager (UPM). In order to add the Chartboost Core Usercentrics Adapter to your project, just add the following to your Unity Project's ***manifest.json*** file. The scoped registry section is required in order to fetch packages from the NpmJS registry.

```json
  "dependencies": {
    "com.chartboost.core.consent.usercentrics": "1.0.6",
    ...
  },
  "scopedRegistries": [
    {
      "name": "NpmJS",
      "url": "https://registry.npmjs.org",
      "scopes": [
        "com.chartboost"
      ]
    }
  ]
```

## Using the public [NuGet package](https://www.nuget.org/packages/Chartboost.CSharp.Core.Unity.Consent.Usercentrics)

To add the Chartboost Core Unity SDK to your project using the NuGet package, you will first need to add the [NugetForUnity](https://github.com/GlitchEnzo/NuGetForUnity) package into your Unity Project.

This can be done by adding the following to your Unity Project's ***manifest.json***

```json
  "dependencies": {
    "com.github-glitchenzo.nugetforunity": "https://github.com/GlitchEnzo/NuGetForUnity.git?path=/src/NuGetForUnity",
    ...
  },
```

Once <code>NugetForUnity</code> is installed, search for `Chartboost.CSharp.Core.Unity.Consent.Usercentrics` in the search bar of Nuget Explorer window(Nuget -> Manage Nuget Packages).
You should be able to see the `Chartboost.CSharp.Core.Unity.Consent.Usercentrics` package. Choose the appropriate version and install.

# Usage

## Client Module
In order to use the `UsercentricsAdapter`, a client instance can be passed along with the `ChartboostCore.Initialize` call as seen in the example below:

```csharp
string chartboostApplicationIdentifier = "CHARTBOOST_APPLICATION_IDENTIFIER";

List<Module> modulesToInitialize = new List<Module>();

// create usercentrics options configuration object
UsercentricsOptions usercentricsOptions = new UsercentricsOptions("USERCENTICS_SETTINGS_ID");

// template to partner id can be passed as an optional paramter, but a default set is provided.
UsercentricsAdapter usercentricsAdapter = new UsercentricsAdapter(usercentricsOptions);

modulesToInitialize.Add(usercentricsAdapter);

SDKConfiguration sdkConfig = new SDKConfiguration(chartboostApplicationIdentifier, modulesToInitialize);

// Initialize Chartboost Core and Usercentrics.
ChartboostCore.Initialize(sdkConfig);
```

# Contributions

We are committed to a fully transparent development process and highly appreciate any contributions. Our team regularly monitors and investigates all submissions for the inclusion in our official adapter releases.

Refer to our [CONTRIBUTING](CONTRIBUTING.md) file for more information on how to contribute.

# License

Refer to our [LICENSE](LICENSE.md) file for more information.