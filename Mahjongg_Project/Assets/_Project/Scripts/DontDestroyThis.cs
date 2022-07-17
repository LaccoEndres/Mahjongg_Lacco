using System;
using UnityEngine;

namespace _Project.Scripts
{
    public class DontDestroyThis : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}