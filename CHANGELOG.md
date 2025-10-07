## Changelog
All notable changes to this project will be documented in this file using the standards as defined at [Keep a Changelog](https://keepachangelog.com/en/1.0.0/). This project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0).

Note the first digit of every adapter version corresponds to the major version of the Chartboost Core SDK compatible with that adapter. 
Adapters are compatible with any Chartboost Core SDK version within that major version.

### Version 1.0.16 *(2025-09-15)*
This version of the Usercentrics Consent Adapter supports the following native SDK dependencies:
  * Android: `com.chartboost:chartboost-core-consent-adapter-usercentrics:1.2.23.+`
  * iOS: `ChartboostCoreConsentAdapterUsercentrics: ~> 1.2.22.0`

### Version 1.0.15 *(2025-08-20)*
This version of the Usercentrics Consent Adapter supports the following native SDK dependencies:
  * Android: `com.chartboost:chartboost-core-consent-adapter-usercentrics:1.2.22.+`
  * iOS: `ChartboostCoreConsentAdapterUsercentrics: ~> 1.2.22.0`

### Version 1.0.14 *(2025-08-08)*
This version of the Usercentrics Consent Adapter supports the following native SDK dependencies:
  * Android: `com.chartboost:chartboost-core-consent-adapter-usercentrics:1.2.22.+`
  * iOS: `ChartboostCoreConsentAdapterUsercentrics: ~> 1.2.21.0`

### Version 1.0.13 *(2025-07-07)*
This version of the Usercentrics Consent Adapter supports the following native SDK dependencies:
  * Android: `com.chartboost:chartboost-core-consent-adapter-usercentrics:1.2.21.+`
  * iOS: `ChartboostCoreConsentAdapterUsercentrics: ~> 1.2.21.0`

### Version 1.0.12 *(2025-06-18)*
This version of the Usercentrics Consent Adapter supports the following native SDK dependencies:
  * Android: `com.chartboost:chartboost-core-consent-adapter-usercentrics:1.2.20.+`
  * iOS: `ChartboostCoreConsentAdapterUsercentrics: ~> 1.2.21.0`

### Version 1.0.11 *(2025-06-02)*
This version of the Usercentrics Consent Adapter supports the following native SDK dependencies:
  * Android: `com.chartboost:chartboost-core-consent-adapter-usercentrics:1.2.20.+`
  * iOS: `ChartboostCoreConsentAdapterUsercentrics: ~> 1.2.20.0`

### Version 1.0.10 *(2025-06-02)*
This version of the Usercentrics Consent Adapter supports the following native SDK dependencies:
  * Android: `com.chartboost:chartboost-core-consent-adapter-usercentrics:1.2.20.+`
  * iOS: `ChartboostCoreConsentAdapterUsercentrics: ~> 1.2.18.0`

### Version 1.0.9 *(2025-06-02)*
This version of the Usercentrics Consent Adapter supports the following native SDK dependencies:
  * Android: `com.chartboost:chartboost-core-consent-adapter-usercentrics:1.2.19.+`
  * iOS: `ChartboostCoreConsentAdapterUsercentrics: ~> 1.2.18.0`

### Version 1.0.8 *(2025-06-02)*
This version of the Usercentrics Consent Adapter supports the following native SDK dependencies:
  * Android: `com.chartboost:chartboost-core-consent-adapter-usercentrics:1.2.18.+`
  * iOS: `ChartboostCoreConsentAdapterUsercentrics: ~> 1.2.18.0`

### Version 1.0.7 *(2025-06-02)*
This version of the Usercentrics Consent Adapter supports the following native SDK dependencies:
  * Android: `com.chartboost:chartboost-core-consent-adapter-usercentrics:1.2.18.+`
  * iOS: `ChartboostCoreConsentAdapterUsercentrics: ~> 1.2.17.0`

### Version 1.0.6 *(2025-06-02)*
This version of the Usercentrics Consent Adapter supports the following native SDK dependencies:
  * Android: `com.chartboost:chartboost-core-consent-adapter-usercentrics:1.2.17.+`
  * iOS: `ChartboostCoreConsentAdapterUsercentrics: ~> 1.2.17.0`

### Version 1.0.5 *(2025-06-02)*
This version of the Usercentrics Consent Adapter supports the following native SDK dependencies:
  * Android: `com.chartboost:chartboost-core-consent-adapter-usercentrics:1.2.17.+`
  * iOS: `ChartboostCoreConsentAdapterUsercentrics: ~> 1.2.16.0`

### Version 1.0.4 *(2025-06-02)*
This version of the Usercentrics Consent Adapter supports the following native SDK dependencies:
  * Android: `com.chartboost:chartboost-core-consent-adapter-usercentrics:1.2.16.+`
  * iOS: `ChartboostCoreConsentAdapterUsercentrics: ~> 1.2.16.0`

### Version 1.0.3 *(2025-06-02)*
This version of the Usercentrics Consent Adapter supports the following native SDK dependencies:
  * Android: `com.chartboost:chartboost-core-consent-adapter-usercentrics:1.2.16.+`
  * iOS: `ChartboostCoreConsentAdapterUsercentrics: ~> 1.2.15.0`

### Version 1.0.2
This version of the Usercentrics Adapter supports the following native SDK dependencies:
* Android: `com.chartboost:chartboost-core-consent-adapter-usercentrics:1.2.15.+`
* iOS: `ChartboostCoreConsentAdapterUsercentrics ~> 1.2.15.0` 

### Version 1.0.1
This version of the Usercentrics Adapter supports the following native SDK dependencies:
* Android: `com.chartboost:chartboost-core-consent-adapter-usercentrics:1.2.14.+`
* iOS: `ChartboostCoreConsentAdapterUsercentrics ~> 1.2.14.0` 

### Version 1.0.0 *(2024-08-01)*
This version of the Usercentrics Adapter supports the following native SDK dependencies:
* Android: `com.chartboost:chartboost-core-consent-adapter-usercentrics:1.2.14.+`
* iOS: `ChartboostCoreConsentAdapterUsercentrics ~> 1.2.8.0` 

Added:
- Distribution through `https://www.nuget.org`.

Removed:
- `dspName` as a constructor parameter for `UsercentricsParameter`.
- `templateIdToPartnerId` as a needed parameter for `UsercentricsParameter`.

Improvements:
- `templateIdToPartnerId` as an optional parameter for `UsercentricsParameter`.
- Default `Dictionary<string, string>` for `templateIdToPartnerId`, any additional template ids to partner id will be added to this default map.
- Improved wrapping coverage for `UsercentricsOptions`.

### Version 0.2.8-4 *(2023-12-07)*
- This version of the adapter is compatible with Chartboost Core 0.4.0
- Added the ability to set a Template ID to Mediation Partner ID dictionary to facilitate per-partner consent.

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
