#import "CBCUnityObserver.h"
#import "ChartboostCoreConsentAdapterUsercentrics-Swift.h"
#import <Usercentrics/Usercentrics.h>

extern "C" {
    const void* _CBCGetUsercentricsAdapter(const char* rulesetId, const char* settingsId, const char* defaultLanguage, const char* version, long timeoutMilliseconds, int consentMediation, int loggerLevel, int networkMode, const char* templateIdToPartnerId){
        UsercentricsUsercentricsOptions* options = [[UsercentricsUsercentricsOptions alloc] init];

        if (rulesetId != NULL)
            [options setRuleSetId:toNSStringOrEmpty(rulesetId)];

        if (settingsId != NULL && rulesetId == NULL)
            [options setSettingsId:toNSStringOrEmpty(settingsId)];

        if (defaultLanguage != NULL)
            [options setDefaultLanguage:toNSStringOrEmpty(defaultLanguage)];

        if (version != NULL)
            [options setVersion:toNSStringOrEmpty(version)];

        if (timeoutMilliseconds != -1)
            [options setTimeoutMillis:timeoutMilliseconds];

        if (consentMediation != -1)
            [options setConsentMediation:(BOOL)consentMediation];

        if (loggerLevel != -1)
        {
            switch (loggerLevel) {
                default:
                case 0:
                    [options setLoggerLevel:[UsercentricsUsercentricsLoggerLevel none]];
                    break;
                case 1:
                    [options setLoggerLevel:[UsercentricsUsercentricsLoggerLevel error]];
                    break;
                case 2:
                    [options setLoggerLevel:[UsercentricsUsercentricsLoggerLevel warning]];
                    break;
                case 3:
                    [options setLoggerLevel:[UsercentricsUsercentricsLoggerLevel debug]];
                    break;
            }
        }

        if (networkMode != -1)
        {
            switch (networkMode) {
                default:
                case 0:
                    [options setNetworkMode:[UsercentricsNetworkMode world]];
                    break;
                case 1:
                    [options setNetworkMode:[UsercentricsNetworkMode eu]];
                    break;
            }
        }

        NSDictionary* partnerIdDictionary = toNSDictionary(templateIdToPartnerId);

        id<CBCModule> usercentricsAdapter = [[CBCUsercentricsAdapter alloc] initWithOptions:options partnerIDMap:partnerIdDictionary];
        [[CBCUnityObserver sharedObserver] storeModule:usercentricsAdapter];
        return (__bridge void*)usercentricsAdapter;
    }
}
