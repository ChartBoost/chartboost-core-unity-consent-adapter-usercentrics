#import "CBCUnityUtilities.h"
#import "CBCUnityObserver.h"
#import <ChartboostCoreConsentAdapterUsercentrics-Swift.h>
#import <Usercentrics/Usercentrics.h>

extern "C" {
    const void* _chartboostCoreGetUsercentricsAdapter(const char* dpsName, const char* settingsId){
        UsercentricsUsercentricsOptions* options = [[UsercentricsUsercentricsOptions alloc] initWithSettingsId:getNSStringOrEmpty(settingsId)];
        id<CBCInitializableModule> usercentricsAdapter = [[CBCUsercentricsAdapter alloc] initWithOptions:options];
        [[CBCUnityObserver sharedObserver] storeModule:usercentricsAdapter];
        return (__bridge void*)usercentricsAdapter;
    }
}
