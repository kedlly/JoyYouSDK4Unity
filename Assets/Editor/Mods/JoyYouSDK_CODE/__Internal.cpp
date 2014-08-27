//
//  __Internal.cpp
//  SDKDEMO
//
//  Created by kedlly on 8/26/14.
//  Copyright (c) 2014 Seven. All rights reserved.
//

#include "__Internal.h"
#include "JoyYouSDK.h"

void U3D_initSDK(int appId, const char * appKey, bool logEnable, int rechargeAmount,
                 bool isLogConnect, bool rechargeEnable,
                 const char * closeRechargeAlertMessage,
                 const char * paramSendMsgNotiClass,
                 bool isOriPortrait,
                 bool isOriLandscapeLeft,
                 bool isOriLandscapeRight,
                 bool isOriPortraitUpsideDown
                 )
{
    JoyYouSDK::Init(appId, appKey, logEnable, rechargeEnable, rechargeAmount, isLogConnect, closeRechargeAlertMessage, isOriPortrait, isOriLandscapeLeft, isOriLandscapeRight, isOriPortraitUpsideDown, paramSendMsgNotiClass);
}

void U3D_showLoginView()
{
    JoyYouSDK::Login();
}

void U3D_showCenterView(){
    JoyYouSDK::ShowUserCentered();
}

void U3D_exchangeGoods(int paramPrice, const char * paramBillNo, const char * paramBillTitle,
                       const char * paramRoleId, int paramZoneId){
    JoyYouSDK::Pay(paramPrice, paramBillNo, paramBillTitle, paramRoleId, paramZoneId);
}
void U3D_logout(){
    JoyYouSDK::Logout();
}