//
//  ABUDCustomDrawAdHelper.m
//  ABUDemo
//
//  Created by ByteDance on 2022/5/6.
//  Copyright © 2022 bytedance. All rights reserved.
//

#import "ABUDCustomDrawAdHelper.h"
#import "ABUDCustomDrawData.h"

@interface ABUDCustomDrawAdHelper ()

@property (nonatomic, strong) ABUDCustomDrawData *data;

@property (nonatomic, strong) UILabel *adTitleLabel;

@property (nonatomic, strong) UIImageView *adImageView;

@end

@implementation ABUDCustomDrawAdHelper

- (instancetype)initWithAdData:(ABUDCustomDrawData *)data{
    if (self = [super init]) {
        _data = data;
        _adTitleLabel = [[UILabel alloc] init];
        _adImageView = [[UIImageView alloc] init];
    }
    return self;
}

#pragma mark - ABUMediatedNativeAdViewCreator
- (UILabel *)titleLabel {
    return self.adTitleLabel;
}

#pragma mark - ABUMediatedNativeAdData
- (NSString *)AdTitle {
    return self.data.title;
}

- (NSString *)AdDescription {
    return self.data.subtitle;
}

- (ABUImage *)adLogo {
    ABUImage *img = [[ABUImage alloc] init];
    img.width = 30;
    img.height = 30;
    img.image = self.data.logoView;
    return img;
}

- (ABUImage *)sdkLogo {
    return self.adLogo;
}

- (ABUMediatedNativeAdMode)imageMode {
    return ABUMediatedNativeAdModeLargeImage;
}
@end
