using UnityEngine;
namespace Apon.AudioManagement
{
    [CreateAssetMenu(fileName = "Audio Property", menuName = "Scriptable Objects/Audio/Audio Property")]
    public class AudioProperty : ScriptableObject
    {
        public AudioClip audioClip;
        [Range(0, 1)] public float audioDefaultVolume;
    }

}

