using FMOD;
using System.Collections.Generic;
using UnityEngine;

public class FmodSoundCreater
{
    private FmodSoundCreater() { }
    public static readonly FmodSoundCreater instance = new FmodSoundCreater();

    // 释放内存
    internal static void ReleaseInstance()
    {
        instance.Release();
    }

    private readonly List<Sound> sounds = new List<Sound>(32);// 为方便删除，减少代码量
    // 初始化一个音频并加入列表
    public void InitSound(string strPath, ref Sound sound, MODE mode = MODE._2D | MODE.LOOP_OFF | MODE.CREATESAMPLE)
    {
        RESULT res = AFmodAudioSystem.CreateSound(strPath, ref sound, mode);
        if (res != RESULT.OK)
        {
            Debug.LogError("CreateSound Error: " + Error.String(res) + " @" + strPath);
            return;
        }

        sounds.Add(sound);
    }

    // 初始化多个音频并加入列表
    public void InitSounds(string strPath, ref Sound[] sounds, int nLength, int nStartIndex = 1, MODE mode = MODE._2D | MODE.LOOP_OFF | MODE.CREATESAMPLE)
    {
        sounds = new Sound[nLength];
        for (int i = 0; i < sounds.Length; i++)
        {
            RESULT res = AFmodAudioSystem.CreateSound(string.Format(strPath, i + nStartIndex), ref sounds[i], mode);
            if (res != RESULT.OK)
            {
                Debug.LogError("CreateSound Error: " + Error.String(res) + " @" + string.Format(strPath, i + nStartIndex));
                continue;
            }

            this.sounds.Add(sounds[i]);
        }
    }

    private void Release()
    {
        for (int i = 0; i < sounds.Count; i++)
        {
            if (sounds[i] != null)
            {
                sounds[i].release();
            }
        }

        sounds.Clear();
    }
}
