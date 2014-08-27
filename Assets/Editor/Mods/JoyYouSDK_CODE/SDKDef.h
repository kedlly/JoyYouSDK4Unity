#ifndef __SDK_DEF__H__
#define __SDK_DEF__H__
#include "SDKConfig.h"

#ifdef THIRD_PFM_PP
#define PLATFORM_ID_PP
#elif defined THIRD_PFM_ITOOLS
#define PLATFORM_ID_ITOOLS
#endif

#endif