//
//  AMoAdSDK.h
//  AMoAdSDK
//
//  Copyright © CyberAgent, Inc. All Rights Reserved.
//

#import <UIKit/UIKit.h>
#import "APSDKAd.h"
#import "APSDKAdView.h"

@interface AMoAdSDK : NSObject {
}
#define APSDK_Ad_Type                   @"APSDKAdType"
#define	APSDK_Ad_Type_Trigger_Wall      @"APSDKImgTrigger" //トリガーからウォール（プリロール）へ遷移する広告フォーマット
#define APSDK_Ad_Key_TriggerId          @"APSDKImgTriggerId"
#define APSDK_Ad_Key_AppKey             @"APSDKAppKey"
#define APSDK_Ad_Key_AutoLoad           @"APSDKAutoLoad"
#define APSDK_Ad_Key_WallDrawSetting    @"APSDKWallDrawSetting"
#define APSDK_Ad_Key_WallDrawSetting_underStatusBar     @"APSDKWallDrawSetting_underStatusBar"
#define APSDK_Ad_Key_WallDrawSetting_belowStatusBar     @"APSDKWallDrawSetting_belowStatusBar"
#define APSDK_Ad_Key_WallDrawSetting_hiddenStatusBar    @"APSDKWallDrawSetting_hiddenStatusBar"
#define APSDK_Ad_Key_orientation        @"APSDKOrientation"

#define	APSDK_Ad_Type_icon                      @"APSDKIcon" //アイコン広告フォーマット
#define APSDK_Ad_Key_icon_app_key               @"ak"
#define APSDK_Ad_Key_icon_ad_count              @"pl"
#define APSDK_Ad_Key_icon_size_px               @"isz"
#define APSDK_Ad_Key_icon_size_px_with_mergin   @"iuz"
#define APSDK_Ad_Key_icon_title_visible         @"idt"
#define APSDK_Ad_Key_icon_title_size            @"fsz"
#define APSDK_Ad_key_icon_title_color           @"itc"
#define APSDK_icon_onSuccessBlock       @"APSDK_icon_onSuccessBlock"
#define APSDK_icon_onFailureBlock       @"APSDK_icon_onFailureBlock"
#define APSDK_icon_onClickBlock         @"APSDK_icon_onClickBlock"


#pragma mark Wall広告
//----------------------------------------------------------------------------------------------------
+ (UIViewController *) showAppliPromotionWall:(UIViewController *)owner;
+ (UIViewController *) showAppliPromotionWall:(UIViewController *)owner
                                  orientation:(UIInterfaceOrientation)orientation;
+ (UIViewController *) showAppliPromotionWall:(UIViewController *)owner onStatusArea:(BOOL)onStatusArea;
+ (UIViewController *) showAppliPromotionWall:(UIViewController *)owner
                                  orientation:(UIInterfaceOrientation)orientation
                                 onStatusArea:(BOOL)onStatusArea;

/**
 *  ウォール広告を表示します
 *
 *  @param owner           現在表示中のUIViewControllerのインスタンス
 *  @param orientation     ウォール広告の表示向き
 *  @param wallDrawSetting ウォール広告描画モード
 *  @param appKey          管理画面で発行されたアプリケーション識別ID
 *
 *  @return
 */
+ (UIViewController *) showAppliPromotionWall:(UIViewController *)owner
                                  orientation:(UIInterfaceOrientation)orientation
                              wallDrawSetting:(NSString*)wallDrawSetting
                                       appKey:(NSString*)appKey
                             onWallCloseBlock:(void(^)())onWallCloseBlock;

#pragma mark Wall誘導枠（画像）
//----------------------------------------------------------------------------------------------------

/**
 *  Wall誘導画像取得メソッド
 *
 *  @param triggerID     Wall誘導枠ID
 *  @param callbackBlock 通信後、sts:ステータス, url:画像URL, width:画像の幅, height:画像の高さ が返却されるブロックになります。
 */
+ (void)sendTriggerID:(NSString *)triggerID
        callbackBlock:(void(^)(NSInteger sts, NSString *url, NSInteger width, NSInteger height))callbackBlock;
+ (void)sendTriggerID:(NSString *)triggerID
               appKey:(NSString *)appKey
        callbackBlock:(void(^)(NSInteger sts, NSString *url, NSInteger width, NSInteger height))callbackBlock;

+ (void)sendTriggerID:(UIViewController *)owner trigger:(NSString *)TriggerID callback:(SEL)callback __attribute__ ((deprecated));

/*
 * Wall誘導枠画像が押下された時に呼び出すメソッド。呼び出すとWallが表示されます。
 * createTriggerImageAdWithTriggerId:isAutoLoad:subImageFileName:wallDrawSetting:orientation:appKey:や
 * showTriggerImageAdWithViewController:TriggerID:locateX:locateY:subImageFileName:wallDrawSetting:orientation:appKey:
 * を使用した場合は使用しないでください。
 */
+ (void)pushTrigger:(UIViewController *)owner
		  TriggerID:(NSString *)triggerID;
+ (void)pushTrigger:(UIViewController *)owner
		orientation:(UIInterfaceOrientation)orientation
		  TriggerID:(NSString *)triggerID;
+ (void)pushTrigger:(UIViewController *)owner
		  TriggerID:(NSString *)triggerID
	   onStatusArea:(BOOL)onStatusArea;
+ (void)pushTrigger:(UIViewController *)owner
		orientation:(UIInterfaceOrientation)orientation
		  TriggerID:(NSString *)triggerID
	   onStatusArea:(BOOL)onStatusArea;

/**
 *  Wall誘導枠画像が設定された広告用Viewを返却します。
 *
 *  @param triggerID  Wall誘導枠ID
 *  @param isAutoLoad 自動的にトリガー画像を表示するかどうか。
 *　                   YES:自動的に広告を読み込み。NO:広告を表示したいタイミングでAPSDKAdView loadAdメソッドをコール
 *  @param subImageFileName 圏外時などでWall誘導枠画像を取得できなかったときに使用する代替画像のファイル名
 *  @param wallDrawSetting  ウォール広告描画モード
 *  @param orientation  ウォールの起動向き
 *  @param appKey           管理画面で発行されたアプリケーション識別ID
 *
 *  @return 広告用View
 */
+(APSDKAdView*)createTriggerImageAdWithTriggerId:(NSString*)triggerID
                                      isAutoLoad:(BOOL)isAutoLoad
                                subImageFileName:(NSString*)subImageFileName
                                 wallDrawSetting:(NSString*)wallDrawSetting
                                     orientation:(UIInterfaceOrientation)orientation
                                          appKey:(NSString*)appKey;

+(void)showTriggerImageAdWithViewController:(UIViewController *)owner
                                  TriggerID:(NSString *)triggerID
                                    locateX:(CGFloat)locateX
                                    locateY:(CGFloat)locateY
                           subImageFileName:(NSString*)subImageFileName
                            wallDrawSetting:(NSString*)wallDrawSetting
                                orientation:(UIInterfaceOrientation)orientation
                                     appKey:(NSString*)appKey;
+(void)hideAllTriggerImageAd;

#pragma mark Wall誘導枠（ポップアップ型）
//----------------------------------------------------------------------------------------------------

/*
 * ポップアップ型のウォール誘導ダイアログを表示。
 */
+ (void)popupDisp:(UIViewController *)owner
		TriggerID:(NSString *)triggerID;
+ (void)popupDisp:(UIViewController *)owner
	  orientation:(UIInterfaceOrientation)orientation
		TriggerID:(NSString *)triggerID;
+ (void)popupDisp:(UIViewController *)owner
		TriggerID:(NSString *)triggerID
	 onStatusArea:(BOOL)onStatusArea;
+ (void)popupDisp:(UIViewController *)owner
	  orientation:(UIInterfaceOrientation)orientation
		TriggerID:(NSString *)triggerID
	 onStatusArea:(BOOL)onStatusArea;
+ (void)popupDisp:(UIViewController *)owner
		TriggerID:(NSString *)triggerID
		 callback:(SEL)callback;
+ (void)popupDisp:(UIViewController *)owner
	  orientation:(UIInterfaceOrientation)orientation
		TriggerID:(NSString *)triggerID
		 callback:(SEL)callback;
+ (void)popupDisp:(UIViewController *)owner
		TriggerID:(NSString *)triggerID
	 onStatusArea:(BOOL)onStatusArea
		 callback:(SEL)callback;
+ (void)popupDisp:(UIViewController *)owner
	  orientation:(UIInterfaceOrientation)orientation
		TriggerID:(NSString *)triggerID
	 onStatusArea:(BOOL)onStatusArea
		 callback:(SEL)callback;

/**
 *  ポップアップ型ウォール広告誘導枠を表示します。
 *
 *  @param owner           現在表示中のUIViewControllerのインスタンス
 *  @param orientation     ウォールの起動向き
 *  @param triggerID       管理画面で発行されたウォール誘導枠ID
 *  @param wallDrawSetting ウォール広告描画モード
 *  @param appKey          管理画面で発行されたアプリケーション識別ID
 *  @param failureBlock    通信失敗時のコールバック
 *  @param completionBlock コールバック
 */
+ (void)popupDisp:(UIViewController *)owner
      orientation:(UIInterfaceOrientation)orientation
        triggerId:(NSString*)triggerID
  wallDrawSetting:(NSString*)wallDrawSetting
           appKey:(NSString*)appKey
     failureBlock:(void(^)(NSInteger resSts))failureBlock
  completionBlock:(void(^)(NSInteger resSts))completionBlock;

/*
 * Wallの表示が初めてかどうかのチェック.
 */
+ (BOOL)isFirstTimeInToday;

/*
 * UUIDの送信.
 */
+ (void)sendUUID;

/**
 *  アイコン広告
 *
 *  @param frame          アイコンの描画領域
 *  @param appKey         アプリケーション識別ID
 *  @param count          表示したいアイコン数
 *  @param onSuccessBlock 表示成功時
 *  @param onFailureBlock 表示失敗時
 *  @param onClickBlock   クリック時
 *
 *  @return
 */
+(APSDKAdView*)getIconViewWithFrame:(CGRect)frame
                             appKey:(NSString*)appKey
                              count:(NSInteger)count
                     onSuccessBlock:(void(^)(void))onSuccessBlock
                     onFailureBlock:(void(^)(void))onFailureBlock
                       onClickBlock:(void(^)(void))onClickBlock;

+(APSDKAdView*)getIconViewWithFrame:(CGRect)frame params:(NSDictionary*)params;

+(void)setParams:(NSDictionary*)params;

+(void)initSDK;

@end

@interface AMoAdSDK (APSDKLowLevelAPI)

+(void)getAdsWithCount:(NSInteger)count completionBlock:(void(^)(NSInteger sts, NSArray* ads))completionBlock;
+(void)getAdsWithCount:(NSInteger)count appKey:(NSString*)appKey llKey:(NSString*)llKey completionBlock:(void(^)(NSInteger sts, NSArray* ads))completionBlock;
+(void)getAdsEnableExludeAppsWithCount:(NSInteger)count completionBlock:(void(^)(NSInteger sts, NSArray* ads))completionBlock;
+(void)getAdsEnableExludeAppsWithCount:(NSInteger)count appKey:(NSString*)appKey llKey:(NSString*)llKey completionBlock:(void(^)(NSInteger sts, NSArray* ads))completionBlock;
+(void)clickWithAd:(APSDKAd*)ad;

@end

