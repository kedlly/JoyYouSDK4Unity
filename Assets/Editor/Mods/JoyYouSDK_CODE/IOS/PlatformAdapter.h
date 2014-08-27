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
	portraitUpsideDown:(BOOL)isPortUD;							// ��Ļ����֧��PortraitUpsideDown

// ��¼
-(void) JY_Login;
// �ǳ�										
-(void) JY_Logout;
// �û�����										
-(void) JY_UserCentered;
// ֧�� 									
-(void) JY_Pay:(int)priceOfGoods 								// �۸� ��λΪԪ
	indentNumber:(NSString*)indNumber 							// ���̶����� ��64�ַ����ڣ����ݾ���ƽ̨������
	payTitle:(NSString*)title									// ֧���������
	roleId:(NSString*)role_id									// ��ɫid
	zoneId:(int)zone_id;										// ����id

@end