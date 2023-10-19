## Changelog

Note the first digit of every adapter version corresponds to the major version of the Chartboost Core SDK compatible with that adapter. 
Adapters are compatible with any Chartboost Core SDK version within that major version.

### Version 0.2.8-3 *(2023-10-19)*
- Updated Unity dependency to Chartboost Core Unity SDK `0.3.1`.

### Version 0.2.8-2 *(2023-10-19)*
- Updated Unity dependency to Chartboost Core Unity SDK `0.3.0`.
- Updated native Android dependency to use `0.2.8.1.3`.
- Updated native iOS dependency to use `0.2.8.0.1`.

### Version 0.2.8-1 *(2023-09-07)*
- Updated Unity dependency to Chartboost Core Unity SDK `0.2.0`.
- Updated native Android dependency to use `0.2.8.1.2`.

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
