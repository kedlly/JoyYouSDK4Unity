
#include "JoyYouSDK.h"
#include "SDKDef.h"
#ifdef SYS_IOS
#include "PlatformAdapter.h"
#endif

void JoyYouSDK::Init(										// ƽ̨��ʼ��
	int appId, 												// ƽ̨ appId
	const char * appKey, 									// ƽ̨ appKey
	bool logEnable, 										// �Ƿ����� ƽ̨SDK ��־
	bool rechargeEnable,									// �Ƿ����� ֧������
	int rechargeAmount,										// ֧�������ʼ֧�����
	bool isLogConnect, 										// �Ƿ�ʹ�ó�����
	const char* closeRechargeAlertMessage,					// ֧������رպ�Ļ�����Ϣ
    bool bPortrait,											// ��Ļ����֧��bPortrait
	bool bLandscapeLeft,									// ��Ļ����֧��LandscapeLeft
	bool bLandscapeRight,									// ��Ļ����֧��bLandscapeRight
	bool bPortraitUpsideDown,								// ��Ļ����֧��PortraitUpsideDown
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

void JoyYouSDK::Login()										// ��¼
{
#ifdef SYS_IOS
	[[JoyYouAdapter sharedInstance] JY_Login];
#else
#endif
}

void JoyYouSDK::Logout()									// �ǳ�
{
#ifdef SYS_IOS
	[[JoyYouAdapter sharedInstance] JY_Logout];
#else
#endif
}
	
void JoyYouSDK::ShowUserCentered()							// �û�����
{
#ifdef SYS_IOS
	[[JoyYouAdapter sharedInstance] JY_UserCentered];
#else
#endif
}

void JoyYouSDK::Pay(										// ֧�� 
	int priceOfGoods, 										// �۸� ��λΪԪ
	const char *indentNumber, 								// ���̶����� ��64�ַ����ڣ����ݾ���ƽ̨������
	const char *title,										// ֧���������
	const char *roleId,										// ��ɫid
	int zoneId												// ����id
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