using System;
using UnityEngine;



public enum SoundType
{
    SFX,
    Music
}
public class Sound : MonoBehaviour
{
    public SoundType Type = SoundType.SFX;
    public bool Loop;
    public float Volume;
    private AudioSource _SourceComp;
    public Action OnSoundDestroy = new Action(() =>
    {
    });

    // Start is called before the first frame update
    void Awake()
    {
        SoundSettings.GetInstance().OnSoundSettingChange += this._UpdateVolume;
        _SourceComp = GetComponent<AudioSource>();
    }

    // Monobehaviour DestroyEvent/ Deconstruct
    void OnDestroy()
    {
        SoundSettings.GetInstance().OnSoundSettingChange -= this._UpdateVolume;
    }

    // Update is called once per frame
    void Update()
    {
        // Not Looping & Finished Playing. Destroy Object
        if (!Loop && _SourceComp.isPlaying == false)
        {
            this.OnSoundDestroy.Invoke();
            GameObject.Destroy(gameObject);
        }
    }

    public void _UpdateVolume()
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
                break;

        }
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
        var newObj = new GameObject($"SFX : {Clip.name}");
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
        SoundFXComp._UpdateVolume();

        return SoundFXComp;
    }


}
