//
//  APUnityPlugin.m
//
//  Created by KazukiOhashi on 13/10/23.
//  Copyright (c) 2013年 AMoAd inc. All rights reserved.
//

#import "APUnityPlugin.h"

extern UIViewController* UnityGetGLViewController();
extern "C" {
    void showAppliPromotionWall_(const char* orientation, bool isClose, bool onStatusArea);
    BOOL isFirstTimeInToday_();
    void sendUUID_();
    void sendTriggerID_(const char*, const char*);
    void pushTrigger_(const char*, const char*, bool onStatusArea);
    void showTriggerImageAd_(const char* triggerId, int locateX, int locateY, bool onStatusArea, const char* orientation, const char* appKey);
    void popupDisp_(const char*, const char*, bool onStatusArea, const char*);
    
    NSString* APcharToString(const char*);
    UIViewController* APgetUnityViewController();
    UIInterfaceOrientation getUIInterfaceOrientationType(NSString *type);
    void setNotification(APUnityDelegate* callback);
    APUnityDelegate *callback;
    
}
#pragma mark - bridge

void showAppliPromotionWall_(const char* orientation, bool isClose, bool onStatusArea) {
    NSString* strOrientation = APcharToString(orientation);
    UIViewController* parent = APgetUnityViewController();
    UIViewController *amoAdView;
    
    if ([strOrientation length] == 0) {
        amoAdView = [AMoAdSDK
                     showAppliPromotionWall:parent
                     onStatusArea:onStatusArea];
    }
    else if ([strOrientation length] > 0) {
        amoAdView = [AMoAdSDK
                     showAppliPromotionWall:parent
                     orientation:getUIInterfaceOrientationType(strOrientation)
                     onStatusArea:onStatusArea];
    }
    if (isClose) {
        if (callback != NULL) {
            NSNotificationCenter *nc = [NSNotificationCenter defaultCenter];
            [nc removeObserver:callback name:UIApplicationDidEnterBackgroundNotification object:nil];
            [callback release];
        }
        callback = [[APUnityDelegate alloc]init];
        [callback setCloseView:amoAdView parentView:parent];
        setNotification(callback);
    }
}

BOOL isFirstTimeInToday_() {
    return [AMoAdSDK isFirstTimeInToday];
}

void sendUUID_() {
    [AMoAdSDK sendUUID];
}

#define UNITY_RESPONSE_PARSECHAR @","
void sendTriggerID_(const char* triggerId, const char* objName) {
    NSString* strCallback = APcharToString(objName);
    NSString* strTriggerId = APcharToString(triggerId);
    
    if(!strCallback){
        return;
    }
    
    [AMoAdSDK sendTriggerID:strTriggerId callbackBlock:^(NSInteger sts, NSString *url, NSInteger width, NSInteger height) {
        NSString *resStr = [NSString stringWithFormat:@"%d%@%@%@%d%@%d",
                            sts, UNITY_RESPONSE_PARSECHAR,
                            url, UNITY_RESPONSE_PARSECHAR,
                            width, UNITY_RESPONSE_PARSECHAR,
                            height];
        
        const char* resChar = [resStr UTF8String];
        const char* objName = [strCallback UTF8String];
        UnitySendMessage(objName, "buttonReady", resChar);
        
    }];
}

void pushTrigger_(const char* orientation, const char* triggerId, bool onStatusArea)
{
    NSString* strOrientation = APcharToString(orientation);
    UIViewController* parent = APgetUnityViewController();
    NSString* strTriggerId = APcharToString(triggerId);
    
    [AMoAdSDK
     pushTrigger:parent
     orientation:getUIInterfaceOrientationType(strOrientation)
     TriggerID:strTriggerId
     onStatusArea:onStatusArea];
}

void showTriggerImageAd_(const char* triggerId, int locateX, int locateY, bool onStatusArea, const char* orientation, const char* appKey){
    NSString* strTriggerId = APcharToString(triggerId);
    NSString* strOrientation = APcharToString(orientation);
    UIInterfaceOrientation intOrientation = getUIInterfaceOrientationType(strOrientation);
    NSString* strAppKey = APcharToString(appKey);
    UIViewController *parent = APgetUnityViewController();
    NSString *wallDrawSetting = onStatusArea ? APSDK_Ad_Key_WallDrawSetting_hiddenStatusBar : APSDK_Ad_Key_WallDrawSetting_belowStatusBar;
    
    [AMoAdSDK showTriggerImageAdWithViewController:parent
                                         TriggerID:strTriggerId
                                           locateX:locateX
                                           locateY:locateY
                                  subImageFileName:nil
                                   wallDrawSetting:wallDrawSetting
                                       orientation:intOrientation
                                            appKey:strAppKey];
}
void popupDisp_(const char *orientation, const char *triggerId, bool onStatusArea, const char *objName)
{
    NSString *strOrientation = APcharToString(orientation);
    NSString *strCallbackObjName = APcharToString(objName);
    UIViewController *parent = APgetUnityViewController();
    NSString *strTriggerId = APcharToString(triggerId);
    NSString *wallDrawSetting = onStatusArea ? APSDK_Ad_Key_WallDrawSetting_hiddenStatusBar : APSDK_Ad_Key_WallDrawSetting_belowStatusBar;
    
    [AMoAdSDK popupDisp:parent
            orientation:getUIInterfaceOrientationType(strOrientation)
              triggerId:strTriggerId
        wallDrawSetting:wallDrawSetting
                 appKey:nil
           failureBlock:^(NSInteger resSts) {
               if([strCallbackObjName length] == 0){
                   return ;
               }
               const char* resChr = [[NSString stringWithFormat:@"%zd", resSts] UTF8String];
               const char* callBackObjName = [strCallbackObjName UTF8String];
               UnitySendMessage(callBackObjName, "returnPopup", resChr);
           } completionBlock:^(NSInteger resSts) {
               if([strCallbackObjName length] == 0){
                   return ;
               }
               const char* resChr = [[NSString stringWithFormat:@"%zd", resSts] UTF8String];
               const char* callBackObjName = [strCallbackObjName UTF8String];
               UnitySendMessage(callBackObjName, "returnPopup", resChr);
               
           }];
}


#pragma mark - private
NSString* APcharToString(const char* c) {
    return [NSString stringWithCString:c encoding:NSUTF8StringEncoding];
}

UIViewController* APgetUnityViewController() {
    return UnityGetGLViewController();
}

UIInterfaceOrientation getUIInterfaceOrientationType(NSString *type) {
    if([type isEqualToString:@"UIInterfaceOrientationPortrait"]) {
        return UIInterfaceOrientationPortrait;
    } else if ([type isEqualToString:@"UIInterfaceOrientationPortraitUpsideDown"]) {
        return UIInterfaceOrientationPortraitUpsideDown;
    } else if ([type isEqualToString:@"UIInterfaceOrientationLandscapeLeft"]) {
        return UIInterfaceOrientationLandscapeLeft;
    } else if ([type isEqualToString:@"UIInterfaceOrientationLandscapeRight"]) {
        return UIInterfaceOrientationLandscapeRight;
    } else {
        return UIInterfaceOrientationPortrait;
    }
}

void setNotification(APUnityDelegate* callback) {
    NSNotificationCenter *nc = [NSNotificationCenter defaultCenter];
    [nc addObserver:callback selector:@selector(applicationDidEnterBackground) name:UIApplicationDidEnterBackgroundNotification object:nil];
}

