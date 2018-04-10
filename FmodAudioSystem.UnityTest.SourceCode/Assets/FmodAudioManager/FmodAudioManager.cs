// Copyright: ZhongShan KPP Technology Co
// Date: 2017-08-23
// Time: 12:28
// Author: Karsion

using FMOD;
using UnityEngine;
using FMODSystem = FMOD.System;

public class FmodAudioManager
{
    //操作所有控制器接口
    public delegate void GetFmodAudioManager(FmodAudioManager mgr);

    //游戏用的Mgr与获取接口
    private static readonly FmodAudioManager[] audioManagerInstances = new FmodAudioManager[6];
    private static readonly FmodAudioManager audioManagerGlobalInstances = new FmodAudioManager(6);

    //通道组
    private ChannelGroup channelGroupBGM;
    private ChannelGroup channelGroupSE;
    private ChannelGroup channelGroupVOC;

    //通道索引
    private readonly int nChannelIndex = 0;

    private FmodAudioManager(int nChannelIndex)
    {
        this.nChannelIndex = nChannelIndex;
    }

    //FMOD系统的全局实例
    private static FMODSystem system { get { return AFmodAudioSystem.system; } }

    internal void Init()
    {
        if (channelGroupSE != null)
        {
            return;
        }

        system.createChannelGroup("SE" + nChannelIndex, out channelGroupSE);
        system.createChannelGroup("BGM" + nChannelIndex, out channelGroupBGM);
        system.createChannelGroup("VOC" + nChannelIndex, out channelGroupVOC);
    }

    internal void Release()
    {
        if (channelGroupSE == null)
        {
            return;
        }

        channelGroupSE.release();
        channelGroupBGM.release();
        channelGroupVOC.release();
    }

    //自动GetAudioManager
    public static FmodAudioManager GetAudioManager(int nChannelIndex)
    {
        return audioManagerInstances[nChannelIndex] ?? (audioManagerInstances[nChannelIndex] = new FmodAudioManager(nChannelIndex));
    }

    public static FmodAudioManager GetAudioManagerGlobal()
    {
        return audioManagerGlobalInstances;
    }

    public static void DOAll(GetFmodAudioManager getter)
    {
        for (int i = 0; i < 6; i++)
        {
            getter.Invoke(GetAudioManager(i));
        }
    }

    //不同玩家用不同声道播放
    private void SetSpeakerMix(ChannelControl channelControl)
    {
        switch (nChannelIndex)
        {
            case 0:
                channelControl.setMixLevelsOutput(1, 0, 0, 0, 0, 0, 0, 0);
                break;
            case 1:
                channelControl.setMixLevelsOutput(0, 1, 0, 0, 0, 0, 0, 0);
                break;
            case 2:
                channelControl.setMixLevelsOutput(0, 0, 0, 0, 0, 0, 1, 0);
                break;
            case 3:
                channelControl.setMixLevelsOutput(0, 0, 0, 0, 0, 0, 0, 1);
                break;
            case 4:
                channelControl.setMixLevelsOutput(0, 0, 0, 0, 1, 0, 0, 0);
                break;
            case 5:
                channelControl.setMixLevelsOutput(0, 0, 0, 0, 0, 1, 0, 0);
                break;
            case 6:
                channelControl.setMixLevelsOutput(1, 1, 0, 0, 1, 1, 1, 1);
                break;
        }
    }

    #region 音量

    //静音
    private bool _mute = false;

    public bool mute
    {
        get { return _mute; }

        set
        {
            if (_mute == value)
            {
                return;
            }

            _mute = value;
            if (system == null)
            {
                return;
            }

            channelGroupBGM.setMute(value || AFmodAudioSystem.mute);
            channelGroupVOC.setMute(value || AFmodAudioSystem.mute);
            channelGroupSE.setMute(value || AFmodAudioSystem.mute);
        }
    }

    // 音量设置
    private float _fVolumeBGM = 1f;
    private float _fVolunmVOC = 1;
    private float _fVolunmSE = 1;

    public float fVolunmBGM
    {
        get { return _fVolumeBGM; }
        set
        {
            if (_fVolumeBGM == value) return;
            _fVolumeBGM = value;
            channelGroupBGM.setVolume(value);
        }
    }

    public float fVolunmVOC
    {
        get { return _fVolunmVOC; }
        set
        {
            if (_fVolunmVOC == value) return;
            _fVolunmVOC = value;
            channelGroupVOC.setVolume(value);
        }
    }

    public float fVolunmSE
    {
        get { return _fVolunmSE; }
        set
        {
            if (_fVolunmSE == value) return;
            _fVolunmSE = value;
            channelGroupSE.setVolume(value);
        }
    }
    #endregion

    #region 播放
    public Channel PlaySE(Sound sound, int nLoopCount = 0, float fVolume = 1)
    {
        if (sound == null)
        {
            return null;
        }

        Channel channelSE = PlaySound(sound, channelGroupSE, fVolume, nLoopCount);
        SetSpeakerMix(channelSE);
        channelSE.setMode(MODE.LOOP_NORMAL);
        return channelSE;
    }

    public Channel PlaySE_Random(Sound[] sound, int nLoopCount = 0, float fVolume = 1)
    {
        int nRange = Random.Range(0, sound.Length);
        return PlaySE(sound[nRange], nLoopCount, fVolume);
    }

    private int nVOCCurLevel = 0;

    public void PlayVOC(Sound sound, int nLevel = 1, float fVolume = 1)
    {
        if (sound == null)
        {
            return;
        }

        bool isPlaying;
        channelGroupVOC.isPlaying(out isPlaying);

        //如果当前级别大于将要播放的级别，而且当前语音还在播放，就不播放
        if (isPlaying && nVOCCurLevel > nLevel)
        {
            return;
        }

        nVOCCurLevel = nLevel;
        StopVOC();
        Channel channel= PlaySound(sound, channelGroupVOC, fVolume, 0);
        SetSpeakerMix(channel);
    }

    public void PlayBGM(Sound sound, float fVolume = 1)
    {
        if (sound == null)
        {
            return;
        }

        StopBGM();
        Channel channel = PlaySound(sound, channelGroupBGM, fVolume, -1);
        SetSpeakerMix(channel);
    }

    private static Channel PlaySound(Sound sound, ChannelGroup channelGroup, float volume, int loopcount)
    {
        Channel channel;

        // 播放时必须先用暂停的模式，防止声道未来得及切换，导致所有声道都破一下音
        system.playSound(sound, channelGroup, true, out channel);
        channel.setLoopCount(loopcount);
        channel.setVolume(volume);
        channel.setPaused(false);
        return channel;
    }
    #endregion

    #region 暂停
    public void PauseSE(bool bPaused)
    {
        channelGroupSE.setPaused(bPaused);
    }

    public void PauseVOC(bool bPaused)
    {
        channelGroupVOC.setPaused(bPaused);
    }

    public void PauseBGM(bool bPaused)
    {
        channelGroupBGM.setPaused(bPaused);
    }

    public void PauseAll(bool bPaused)
    {
        if (system == null)
        {
            return;
        }

        channelGroupVOC.setPaused(bPaused);
        channelGroupBGM.setPaused(bPaused);
        channelGroupSE.setPaused(bPaused);
    }
    #endregion

    #region 停止
    public void StopSE()
    {
        channelGroupSE.stop();
    }

    public void StopVOC()
    {
        channelGroupVOC.stop();
    }

    public void StopBGM()
    {
        channelGroupBGM.stop();
    }

    public void StopAll()
    {
        channelGroupSE.stop();
        channelGroupVOC.stop();
        channelGroupBGM.stop();
    }
    #endregion
}