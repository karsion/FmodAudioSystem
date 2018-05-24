/********************************************************************
	作  者：	        Unreal-M
	文件说明：	单例类(主动)
*********************************************************************/

public class Singleton<T> where T : class, new()
{
    public static readonly T instance = new T();
}