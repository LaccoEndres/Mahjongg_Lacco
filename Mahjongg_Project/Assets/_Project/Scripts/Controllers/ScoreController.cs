using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace _Project.Scripts
{
    public class ScoreController : MonoBehaviour
    {
        public static ScoreController I;
        
        [SerializeField] private TextMeshProUGUI scoreTxt;
        [SerializeField] private TextMeshProUGUI multiplierTxt;
        
        private int _scorePerCombination = 100;
        private int _currentScore = 0;
        private int _currentMultiplier = 1;
        private float _multiplierCooldown = 3f;
        private Animator _scoreAnimator;
        private Animator _multiplierAnimator;
        private static readonly int ScoreTrigger = Animator.StringToHash("AddScore");
        private static readonly int MultiplierTrigger = Animator.StringToHash("Multiplier");

        private void Awake()
        {
            I = this;
        }

        private void Start()
        {
            Init();
            ResetMultiplier();
        }

        private void Init()
        {
            LevelScriptable level = StaticLevelScriptableReference.GetLevelScriptable();
            _scorePerCombination = level.scorePerCombination;
            _multiplierCooldown = level.multiplierCooldown;
            _multiplierAnimator = multiplierTxt.GetComponentInParent<Animator>();
            _scoreAnimator = scoreTxt.GetComponentInParent<Animator>();
        }

        public void OnRightCombination()
        {
            AddMultiplier();
            AddScore();
            
            if (IsInvoking(nameof(ResetMultiplier)))
                CancelInvoke(nameof(ResetMultiplier));
            Invoke(nameof(ResetMultiplier), _multiplierCooldown);
        }

        private void AddMultiplier()
        {
            _currentMultiplier++;
            multiplierTxt.text = $"{_currentMultiplier}x Points!";
            multiplierTxt.enabled = _currentMultiplier > 1;
            
            _multiplierAnimator.SetInteger(MultiplierTrigger, _currentMultiplier);
        }

        private void AddScore()
        {
            _currentScore += _scorePerCombination * _currentMultiplier;
            scoreTxt.text = $"{_currentScore}";
            _scoreAnimator.SetTrigger(ScoreTrigger);
        }

        private void ResetMultiplier()
        {
            _currentMultiplier = 0;
            multiplierTxt.enabled = false;
        }

        public void SaveCurrentScore()
        {
            PlayerPrefs.SetInt("Score", _currentScore);
        }
    }
}
