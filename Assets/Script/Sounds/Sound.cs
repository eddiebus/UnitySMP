using System;
using TMPro;
using UnityEngine;



public enum SoundType
{
    SFX,
    Music,
    Generic
}
public class Sound : MonoBehaviour
{
    public SoundType Type = SoundType.Generic;
    public bool Loop;
    public float StartLoopPoint = 0.0f;
    public float EndLoopPoint = float.MaxValue;
    public float Volume;
    private AudioSource _SourceComp;
    public Action OnSoundDestroy = new Action(() =>
    {
    });


    // Awake called on object create
    void Awake()
    {
        // Assign to volume change event
        SoundSettings.GetInstance().OnSoundSettingChange += this.UpdateVolume;
        _SourceComp = GetComponent<AudioSource>();
    }

    // Monobehaviour DestroyEvent/ Deconstruct
    void OnDestroy()
    {
        SoundSettings.GetInstance().OnSoundSettingChange -= this.UpdateVolume;
    }

    // Update is called once per frame
    void Update()
    {
        string typeName = Enum.GetName(typeof(SoundType), this.Type);
        string clipName = _SourceComp.clip.name;

        gameObject.name = $"Sound({typeName}): {clipName}";

        // Not Looping & Finished Playing. Destroy Object
        if (!Loop && _SourceComp.isPlaying == false)
        {
            this.OnSoundDestroy.Invoke();
            GameObject.Destroy(gameObject);
        }
        else if (_SourceComp.time >= EndLoopPoint)
        {
            _SourceComp.time = StartLoopPoint;
        }
    }

    public void UpdateVolume()
    {
        if (_SourceComp != null)
        {
            var soundSetting = SoundSettings.GetInstance();
            switch (this.Type)
            {
                case SoundType.SFX:
                    _SourceComp.volume = Volume * soundSetting.SFXVolume * soundSetting.MasterVolume;
                    break;
                case SoundType.Music:
                    _SourceComp.volume = Volume * soundSetting.MusicVolume * soundSetting.MasterVolume;
                    break;
                default:
                    _SourceComp.volume = Volume;
                    break;
            }
        }
    }


    public float GetAudioTime() => _SourceComp.time;

    // Set position track is currently at.
    public void SetAudioTime(float newTime)
    {
        _SourceComp.time = newTime;
    }

    public void SetLoopTime(float start, float end)
    {
        this.StartLoopPoint = start;
        this.EndLoopPoint = end;
    }


    public static Sound SpawnSound(Vector3 WorldPos, AudioClip Clip, float Volume = 0.5f, bool Loop = false, SoundType type = SoundType.SFX)
    {
        // No Clip Given. Do Nothing
        if (!Clip) return null;

        // Check for AudioListener in scene
        var listener = FindFirstObjectByType<AudioListener>();

#if UNITY_EDITOR
        if (!listener)
        {
            Debug.LogWarning("SoundFX : Can't Play Sounds no Sound Listener in Scene");
            return null;
        }
#endif

        var soundSetting = SoundSettings.GetInstance();
        //Create Obj
        var newObj = new GameObject($"Sound : {Clip.name}");
        newObj.transform.position = WorldPos;

        // Add AudioSource Comp
        var audioSource = newObj.AddComponent<AudioSource>();
        audioSource.volume = Volume;
        audioSource.clip = Clip;
        audioSource.loop = Loop;
        audioSource.Play();

        // Create and Set Sound Component
        var SoundFXComp = newObj.AddComponent<Sound>();
        SoundFXComp.Type = type;
        SoundFXComp.Volume = Volume;
        SoundFXComp.Loop = Loop;
        SoundFXComp.UpdateVolume(); 

        // return new Sound component
        return SoundFXComp;
    }

    public static Sound SpawnSound(
        AudioClip Clip,
        float Volume = 1.0f,
        bool loop = false, 
        SoundType type = SoundType.SFX)
    {
        var AudioListenComp = FindAnyObjectByType<AudioListener>();

        // No Clip or audio source
        if (!Clip || AudioListenComp)
        {
#if UNITY_EDITOR
            Debug.LogWarning("Sound: No clip or AudioListener component no playing sound.");
#endif 
            return null;
        }
        else
        {

        }

        return null;
    } 
}
