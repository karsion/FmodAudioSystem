using FMOD;

public class FmodSE : Singleton<FmodSE>
{
    //通用
    public Sound 敲击选项;

    public void InitSounds()
    {
        FmodSoundCreater fsc = FmodSoundCreater.instance;

        //通用
        fsc.InitSound("SE/01吃到金币", ref 敲击选项);
    }
}
