
#include "JoyYouSDK.h"
#include "SDKDef.h"
#ifdef SYS_IOS
#include "PlatformAdapter.h"
#endif

void JoyYouSDK::Init(										// 平台初始化
	int appId, 												// 平台 appId
	const char * appKey, 									// 平台 appKey
	bool logEnable, 										// 是否启用 平台SDK 日志
	bool rechargeEnable,									// 是否启用 支付功能
	int rechargeAmount,										// 支付界面初始支付金额
	bool isLogConnect, 										// 是否使用长连接
	const char* closeRechargeAlertMessage,					// 支付界面关闭后的回显信息
    bool bPortrait,											// 屏幕方向支持bPortrait
	bool bLandscapeLeft,									// 屏幕方向支持LandscapeLeft
	bool bLandscapeRight,									// 屏幕方向支持bLandscapeRight
	bool bPortraitUpsideDown,								// 屏幕方向支持PortraitUpsideDown
    const char * params
)
{
#ifdef SYS_IOS
	int _appId = appId;
	NSString * _appKey = [[NSString alloc] initWithUTF8String:appKey];
	BOOL _logEnable = logEnable ? YES : NO;
	BOOL _rechargeEnable = rechargeEnable ? YES : NO;
	int _rechargeAmount = rechargeAmount;
	BOOL _isLogConnect = isLogConnect ? YES : NO;
	NSString * _closeRechargeAlertMessage = [[NSString alloc] initWithUTF8String:closeRechargeAlertMessage];
	BOOL _bLandscapeLeft = bLandscapeLeft ? YES : NO;
	BOOL _bLandscapeRight = bLandscapeRight ? YES : NO;
	BOOL _bPortrait = bPortrait ? YES : NO;
	BOOL _bPortraitUpsideDown = bPortraitUpsideDown ? YES : NO;
    NSString * _params = [[NSString alloc] initWithUTF8String:params];
	[[JoyYouAdapter sharedInstance] InitPNObjectWithParam:_params];
    [_params release];
	[[JoyYouAdapter sharedInstance] 
		JY_Init:_appId 
		appKey:_appKey
		logEnable:_logEnable
		rechargeEnable:_rechargeEnable
		rechargeAmount:_rechargeAmount
		longConnect:_isLogConnect
		closeRechargeAlertMessage:_closeRechargeAlertMessage
		landscapeLeft:_bLandscapeLeft
		landscapeRight:_bLandscapeRight
		portrait:_bPortrait
		portraitUpsideDown:_bPortraitUpsideDown
		];
    [_appKey release];
    [_closeRechargeAlertMessage release];
#else
#endif
}

void JoyYouSDK::Login()										// 登录
{
#ifdef SYS_IOS
	[[JoyYouAdapter sharedInstance] JY_Login];
#else
#endif
}

void JoyYouSDK::Logout()									// 登出
{
#ifdef SYS_IOS
	[[JoyYouAdapter sharedInstance] JY_Logout];
#else
#endif
}
	
void JoyYouSDK::ShowUserCentered()							// 用户中心
{
#ifdef SYS_IOS
	[[JoyYouAdapter sharedInstance] JY_UserCentered];
#else
#endif
}

void JoyYouSDK::Pay(										// 支付 
	int priceOfGoods, 										// 价格 单位为元
	const char *indentNumber, 								// 厂商订单号 （64字符以内，根据具体平台而定）
	const char *title,										// 支付界面标题
	const char *roleId,										// 角色id
	int zoneId												// 分区id
)
{
#ifdef SYS_IOS
	int _priceOfGoods = priceOfGoods;
	NSString * _indentNumber = [[NSString alloc] initWithUTF8String:indentNumber];
	NSString * _title = [[NSString alloc] initWithUTF8String:title];
	NSString * _roleId = [[NSString alloc] initWithUTF8String:roleId];
	int _zoneId = zoneId;
	[[JoyYouAdapter sharedInstance] 
		JY_Pay:_priceOfGoods
		indentNumber:_indentNumber
		payTitle:_title
		roleId:_roleId
		zoneId:_zoneId
		];
#else
#endif
}