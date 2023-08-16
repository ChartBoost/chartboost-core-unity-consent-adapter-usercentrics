## Changelog

Note the first digit of every adapter version corresponds to the major version of the Chartboost Core SDK compatible with that adapter. 
Adapters are compatible with any Chartboost Core SDK version within that major version.

### Version 0.2.8-0
Added:
- `UsercentricsAdapter` class.
- `UsercentricsOptions` class.
- Usercentrics Android - NativeModule.
- Usercentrics iOS - NativeModule.
    - `CBCUsercentricsAdapterBridge.mm` to bridge Objective-C & C# functionality.
- `ChartboostCoreConsentUsercentricDependencies.xml`.

- This version of the adapter has been certified with Android: Usercentrics SDK 2.8.1.
- This version of the adapter has been certified with iOS: UsercentricsUI 2.8.0.