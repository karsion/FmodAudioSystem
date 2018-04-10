// /****************************************************************************
//  * Copyright (c) 2018 ZhongShan KPP Technology Co
//  * Date: 2018-04-09 16:44
//  ****************************************************************************/

using FMOD;
using UnityEngine;
using FMODSystem = FMOD.System;

public class FmodAudioManager
{
    //操作所有控制器接口
    public delegate void GetFmodAudioManager(FmodAudioManager mgr);

    //游戏用的Mgr与获取接口，Init的时候实例化7个，分别配置不同的声道
    private static FmodAudioManager[] audioManagerInstances;

    //通道组
    private ChannelGroup channelGroupBGM;
    private ChannelGroup channelGroupSE;
    private ChannelGroup channelGroupVOC;

    //通道索引
    private readonly int nChannelIndex = 0;

    //实例化的同时配置声道
    private FmodAudioManager(int nChannelIndex)
    {
        this.nChannelIndex = nChannelIndex;
        switch (nChannelIndex)
        {
            case 0:
                SetMixLevelsOutput(1, 0, 0, 0, 0, 0);
                break;
            case 1:
                SetMixLevelsOutput(0, 1, 0, 0, 0, 0);
                break;
            case 2:
                SetMixLevelsOutput(0, 0, 0, 0, 1, 0);
                break;
            case 3:
                SetMixLevelsOutput(0, 0, 0, 0, 0, 1);
                break;
            case 4:
                SetMixLevelsOutput(0, 0, 1, 0, 0, 0);
                break;
            case 5:
                SetMixLevelsOutput(0, 0, 0, 1, 0, 0);
                break;
            case 6:
                SetMixLevelsOutput(1, 1, 1, 1, 1, 1);
                break;
        }
    }

    //FMOD系统的全局实例
    private static FMODSystem system { get { return AFmodAudioSystem.system; } }

    //全局实例化
    internal static void Init()
    {
        audioManagerInstances = new FmodAudioManager[7];
        for (int i = 0; i < 7; i++)
        {
            audioManagerInstances[i] = new FmodAudioManager(i);
            audioManagerInstances[i].InitChannelGroup();
        }
    }

    private void InitChannelGroup()
    {
        system.createChannelGroup("SE" + nChannelIndex, out channelGroupSE);
        system.createChannelGroup("BGM" + nChannelIndex, out channelGroupBGM);
        system.createChannelGroup("VOC" + nChannelIndex, out channelGroupVOC);
    }

    //如果未实例化，就直接返回
    internal static void Release()
    {
        if (audioManagerInstances == null)
        {
            return;
        }

        for (int i = 0; i < 7; i++)
        {
            audioManagerInstances[i].ReleaseChannelGroup();
        }
    }

    internal void ReleaseChannelGroup()
    {
        channelGroupSE.release();
        channelGroupBGM.release();
        channelGroupVOC.release();
    }

    //获取管理器，0~5单个声道，6是全局了
    public static FmodAudioManager GetAudioManager(int nChannelIndex)
    {
        return audioManagerInstances[nChannelIndex];
    }

    //获取全局管理器6个声道
    public static FmodAudioManager GetAudioManagerGlobal()
    {
        return audioManagerInstances[6];
    }

    //操作所有管理器
    public static void DOAll(GetFmodAudioManager getter)
    {
        if (audioManagerInstances == null)
        {
            return;
        }

        for (int i = 0; i < 7; i++)
        {
            getter.Invoke(audioManagerInstances[i]);
        }
    }

    //通道数组,用于设置通道
    private readonly float[] mixLevels =
    {
        0, 0, 0, 0, 0, 0
    };

    //自定义设置声道音量
    public void SetMixLevelsOutput(float frontleft, float frontright, float surroundleft, float surroundright, float backleft, float backright)
    {
        mixLevels[0] = frontleft;
        mixLevels[1] = frontright;
        mixLevels[2] = surroundleft;
        mixLevels[3] = surroundright;
        mixLevels[4] = backleft;
        mixLevels[5] = backright;
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
            if (_fVolumeBGM == value)
            {
                return;
            }
            _fVolumeBGM = value;
            channelGroupBGM.setVolume(value);
        }
    }

    public float fVolunmVOC
    {
        get { return _fVolunmVOC; }
        set
        {
            if (_fVolunmVOC == value)
            {
                return;
            }
            _fVolunmVOC = value;
            channelGroupVOC.setVolume(value);
        }
    }

    public float fVolunmSE
    {
        get { return _fVolunmSE; }
        set
        {
            if (_fVolunmSE == value)
            {
                return;
            }
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

        Channel channel = PlaySound(sound, channelGroupSE, fVolume, nLoopCount, mixLevels);
        return channel;
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
        PlaySound(sound, channelGroupVOC, fVolume, 0, mixLevels);
    }

    public void PlayBGM(Sound sound, float fVolume = 1)
    {
        if (sound == null)
        {
            return;
        }

        StopBGM();
        PlaySound(sound, channelGroupBGM, fVolume, -1, mixLevels);
    }

    private static Channel PlaySound(Sound sound, ChannelGroup channelGroup, float volume, int loopcount, float[] mixLevels)
    {
        Channel channel;

        // 播放时必须先用暂停的模式，防止声道未来得及切换，导致所有声道都破一下音
        system.playSound(sound, channelGroup, true, out channel);
        channel.setLoopCount(loopcount);
        channel.setVolume(volume);
        channel.setMode(MODE.LOOP_NORMAL);

        //不同玩家用不同声道播放
        channel.setMixLevelsOutput(mixLevels[0], mixLevels[1], 0, 0, mixLevels[4], mixLevels[5], mixLevels[2], mixLevels[3]);
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