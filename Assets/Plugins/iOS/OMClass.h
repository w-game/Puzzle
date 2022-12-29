// Copyright 2020 ADTIMING TECHNOLOGY COMPANY LIMITED
// Licensed under the GNU Lesser General Public License Version 3

#ifndef OMClass_h
#define OMClass_h

typedef NS_ENUM(NSInteger, OpenMediationAdFormat) {
    OpenMediationAdFormatBanner = (1 << 0),
    OpenMediationAdFormatNative = (1 << 1),
    OpenMediationAdFormatRewardedVideo = (1 << 2),
    OpenMediationAdFormatInterstitial = (1 << 3),
    OpenMediationAdFormatSplash = (1 << 4),
    OpenMediationAdFormatCrossPromotion = (1 << 5),
};

typedef NS_ENUM(NSInteger, OMGender) {
    OMGenderUnknown,
    OMGenderMale,
    OMGenderFemale,
};

typedef NS_ENUM(NSInteger, OMConsentStatus) {
    OMConsentStatusUnknown,
    OMConsentStatusDenied,
    OMConsentStatusConsented,
};

@interface OMImpressionData : NSObject

@property (readonly, copy) NSString *impressionId;

@property (readonly, copy) NSString *placementId;

@property (readonly, copy) NSString *placementName;

@property (readonly, copy) NSString *placementAdType;

@property (readonly, copy) NSString *sceneName;

@property (readonly, copy) NSString *ruleId;

@property (readonly, copy) NSString *ruleName;

@property (readonly, copy) NSNumber *ruleType;

@property (readonly, copy) NSNumber *rulePriority;

@property (readonly, copy) NSString *abGroup;

@property (readonly, copy) NSString *instanceId;

@property (readonly, copy) NSString *instanceName;

@property (readonly, copy) NSNumber *instancePriority;

@property (readonly, copy) NSString *adNetworkId;

@property (readonly, copy) NSString *adNetworkName;

@property (readonly, copy) NSString *adNetworkUnitId;

@property (readonly, copy) NSString *precision;

@property (readonly, copy) NSString *currency;

@property (readonly, copy) NSNumber *revenue;

@property (readonly, copy) NSNumber *lifeTimeValue;

@property (readonly, copy) NSString *userID;

@end

@protocol OMImpressionDataDelegate<NSObject>

- (void)omImpressionData:(OMImpressionData * _Nullable)impressionData error:(NSError* _Nullable)error;

@end

@interface OpenMediation : NSObject

/// Initializes OpenMediation's SDK with all the ad types that are defined in the platform.
+ (void)initWithAppKey:(NSString*)appKey;

/// Initializes OpenMediation's SDK with the requested ad types.
+ (void)initWithAppKey:(NSString *)appKey adFormat:(OpenMediationAdFormat)initAdFormats;

/// Initializes OpenMediation's SDK with host.
+ (void)initWithAppKey:(NSString *)appKey baseHost:(NSString*)host adFormat:(OpenMediationAdFormat)initAdFormats;

/// Check that `OpenMediation` has been initialized
+ (BOOL)isInitialized;

/// current SDK version
+ (NSString *)SDKVersion;

/// setUserConsent "NO" is Refuse，"YES" is Accepted. //GDPR
/// According to the GDPR, set method of this property must be called before "initWithAppKey:", or by default will collect user's information.
+ (void)setGDPRConsent:(BOOL)consent;

/// Get the GDPR current consent status of this user.
+ (OMConsentStatus)currentConsentStatus;

///According to the CCPA, set method of this property must be called before "initWithAppKey:", or by default will collect user's information.
+ (void)setUSPrivacyLimit:(BOOL)privacyLimit;

/// If you call this method with YES, you are indicating that your app should be treated as child-directed for purposes of the Children’s Online Privacy Protection Act (COPPA).
/// If you call this method with NO, you are indicating that your app should not be treated as child-directed for purposes of the Children’s Online Privacy Protection Act (COPPA).
+ (void)setUserAgeRestricted:(BOOL)restricted;

/// log enable,default is YES
+ (void)setLogEnable:(BOOL)logEnable;

/// Set this property to configure the user's age.
+ (void)setUserAge:(NSInteger)userAge;

/// Set the gender of the current user
+ (void)setUserGender:(OMGender)userGender;

/// user in-app purchase
+ (void)userPurchase:(CGFloat)amount currency:(NSString*)currencyUnit;

/// A tool to verify a successful integration of the OpenMediation SDK and any additional adapters.
+ (void)validateIntegration;

///Send conversion attribution data to server
+ (void)sendAFConversionData:(NSDictionary*)conversionInfo;

///Send  deep link attribution data to server
+ (void)sendAFDeepLinkData:(NSDictionary*)attributionData;

///Add Impression Data delegate
+ (void)addImpressionDataDelegate:(id<OMImpressionDataDelegate>)delegate;

///Remove Impression Data delegate
+ (void)rmoveImpressionDataDelegate:(id<OMImpressionDataDelegate>)delegate;

/// Set custom user id
+ (void)setUserID:(NSString*)userID;

/// Get custom user id
+ (NSString*)getUserID;

+ (void)setCustomTag:(NSString*)tag withString:(NSString*)value;

+ (void)setCustomTag:(NSString*)tag withNumber:(NSNumber*)value;

+ (void)setCustomTag:(NSString*)tag withStrings:(NSArray *)values;

+ (void)setCustomTag:(NSString*)tag withNumbers:(NSArray *)values;

+ (void)removeTag:(NSString*)tag;

+ (NSDictionary*)allCustomTags;

@end

@interface OMScene : NSObject

@property(nonatomic, copy)NSString *sceneName;

@end



@protocol OMInterstitialDelegate <NSObject>

@optional

/// Invoked when a interstitial video is available.
- (void)omInterstitialChangedAvailability:(BOOL)available;

/// Sent immediately when a interstitial video is opened.
- (void)omInterstitialDidOpen:(OMScene*)scene;

/// Sent immediately when a interstitial video starts to play.
- (void)omInterstitialDidShow:(OMScene*)scene;

/// Sent after a interstitial video has been clicked.
- (void)omInterstitialDidClick:(OMScene*)scene;

/// Sent after a interstitial video has been closed.
- (void)omInterstitialDidClose:(OMScene*)scene;

/// Sent after a interstitial video has failed to play.
- (void)omInterstitialDidFailToShow:(OMScene*)scene withError:(NSError *)error;

@end


@interface OMInterstitial : NSObject

/// Returns the singleton instance.
+ (instancetype)sharedInstance;

/// Add delegate
- (void)addDelegate:(id<OMInterstitialDelegate>)delegate;

/// Remove delegate
- (void)removeDelegate:(id<OMInterstitialDelegate>)delegate;

/// Indicates whether the interstitial video is ready to show ad.
- (BOOL)isReady;

/// Indicates whether the scene has reached the display frequency.
- (BOOL)isCappedForScene:(NSString *)sceneName;

/// Presents the interstitial video ad modally from the specified view controller.
/// Parameter viewController: The view controller that will be used to present the video ad.
/// Parameter sceneName: The name of th ad scene. Default scene if null.
- (void)showWithViewController:(UIViewController *)viewController scene:(NSString *)sceneName;

@end

@protocol OMRewardedVideoDelegate <NSObject>

@optional

/// Invoked when a rewarded video is available.
- (void)omRewardedVideoChangedAvailability:(BOOL)available;

/// Sent immediately when a rewarded video is opened.
- (void)omRewardedVideoDidOpen:(OMScene*)scene;

/// Sent immediately when a rewarded video starts to play.
- (void)omRewardedVideoPlayStart:(OMScene*)scene;

/// Send after a rewarded video has been completed.
- (void)omRewardedVideoPlayEnd:(OMScene*)scene;

/// Sent after a rewarded video has been clicked.
- (void)omRewardedVideoDidClick:(OMScene*)scene;

/// Sent after a user has been granted a reward.
- (void)omRewardedVideoDidReceiveReward:(OMScene*)scene;

/// Sent after a rewarded video has been closed.
- (void)omRewardedVideoDidClose:(OMScene*)scene;

/// Sent after a rewarded video has failed to play.
- (void)omRewardedVideoDidFailToShow:(OMScene*)scene withError:(NSError *)error;

@end


@interface OMRewardedVideo : NSObject

/// Returns the singleton instance.
+ (instancetype)sharedInstance;

/// Add delegate
- (void)addDelegate:(id<OMRewardedVideoDelegate>)delegate;

/// Remove delegate
- (void)removeDelegate:(id<OMRewardedVideoDelegate>)delegate;

/// Indicates whether the rewarded video is ready to show ad.
- (BOOL)isReady;

/// Indicates whether the scene has reached the display frequency.
- (BOOL)isCappedForScene:(NSString *)sceneName;

/// Presents the rewarded video ad modally from the specified view controller.
/// Parameter viewController: The view controller that will be used to present the video ad.
/// Parameter sceneName: The name of th ad scene.
- (void)showWithViewController:(UIViewController *)viewController scene:(NSString *)sceneName;

/// Presents the rewarded video ad modally from the specified view controller.
/// Parameter viewController: The view controller that will be used to present the video ad.
/// Parameter sceneName: The name of th ad scene. Default scene if null.
/// Parameter extraParams: Exciting video Id.
- (void)showWithViewController:(UIViewController *)viewController scene:(NSString *)sceneName extraParams:(NSString*)extraParams;

@end

@protocol OMCrossPromotionDelegate <NSObject>

@optional

/// Invoked when a rewarded video is available.
- (void)omCrossPromotionChangedAvailability:(BOOL)available;

/// Sent immediately when promotion ad will appear.
- (void)omCrossPromotionWillAppear:(OMScene*)scene;

/// Sent after a promotion ad has been clicked.
- (void)omCrossPromotionDidClick:(OMScene*)scene;

/// Sent after a promotion ad did sdisappear.
- (void)omCrossPromotionDidDisappear:(OMScene*)scene;

/// Sent after a promotion video has failed to play.
- (void)omCrossPromotionDidFailToShow:(OMScene*)scene withError:(NSError *)error;

@end

@interface OMCrossPromotion : NSObject
/// Returns the singleton instance.
+ (instancetype)sharedInstance;

/// Add delegate
- (void)addDelegate:(id<OMCrossPromotionDelegate>)delegate;

/// Remove delegate
- (void)removeDelegate:(id<OMCrossPromotionDelegate>)delegate;

/// Indicates whether the promotion ad is ready to show ad.
- (BOOL)isReady;

/// Indicates whether the scene has reached the display frequency.
- (BOOL)isCappedForScene:(NSString *)sceneName;

/// Show promotion ad on top view
/// Parameter scaleXY: the value is a CGPonit, x is width percentage, y is height percentage. eg screen center CGPointMake(0.5,0.5)
/// Parameter scene: the value is an NSString, ad scene name in AdTiming dashboard setting.
- (void)showAdWithscreenPoint:(CGPoint)scaleXY scene:(NSString *)sceneName;


/// Show promotion ad on top view.
/// @param scaleXY  The value is a CGPonit, x is width percentage, y is height percentage. eg screen center CGPointMake(0.5,0.5)
/// @param size  ad size
/// @param angle  Rotated angle in clockwise.
/// @param sceneName  The value is an NSString, ad scene name in OpenMediation dashboard setting.
- (void)showAdWithScreenPoint:(CGPoint)scaleXY adSize:(CGSize)size angle:(CGFloat) angle scene:(NSString *)sceneName;

/// Hide promotion ad.
- (void)hideAd;

@end


/// Banner Ad Size
typedef NS_ENUM(NSInteger, OMBannerType) {
    OMBannerTypeDefault = 0,        ///ad size: 320 x 50
    OMBannerTypeMediumRectangle = 1,///ad size: 300 x 250
    OMBannerTypeLeaderboard = 2,    ///ad size: 728x90
    OMBannerTypeSmart = 3           ///phone ad size:320x50, pad ad size:728x90
};

/// Banner Ad layout attribute
typedef NS_ENUM(NSInteger, OMBannerLayoutAttribute) {
    OMBannerLayoutAttributeTop = 0,
    OMBannerLayoutAttributeLeft = 1,
    OMBannerLayoutAttributeBottom = 2,
    OMBannerLayoutAttributeRight = 3,
    OMBannerLayoutAttributeHorizontally = 4,
    OMBannerLayoutAttributeVertically = 5
};


@class OMBanner;


/// The methods declared by the OMBannerDelegate protocol allow the adopting delegate to respond to messages from the OMBanner class and thus respond to operations such as whether the ad has been loaded, the person has clicked the ad.
@protocol OMBannerDelegate<NSObject>

@optional

/// Sent when an ad has been successfully loaded.
- (void)omBannerDidLoad:(OMBanner *)banner;

/// Sent after an OMBanner fails to load the ad.
- (void)omBanner:(OMBanner *)banner didFailWithError:(NSError *)error;

/// Sent immediately before the impression of an OMBanner object will be logged.
- (void)omBannerWillExposure:(OMBanner *)banner;

/// Sent after an ad has been clicked by the person.
- (void)omBannerDidClick:(OMBanner *)banner;

/// Sent when a banner is about to present a full screen content
- (void)omBannerWillPresentScreen:(OMBanner *)banner;

/// Sent after a full screen content has been dismissed.
- (void)omBannerDidDismissScreen:(OMBanner *)banner;

 /// Sent when a user would be taken out of the application context.
- (void)omBannerWillLeaveApplication:(OMBanner *)banner;

@end

/// A customized UIView to represent a OpenMediation ad (banner ad).
@interface OMBanner : UIView

@property(nonatomic, readonly, nullable) NSString *placementID;

/// the delegate
@property (nonatomic, weak)id<OMBannerDelegate> delegate;

/// The banner's ad placement ID.
- (NSString*)placementID;


/// This is a method to initialize an OMBanner.
/// type: The size of the ad. Default is OMBannerTypeDefault.
/// placementID: Typed access to the id of the ad placement.
- (instancetype)initWithBannerType:(OMBannerType)type placementID:(NSString *)placementID;

/// set the banner position.
- (void)addLayoutAttribute:(OMBannerLayoutAttribute)attribute constant:(CGFloat)constant;

/// Begins loading the OMBanner content. And to show with default controller([UIApplication sharedApplication].keyWindow.rootViewController) when load success.
- (void)loadAndShow;

@end


@class OMSplash;

@protocol OMSplashDelegate<NSObject>

@optional

- (void)omSplashDidLoad:(OMSplash *)splash;

- (void)omSplashFailToLoad:(OMSplash *)splash withError:(NSError *)error;

- (void)omSplashDidShow:(OMSplash *)splash;

- (void)omSplashDidClick:(OMSplash *)splash;

- (void)omSplashDidClose:(OMSplash *)splash;

- (void)omSplashDidFailToShow:(OMSplash *)splash withError:(NSError *)error;

@end

@interface OMSplash : NSObject

@property (nonatomic, weak)id<OMSplashDelegate> delegate;

- (instancetype)initWithPlacementId:(NSString *)placementId adSize:(CGSize)size;

- (void)loadAd;

- (BOOL)isReady;

- (void)showWithWindow:(UIWindow *)window customView:(nullable UIView *)customView;

- (NSString*)placementID;
@end

@interface OMIronSourceAdapter : NSObject
@property (class, nonatomic) BOOL mediationAPI;
@end

#endif /* OMClass_h */

