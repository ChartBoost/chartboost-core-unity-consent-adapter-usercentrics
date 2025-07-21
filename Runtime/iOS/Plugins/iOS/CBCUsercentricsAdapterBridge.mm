#import "CBCUnityObserver.h"
#import "ChartboostCoreConsentAdapterUsercentrics-Swift.h"
#import <Usercentrics/Usercentrics.h>

NSString* const CBCUsercentricsTAG = @"CBCUsercentrics";

extern "C" {
    const void* _CBCGetUsercentricsAdapter(const char* rulesetId, const char* settingsId, const char* defaultLanguage, const char* version, long timeoutMilliseconds, int consentMediation, int loggerLevel, int networkMode, const char* templateIdToPartnerId){
        UsercentricsUsercentricsOptions* options = [[UsercentricsUsercentricsOptions alloc] init];

        if (rulesetId != NULL) {
            NSString* nsRulesetId = toNSStringOrEmpty(rulesetId);
            [options setRuleSetId:nsRulesetId];
            [[CBLUnityLoggingBridge sharedLogger] logWithTag:CBCUsercentricsTAG log:[NSString stringWithFormat:@"Set RulesetId: %@", nsRulesetId] logLevel:CBLLogLevelVerbose];
        }

        if (settingsId != NULL && rulesetId == NULL) {
            NSString* nsSettingsId = toNSStringOrEmpty(settingsId);
            [options setSettingsId:nsSettingsId];
            [[CBLUnityLoggingBridge sharedLogger] logWithTag:CBCUsercentricsTAG log:[NSString stringWithFormat:@"Set SettingsId: %@", nsSettingsId] logLevel:CBLLogLevelVerbose];
        }

        if (defaultLanguage != NULL) {
            NSString* nsDefaultLanguage = toNSStringOrEmpty(defaultLanguage);
            [options setDefaultLanguage:nsDefaultLanguage];
            [[CBLUnityLoggingBridge sharedLogger] logWithTag:CBCUsercentricsTAG log:[NSString stringWithFormat:@"Set Default Language: %@", nsDefaultLanguage] logLevel:CBLLogLevelVerbose];
        }

        if (version != NULL) {
            NSString* nsVersion = toNSStringOrEmpty(version);
            [options setVersion:toNSStringOrEmpty(version)];
            [[CBLUnityLoggingBridge sharedLogger] logWithTag:CBCUsercentricsTAG log:[NSString stringWithFormat:@"Set Version: %@", nsVersion] logLevel:CBLLogLevelVerbose];
        }

        if (timeoutMilliseconds != -1) {
            [options setTimeoutMillis:timeoutMilliseconds];
            [[CBLUnityLoggingBridge sharedLogger] logWithTag:CBCUsercentricsTAG log:[NSString stringWithFormat:@"Set TimeoutMilliseconds: %ld", timeoutMilliseconds] logLevel:CBLLogLevelVerbose];
        }

        if (consentMediation != -1)
        {
            [options setConsentMediation:(BOOL)consentMediation];
            [[CBLUnityLoggingBridge sharedLogger] logWithTag:CBCUsercentricsTAG log:[NSString stringWithFormat:@"Set Consent Mediation: %d", consentMediation] logLevel:CBLLogLevelVerbose];
        }

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
            [[CBLUnityLoggingBridge sharedLogger] logWithTag:CBCUsercentricsTAG log:[NSString stringWithFormat:@"Set Usercentrics Logger Level: %d", loggerLevel] logLevel:CBLLogLevelVerbose];
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
            [[CBLUnityLoggingBridge sharedLogger] logWithTag:CBCUsercentricsTAG log:[NSString stringWithFormat:@"Set Usercentrics Network Mode: %d", networkMode] logLevel:CBLLogLevelVerbose];
        }

        NSDictionary* partnerIdDictionary = toNSDictionary(templateIdToPartnerId);

        id<CBCModule> usercentricsAdapter = [[CBCUsercentricsAdapter alloc] initWithOptions:options partnerIDMap:partnerIdDictionary];
        [[CBCUnityObserver sharedObserver] storeModule:usercentricsAdapter];
        return (__bridge void*)usercentricsAdapter;
    }
}
