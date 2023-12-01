#import "CBCUnityUtilities.h"
#import "CBCUnityObserver.h"
#import <ChartboostCoreConsentAdapterUsercentrics-Swift.h>
#import <Usercentrics/Usercentrics.h>

extern "C" {
    const void* _chartboostCoreGetUsercentricsAdapter(const char* dpsName, const char* settingsId, const char* templateIdToPartnerId){
        UsercentricsUsercentricsOptions* options = [[UsercentricsUsercentricsOptions alloc] initWithSettingsId:getNSStringOrEmpty(settingsId)];
        NSDictionary* partnerIdDictionary = stringToNSDictionary(templateIdToPartnerId);
        id<CBCInitializableModule> usercentricsAdapter = [[CBCUsercentricsAdapter alloc] initWithOptions:options chartboostCoreDPSName:getNSStringOrEmpty(dpsName) partnerIDMap:partnerIdDictionary];
        [[CBCUnityObserver sharedObserver] storeModule:usercentricsAdapter];
        return (__bridge void*)usercentricsAdapter;
    }
}
