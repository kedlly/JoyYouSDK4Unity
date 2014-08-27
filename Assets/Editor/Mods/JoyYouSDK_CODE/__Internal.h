//
//  __Internal.h
//  SDKDEMO
//
//  Created by kedlly on 8/26/14.
//  Copyright (c) 2014 Seven. All rights reserved.
//

#ifndef __SDKDEMO____Internal__
#define __SDKDEMO____Internal__

#include <iostream>

extern "C"
{
    void U3D_initSDK(int appId, const char * appKey, bool logEnable, int rechargeAmount,
                                           bool isLogConnect, bool rechargeEnable,
                                           const char * closeRechargeAlertMessage,
                                           const char * paramSendMsgNotiClass,
                                           bool isOriPortrait,
                                           bool isOriLandscapeLeft,
                                           bool isOriLandscapeRight,
                                           bool isOriPortraitUpsideDown
                                           );
    void U3D_showLoginView();
    
    void U3D_showCenterView();
    
    void U3D_exchangeGoods(int paramPrice, const char * paramBillNo, const char * paramBillTitle,
                                                 const char * paramRoleId, int paramZoneId);
    void U3D_logout();
    
}

#endif /* defined(__SDKDEMO____Internal__) */

