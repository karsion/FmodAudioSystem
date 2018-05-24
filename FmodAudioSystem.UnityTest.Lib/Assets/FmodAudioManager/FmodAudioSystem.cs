public class FmodAudioSystem : AFmodAudioSystem
{
    //Init the Audio
    public override void InitSounds()
    {
        //FmodBGM.instance.InitSounds();
        FmodSE.instance.InitSounds();
        //FmodVOC.instance.InitSounds();
    }
}