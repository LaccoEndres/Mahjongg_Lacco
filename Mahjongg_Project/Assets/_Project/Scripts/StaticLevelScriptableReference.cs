using System;
using UnityEngine;

namespace _Project.Scripts
{
    public class StaticLevelScriptableReference : MonoBehaviour
    {
        [SerializeField] private LevelScriptable levelScriptable;

        private static LevelScriptable _scriptable;

        private void Awake()
        {
            _scriptable = levelScriptable;
        }

        public static LevelScriptable GetLevelScriptable()
        {
            return _scriptable;
        }
    }
}