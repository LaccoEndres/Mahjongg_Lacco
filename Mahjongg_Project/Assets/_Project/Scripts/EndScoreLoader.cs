using System;
using TMPro;
using UnityEngine;

namespace _Project.Scripts
{
    public class EndScoreLoader : MonoBehaviour
    {
        private void Start()
        {
            this.GetComponent<TextMeshProUGUI>().text = $"{PlayerPrefs.GetInt("Score")}";
        }
    }
}