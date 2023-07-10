using UnityEngine;
using Apon.AudioManagement;

namespace Apon.AudioManagement
{
    [RequireComponent(typeof(AudioEvent))]
    public class AudioManager : MonoBehaviour
    {
        #region singleton
        private static AudioManager _instance;

        public static AudioManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = GameObject.FindObjectOfType<AudioManager>();
                }

                return _instance;
            }
        }
        #endregion singleton

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        [Header("SFX")]
        [SerializeField] private AudioSource sfxAudioSource;
        [SerializeField] private AudioProperty[] audioSFX;
        [Header("Music")]
        [SerializeField] private AudioSource musicAudioSource;
        [SerializeField] private AudioProperty[] audioMusic;

        private int isVibrationEnable;


        private AudioEvent audioEvent;
        private void OnEnable()
        {
            audioEvent = GetComponent<AudioEvent>();
        }

        public void PlayMusic(AudioProperty audioProperty)
        {

        }

        public void PlaySFX(AudioProperty audioProperty)
        {
            sfxAudioSource.PlayOneShot(audioProperty.audioClip);
        }

        public void SetMusicVolume(float amount)
        {
            musicAudioSource.volume = amount;
            AudioPrefsHandler.SetAudioMusicPrefs(amount);
        }

        public void SetSFXVolume(float amount)
        {
            sfxAudioSource.volume = amount;
            AudioPrefsHandler.SetAudioSFXPrefs(amount);
        }

        public void SwitchMusicVolume()
        {
            if (AudioPrefsHandler.GetAudioMusicPrefs() > 0.5f)
            {
                SetMusicVolume(0f);
            }
            else
            {
                SetMusicVolume(1f);
            }
        }
        public void SwitchSFXVolume()
        {
            if (AudioPrefsHandler.GetAudioSFXPrefs() > 0.5f)
            {
                SetSFXVolume(0f);
            }
            else
            {
                SetSFXVolume(1f);
            }
        }
        public void SwitchVibrationValue()
        {
            if (AudioPrefsHandler.GetVibrationPrefs() > 0.5f)
            {
                isVibrationEnable = 0;
                AudioPrefsHandler.SetVibrationPrefs(0);
            }
            else
            {
                isVibrationEnable = 1;
                AudioPrefsHandler.SetVibrationPrefs(1);
            }
        }

        private AudioSettingProperty CreateNewAudioSettingProperty(float musicVolume, float sfxVolume, int vibrationValue)
        {
            AudioSettingProperty audioSettingProperty = new AudioSettingProperty();
            audioSettingProperty.MusicVolumeAmount = musicVolume;
            audioSettingProperty.SfxVolumeAmount = sfxVolume;
            audioSettingProperty.VibrationValue = vibrationValue;

            return audioSettingProperty;
        }
    }
}

