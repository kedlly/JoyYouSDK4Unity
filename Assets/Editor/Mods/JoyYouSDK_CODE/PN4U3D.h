#ifndef __PN4U3D__H_
#define __PN4U3D__H_

#include "PlatformNotification.h"
#include "string"

class CPlatformNotify_U3D : public CPlatformNotification
{
public:
	virtual	void OnLogin(const char* token_key);
	virtual	void OnLogout();
	virtual	void OnUserCenterClosed();
	virtual	void OnPayReplay(bool flag);
	virtual	void OnVerifyingUpdate();
	virtual void SetNFParam(void *p);
private:
    std::string nfObjName;
};

#endif