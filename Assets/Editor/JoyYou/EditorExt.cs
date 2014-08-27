using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using System;
using UnityEditor.Callbacks;
using Assets.SDK;
using UnityEditor.XCodeEditor;

namespace JoyYou
{
	internal class ConfigWindow : EditorWindow
	{
		protected BuildTargetGroup curTargetGroup;
		protected BuildTarget curBuildTarget;
		public ConfigWindow()
			: base()
		{
			this.title = "BuildSettings";
		}

		public void BuildProject(string directory)
		{
			try
			{
				GenericBuild(FindEnabledEditorScenes(), directory, curBuildTarget, BuildOptions.None);
				PostBuildEvent();
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		public virtual void PostBuildEvent()
		{ }

		public static string[] FindEnabledEditorScenes()
		{
			List<string> EditorScenes = new List<string>();
			foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
			{
				if (!scene.enabled) continue;
				EditorScenes.Add(scene.path);
			}
			return EditorScenes.ToArray();
		}

		public static void GenericBuild(string[] scenes, string target_dir, BuildTarget build_target, BuildOptions build_options)
		{
			EditorUserBuildSettings.SwitchActiveBuildTarget(build_target);
			string res = BuildPipeline.BuildPlayer(scenes, target_dir, build_target, build_options);

			if (res.Length > 0)
			{
				throw new Exception("BuildPlayer failure: " + res);
			}
		}
	}

	class ConfigWindow4IOS : ConfigWindow
	{
		public string bundleIdentifier = "";
		public string bundleVersion = "";
		public string macros = "";
		public string directory4export = "";

		public ConfigWindow4IOS()
			: base()
		{
			curTargetGroup = BuildTargetGroup.iPhone;
			curBuildTarget = BuildTarget.iPhone;

			bundleIdentifier = PlayerSettings.bundleIdentifier;
			bundleVersion = PlayerSettings.bundleVersion;
			macros = PlayerSettings.GetScriptingDefineSymbolsForGroup(curTargetGroup);
		}

		void OnGUI()
		{
			GUILayout.Label("Unity Packages:", EditorStyles.boldLabel);
			string s = string.Join("\r\n", ConfigWindow.FindEnabledEditorScenes());
			GUILayout.TextArea(s);
			EditorGUILayout.Space();
			GUILayout.Label("Base Settings", EditorStyles.boldLabel);
			this.bundleIdentifier = EditorGUILayout.TextField("Bundle Identifier:", this.bundleIdentifier);
			this.bundleVersion = EditorGUILayout.TextField("Bundle Version:", this.bundleVersion);
			EditorGUILayout.Space();
			this.macros = EditorGUILayout.TextField("Macro Definition", this.macros);
			//EditorGUIUtility.LookLikeControls();
			EditorGUILayout.Space();
			GUILayout.Label("Project export Dir:", EditorStyles.boldLabel);
			this.directory4export = EditorGUILayout.TextField("Output Directory:", this.directory4export);
			EditorGUILayout.Space();
			if (GUILayout.Button("Build", GUILayout.Width(100)))
			{
				if (directory4export == "" || !Directory.Exists(directory4export))
				{
					Debug.LogError("invalid path to export !");
					return;
				}
				PlayerSettings.bundleIdentifier = bundleIdentifier;
				PlayerSettings.bundleVersion = bundleVersion;
				PlayerSettings.SetScriptingDefineSymbolsForGroup(curTargetGroup, macros);
				//Debug.Log(this.macros + "/" + this.bundleIdentifier + "/" + this.bundleVersion);
				BuildProject(directory4export);
			}

		}

		public override void PostBuildEvent()
		{
 			 base.PostBuildEvent();
			 //Debug.Log("------------------------------");
			 //Debug.Log("build succeed event");			 
			 //Debug.Log(Directory.Exists("E:\\test\\Data\\Managed"));
		}

	}


	class CodeGenerateWindow : ConfigWindow
	{
		//------------------------------------------------------------------------------
		private static string sdk_generate_file_name = "joyyou_sdk_generate_class.cs";
		private static string mainDir = "JoyYouSDK";
		private static string generated_dir = "Generated";
		private static Dictionary<string, string> code_template_ppsdk_client = new Dictionary<string,string>
		{
			{"general_start", "namespace Assets.SDK {\r\n"},
			{"pp_target_flag", "[InitPPSDKParam({0}, \"{1}\", \"{2}\", {3}, {4}, {5}, \"{6}\", {7}, {8}, {9}, {10}, {11})]\r\n"},
			{"general_partial_class_JoyYouSDK", "public partial class JoyYouSDK { }\r\n"},
			{"general_end", "}\r\n"},
		};
		//------------------------------------------------------------------------------
		public string appId = "20140818";
		public string appKey = "abcdefghijklmnopqrstuvwxyz0123456789";
		public string notificationObjectName = "Main Camera";
		public string closeRechargeAlertMsg = "关闭充值提示语";
		public bool rechargeEnable = true;
		public string rechargeAmount = "10";
		public bool isLogConnect = true;
		public bool isOriLandscapeLeft = PlayerSettings.allowedAutorotateToLandscapeLeft;
		public bool isOriLandscapeRight = PlayerSettings.allowedAutorotateToLandscapeRight;
		public bool isOriPortrait = PlayerSettings.allowedAutorotateToPortrait;
		public bool isOriPortraitUpsideDown = PlayerSettings.allowedAutorotateToPortraitUpsideDown;
		public bool logEnable;
		void OnGUI()
		{
			GUILayout.Label(
				"代码生成设置",//"Code generate settings:", 
				EditorStyles.boldLabel);
			EditorGUILayout.Space();
			GUILayout.Label(
				"基本设置",//"Base Settings", 
				EditorStyles.boldLabel);
			this.appId = EditorGUILayout.TextField("AppId:", this.appId);
			this.appKey = EditorGUILayout.TextField("AppKey:", this.appKey);
			this.notificationObjectName = EditorGUILayout.TextField(
				"接受通知的对象名", //"Object name of replayed"
				this.notificationObjectName);
			this.isLogConnect = GUILayout.Toggle(this.isLogConnect,
				"游戏客户端与服务器长连接"//"SDK Use Long time connection"
				);
			this.rechargeEnable = GUILayout.Toggle(this.rechargeEnable, 
				"启用支付功能"//"Recharge Enable"
				);
			this.rechargeAmount = EditorGUILayout.TextField(
				"默认支付金额",//"Default Recharge Amount:", 
				this.rechargeAmount);
			this.logEnable = GUILayout.Toggle(this.logEnable, 
				"启用平台日志功能"//"Platform Log Enable"
				);
			//朝向支持
			GUILayout.Label(
				"设备方向",//"Device Orientation", 
				EditorStyles.boldLabel);
			this.isOriPortrait = GUILayout.Toggle(this.isOriPortrait, "Portrait");
			this.isOriLandscapeLeft = GUILayout.Toggle(this.isOriLandscapeLeft, "LandscapeLeft");
			this.isOriLandscapeRight = GUILayout.Toggle(this.isOriLandscapeRight, "LandscapeRight");
			this.isOriPortraitUpsideDown = GUILayout.Toggle(this.isOriPortraitUpsideDown, "PortraitUpsideDown");
			//EditorGUIUtility.LookLikeControls();
			//EditorGUILayout.Space();
			EditorGUILayout.Space();
			if (GUILayout.Button("OK", GUILayout.Width(100)))
			{
				GenerateFiles(
					int.Parse(this.appId), 
					this.appKey, 
					this.notificationObjectName, 
					this.isLogConnect, 
					this.rechargeEnable, 
					int.Parse(this.rechargeAmount),
					this.closeRechargeAlertMsg, 
					this.isOriPortrait, 
					this.isOriLandscapeLeft, 
					this.isOriLandscapeRight, 
					this.isOriPortraitUpsideDown,
					this.logEnable);
			}
		}

		private static string convertBoolean(bool b)
		{
			return b ? "true" : "false";
		}
		private static void GenerateFiles(
			int appId, string appKey, string noficationObjectName, bool isLogConnect, bool rechargeEnable, int rechargeAmount,
			string closeRechargeAlertMsg,
			bool isOriPortrait,
			bool isOriLandscapeLeft,
			bool isOriLandscapeRight,			
			bool isOriPortraitUpsideDown,
			bool logEnabl)
		{
			string sdk_dir = Path.Combine(Application.dataPath, mainDir);
			if (Directory.Exists(sdk_dir)
				&& File.Exists(Path.Combine(sdk_dir, "IJoyYou.cs"))
				&& File.Exists(Path.Combine(sdk_dir, "JoyYouNativeInterface.cs"))
				&& File.Exists(Path.Combine(sdk_dir, "SDKAssist.cs")))
			{
				string genDir = Path.Combine(sdk_dir, generated_dir);
				if (!new DirectoryInfo(genDir).Exists)
				{
					Directory.CreateDirectory(genDir);
				}
				string genFilePath = Path.Combine(genDir, sdk_generate_file_name);
				FileStream fs = new FileStream(genFilePath, FileMode.Create);
				if (fs != null)
				{
					StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.GetEncoding("utf-8"));
					sw.Write(code_template_ppsdk_client["general_start"]);
					sw.Write(string.Format(code_template_ppsdk_client["pp_target_flag"],
						appId, appKey, noficationObjectName, convertBoolean(isLogConnect), convertBoolean(rechargeEnable), rechargeAmount, closeRechargeAlertMsg,
						convertBoolean(isOriPortrait), convertBoolean(isOriLandscapeLeft), convertBoolean(isOriLandscapeRight), convertBoolean(isOriPortraitUpsideDown),
						convertBoolean(logEnabl)));
					sw.Write(code_template_ppsdk_client["general_partial_class_JoyYouSDK"]);
					sw.Write(code_template_ppsdk_client["general_end"]);
					sw.Flush();
					sw.Close();
					fs.Close();
					AssetDatabase.Refresh();
				}
			}
			else
			{
				Debug.LogError("SDK 文件原始检查失败 ！");
			}
		}
	}

	public enum ProjectPlatform
	{
		PP,ITOOLS
	}
	public class JoyYouSDKPlugin
	{

		static readonly Dictionary <ProjectPlatform, string> Settings = new Dictionary<ProjectPlatform, string> ()
		{
			{ProjectPlatform.PP, "JoyYouSDK_PP.projmods"},
			{ProjectPlatform.ITOOLS, "JoyYouSDK_ITOOLS.projmods"},
		};
		public static ProjectPlatform PlatformSDK { get; set; }
		
		[MenuItem("SDKPlugins/IOS/PP助手/Build")]
		static void Bulid_PP_SDK()
		{
			PlatformSDK = ProjectPlatform.PP;
			ConfigWindow4IOS obj = ScriptableObject.CreateInstance<ConfigWindow4IOS>();
			if (obj != null)
			{
				obj.Show();
			}
		}

		[MenuItem("SDKPlugins/IOS/PP助手/代码生成")]
		static void Generate_PP_CODE()
		{
			CodeGenerateWindow obj = ScriptableObject.CreateInstance<CodeGenerateWindow>();
			if (obj != null)
			{
				obj.Show();
			}
		}

		[MenuItem("SDKPlugins/IOS/iTools")]
		static void Bulid_iTools()
		{
			PlatformSDK = ProjectPlatform.ITOOLS;
			//PlatformSDK = ProjectPlatform.PP;
		}

		[PostProcessBuild]
		static void OnBuildingEnd(BuildTarget target, string path)
		{
			Debug.Log("PostProcessBuild" + '/' + target.ToString() + '/' + path);

		}
		/*
		[PostProcessScene]
		static void OnBuildingStart()
		{
			Debug.Log("PostProcess staring ...............");
		}*/
		#if UNITY_EDITOR
		[PostProcessBuild(999)]
		public static void OnPostProcessBuild( BuildTarget target, string pathToBuiltProject )
		{
			if (target != BuildTarget.iPhone) {
				Debug.LogWarning("Target is not iPhone. XCodePostProcess will not run");
				return;
			}
			
			// Create a new project object from build target
			XCProject project = new XCProject( pathToBuiltProject );
			
			// Find and run through all projmods files to patch the project.
			// Please pay attention that ALL projmods files in your project folder will be excuted!
			string[] files = Directory.GetFiles( 
			                     Application.dataPath, 
			                     JoyYouSDKPlugin.Settings[JoyYouSDKPlugin.PlatformSDK], 
			                                    SearchOption.AllDirectories );
			foreach( string file in files ) {
				UnityEngine.Debug.Log("ProjMod File: "+file);
				project.ApplyMod( file );
			}
			
			//TODO implement generic settings as a module option
			//project.overwriteBuildSetting("CODE_SIGN_IDENTITY[sdk=iphoneos*]", "iPhone Distribution", "Release");
			
			// Finally save the xcode project
			WritePlistFile (pathToBuiltProject);
			AddExtCode (pathToBuiltProject);
			project.Save();
			
		}
		#endif

		static void WritePlistFile(string path)
		{
			if (JoyYouSDKPlugin.PlatformSDK == ProjectPlatform.PP) 
			{
				XCPlist list =new XCPlist(path);
				string ppKeyStr = @"
	<key>CFBundleURLTypes</key>
	<array>
		<dict>
			<key>CFBundleURLName</key>
			<string></string>
			<key>CFBundleURLSchemes</key>
			<array>
				<string>teiron%appId%</string>
			</array>
		</dict>
	</array>";
				string s_appId = "";
				foreach (var attr in typeof(JoyYouSDK).GetCustomAttributes(false))
				{
					InitPPSDKParamAttribute ppSDKParams = attr as InitPPSDKParamAttribute;
					if (ppSDKParams != null)
					{
						s_appId = ppSDKParams.appId.ToString();
						break;
					}
				}
				ppKeyStr = ppKeyStr.Replace ("%appId%", s_appId);
				list.AddKey (ppKeyStr);
				list.Save ();
			}
		}

		static void AddExtCode(string filePath)
		{
			if (JoyYouSDKPlugin.PlatformSDK == ProjectPlatform.PP) 
			{
				//读取UnityAppController.mm文件
				XClass UnityAppController = new XClass(filePath + "/Classes/UnityAppController.mm");
				
				//在指定代码后面增加一行代码
				UnityAppController.WriteBelow("#include \"PluginBase/AppDelegateListener.h\"","#import <PPAppPlatformKit/PPAppPlatformKit.h>");
				
				//在指定代码中替换一行
				//UnityAppController.Replace("return YES;","return [ShareSDK handleOpenURL:url sourceApplication:sourceApplication annotation:annotation wxDelegate:nil];");
				
				//在指定代码后面增加一行
				UnityAppController.WriteBelow("UnityCleanup();\n}", 
@"
- (BOOL)application:(UIApplication *)application handleOpenURL:(NSURL *)url {
    [[PPAppPlatformKit sharedInstance] alixPayResult:url];
	return YES;
}");
			}
		}
	}
}
