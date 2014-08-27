#import "../SDKDef.h"
#import <Foundation/NSString.h>
#import <Foundation/Foundation.h>
#ifdef PLATFORM_ID_PP
#import <PPAppPlatformKit/PPAppPlatformKit.h>
#elif defined PLATFORM_ID_ITOOLS
#endif
@interface JoyYouAdapter : NSObject<
#ifdef PLATFORM_ID_PP
	PPAppPlatformKitDelegate
#elif defined PLATFORM_ID_ITOOLS
#endif
	>
-(void) InitPNObjectWithParam:(NSString*)param;
+ (JoyYouAdapter *)sharedInstance;
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
	portraitUpsideDown:(BOOL)isPortUD;							// 屏幕方向支持PortraitUpsideDown

// 登录
-(void) JY_Login;
// 登出										
-(void) JY_Logout;
// 用户中心										
-(void) JY_UserCentered;
// 支付 									
-(void) JY_Pay:(int)priceOfGoods 								// 价格 单位为元
	indentNumber:(NSString*)indNumber 							// 厂商订单号 （64字符以内，根据具体平台而定）
	payTitle:(NSString*)title									// 支付界面标题
	roleId:(NSString*)role_id									// 角色id
	zoneId:(int)zone_id;										// 分区id

@end