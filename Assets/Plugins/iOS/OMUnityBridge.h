// Copyright 2020 ADTIMING TECHNOLOGY COMPANY LIMITED
// Licensed under the GNU Lesser General Public License Version 3

#import <Foundation/Foundation.h>
#import "OMClass.h"
NS_ASSUME_NONNULL_BEGIN

@interface OMUnityBridge : NSObject<OMInterstitialDelegate,OMRewardedVideoDelegate,OMBannerDelegate,OMCrossPromotionDelegate,OMSplashDelegate,OMImpressionDataDelegate>
@property (nonatomic, strong) NSMutableDictionary* bannerAdMap;
@property (nonatomic, strong) NSMutableDictionary* bannerPostionMap;
@property (nonatomic, strong) NSMutableDictionary* splashAdMap;
+ (instancetype)sharedInstance;
//init
- (void)initWithAppKey:(NSString*)appKey;

- (void)initWithAppKey:(NSString*)appKey host:(NSString*)host;

//interstitial
- (BOOL)interstitialIsReady;

- (void)showInterstitial;

- (void)showInterstitialWithScene:(NSString *)scene;

//video
- (BOOL)videoIsReady;

- (void)showVideo;

- (void)showVideoWithScene:(NSString *)scene;

- (void)showVideoWithExtraParams:(NSString *)scene extraParams:(NSString *)extraParams;

//CrossPromotion
- (BOOL)promotionAdIsReady;

- (void)showPromotionWithScene:(NSString*)scene width:(NSInteger)width height:(NSInteger)height scaleX:(CGFloat)scaleX scaleY:(CGFloat)scaleY  angle:(CGFloat)angle;

- (void)hidePromotionAd;

//Banner
- (void)loadBanner:(NSInteger)bannerType position:(NSInteger)position placement:(NSString *)placementID;

- (void)destroyBanner:(NSString*)placementId;

- (void)displayBanner:(NSString*)placementId;

- (void)hideBanner:(NSString*)placementId;

//SplashAd
- (void)loadSplashAd:(NSString*)placementId;

- (BOOL)splashAdIsReady:(NSString*)placementId;

- (void)showSplashAd:(NSString*)placementId;

@end

NS_ASSUME_NONNULL_END
