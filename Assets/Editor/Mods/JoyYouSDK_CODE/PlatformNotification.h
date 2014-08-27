#ifndef __PLAT__NOTIFY__H__
#define __PLAT__NOTIFY__H__

class CPlatformNotification
{	
public:
	virtual	void OnLogin(const char* token_key);
	virtual	void OnLogout();
	virtual	void OnUserCenterClosed();
	virtual	void OnPayReplay(bool flag);
	virtual	void OnVerifyingUpdate();
    virtual void SetNFParam(void *p);
};

#endif