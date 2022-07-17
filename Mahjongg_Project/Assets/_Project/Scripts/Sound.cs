using System;
using UnityEngine;

namespace _Project.Scripts
{
    public class Sound : MonoBehaviour
    {
        public void OnMute()
        {
            AudioListener.volume = 0;
        }

        public void Unmute()
        {
            AudioListener.volume = 1;
        }
    }
}