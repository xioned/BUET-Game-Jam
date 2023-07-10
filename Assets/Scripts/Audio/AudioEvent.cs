using System;
using UnityEngine;
namespace Apon.AudioManagement
{
    public class AudioEvent : MonoBehaviour
    {
        public event Action<AudioSettingProperty> audioUiUpdateEvent;

        public void CallAudioUiUpdateEvent(AudioSettingProperty AudioSettingProperty)
        {
            audioUiUpdateEvent?.Invoke(AudioSettingProperty);
        }
    }
}

