
RPG Music Set
===============

* Description
RPG Music Set is a game music set package for JRPG.
It contains 6 high quality tracks.


* Format
48000Hz, 24bit, wav


* Loop Point
All tracks have loop points for seamless connection arrangement and reverberation.

   filename        title         loop start(sec)  loop end(sec)
  SGB_JRPG_M01  The Royal Garden      0.958         165.549
  SGB_JRPG_M02  Lullaby               1.503          49.507
  SGB_JRPG_M03  Hometown              0.000          80.869
  SGB_JRPG_M04  Suspense              3.000          86.998
  SGB_JRPG_M05  Dramatic Battle       2.604          47.971
  SGB_JRPG_M06  Brave Journey         0.463          44.770

Unity default audio player does not support loop with intro section.
The following is an example code of loop audio player with loop point:

-- LoopPlayer.cs ------------------------------------------
using UnityEngine;

public class LoopPlayer : MonoBehaviour
{
    public AudioClip audioFile;
    public float volume;
    public float loopPointStart;
    public float loopPointEnd;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioFile;
        audioSource.volume = volume;
        audioSource.Play();
    }

    void Update()
    {
        if (audioSource.time > loopPointEnd)
        {
            audioSource.time = loopPointStart;
        }
    }
}
----------------------------------------------

* Credit
Copyright(C) 2019 SmileBoom Co.Ltd. All Rights Reserved.

