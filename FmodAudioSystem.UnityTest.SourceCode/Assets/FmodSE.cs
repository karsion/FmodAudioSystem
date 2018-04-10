using FMOD;

public class FmodSE : Singleton<FmodSE>
{
    //通用
    public Sound coin;

    public void InitSounds()
    {
        FmodSoundCreater fsc = FmodSoundCreater.instance;

        //通用
        fsc.InitSound("SE/Coin", ref coin);
    }
}
