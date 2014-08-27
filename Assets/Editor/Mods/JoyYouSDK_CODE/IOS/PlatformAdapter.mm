
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

// ƽ̨��ʼ��
-(void) JY_Init:(int)appId										// ƽ̨ appId							
	appKey:(NSString*) theKey 									// ƽ̨ appKey
	logEnable:(BOOL)isEnable 									// �Ƿ����� ƽ̨SDK ��־
	rechargeEnable:(BOOL)isRecEnable							// �Ƿ����� ֧������
	rechargeAmount:(int)recAmount								// ֧�������ʼ֧�����
	longConnect:(BOOL)isLongConnect								// �Ƿ�ʹ�ó�����
	closeRechargeAlertMessage:(NSString*)message				// ֧������رպ�Ļ�����Ϣ
	landscapeLeft:(BOOL)isLsLeft								// ��Ļ����֧��LandscapeLeft
	landscapeRight:(BOOL)isLsRight								// ��Ļ����֧��bLandscapeRight
	portrait:(BOOL)isPort										// ��Ļ����֧��bPortrait
	portraitUpsideDown:(BOOL)isPortUD							// ��Ļ����֧��PortraitUpsideDown
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

// ��¼
-(void) JY_Login
{
#ifdef PLATFORM_ID_PP
	[[PPAppPlatformKit sharedInstance] showLogin];
#elif defined PLATFORM_ID_ITOOLS
#endif
}
// �ǳ�										
-(void) JY_Logout
{
#ifdef PLATFORM_ID_PP
	[[PPAppPlatformKit sharedInstance] PPlogout];
#elif defined PLATFORM_ID_ITOOLS
#endif
}
// �û�����										
-(void) JY_UserCentered
{
#ifdef PLATFORM_ID_PP
	[[PPAppPlatformKit sharedInstance] showCenter];
#elif defined PLATFORM_ID_ITOOLS
#endif
}
// ֧�� 									
-(void) JY_Pay:(int)priceOfGoods 								// �۸� ��λΪԪ
	indentNumber:(NSString*)indNumber 							// ���̶����� ��64�ַ����ڣ����ݾ���ƽ̨������
	payTitle:(NSString*)title									// ֧���������
	roleId:(NSString*)role_id									// ��ɫid
	zoneId:(int)zone_id											// ����id
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
 * @brief   ���������������
 * @param   INPUT   paramPPPayResultCode       �ӿڷ��صĽ������
 * @return  �޷���
 */
- (void)ppPayResultCallBack:(PPPayResultCode)paramPPPayResultCode
{
	PN_ON_PAY_REPLAY(paramPPPayResultCode==PPPayResultCodeSucceed);
}
/**
 * @brief   ��֤���³ɹ���
 * @noti    �ֱ��ڷ�ǿ�Ƹ��µ��ȡ�����º����޸���ʱ�����ص�����֪ͨ������¼����
 * @return  �޷���
 */
- (void)ppVerifyingUpdatePassCallBack
{
	PN_ON_VERIFY_UPDATE();
}
/**
 * @brief   ��¼�ɹ��ص�������һ����֤���ɡ�
 * @param   INPUT   paramStrToKenKey       �ַ���token
 * @return  �޷���
 */
- (void)ppLoginStrCallBack:(NSString *)paramStrToKenKey
{
    [[PPAppPlatformKit sharedInstance] getUserInfoSecurity];
	PN_ON_LOGIN([paramStrToKenKey UTF8String]);
}
/**
 * @brief   �ر�Webҳ���Ļص�
 * @param   INPUT   paramPPWebViewCode    �ӿڷ��ص�ҳ�����
 * @return  �޷���
 */
- (void)ppCloseWebViewCallBack:(PPWebViewCode)paramPPWebViewCode
{

}
/**
 * @brief   �ر�SDK�ͻ���ҳ���Ļص�
 * @param   INPUT   paramPPPageCode       �ӿڷ��ص�ҳ�����
 * @return  �޷���
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
 * @brief   ע����Ļص�
 * @return  �޷���
 */
- (void)ppLogOffCallBack
{
	PN_ON_LOGOUT();
}

#elif defined PLATFORM_ID_ITOOLS
#endif


@end