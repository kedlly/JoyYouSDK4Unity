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
	// 公共接口
	static void Init(											// 平台初始化
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
	);
	static void Login();										// 登录
	static void Logout();										// 登出
	static void ShowUserCentered();								// 用户中心
	static void Pay(											// 支付 
		int priceOfGoods, 										// 价格 单位为元
		const char *indentNumber, 								// 厂商订单号 （64字符以内，根据具体平台而定）
		const char *title,										// 支付界面标题
		const char *roleId,										// 角色id
		int zoneId												// 分区id
	);
};

#endif