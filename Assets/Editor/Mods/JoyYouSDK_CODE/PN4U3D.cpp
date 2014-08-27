#include "PN4U3D.h"
#include <stdio.h>
#include "SDKDef.h"

#ifdef CODE_PFM_U3D_API_SIMULATE

void UnitySendMessage(const char * notfiyObjName, const char *method, const char *msg)
{
    printf("Send Message to Unity : \n\tObjName=%s\n\tMethod=%s\n\tMessage=%s\n",
           notfiyObjName, method, msg);
}

#endif

#define NF_LOGIN_METHOD "LoginCallBack"
#define NF_LOGOUT_METHOD "LogoutCallBack"
#define NF_UCC_METHOD "UserCenteredClosedCallBack"
#define NF_PAY_METHOD "PayCallBack"
#define NF_VU_METHOD "VerifyingUpdatePassCallBack"

void CPlatformNotify_U3D::OnLogin(const char* token_key)
{
    printf("-----------------------------\n");
    printf("%s\n",token_key);
    printf("-----------------------------\n");
    UnitySendMessage(this->nfObjName.c_str(),
                     NF_LOGIN_METHOD, token_key);

}
void CPlatformNotify_U3D::OnLogout()
{
    printf("-----------------------------\n");
    printf("%s\n","OnLogout");
    printf("-----------------------------\n");
    UnitySendMessage(this->nfObjName.c_str(),
                     NF_LOGOUT_METHOD, "");
    

}                    
void CPlatformNotify_U3D::OnUserCenterClosed()
{
    printf("-----------------------------\n");
    printf("%s\n","OnUserCenterClosed");
    printf("-----------------------------\n");
    UnitySendMessage(this->nfObjName.c_str(),
                     NF_UCC_METHOD, "");
}          
void CPlatformNotify_U3D::OnPayReplay(bool flag)
{
    printf("-----------------------------\n");
    printf("%s\n","OnPayReplay");
    printf("-----------------------------\n");
    UnitySendMessage(this->nfObjName.c_str(),
                     NF_PAY_METHOD, flag ? "true" : "false");
}                 
void CPlatformNotify_U3D::OnVerifyingUpdate()
{
    printf("-----------------------------\n");
    printf("%s\n","OnVerifyingUpdate");
    printf("-----------------------------\n");
    UnitySendMessage(this->nfObjName.c_str(),
                     NF_VU_METHOD, "");
}

void CPlatformNotify_U3D::SetNFParam(void *p)
{
    this->nfObjName.assign((const char *)p);
    
}
