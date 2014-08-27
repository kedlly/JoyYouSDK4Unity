using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

namespace Assets.SDK
{
	public class JoyYouNativeInterface
	{
//#if UNITY_IPHONE
		[DllImport("__Internal")]
		private static extern void U3D_initSDK(int appId, string appKey, bool logEnable, int rechargeAmount,
									bool isLogConnect, bool rechargeEnable,
									string closeRechargeAlertMessage,
									string paramSendMsgNotiClass,
									bool isOriPortrait,
									bool isOriLandscapeLeft,
									bool isOriLandscapeRight, 
									bool isOriPortraitUpsideDown
									);
		[DllImport("__Internal")]
		private static extern void U3D_showLoginView();

		[DllImport("__Internal")]
		private static extern void U3D_showCenterView();

		[DllImport("__Internal")]
		private static extern void U3D_exchangeGoods(int paramPrice, string paramBillNo, string paramBillTitle,
											 string paramRoleId, int paramZoneId);
		[DllImport("__Internal")]
		private static extern void U3D_logout();

//#endif
		public static void InitSDK(int appId, 
			string appKey, 
			bool logEnable, 
			int rechargeAmount, 
			bool isLogConnect, 
			bool rechargeEnable, 
			string closeRechargeAlertMsg,
			string noficationObjectName,
			bool isOriPortrait,
			bool isOriLandscapeLeft,
			bool isOriLandscapeRight, 
			bool isOriPortraitUpsideDown)
		{
			if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
			U3D_initSDK(appId, appKey, logEnable, rechargeAmount, isLogConnect, rechargeEnable, closeRechargeAlertMsg, noficationObjectName,
				isOriPortrait,
				isOriLandscapeLeft,
				isOriLandscapeRight, 
				isOriPortraitUpsideDown				
				);
		}

		public static void ShowLoginView()
		{
			if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
			U3D_showLoginView();
		}

		public static void ShowCenterView()
		{
			if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
			U3D_showCenterView();
		}

		public static void Logout()
		{
			if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
			U3D_logout();
		}

		public static void ExchangeGoods(int price, string billNo, string billTitle, string roleId, int zoneId)
		{
			if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
			U3D_exchangeGoods(price, billNo, billTitle, roleId, zoneId);
		}

	}
}
