using UnityEngine;

public struct SoundSettingsInput
{
    public float MasterVol;
    public float MusicVol;
    public float SFXVol;
}

public class SoundSettings
{
    private static SoundSettings _Instance = null;
    protected float _MasterVolume = 1.0f;
    protected float _MusicVolume = 0.5f;
    protected float _SFXVolume = 0.5f;


    public float MasterVolume => _MasterVolume;
    public float SFXVolume => _SFXVolume;

    /* Unmerged change from project 'Assembly-CSharp.Player'
    Before:
        public float MusicVolume => _MusicVolume;



        public  const string Pref_MasterVol = "Setting: MasterVolume";
    After:
        public float MusicVolume => _MusicVolume;



        public  const string Pref_MasterVol = "Setting: MasterVolume";
    */
    public float MusicVolume => _MusicVolume;



    public const string Pref_MasterVol = "Setting: MasterVolume";
    public const string Pref_MusicVol = "Setting: Music Volume";
    public const string Pref_SFXVol = "Setting: SFX Volume";

    public SoundSettings()
    {
        LoadSettings();
    }

    public static SoundSettings GetInstance()
    {
        if (_Instance == null)
        {
            _Instance = new SoundSettings();
            _Instance.LoadSettings();
        }

        return _Instance;
    }

    private void LoadSettings()
    {
        _MasterVolume = PlayerPrefs.GetFloat(Pref_MasterVol, 1.0f);
        _MusicVolume = PlayerPrefs.GetFloat(Pref_MusicVol, 0.7f);
        _SFXVolume = PlayerPrefs.GetFloat(Pref_SFXVol, 0.7f);
    }

    public void SetSettings(SoundSettingsInput input)
    {
        _MasterVolume = input.MasterVol;
        _MusicVolume = input.MusicVol;
        _SFXVolume = input.SFXVol;
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat(
            Pref_MasterVol,
            _MasterVolume
        );

        PlayerPrefs.SetFloat(
            Pref_MusicVol,
            _MusicVolume
        );

        PlayerPrefs.SetFloat(
            Pref_SFXVol,
            _SFXVolume
        );

        PlayerPrefs.Save();
    }
}
