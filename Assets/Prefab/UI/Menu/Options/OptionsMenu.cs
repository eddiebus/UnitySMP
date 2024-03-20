using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Slider MasterVolSlider;
    public Slider MusicVolSlider;
    public Slider SFXVolSlider;

    protected const string Pref_MasterVol = "Setting: MasterVolume";
    protected const string Pref_MusicVol = "Setting: Music Volume";
    protected const string Pref_SFXVol = "Setting: SFX Volume";
    // Start is called before the first frame update
    void Start()
    {
        var allSlider = GetComponentsInChildren<Slider>();
        foreach (var slider in allSlider)
        {
            slider.onValueChanged.AddListener((float value) => { UpdateSettings(); });
        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadCurrentSettings(){
        MasterVolSlider.value = SoundSettings.GetInstance().MasterVolume;
        MusicVolSlider.value = SoundSettings.GetInstance().MusicVolume;
        SFXVolSlider.value = SoundSettings.GetInstance().SFXVolume;

        
    }

    private void UpdateSettings()
    {
        SoundSettingsInput newSettings = new SoundSettingsInput();
        newSettings.MasterVol = MasterVolSlider.value;
        newSettings.MusicVol = MusicVolSlider.value;
        newSettings.SFXVol = SFXVolSlider.value;

        SoundSettings.GetInstance().SetSettings(
            newSettings
        );
    }

    public void SaveSettings() => SoundSettings.GetInstance().SaveSettings();
}
