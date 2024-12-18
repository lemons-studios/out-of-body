using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using TMPro;
using UnityEngine.Assertions;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

// Most of this is borrowed from my previous project
public class GameSettings : MonoBehaviour
{
    public AudioMixer mainVolume;
    public VolumeProfile postProcessData;

    public GameObject SettingsMenuObject;
    public TextMeshProUGUI volumeValue;
    public TMP_Dropdown qualityDropdown, aaQualityDropdown, crtDropdown;
    public Slider volumeSlider;
    
    public UniversalAdditionalCameraData[] urpCamData;

    private void Awake()
    {
        SetOptionsValues();
    }

    private void SetOptionsValues()
    {
        qualityDropdown.value = PlayerPrefs.GetInt("CrtEnabled");
        ToggleCRTEffect(qualityDropdown.value);

        aaQualityDropdown.value = PlayerPrefs.GetInt("AntiAliasingQuality");
        SetAntiAliasingQuality(aaQualityDropdown.value);

        qualityDropdown.value = PlayerPrefs.GetInt("QualityLevel");
        setRenderQuality(qualityDropdown.value);
        
        volumeSlider.value = PlayerPrefs.GetFloat("Volume");
        SetVolume(volumeSlider.value);
        
        if (SceneManager.GetActiveScene().buildIndex != 0) SettingsMenuObject.SetActive(false); 
    }

    public void setRenderQuality(int quality)
    {
        QualitySettings.SetQualityLevel(quality);
        PlayerPrefs.SetInt("QualityLevel", quality);
    }

    public void SetVolume(float volume)
    {
        mainVolume.SetFloat("Volume", Mathf.Log10(volume) * 20);
        int textDisplayVolume = Mathf.RoundToInt(volume * 100);
        volumeValue.text = textDisplayVolume + "%";
        
        PlayerPrefs.SetFloat("Volume", volume);
    }
    
    public void SetAntiAliasingQuality(int newQuality)
    {
        foreach (UniversalAdditionalCameraData camData in urpCamData)
        {
            camData.antialiasingQuality = newQuality switch
            {
                1 =>
                    AntialiasingQuality.Low,
                2 =>
                    AntialiasingQuality.Medium,
                3 =>
                    AntialiasingQuality.High,
                _ => camData.antialiasingQuality
            };
        }
        
        PlayerPrefs.SetInt("AntiAliasingQuality", newQuality);
    }
    
    
    public void ToggleCRTEffect(int status)
    {
        bool toggleMode = !toBool(status); // dropdown inverses without this, I'll figure out why later (lol)
        
        if (postProcessData.TryGet<FilmGrain>(out var filmGrain))
        {
            filmGrain.active = toggleMode;
        }
        PlayerPrefs.SetInt("CrtEnabled", status);
    }

    private bool toBool(int x)
    {
        return x == 1;
    }
}
