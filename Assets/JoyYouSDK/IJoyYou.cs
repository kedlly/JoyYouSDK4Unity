using System;
using UnityEngine;
namespace Assets.SDK
{
	public interface IJoyYou
	{
		//void InitSDK();
		/*打开登录界面*/
		void ShowLoginView();
		/*用户登出*/
		void Logout();
		/*
		 * 支付
		 * price 价格， 单位为分(RMB) 
		 * billNo 订单号
		 * billTitle 支付界面提示信息
		 * roleId 角色ID
		 * zoneId 分区ID
		 */
		void Pay(int price, string billNo, string billTitle, string roleId, int zoneId);

		/**
		 * 打开用户中心
		 */
		void ShowCenter();
	}

	public interface IJoyYouCB
	{
		void LoginCallBack(string token);
		void LogoutCallBack(string msg);
		void UserCenteredClosedCallBack(string msg);
		void PayCallBack(string msg);
		void VerifyingUpdatePassCallBack(string msg);
	}

	public partial class JoyYouSDK : IJoyYou
	{
		static bool isInitialised = false;
		static JoyYouSDK()
		{
			SDKParams.Parse(typeof(JoyYouSDK));
		}

		void IJoyYou.ShowLoginView()
		{
			if (isInitialised)
			{
				JoyYouNativeInterface.ShowLoginView();
			}
		}

		void IJoyYou.Logout()
		{
			if (isInitialised)
			{
				JoyYouNativeInterface.Logout();
			}
		}

		void IJoyYou.Pay(int price, string billNo, string billTitle, string roleId, int zoneId)
		{
			if (isInitialised)
			{
				JoyYouNativeInterface.ExchangeGoods(price, billNo, billTitle, roleId, zoneId);
			}
		}
		void IJoyYou.ShowCenter()
		{
			if (isInitialised)
			{
				JoyYouNativeInterface.ShowCenterView();
			}
		}
	}	
}
