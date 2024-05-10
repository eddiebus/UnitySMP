using System;
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
    public float Volume;
    private AudioSource _SourceComp;
    public Action OnSoundDestroy = new Action(() =>
    {
    });

    // Start is called before the first frame update
    void Awake()
    {
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
        string typeName = Enum.GetName(typeof(SoundType),this.Type);
        string clipName = _SourceComp.clip.name;
        
        gameObject.name = $"Sound({typeName}): {clipName}";

        // Not Looping & Finished Playing. Destroy Object
        if (!Loop && _SourceComp.isPlaying == false)
        {
            this.OnSoundDestroy.Invoke();
            GameObject.Destroy(gameObject);
        }

    }

    public void UpdateVolume()
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

    public static Sound SpawnSound(AudioClip Clip, float Volume, bool Loop, SoundType Type){
        var newSoundObj = new GameObject($"Sound");
        var soundComp = newSoundObj.AddComponent<Sound>();
        return null;
    }

    public static Sound SpawnSound(Vector3 WorldPos, AudioClip Clip, float Volume = 0.5f, bool Loop = false, SoundType type = SoundType.SFX)
    {
        // No Clip Given. Do Nothing
        if (!Clip) return null;

        var listener = FindFirstObjectByType<AudioListener>();

        if (!listener)
        {
            Debug.LogWarning("SoundFX : Can't Play Sounds no Sound Listener in Scene");
            return null;
        }

        var soundSetting = SoundSettings.GetInstance();
        //Create Obj
        var newObj = new GameObject($"Sound : {Clip.name}");
        newObj.transform.position = WorldPos;

        var audioSource = newObj.AddComponent<AudioSource>();
        audioSource.volume = Volume;
        audioSource.clip = Clip;
        audioSource.loop = Loop;
        audioSource.Play();
        var SoundFXComp = newObj.AddComponent<Sound>();
        SoundFXComp.Type = type;
        SoundFXComp.Volume = Volume;
        SoundFXComp.Loop = Loop;
        SoundFXComp.UpdateVolume();

        return SoundFXComp;
    }


}
