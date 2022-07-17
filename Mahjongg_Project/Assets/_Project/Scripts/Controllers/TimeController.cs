using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts
{
    public class TimeController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI timerTxt;
        
        private int _currentSeconds = 0;
        private Coroutine _coroutine;
        private readonly WaitForSeconds _wait = new WaitForSeconds(1f);

        private void Start()
        {   
            NewStage();
        }

        public void NewStage()
        {
            LevelScriptable level = StaticLevelScriptableReference.GetLevelScriptable();
            _currentSeconds = level.totalMinutes * 60 + level.totalSeconds;
            if (_coroutine != null)
                StopCoroutine(_coroutine);
            _coroutine = StartCoroutine(nameof(Timer));
        }

        private IEnumerator Timer()
        {
            TimeSpan time;
            while (_currentSeconds > 0)
            {
                yield return _wait;
                _currentSeconds--;
                time = TimeSpan.FromSeconds(_currentSeconds);
                timerTxt.text = $"{time.Minutes}:{time.Seconds}";
            }

            ScoreController.I.SaveCurrentScore();
            SceneManager.LoadScene("GameOver");
        }
    }
}