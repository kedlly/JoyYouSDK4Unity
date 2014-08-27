#ifndef __JOY_U_H__
#define __JOY_U_H__

extern "C" 
{/*
	void U3D_initSDK(int appId, const char * appKey, bool logEnable, int rechargeAmount,
	   bool isLogConnect, bool rechargeEnable,
	   const char* closeRechargeAlertMessage,
	   const char* paramSendMsgNotiClass);
	void U3D_showLoginView();

	void U3D_showCenterView();

	void U3D_exchangeGoods(double paramPrice, string paramBillNo, string paramBillTitle,
				 string paramRoleId, int paramZoneId);
	void U3D_logout();*/
}

struct JoyYouSDK
{
	// �����ӿ�
	static void Init(											// ƽ̨��ʼ��
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
	);
	static void Login();										// ��¼
	static void Logout();										// �ǳ�
	static void ShowUserCentered();								// �û�����
	static void Pay(											// ֧�� 
		int priceOfGoods, 										// �۸� ��λΪԪ
		const char *indentNumber, 								// ���̶����� ��64�ַ����ڣ����ݾ���ƽ̨������
		const char *title,										// ֧���������
		const char *roleId,										// ��ɫid
		int zoneId												// ����id
	);
};

#endif