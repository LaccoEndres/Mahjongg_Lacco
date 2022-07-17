using System;
using UnityEngine;

namespace _Project.Scripts
{
    public class Pause : MonoBehaviour
    {
        private void Start()
        {
            OnPause();
        }

        private void OnPause()
        {
            Time.timeScale = 0;
        }

        public void UnPause()
        {
            Time.timeScale = 1;
        }
    }
}