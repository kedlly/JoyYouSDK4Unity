using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.SDK;
public class Main : MonoBehaviour
{
		
	string text = "111111";
	IJoyYou _interface;
	
	// Use this for initialization
	void Start () {
		//Bonjour.initSDK(76,"04569029582680d7602989feb0a0a7e2",false,10,true,true,true,"close recharge message",true,true,true,true,"Main Camera");
		_interface = new JoyYouSDK();
		
	}
	
	// Update is called once per frame
	void Update () {
		//Assets.SDK.JoyYouNativeInterface.
	}
	
	
	
	void OnGUI(){
		
		//showLoginViewButton
        if(GUI.Button(new Rect(0,44,120,120),"Login"))
		{   
			_interface.ShowLoginView();
        } 
		
		
		//showCenterViewButton
		if(GUI.Button(new Rect(200,44,120,120),"PP Center"))
		{   
			_interface.ShowCenter();
        } 
		
		
		 if(GUI.Button(new Rect(0,200,120,120),"1 RMB"))
		{
			_interface.Pay(1, "123131", "1 rmb goods", "0", 0);
        } 
		
		if(GUI.Button(new Rect(200,200,120,120),"3 RMB"))
		{
			_interface.Pay(3, "asdfafdafd", "3 rmb goods", "0", 0);
        } 
	
		GUILayout.Label(text,GUILayout.Width(210));
	
	}
	
	
	void U3D_ppVerifyingUpdatePassCallBack(){
			//Bonjour.showLoginView();
			
			text = "showLoginView-";
		
	}

	public void LoginCallBack(string token)
	{
		System.Console.WriteLine("U3D_loginCallBack------token" + token);
		text = "U3D_loginCallBack------token" + token;
	}	
	public void LogoutCallBack(string msg)
	{
		System.Console.WriteLine("U3D_loginCallBack");
		text = "Logout";
	}
	public void UserCenteredClosedCallBack(string msg)
	{
		System.Console.WriteLine("UserCenteredClosedCallBack");
		text = "UserCenteredClosedCallBack";
	}
	public void PayCallBack(string msg)
	{
		System.Console.WriteLine("PayCallBack");
		text = "PayCallBack";
	}
	public void VerifyingUpdatePassCallBack(string msg)
	{
		System.Console.WriteLine("VerifyingUpdatePassCallBack");
		text = "VerifyingUpdatePassCallBack";
	}

}
