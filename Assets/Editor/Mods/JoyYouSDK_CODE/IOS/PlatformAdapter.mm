
#import "PlatformAdapter.h"
#ifdef PLATFORM_ID_PP
#import <PPAppPlatformKit/PPAppPlatformKit.h>
#elif defined PLATFORM_ID_ITOOLS
#endif

#include "../PN.h"

@implementation JoyYouAdapter
{
	DECL_PN_PN_OBJ_PTR();
}

static JoyYouAdapter* jya_instance = nil;
	
+(JoyYouAdapter *)sharedInstance
{
	@synchronized(self)
	{
		if (jya_instance == nil)
		{
			jya_instance = [[JoyYouAdapter alloc] init];
		}
	}
	return jya_instance;
}

-(void)InitPNObjectWithParam:(NSString*)param
{
	CREATE_PNOBJECT();
    PN_SET_PARAMS_VOID_PTR((void *)[param UTF8String]);
}

// 平台初始化
-(void) JY_Init:(int)appId										// 平台 appId							
	appKey:(NSString*) theKey 									// 平台 appKey
	logEnable:(BOOL)isEnable 									// 是否启用 平台SDK 日志
	rechargeEnable:(BOOL)isRecEnable							// 是否启用 支付功能
	rechargeAmount:(int)recAmount								// 支付界面初始支付金额
	longConnect:(BOOL)isLongConnect								// 是否使用长连接
	closeRechargeAlertMessage:(NSString*)message				// 支付界面关闭后的回显信息
	landscapeLeft:(BOOL)isLsLeft								// 屏幕方向支持LandscapeLeft
	landscapeRight:(BOOL)isLsRight								// 屏幕方向支持bLandscapeRight
	portrait:(BOOL)isPort										// 屏幕方向支持bPortrait
	portraitUpsideDown:(BOOL)isPortUD							// 屏幕方向支持PortraitUpsideDown
{
#ifdef PLATFORM_ID_PP
	[[PPAppPlatformKit sharedInstance] setAppId:appId AppKey:theKey];
    [[PPAppPlatformKit sharedInstance] setIsNSlogData:NO];
    [[PPAppPlatformKit sharedInstance] setRechargeAmount:recAmount];
    [[PPAppPlatformKit sharedInstance] setIsLongComet:isLongConnect];
    [[PPAppPlatformKit sharedInstance] setIsLogOutPushLoginView:NO];
    [[PPAppPlatformKit sharedInstance] setIsOpenRecharge:isRecEnable];
    [[PPAppPlatformKit sharedInstance] setCloseRechargeAlertMessage:message];
    
    [[PPUIKit sharedInstance] checkGameUpdate];
    [PPUIKit setIsDeviceOrientationLandscapeLeft:isLsLeft];
    [PPUIKit setIsDeviceOrientationLandscapeRight:isLsRight];
    [PPUIKit setIsDeviceOrientationPortrait:isPort];
    [PPUIKit setIsDeviceOrientationPortraitUpsideDown:isPortUD];
    
    [[PPAppPlatformKit sharedInstance] setDelegate:self];
    [[UIApplication sharedApplication] setStatusBarHidden:YES];
#elif defined PLATFORM_ID_ITOOLS
#endif	
}

// 登录
-(void) JY_Login
{
#ifdef PLATFORM_ID_PP
	[[PPAppPlatformKit sharedInstance] showLogin];
#elif defined PLATFORM_ID_ITOOLS
#endif
}
// 登出										
-(void) JY_Logout
{
#ifdef PLATFORM_ID_PP
	[[PPAppPlatformKit sharedInstance] PPlogout];
#elif defined PLATFORM_ID_ITOOLS
#endif
}
// 用户中心										
-(void) JY_UserCentered
{
#ifdef PLATFORM_ID_PP
	[[PPAppPlatformKit sharedInstance] showCenter];
#elif defined PLATFORM_ID_ITOOLS
#endif
}
// 支付 									
-(void) JY_Pay:(int)priceOfGoods 								// 价格 单位为元
	indentNumber:(NSString*)indNumber 							// 厂商订单号 （64字符以内，根据具体平台而定）
	payTitle:(NSString*)title									// 支付界面标题
	roleId:(NSString*)role_id									// 角色id
	zoneId:(int)zone_id											// 分区id
{
#ifdef PLATFORM_ID_PP
	[[PPAppPlatformKit sharedInstance] 
		exchangeGoods:priceOfGoods
		BillNo:indNumber
        BillTitle:title
       RoleId:role_id
       ZoneId:zone_id];
#elif defined PLATFORM_ID_ITOOLS
#endif
}

#ifdef PLATFORM_ID_PP
#pragma mark --- PP Events -----
/**
 * @brief   余额大于所购买道具
 * @param   INPUT   paramPPPayResultCode       接口返回的结果编码
 * @return  无返回
 */
- (void)ppPayResultCallBack:(PPPayResultCode)paramPPPayResultCode
{
	PN_ON_PAY_REPLAY(paramPPPayResultCode==PPPayResultCodeSucceed);
}
/**
 * @brief   验证更新成功后
 * @noti    分别在非强制更新点击取消更新和暂无更新时触发回调用于通知弹出登录界面
 * @return  无返回
 */
- (void)ppVerifyingUpdatePassCallBack
{
	PN_ON_VERIFY_UPDATE();
}
/**
 * @brief   登录成功回调【任其一种验证即可】
 * @param   INPUT   paramStrToKenKey       字符串token
 * @return  无返回
 */
- (void)ppLoginStrCallBack:(NSString *)paramStrToKenKey
{
    [[PPAppPlatformKit sharedInstance] getUserInfoSecurity];
	PN_ON_LOGIN([paramStrToKenKey UTF8String]);
}
/**
 * @brief   关闭Web页面后的回调
 * @param   INPUT   paramPPWebViewCode    接口返回的页面编码
 * @return  无返回
 */
- (void)ppCloseWebViewCallBack:(PPWebViewCode)paramPPWebViewCode
{

}
/**
 * @brief   关闭SDK客户端页面后的回调
 * @param   INPUT   paramPPPageCode       接口返回的页面编码
 * @return  无返回
 */
- (void)ppClosePageViewCallBack:(PPPageCode)paramPPPageCode
{
	if (paramPPPageCode == PPOtherViewPageCode)
	{
		PN_ON_CENTER_CLOSED();
	}
    else
    {
        [self JY_Login];
    }
}
/**
 * @brief   注销后的回调
 * @return  无返回
 */
- (void)ppLogOffCallBack
{
	PN_ON_LOGOUT();
}

#elif defined PLATFORM_ID_ITOOLS
#endif


@end