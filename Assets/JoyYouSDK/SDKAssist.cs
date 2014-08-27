
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.SDK
{
	[AttributeUsage(AttributeTargets.Class, Inherited = true)]
	public class JoyYouSDKAttribute : System.Attribute
	{
		public virtual void InitSDK()
		{
			Debug.Log("JoyYou common sdk initialise now .");
		}
	}
	
	public class InitPPSDKParamAttribute : JoyYouSDKAttribute
	{
		public int appId { get; private set; }
		public string appKey { get; private set; }
		public string notifyObjName { get; private set; }
		public bool logEnable { get; private set; }
		public int rechargeAmount { get; private set; }
		public bool isLogConnect { get; private set; }
		public bool rechargeEnable { get; private set; }
		public string closeRechargeAlertMsg { get; private set; }
		public bool isOriLandscapeLeft { get; private set; }
		public bool isOriLandscapeRight { get; private set; }
		public bool isOriPortrait { get; private set; }
		public bool isOriPortraitUpsideDown { get; private set; }
		private bool useDefaultOri { get; set; }
		public InitPPSDKParamAttribute(
			int appId,
			string appKey,
			string noficationObjectName,
			bool isLogConnect,
			bool rechargeEnable,
			int rechargeAmount,			
			string closeRechargeAlertMsg,
			bool isOriPortrait,
			bool isOriLandscapeLeft,
			bool isOriLandscapeRight, 
			bool isOriPortraitUpsideDown,
			bool logEnable)
		{
			this.appId = appId;
			this.appKey = appKey;
			this.notifyObjName = noficationObjectName;
			this.logEnable = logEnable;
			this.rechargeAmount = rechargeAmount;
			this.isLogConnect = isLogConnect;
			this.rechargeEnable = rechargeEnable;
			this.closeRechargeAlertMsg = closeRechargeAlertMsg;
			this.isOriLandscapeLeft = isOriLandscapeLeft;
			this.isOriLandscapeRight = isOriLandscapeRight;
			this.isOriPortrait = isOriLandscapeRight;
			this.isOriPortraitUpsideDown = isOriLandscapeRight;
			useDefaultOri = false;
		}

		public InitPPSDKParamAttribute(
			int appId,
			string appKey,
			string noficationObjectName,
			bool isLogConnect,
			bool rechargeEnable,
			int rechargeAmount,
			string closeRechargeAlertMsg,
			bool logEnable)
		{
			this.appId = appId;
			this.appKey = appKey;
			this.notifyObjName = noficationObjectName;
			this.logEnable = logEnable;
			this.rechargeAmount = rechargeAmount;
			this.isLogConnect = isLogConnect;
			this.rechargeEnable = rechargeEnable;
			this.closeRechargeAlertMsg = closeRechargeAlertMsg;
			useDefaultOri = true;
		}
		public override void InitSDK()
		{
			base.InitSDK();
			/*
			string[] args = {
				this.appId.ToString(),
				this.appKey,
				this.logEnable.ToString(),
				this.rechargeAmount.ToString(), 
				this.isLogConnect.ToString(),
				this.rechargeEnable.ToString(),
				this.closeRechargeAlertMsg,
				this.notifyObjName,
				this.isOriPortrait.ToString(),
				this.isOriLandscapeLeft.ToString(),
				this.isOriLandscapeRight.ToString(),
				this.isOriPortraitUpsideDown.ToString()};
			Debug.Log(string.Join("/", args));*/
			JoyYouNativeInterface.InitSDK(
				this.appId,
				this.appKey,
				this.logEnable,
				this.rechargeAmount, 
				this.isLogConnect,
				this.rechargeEnable,
				this.closeRechargeAlertMsg,
				this.notifyObjName,
				this.isOriPortrait,
				this.isOriLandscapeLeft,
				this.isOriLandscapeRight,
				this.isOriPortraitUpsideDown);
		}
	}

	public static class SDKParams
	{
		public static void Parse(Type t)
		{
			bool sdk_init_flag = false;
			foreach (var attr in t.GetCustomAttributes(false))
			{
				JoyYouSDKAttribute jySDKAttr = attr as JoyYouSDKAttribute;
				if (jySDKAttr != null)
				{
					jySDKAttr.InitSDK();					
					sdk_init_flag = true;
					break;
				}
			}
			var pi = t.GetField("isInitialised", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
			pi.SetValue(t, sdk_init_flag);
			if (!sdk_init_flag)
			{
				Debug.LogWarning("SDK initialised failed!");
			}
		}
	}

}
