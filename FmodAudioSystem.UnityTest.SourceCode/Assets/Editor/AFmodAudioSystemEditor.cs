// /****************************************************************************
//  * Copyright (c) 2018 ZhongShan KPP Technology Co
//  * Date: 2018-04-09 10:25
//  ****************************************************************************/

using UnityEditor;

[CustomEditor(typeof(AFmodAudioSystem))]
public class AFmodAudioSystemEditor : Editor
{
    [InitializeOnLoadMethod]
    private static void StartInitializeOnLoadMethod()
    {
        EditorApplication.playmodeStateChanged += Paused;
        EditorApplication.update += Mute;
    }

    //Follow UnityEditor paused
    private static void Paused()
    {
        FmodAudioManager.DOAll(m => m.PauseAll(EditorApplication.isPaused));
    }

    //Follow UnityEditor mute
    protected static void Mute()
    {
        if (AFmodAudioSystem.mute != EditorUtility.audioMasterMute)
        {
            AFmodAudioSystem.mute = EditorUtility.audioMasterMute;
        }
    }
}