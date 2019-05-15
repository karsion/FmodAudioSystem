﻿using System;
using System.IO;
using FMOD;
using UnityEngine;
using FMODSystem = FMOD.System;

public abstract class AFmodAudioSystem : MonoBehaviour
{
    internal static FMODSystem system;
    private static string strFilePath;

    private static readonly string[] strExtensions =
    {
        ".ogg", ".wav", ".mp3"
    };

    private static bool _mute;

    public static AFmodAudioSystem instance { get; private set; }

    public static bool mute
    {
        get { return _mute; }
        set
        {
            if (_mute == value)
            {
                return;
            }

            _mute = value;
            FmodAudioManager.DOAll(m => m.mute = value);
        }
    }

    private static string GetExtension(string strPath)
    {
        for (int i = 0; i < strExtensions.Length; i++)
        {
            string item = strExtensions[i];
            string strFile = strFilePath + strPath + item;
            if (File.Exists(strFile))
            {
                return strFile;
            }
        }

        return null;
    }

    protected virtual void Awake()
    {
        if (!Application.isPlaying)
        {
            return;
        }

        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = GetComponent<AFmodAudioSystem>();
        if (!transform.parent)
        {
            DontDestroyOnLoad(gameObject);
        }

        strFilePath = Application.streamingAssetsPath + "/Audio/";
        if (system == null)
        {
            Factory.System_Create(out system);
            system.setSoftwareFormat(48000, SPEAKERMODE.MAX, 0);
            system.init(64, INITFLAGS.NORMAL, (IntPtr)null);
        }

        FmodAudioManager.Init();
        InitSounds();
        DontDestroyOnLoad(gameObject);
    }

    public abstract void InitSounds();

    private void Update()
    {
        if (system != null)
        {
            system.update();
        }
    }

    internal static RESULT CreateSound(string strFileName, ref Sound sound, MODE mode = MODE._2D | MODE.LOOP_OFF | MODE.CREATESAMPLE)
    {
        string strFile = GetExtension(strFileName);
        return strFile == null ? RESULT.ERR_FILE_NOTFOUND : system.createSound(strFile, mode, out sound);
    }

    private void OnDestroy()
    {
        if (instance != this)
        {
            return;
        }

        if (system == null)
        {
            return;
        }

        int n = 0, m = 0;
        Memory.GetStats(out n, out m);
        FmodAudioManager.Release();
        //DOAll(fam => fam.Release());
        FmodSoundCreater.ReleaseInstance();
        system.close();
        system.release();
        system = null;
        Memory.GetStats(out n, out m);
        if (n > 0)
        {
            Debug.LogError("FmodAudioSystem Memory Leak：Current  " + n + " Maximum " + m);
        }
    }

    //public void Dispose()
    //{
    //    OnDestroy();
    //}
}