using UnityEngine;

namespace _Project.Scripts
{
    [CreateAssetMenu(fileName = "NewTile", menuName = "Scriptables/Create New Level", order = 0)]
    public class LevelScriptable : ScriptableObject
    {
        [Header("Tile Logic")]
        public bool randomSeedOnStart = false;
        
        [Header("Puzzle Duration")]
        public int totalMinutes = 5;
        public int totalSeconds = 0;

        [Header("Tile Score")]
        public int scorePerCombination = 100;
        public float multiplierCooldown = 3f;
    }
}