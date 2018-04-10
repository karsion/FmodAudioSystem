// /****************************************************************************
//  * Copyright (c) 2018 ZhongShan KPP Technology Co
//  * Date: 2018-04-10 22:46
//  ****************************************************************************/

using UnityEngine;

public class Test : MonoBehaviour
{
    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            FmodAudioManager.GetAudioManager(0).PlaySE(FmodSE.instance.coin);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            FmodAudioManager.GetAudioManager(1).PlaySE(FmodSE.instance.coin);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            FmodAudioManager.GetAudioManager(2).PlaySE(FmodSE.instance.coin);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            FmodAudioManager.GetAudioManager(3).PlaySE(FmodSE.instance.coin);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            FmodAudioManager.GetAudioManager(4).PlaySE(FmodSE.instance.coin);
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            FmodAudioManager.GetAudioManager(5).PlaySE(FmodSE.instance.coin);
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            FmodAudioManager.GetAudioManagerGlobal().PlaySE(FmodSE.instance.coin);
        }

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            FmodAudioManager.GetAudioManager(0).PlayVOC(FmodSE.instance.coin);
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            FmodAudioManager.GetAudioManager(0).PauseVOC(true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            FmodAudioManager.GetAudioManager(0).PauseVOC(false);
        }
    }
}