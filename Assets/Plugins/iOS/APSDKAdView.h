//
//  APSDKAdView.h
//  AMoAdSDK
//
//  Copyright © CyberAgent, Inc. All Rights Reserved.
//
//

#import <UIKit/UIKit.h>

@interface APSDKAdView : UIImageView<UIWebViewDelegate>

@property (nonatomic, strong)NSString* adType;
-(void)loadAd;
-(instancetype)setAttributes:(NSDictionary*)params;

@end
