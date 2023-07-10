using UnityEngine;
namespace Apon.AudioManagement
{
    public static class AudioPrefsHandler
    {
        //Set
        private static void SetAudioSettingPrefs(float music, float sfx, int vibration)
        {
            SetAudioMusicPrefs(music);
            SetAudioSFXPrefs(sfx);
            SetVibrationPrefs(vibration);
        }

        public static void SetAudioMusicPrefs(float music)
        {
            PlayerPrefs.SetFloat("AudioSFXSettingPrefsKey", music);
        }

        public static void SetAudioSFXPrefs(float sfx)
        {
            PlayerPrefs.SetFloat("AudioMusicSettingPrefsKey", sfx);
        }

        public static void SetVibrationPrefs(int vibration)
        {
            PlayerPrefs.SetInt("AudioVibrationSettingPrefsKey", vibration);
        }

        //Get

        public static AudioSettingProperty GetAudioPlayerPrefs()
        {
            AudioSettingProperty audioSettingProperty = new AudioSettingProperty();
            CheckIfAudioSettingPresExist();
            audioSettingProperty.MusicVolumeAmount = GetAudioMusicPrefs();
            audioSettingProperty.SfxVolumeAmount = GetAudioSFXPrefs();
            audioSettingProperty.VibrationValue = GetVibrationPrefs();

            return audioSettingProperty;
        }

        public static float GetAudioMusicPrefs()
        {
            return PlayerPrefs.GetFloat("AudioSFXSettingPrefsKey");
        }

        public static float GetAudioSFXPrefs()
        {
            return PlayerPrefs.GetFloat("AudioMusicSettingPrefsKey");
        }

        public static int GetVibrationPrefs()
        {
            return PlayerPrefs.GetInt("AudioVibrationSettingPrefsKey");
        }

        //Check
        private static void CheckIfAudioSettingPresExist()
        {
            if (PlayerPrefs.HasKey("AudioSFXSettingPrefsKey") &&
                PlayerPrefs.HasKey("AudioMusicSettingPrefsKey") &&
                PlayerPrefs.HasKey("AudioVibrationSettingPrefsKey"))
            {
                return;
            }

            float defualtMusicVolumeAmount = 1;
            float defualtSfxVolumeAmount = 1;
            int defualtVibration = 1;
            SetAudioSettingPrefs(defualtMusicVolumeAmount, defualtSfxVolumeAmount, defualtVibration);
        }
    }
}

