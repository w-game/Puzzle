// Copyright 2020 ADTIMING TECHNOLOGY COMPANY LIMITED
// Licensed under the GNU Lesser General Public License Version 3

#import "OMUnityBridge.h"

#define OM_BANNER_POSITION_BOTTOM 0
#define OM_BANNER_POSITION_TOP 1


#define OMNSString2CString( str ) ( str != NULL && [str isKindOfClass:[NSString class]] ) ?  [str UTF8String] : ""


#define OMCString2NSString( str ) ( str != NULL ) ? [NSString stringWithUTF8String:str] : [NSString stringWithUTF8String:""]

#define MakeStringCopy( _x_ ) ( _x_ != NULL && [_x_ isKindOfClass:[NSString class]] ) ? strdup( [_x_ UTF8String] ) : NULL

#ifdef __cplusplus
extern "C" {
#endif
  
extern void UnitySendMessage( const char *className, const char *methodName, const char *param );
    
    
void OmLog(const char* log){
    NSLog(@"%s",log);
}

void OmSetLogEnable(bool logEnable)
{
    Class omClass = NSClassFromString(@"OpenMediation");
    if (omClass && [omClass respondsToSelector:@selector(setLogEnable)]) {
         [omClass setLogEnable:logEnable];
    }
}


void OmInitWithAppKey(const char* appKey)
{
    NSString* nsAppKey = [NSString stringWithUTF8String:appKey];
    [[OMUnityBridge sharedInstance]initWithAppKey:nsAppKey];

}

void OmInitWithAppKeyAndHost(const char* appKey, const char* host)
{
    NSString* nsAppKey = [NSString stringWithUTF8String:appKey];
    NSString* nsHost = [NSString stringWithUTF8String:host];
    [[OMUnityBridge sharedInstance]initWithAppKey:nsAppKey host:nsHost];

}

bool OmInitialized()
{
    bool initialized = false;
    Class omClass = NSClassFromString(@"OpenMediation");
    if (omClass && [omClass respondsToSelector:@selector(isInitialized)]) {
        initialized = [omClass isInitialized];
    }
    return initialized;
}

void OmSetGDPRConsent(BOOL consent)
{
    Class omClass = NSClassFromString(@"OpenMediation");
    if (omClass && [omClass respondsToSelector:@selector(setGDPRConsent:)]) {
        [omClass setGDPRConsent:consent];
    }
}
    
    bool OmGetGDPRConsent()
    {
        OMConsentStatus content = OMConsentStatusUnknown;
        Class omClass = NSClassFromString(@"OpenMediation");
        if (omClass && [omClass respondsToSelector:@selector(currentConsentStatus)]) {
            content = [omClass currentConsentStatus];
            if (content == OMConsentStatusDenied) {
                return NO;
            }
        }
        return YES;
    }
    
void OmSetIap(float count, const char* currency)
{
    NSString* curr = OMCString2NSString(currency);
    Class omClass = NSClassFromString(@"OpenMediation");
    if (omClass && [omClass respondsToSelector:@selector(userPurchase:currency:)]) {
        [omClass userPurchase:count currency:curr];
    }

}
    
void OmSetUserAge(int age)
{
    Class omClass = NSClassFromString(@"OpenMediation");
    if (omClass && [omClass respondsToSelector:@selector(setUserAge:)]) {
        [omClass setUserAge:age];
    }

}
    
void OmSetUserGender(const char* gender)
{
    NSString* genderStr = OMCString2NSString(gender);
    
    OMGender userGender = OMGenderUnknown;
    if ([genderStr isEqualToString:@"male"]) {
        userGender = OMGenderMale;
    } else if ([genderStr isEqualToString:@"female"]) {
        userGender = OMGenderFemale;
    }
    
    Class omClass = NSClassFromString(@"OpenMediation");
    if (omClass && [omClass respondsToSelector:@selector(setUserGender:)]) {
        [omClass setUserGender:userGender];
    }

}

void OmSetUSPrivacyLimit(BOOL limit)
{
    Class omClass = NSClassFromString(@"OpenMediation");
    if (omClass && [omClass respondsToSelector:@selector(setUSPrivacyLimit:)]) {
        [omClass setUSPrivacyLimit:limit];
    }

}
    
void OmSetAgeRestricted(BOOL restricted)
{
    Class omClass = NSClassFromString(@"OpenMediation");
    if (omClass && [omClass respondsToSelector:@selector(setUserAgeRestricted:)]) {
        [omClass setUserAgeRestricted:restricted];
    }

}


void OmSendAFConversionData(const char* conversionData)
{
    if (conversionData) {
        NSString *cd = OMCString2NSString(conversionData);
        if ([cd length] > 0) {
            NSData *data = [cd dataUsingEncoding:NSUTF8StringEncoding];
            NSError *jsonErr = nil;
            NSDictionary *dict = [NSJSONSerialization JSONObjectWithData:data options:NSJSONReadingAllowFragments error:&jsonErr];
            if (!jsonErr && [dict isKindOfClass:[NSDictionary class]]) {
                    Class omClass = NSClassFromString(@"OpenMediation");
                if (omClass && [omClass respondsToSelector:@selector(sendAFConversionData:)]) {
                    [omClass sendAFConversionData:dict];
                }

            }
        }
     }

}

void OmSendAFDeepLinkData(const char* attributionData){
    if (attributionData) {
        NSString *cd = OMCString2NSString(attributionData);
        if ([cd length] > 0) {
            NSData *data = [cd dataUsingEncoding:NSUTF8StringEncoding];
            NSError *jsonErr = nil;
            NSDictionary *dict = [NSJSONSerialization JSONObjectWithData:data options:NSJSONReadingAllowFragments error:&jsonErr];
            if (!jsonErr && [dict isKindOfClass:[NSDictionary class]]) {
                Class omClass = NSClassFromString(@"OpenMediation");
                if (omClass && [omClass respondsToSelector:@selector(sendAFDeepLinkData:)]) {
                    [omClass sendAFDeepLinkData:dict];
                }
            }
        }
    }
}

void OmSetIronSourceMediationMode(bool mode) {
    Class isAdapter = NSClassFromString(@"OMIronSourceAdapter");
    if (isAdapter && [isAdapter respondsToSelector:@selector(setMediationAPI:)]) {
        [isAdapter setMediationAPI:mode];
    }
}

void OmSetCustomTag(const char* key, const char* value) {
    NSString *keyString = OMCString2NSString(key);
    NSString *valueString = OMCString2NSString(value);
    Class omClass = NSClassFromString(@"OpenMediation");
    if (omClass && [omClass respondsToSelector:@selector(setCustomTag:withString:)]) {
        [omClass setCustomTag:keyString withString:valueString];
    }
}
    
void OmSetCustomTags(const char* key, const char* values[], int length) {
    NSString *keyString = OMCString2NSString(key);
    NSMutableArray *valueArray = [NSMutableArray arrayWithCapacity:length];
    for (int i=0; i<length; i++) {
        NSString *valueString = OMCString2NSString(values[i]);
        [valueArray addObject:valueString];
    }
    Class omClass = NSClassFromString(@"OpenMediation");
    if (omClass && [omClass respondsToSelector:@selector(setCustomTag:withStrings:)]) {
        [omClass setCustomTag:keyString withStrings:valueArray];
    }
}

    
void OmRemoveCustomTag(const char* key) {
    NSString *keyString = OMCString2NSString(key);
    Class omClass = NSClassFromString(@"OpenMediation");
    if (omClass && [omClass respondsToSelector:@selector(removeTag:)]) {
        [omClass removeTag:keyString];
    }
}
    
    char* cStringCopy(const char* string)
    {
        if (string == NULL)
            return NULL;
        char* cstring = (char*)malloc(strlen(string) + 1);
        strcpy(cstring, string);
        return cstring;
    }
    
    const char *OmGetCustomTags() {
        Class omClass = NSClassFromString(@"OpenMediation");
        if (omClass && [omClass respondsToSelector:@selector(allCustomTags)]) {
            NSDictionary *dic = [omClass allCustomTags];
            NSError *jsonError = nil;
            NSData *jsonData = [NSJSONSerialization dataWithJSONObject:dic options:NSJSONWritingPrettyPrinted error:&jsonError];
            NSString *jsonString = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
            return cStringCopy(OMNSString2CString(jsonString));
        }
        return cStringCopy(OMNSString2CString(@""));;
    }

void OmSetUserId(const char* userId) {
    NSString *userIdString = OMCString2NSString(userId);
    Class omClass = NSClassFromString(@"OpenMediation");
    if (omClass && [omClass respondsToSelector:@selector(setUserID:)]) {
        [omClass setUserID:userIdString];
    }
}
    
const char *OmGetUserId() {
    Class omClass = NSClassFromString(@"OpenMediation");
    if (omClass && [omClass respondsToSelector:@selector(getUserID)]) {
        return cStringCopy(OMNSString2CString([omClass getUserID]));
    }
    return cStringCopy(OMNSString2CString(@""));
}

//interstitial

void OmShowInterstitial()
{
    if ([[OMUnityBridge sharedInstance] interstitialIsReady])
    {
        [[OMUnityBridge sharedInstance] showInterstitial];
    }
}

void OmShowInterstitialWithScene(const char* scene)
{
    NSString* OmScene = [NSString stringWithUTF8String:scene];
    if ([[OMUnityBridge sharedInstance] interstitialIsReady]) {
        [[OMUnityBridge sharedInstance] showInterstitialWithScene:OmScene];
    }
}

bool OmInterstitialIsReady()
{
    return [[OMUnityBridge sharedInstance] interstitialIsReady];
}


//video

void OmShowRewardedVideo()
{
    if ([[OMUnityBridge sharedInstance] videoIsReady]){
        [[OMUnityBridge sharedInstance] showVideo];
    }
}

void OmShowRewardedVideoWithScene(const char* scene)
{
    NSString* OmScene = [NSString stringWithUTF8String:scene];
    if ([[OMUnityBridge sharedInstance] videoIsReady]) {
        [[OMUnityBridge sharedInstance] showVideoWithScene:OmScene];
    }
}

void OmShowRewardedVideoWithExtraParams(const char* scene, const char* extraParams)
{
    NSString* OmScene = [NSString stringWithUTF8String:scene];
    NSString* OmParams = [NSString stringWithUTF8String:extraParams];
    if ([[OMUnityBridge sharedInstance] videoIsReady]) {
        [[OMUnityBridge sharedInstance] showVideoWithExtraParams:OmScene extraParams:OmParams];
    }
}

bool OmRewardedVideoIsReady()
{
    return [[OMUnityBridge sharedInstance] videoIsReady];
}
    
#pragma mark Promotion
    
void OmShowPromotionAd(char* scene, int width, int height, float scaleX, float scaleY, float angle) {
    NSString* OmScene = [NSString stringWithUTF8String:scene];
    [[OMUnityBridge sharedInstance] showPromotionWithScene:OmScene width:width height:height scaleX:scaleX scaleY:scaleY angle: angle];
}

void OmHidePromotionAd() {
    [[OMUnityBridge sharedInstance] hidePromotionAd];
}

bool OmIsPromotionAdReady() {
    return [[OMUnityBridge sharedInstance] promotionAdIsReady];
}
    
#pragma mark Banner API
    
void OmLoadBanner(int bannerType, int position, char* placementId)
{
    [[OMUnityBridge sharedInstance] loadBanner:bannerType position:position placement:OMCString2NSString(placementId)];
}

void OmDestroyBanner (char* placementId)
{
    [[OMUnityBridge sharedInstance] destroyBanner:OMCString2NSString(placementId)];
}

void OmDisplayBanner (char* placementId)
{
    [[OMUnityBridge sharedInstance] displayBanner:OMCString2NSString(placementId)];
}

void OmHideBanner (char* placementId)
{
    [[OMUnityBridge sharedInstance] hideBanner:OMCString2NSString(placementId)];
}
    
#pragma mark SplashAd API
    
void OmLoadSplashAd(char* placementId)
{
    [[OMUnityBridge sharedInstance] loadSplashAd:OMCString2NSString(placementId)];
}
    
bool OmIsSplashAdReady(char* placementId)
{
    return [[OMUnityBridge sharedInstance] splashAdIsReady:OMCString2NSString(placementId)];
}

void OmShowSplashAd (char* placementId)
{
    [[OMUnityBridge sharedInstance] showSplashAd:OMCString2NSString(placementId)];
}

    
#ifdef __cplusplus
}
#endif

static OMUnityBridge * _instance = nil;

@implementation OMUnityBridge

char *const kOMEvents = "OmEvents";

+ (instancetype)sharedInstance{
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        _instance = [[self alloc] init];
    });
    return _instance;
}

- (instancetype)init{
    if (self = [super init]) {
        _bannerAdMap = [NSMutableDictionary dictionary];
        _bannerPostionMap = [NSMutableDictionary dictionary];
        _splashAdMap = [NSMutableDictionary dictionary];
        
        [[NSNotificationCenter defaultCenter] addObserver:self selector:@selector(initSuccess) name:@"kOpenMediatonInitSuccessNotification" object:nil];
        
        Class interstitialClass = NSClassFromString(@"OMInterstitial");
        if(interstitialClass && [interstitialClass respondsToSelector:@selector(sharedInstance)] && [interstitialClass instancesRespondToSelector:@selector(addDelegate:)]) {
            [[interstitialClass sharedInstance] addDelegate:self];
        }
        
        Class rewardedVideoClass = NSClassFromString(@"OMRewardedVideo");
        if(rewardedVideoClass && [rewardedVideoClass respondsToSelector:@selector(sharedInstance)] && [rewardedVideoClass instancesRespondToSelector:@selector(addDelegate:)]) {
            [[rewardedVideoClass sharedInstance] addDelegate:self];
        }

        Class promotionClass = NSClassFromString(@"OMCrossPromotion");
        if(promotionClass && [promotionClass respondsToSelector:@selector(sharedInstance)] && [promotionClass instancesRespondToSelector:@selector(addDelegate:)]) {
            [[promotionClass sharedInstance] addDelegate:self];
        }
        
        Class impressionClass = NSClassFromString(@"OpenMediation");
        if (impressionClass && [impressionClass respondsToSelector:@selector(addImpressionDataDelegate:)]) {
            [impressionClass addImpressionDataDelegate:self];
        }
        
        [[NSNotificationCenter defaultCenter] addObserver:self selector:@selector(orientationChanged:)
                                                     name:UIDeviceOrientationDidChangeNotification object:nil];
    }
    return self;
}

- (void)initWithAppKey:(NSString*)appKey {
    Class omClass = NSClassFromString(@"OpenMediation");
    if (omClass && [omClass respondsToSelector:@selector(initWithAppKey:)]) {
        [omClass initWithAppKey:appKey];
    }
}

- (void)initWithAppKey:(NSString*)appKey host:(NSString*)host {
    Class omClass = NSClassFromString(@"OpenMediation");
    if (omClass && [omClass respondsToSelector:@selector(initWithAppKey:baseHost:adFormat:)]) {
        [omClass initWithAppKey:appKey baseHost:host adFormat:(OpenMediationAdFormatInterstitial|OpenMediationAdFormatRewardedVideo|OpenMediationAdFormatCrossPromotion)];
    }
}


- (void)initSuccess {
    UnitySendMessage(kOMEvents,"onSdkInitSuccess","");
}

- (BOOL)interstitialIsReady {
    BOOL isReady = NO;
    Class interstitialClass = NSClassFromString(@"OMInterstitial");
    if(interstitialClass && [interstitialClass respondsToSelector:@selector(sharedInstance)] && [interstitialClass instancesRespondToSelector:@selector(isReady)]) {
        isReady = [[interstitialClass sharedInstance] isReady];
    }
    return isReady;
}

- (void)showInterstitial {
    
    Class interstitialClass = NSClassFromString(@"OMInterstitial");
    if(interstitialClass && [interstitialClass respondsToSelector:@selector(sharedInstance)] && [interstitialClass instancesRespondToSelector:@selector(showWithViewController:scene:)]) {
        [[interstitialClass sharedInstance] showWithViewController:[UIApplication sharedApplication].keyWindow.rootViewController scene:@""];
    }

}

- (void)showInterstitialWithScene:(NSString *)scene {
    
    Class interstitialClass = NSClassFromString(@"OMInterstitial");
    if(interstitialClass && [interstitialClass respondsToSelector:@selector(sharedInstance)] && [interstitialClass instancesRespondToSelector:@selector(showWithViewController:scene:)]) {
        [[interstitialClass sharedInstance] showWithViewController:[UIApplication sharedApplication].keyWindow.rootViewController scene:scene];
    }
}

/// Invoked when a interstitial video is available.
- (void)omInterstitialChangedAvailability:(BOOL)available {
    UnitySendMessage(kOMEvents,"onInterstitialAvailabilityChanged",(available) ? "true" : "false");
}

/// Sent immediately when a interstitial video is opened.
- (void)omInterstitialDidOpen:(OMScene*)scene {

}
/////////////
/// Sent immediately when a interstitial video starts to play.
- (void)omInterstitialDidShow:(OMScene*)scene {
    UnitySendMessage(kOMEvents,"onInterstitialShowed",OMNSString2CString(scene.sceneName));

}

/// Sent after a interstitial video has been clicked.
- (void)omInterstitialDidClick:(OMScene*)scene {
    UnitySendMessage(kOMEvents,"onInterstitialClicked",OMNSString2CString(scene.sceneName));

}

/// Sent after a interstitial video has been closed.
- (void)omInterstitialDidClose:(OMScene*)scene {
    UnitySendMessage(kOMEvents,"onInterstitialClosed",OMNSString2CString(scene.sceneName));

}

/// Sent after a interstitial video has failed to play.
- (void)omInterstitialDidFailToShow:(OMScene*)scene withError:(NSError *)error {
    if (error) {
        NSString *errorStr = [NSString stringWithFormat:@"error code:%ld msg:%@", (long)[error code],[error localizedDescription]];
            UnitySendMessage(kOMEvents,"onInterstitialShowFailed",OMNSString2CString(errorStr));
    } else {
         UnitySendMessage(kOMEvents,"onInterstitialShowFailed","");
    }
}

#pragma mark -- video
- (BOOL)videoIsReady {
    BOOL isReady = NO;
    Class rewardedVideoClass = NSClassFromString(@"OMRewardedVideo");
    if(rewardedVideoClass && [rewardedVideoClass respondsToSelector:@selector(sharedInstance)] && [rewardedVideoClass instancesRespondToSelector:@selector(isReady)]) {
        isReady = [[rewardedVideoClass sharedInstance] isReady];
    }
    return isReady;
}

- (void)showVideo {
    Class rewardedVideoClass = NSClassFromString(@"OMRewardedVideo");
    if(rewardedVideoClass && [rewardedVideoClass respondsToSelector:@selector(sharedInstance)] && [rewardedVideoClass instancesRespondToSelector:@selector(showWithViewController:scene:)]) {
        [[rewardedVideoClass sharedInstance] showWithViewController:[UIApplication sharedApplication].keyWindow.rootViewController scene:@""];
    }
}

- (void)showVideoWithScene:(NSString *)scene {
    Class rewardedVideoClass = NSClassFromString(@"OMRewardedVideo");
    if(rewardedVideoClass && [rewardedVideoClass respondsToSelector:@selector(sharedInstance)] && [rewardedVideoClass instancesRespondToSelector:@selector(showWithViewController:scene:)]) {
        [[rewardedVideoClass sharedInstance] showWithViewController:[UIApplication sharedApplication].keyWindow.rootViewController scene:scene];
    }
}

- (void)showVideoWithExtraParams:(NSString *)scene extraParams:(NSString *)extraParams {
    Class rewardedVideoClass = NSClassFromString(@"OMRewardedVideo");
    if(rewardedVideoClass && [rewardedVideoClass respondsToSelector:@selector(sharedInstance)] && [rewardedVideoClass instancesRespondToSelector:@selector(showWithViewController:scene:extraParams:)]) {
    [[rewardedVideoClass sharedInstance] showWithViewController:[UIApplication sharedApplication].keyWindow.rootViewController scene:scene extraParams:extraParams];
    }

}


#pragma mark -- OMRewardedVideoDelegate

- (void)omRewardedVideoChangedAvailability:(BOOL)available {
    UnitySendMessage(kOMEvents,"onRewardedVideoAvailabilityChanged",(available) ? "true" : "false");
}

- (void)omRewardedVideoDidOpen:(OMScene*)scene {
    UnitySendMessage(kOMEvents,"onRewardedVideoShowed",OMNSString2CString(scene.sceneName));

}

- (void)omRewardedVideoPlayStart:(OMScene *)scene {
    UnitySendMessage(kOMEvents,"onRewardedVideoStarted",OMNSString2CString(scene.sceneName));

}

- (void)omRewardedVideoDidClick:(OMScene *)scene {
    UnitySendMessage(kOMEvents,"onRewardedVideoClicked",OMNSString2CString(scene.sceneName));

}

- (void)omRewardedVideoDidClose:(OMScene *)scene{
    UnitySendMessage(kOMEvents,"onRewardedVideoClosed",OMNSString2CString(scene.sceneName));

}

- (void)omRewardedVideoPlayEnd:(OMScene*)scene {
    UnitySendMessage(kOMEvents,"onRewardedVideoEnded",OMNSString2CString(scene.sceneName));

}

- (void)omRewardedVideoDidReceiveReward:(OMScene*)scene {
    UnitySendMessage(kOMEvents,"onRewardedVideoRewarded",OMNSString2CString(scene.sceneName));

}

- (void)omRewardedVideoDidFailToShow:(OMScene *)scene withError:(NSError *)error {
    
    if (error) {
        NSString *errorStr = [NSString stringWithFormat:@"error code:%ld msg:%@", (long)[error code],[error localizedDescription]];
            UnitySendMessage(kOMEvents,"onRewardedVideoShowFailed",OMNSString2CString(errorStr));
    } else {
         UnitySendMessage(kOMEvents,"onRewardedVideoShowFailed","");
    }
}


#pragma mark Banner API

- (void)loadBanner:(NSInteger)bannerType position:(NSInteger)position placement:(NSString *)placementId {
    @synchronized(self) {
            OMBanner *banner = [_bannerAdMap objectForKey:placementId];
            Class bannerClass =  NSClassFromString(@"OMBanner");
            if (!banner && bannerClass && [bannerClass instancesRespondToSelector:@selector(initWithBannerType:placementID:)]) {
                banner = [[bannerClass alloc]initWithBannerType:bannerType placementID:placementId];
                banner.delegate = self;
            }
            if (banner) {
                [_bannerAdMap setObject:banner forKey:placementId];
                [_bannerPostionMap setObject:[NSNumber numberWithInteger:position] forKey:placementId];
                banner.center = [self getBannerCenter:banner position:position];
                [[UIApplication sharedApplication].keyWindow.rootViewController.view addSubview:banner];
                [banner loadAndShow];
            }

            
    }
}

- (void)destroyBanner:(NSString*)placementId {
    dispatch_async(dispatch_get_main_queue(), ^{
        @synchronized(self) {
            OMBanner *banner = [_bannerAdMap objectForKey:placementId];
            if (banner != nil) {
                [banner removeFromSuperview];
            }
            [_bannerAdMap removeObjectForKey:placementId];
            [_bannerPostionMap removeObjectForKey:placementId];
        }
    });
}

- (void)displayBanner:(NSString*)placementId {
    dispatch_async(dispatch_get_main_queue(), ^{
        @synchronized(self) {
            OMBanner *banner = [_bannerAdMap objectForKey:placementId];
            if (banner != nil) {
                [banner setHidden:NO];
            }
        }
    });
}

- (void)hideBanner:(NSString*)placementId {
    dispatch_async(dispatch_get_main_queue(), ^{
        @synchronized(self) {
            OMBanner *banner = [_bannerAdMap objectForKey:placementId];
            if (banner != nil) {
                [banner setHidden:YES];
            }
        }
    });
}

- (CGPoint)getBannerCenter:(OMBanner*)bannerView position:(NSInteger)position {
    CGFloat y;
    UIView *topView = [UIApplication sharedApplication].keyWindow.rootViewController.view;
    if (position == OM_BANNER_POSITION_TOP) {
        y = (bannerView.frame.size.height / 2);
        if (@available(ios 11.0, *)) {
            y += topView.safeAreaInsets.top;
        }
    } else {
        y = topView.frame.size.height - (bannerView.frame.size.height / 2);
        if (@available(ios 11.0, *)) {
            y -= topView.safeAreaInsets.bottom;
        }
    }
    
    return CGPointMake(topView.frame.size.width / 2, y);
}

- (void)centerBanner {
    dispatch_async(dispatch_get_main_queue(), ^{
        @synchronized(self) {
            NSArray *placements = [_bannerAdMap allKeys];
            for (NSString *placementId in placements) {
                OMBanner *banner = [_bannerAdMap objectForKey:placementId];
                NSInteger postion = [[_bannerPostionMap objectForKey:placementId]integerValue];
                banner.center = [self getBannerCenter:banner position:postion];
            }
        }
    });
}

- (void)orientationChanged:(NSNotification *)notification {
    [self centerBanner];
}


#pragma mark Banner Delegate

- (void)omBannerDidLoad:(OMBanner *)banner {
    UnitySendMessage(kOMEvents, "onBannerLoadSuccess", OMNSString2CString(banner.placementID));
}

- (void)omBannerDidFailToLoad:(OMBanner *)banner withError:(NSError *)error {
    NSArray *parameters;
    if (error) {
        NSString *errorStr = [NSString stringWithFormat:@"error code:%ld msg:%@", (long)[error code],[error localizedDescription]];
        parameters = @[banner.placementID, errorStr];
        
        UnitySendMessage(kOMEvents, "onBannerLoadFailed", MakeStringCopy([self getJsonFromObj:parameters]));
    } else {
        parameters = @[banner.placementID, @""];
        UnitySendMessage(kOMEvents, "onBannerLoadFailed", MakeStringCopy([self getJsonFromObj:parameters]));
    }
}

- (NSString *)getJsonFromObj:(id)obj {
    NSError *error;
    NSData *jsonData = [NSJSONSerialization dataWithJSONObject:obj options:0 error:&error];
    if (!jsonData) {
        return @"";
    } else {
        NSString *jsonString = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
        return jsonString;
    }
}

- (NSString *)gs_jsonStringCompactFormatForNSArray:(NSArray *)arrJson {
    if (![arrJson isKindOfClass:[NSArray class]] || ![NSJSONSerialization isValidJSONObject:arrJson]) {
        return nil;
    }
    NSData *jsonData = [NSJSONSerialization dataWithJSONObject:arrJson options:0 error:nil];
    NSString *strJson = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
    return strJson;
}

- (void)omBannerWillExposure:(OMBanner *)banner {
    //UnitySendMessage(kOMEvents, "onBannerShow", "");
}


- (void)omBannerDidClick:(OMBanner *)banner {
    UnitySendMessage(kOMEvents, "onBannerClicked", OMNSString2CString(banner.placementID));
}


- (void)omBannerWillPresentScreen:(OMBanner *)banner {
    //UnitySendMessage(kOMEvents, "onBannerScreenPresented", "");
}


- (void)omBannerDidDismissScreen:(OMBanner *)banner {
    //UnitySendMessage(kOMEvents, "onBannerScreenDismissed", "");
}


- (void)omBannerWillLeaveApplication:(OMBanner *)banner {
    //UnitySendMessage(kOMEvents, "onBannerLeftApplication", "");
}


#pragma mark SplashAd API

- (void)loadSplashAd:(NSString*)placementId {
    @synchronized(self) {
            OMSplash *splash = [_splashAdMap objectForKey:placementId];
            Class splashClass =  NSClassFromString(@"OMSplash");
            if (!splash && splashClass && [splashClass instancesRespondToSelector:@selector(initWithPlacementId:adSize:)]) {
                splash = [[splashClass alloc]initWithPlacementId:placementId adSize:CGSizeZero];
                splash.delegate = self;
            }
            if (splash) {
                [_splashAdMap setObject:splash forKey:placementId];
                [splash loadAd];
            }
            
    }
}

- (BOOL)splashAdIsReady:(NSString*)placementId {
    BOOL isReady = NO;
    OMSplash *splash = [_splashAdMap objectForKey:placementId];
    if (splash && [splash respondsToSelector:@selector(isReady)]) {
        isReady = [splash isReady];
    }
    return isReady;
}

- (void)showSplashAd:(NSString*)placementId {
    OMSplash *splash = [_splashAdMap objectForKey:placementId];
    if (splash && [splash respondsToSelector:@selector(showWithWindow:customView:)]) {
         [splash showWithWindow:[UIApplication sharedApplication].keyWindow customView:nil];
    }
}


- (void)omSplashDidLoad:(OMSplash *)splash {
    UnitySendMessage(kOMEvents,"onSplashAdLoadSuccess",OMNSString2CString(splash.placementID));
}

- (void)omSplashFailToLoad:(OMSplash *)splash withError:(NSError *)error {
    UnitySendMessage(kOMEvents,"onSplashAdLoadFailed",OMNSString2CString(splash.placementID));
}

- (void)omSplashDidShow:(OMSplash *)splash {
    UnitySendMessage(kOMEvents,"onSplashAdShowed",OMNSString2CString(splash.placementID));
}

- (void)omSplashDidClick:(OMSplash *)splash {
    UnitySendMessage(kOMEvents,"onSplashAdClick",OMNSString2CString(splash.placementID));
}

- (void)omSplashDidClose:(OMSplash *)splash {
    UnitySendMessage(kOMEvents,"onSplashAdClosed",OMNSString2CString(splash.placementID));
}

- (void)omSplashDidFailToShow:(OMSplash *)splash withError:(NSError *)error {
    UnitySendMessage(kOMEvents,"onSplashAdShowFailed",OMNSString2CString(splash.placementID));
}


- (BOOL)promotionAdIsReady {
    BOOL isReady = NO;
    Class promotionClass = NSClassFromString(@"OMCrossPromotion");
    if(promotionClass && [promotionClass respondsToSelector:@selector(sharedInstance)] && [promotionClass instancesRespondToSelector:@selector(isReady)]) {
        isReady = [[promotionClass sharedInstance] isReady];
    }
    return isReady;
}

- (void)showPromotionWithScene:(NSString*)scene width:(NSInteger)width height:(NSInteger)height scaleX:(CGFloat)scaleX scaleY:(CGFloat)scaleY  angle:(CGFloat)angle {
    
    Class promotionClass = NSClassFromString(@"OMCrossPromotion");
    if(promotionClass && [promotionClass respondsToSelector:@selector(sharedInstance)] && [promotionClass instancesRespondToSelector:@selector(showAdWithScreenPoint:adSize:angle:scene:)]) {
        
        [[promotionClass sharedInstance]showAdWithScreenPoint:CGPointMake(scaleX,scaleY) adSize:CGSizeMake(width,height) angle:angle scene:scene];
    }

}

- (void)hidePromotionAd {
    Class promotionClass = NSClassFromString(@"OMCrossPromotion");
    if(promotionClass && [promotionClass respondsToSelector:@selector(sharedInstance)] && [promotionClass instancesRespondToSelector:@selector(hideAd)]) {
        [[promotionClass sharedInstance]hideAd];
    }
}

#pragma mark Promotion Delegate

- (void)omCrossPromotionChangedAvailability:(BOOL)available {
    UnitySendMessage(kOMEvents,"onPromotionAdAvailabilityChanged",(available) ? "true" : "false");
}

- (void)omCrossPromotionWillAppear:(OMScene*)scene {
    UnitySendMessage(kOMEvents,"onPromotionAdShowed",OMNSString2CString(scene.sceneName));
}

- (void)omCrossPromotionDidClick:(OMScene*)scene {
    UnitySendMessage(kOMEvents,"onPromotionAdClicked",OMNSString2CString(scene.sceneName));
}

- (void)omCrossPromotionDidDisappear:(OMScene*)scene {
    UnitySendMessage(kOMEvents,"onPromotionAdHidden",OMNSString2CString(scene.sceneName));
}

- (void)omCrossPromotionDidFailToShow:(OMScene*)scene withError:(NSError *)error {
    if (error) {
        NSString *errorStr = [NSString stringWithFormat:@"error code:%ld msg:%@", (long)[error code],[error localizedDescription]];
        UnitySendMessage(kOMEvents, "onPromotionAdShowFailed",[errorStr UTF8String] );
    } else {
        UnitySendMessage(kOMEvents, "onPromotionAdShowFailed", "");
    }
}


#pragma mark -- OMImpressionDataDelegate
- (void)omImpressionData:(OMImpressionData * _Nullable)impressionData error:(NSError* _Nullable)error {
    NSString *impressionStr;
    if (error) {
        NSString *errorStr = [NSString stringWithFormat:@"error code:%ld msg:%@", (long)[error code],[error localizedDescription]];
        UnitySendMessage(kOMEvents,"onImpressionDataError",OMNSString2CString(errorStr));
    } else {
        if (impressionData) {
            NSError *jsonErr = nil;
            NSDictionary *dict = @{
                @"impression_id":impressionData.impressionId,
                @"placement_id":impressionData.placementId,
                @"placement_name":impressionData.placementName,
                @"placement_ad_type":impressionData.placementAdType,
                @"scene_name":impressionData.sceneName,
                @"mediation_rule_id":impressionData.ruleId,
                @"mediation_rule_name":impressionData.ruleName,
                @"mediation_rule_type":impressionData.ruleType,
                @"mediation_rule_priority":impressionData.rulePriority,
                @"ab_group":impressionData.abGroup,
                @"instance_id":impressionData.instanceId,
                @"instance_name":impressionData.instanceName,
                @"instance_priority":impressionData.instancePriority,
                @"ad_network_id":impressionData.adNetworkId,
                @"ad_network_name":impressionData.adNetworkName,
                @"ad_network_unit_id":impressionData.adNetworkUnitId,
                @"precision":impressionData.precision,
                @"currency":impressionData.currency,
                @"revenue":impressionData.revenue,
                @"lifetime_value":impressionData.lifeTimeValue,
            };
            
            NSData *jsonData = [NSJSONSerialization dataWithJSONObject:dict options:NSJSONWritingPrettyPrinted error:&jsonErr];
            impressionStr = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
        }
        UnitySendMessage(kOMEvents,"onImpressionData",OMNSString2CString(impressionStr));
    }
}

@end



