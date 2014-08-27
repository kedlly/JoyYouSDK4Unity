#ifndef __PN__H__
#define __PN__H__

#include "SDKConfig.h"

#include "PlatformNotification.h"

#define _OBJ_NAME __pPnObj
#define _DECL_PN_OBJ(name) CPlatformNotification* name;

#ifdef CODE_PFM_U3D
#include "PN4U3D.h"
#define _PN_CLS CPlatformNotify_U3D
#endif

#ifndef _PN_CLS
#define _CREATE_PNOBJECT() 		
#define _PN_ON_LOGIN(token) 	
#define _PN_ON_LOGOUT() 		
#define _PN_ON_CENTER_CLOSED()
#define _PN_ON_PAY_REPLAY(flag)
#define _PN_ON_VERIFY_UPDATE()
#deinfe _PN_SET_PARAMS(arg)
#else
#define _CREATE_PNOBJECT() 		if(!_OBJ_NAME) {_OBJ_NAME = new _PN_CLS();}
#define _PN_ON_LOGIN(token) 	if(_OBJ_NAME) {_OBJ_NAME->OnLogin(token);}
#define _PN_ON_LOGOUT() 		if(_OBJ_NAME) {_OBJ_NAME->OnLogout();}
#define _PN_ON_CENTER_CLOSED()	if(_OBJ_NAME) {_OBJ_NAME->OnUserCenterClosed();}
#define _PN_ON_PAY_REPLAY(flag)	if(_OBJ_NAME) {_OBJ_NAME->OnPayReplay((flag));}
#define _PN_ON_VERIFY_UPDATE()	if(_OBJ_NAME) {_OBJ_NAME->OnVerifyingUpdate();}
#define _PN_SET_PARAMS(arg)     if(_OBJ_NAME) {_OBJ_NAME->SetNFParam((arg));}
#endif

#define DECL_PN_PN_OBJ_PTR()	_DECL_PN_OBJ(_OBJ_NAME)
#define CREATE_PNOBJECT()		_CREATE_PNOBJECT()
#define PN_ON_LOGIN(token) 		_PN_ON_LOGIN(token)
#define PN_ON_LOGOUT() 			_PN_ON_LOGOUT() 
#define PN_ON_CENTER_CLOSED() 	_PN_ON_CENTER_CLOSED()
#define PN_ON_PAY_REPLAY(flag)	_PN_ON_PAY_REPLAY((flag))
#define PN_ON_VERIFY_UPDATE() 	_PN_ON_VERIFY_UPDATE()
#define PN_SET_PARAMS_VOID_PTR(arg)      _PN_SET_PARAMS((arg))


#endif