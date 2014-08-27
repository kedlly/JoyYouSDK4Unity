#ifndef __TPLT_SINGLETON_H__
#define __TPLT_SINGLETON_H__

template <typename T> 
class CSingleton
{
public:
    
    static T* Instance()
    {
        if (m_instance == NULL) m_instance = new T;
        
        ASSERT(m_instance != NULL);

        return m_instance;
    };

    static DestroyInstance()
    {
        delete m_instance;
        m_instance = NULL;
    };

protected:

    CSingleton()
    {
    };

    virtual ~CSingleton()
    {
    };

private:

    CSingleton(const CSingleton& source)
    {
    };

    static T* m_instance; 
};

template <typename T> T* CSingleton<T>::m_instance = NULL;
	
#endif