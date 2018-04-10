# FmodAudioSystem
## A Unity3D C# 6 speaker mix level audio(speaker separated) module using FMOD. Also follow the unity editor mute or pause

### Init FmodAudioSystem
Implementing abstract class AFmodAudioSystem, and drag onto the game object in scene.
The system using Awake() function init，using Update() function update.
```
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
```

### Init Sounds 
```
public class FmodSE : Singleton<FmodSE>
{
    public Sound coin;

    public void InitSounds()
    {
        FmodSoundCreater fsc = FmodSoundCreater.instance;
        //Init sound file relative path [Application.streamingAssetsPath + "/Audio/"]
        fsc.InitSound("SE/Coin", ref coin);
    }
}
```

### Use FmodAudioManager
- 6 separated channel manager.
```
FmodAudioManager.GetAudioManager(0~5)
```
- 1 all channel manager.
```
FmodAudioManager.GetAudioManagerGlobal()
```
- Also can custom the speaker mix levels.
```
public void SetMixLevelsOutput(float frontleft, float frontright, float surroundleft, float surroundright, float backleft, float backright)
```


### Audio control, Provides three types of audio controls [SE/VOC/BGM]
- Play
```
public Channel PlaySE(Sound sound, int nLoopCount = 0, float fVolume = 1)
public void PlayVOC(Sound sound, int nLevel = 1, float fVolume = 1)
public void PlayBGM(Sound sound, float fVolume = 1)
```
- Pause
```
public void PauseSE(bool bPaused)
public void PauseVOC(bool bPaused)
public void PauseBGM(bool bPaused)
public void PauseAll(bool bPaused)
```
- Stop
```
public void StopSE()
public void StopVOC()
public void StopBGM()
public void StopAll()

```
- eg.
```
FmodAudioManager.GetAudioManager(0).PlaySE(FmodSE.instance.coin);
FmodAudioManager.GetAudioManager(0).PlayVOC(FmodSE.instance.coin);
FmodAudioManager.GetAudioManager(0).PlayBGM(FmodSE.instance.coin);
...
FmodAudioManager.GetAudioManager(0).StopSE();
```

### Volume Control [SE/VOC/BGM/Mute]
```
public bool mute
public float fVolunmSE
public float fVolunmVOC
public float fVolunmBGM
```
- eg.
```
FmodAudioManager.GetAudioManager(0).fVolunmSE = 1;
FmodAudioManager.GetAudioManager(0).fVolunmVOC = 1;
FmodAudioManager.GetAudioManager(0).fVolunmBGM = 0.6f;
FmodAudioManager.GetAudioManager(0).mute = true;
```
