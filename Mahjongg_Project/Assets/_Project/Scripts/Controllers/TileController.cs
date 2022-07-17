using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace _Project.Scripts
{
    public class TileController : MonoBehaviour
    {
        public static TileController I;

        [Header("Tiles Configurations")]
        [SerializeField] private Transform tilesParent;
        [SerializeField] private bool editorNewRandomSeed;
        [SerializeField] private TileTypeEnum[] arrTileTypesOnThisStage = new []{TileTypeEnum.Blue, TileTypeEnum.Green, TileTypeEnum.Pink, TileTypeEnum.Red, TileTypeEnum.Yellow, TileTypeEnum.LightBlue};
        [SerializeField] private List<GameObject> listTitles = new List<GameObject>();

        private int _totalTiles;
        private bool _randomSeedOnStart = false;
        
        [Header("Select")]
        [SerializeField] private Transform highlightPrefab;
        private Transform _mouseEnterHighlight;
        private Transform _selectedHighlight = null;
        private TileConfiguration _firstTileSelected = null;
        private AudioSource _audio;

        private readonly WaitForSeconds _waitStartAnimation = new WaitForSeconds(0.03f);
        private void OnValidate()
        {
            if (!editorNewRandomSeed)
                return;
            editorNewRandomSeed = false;

            NewRandomSeed();
        }

        private void Awake()
        {
            I = this; //Simple access without checking for multiple copies/singleton
        }

        private void Start()
        {
            _audio = this.GetComponent<AudioSource>();
            CheckListDirty();
            _totalTiles = listTitles.Count / 2;
            
            _randomSeedOnStart = StaticLevelScriptableReference.GetLevelScriptable().randomSeedOnStart;
            if (_randomSeedOnStart)
                NewRandomSeed();
            
            _mouseEnterHighlight = Instantiate(highlightPrefab, tilesParent);
            _selectedHighlight = Instantiate(highlightPrefab, tilesParent);
            StartCoroutine(nameof(StartAnimation));
        }

        private void NewRandomSeed()
        {
            if (!tilesParent)
            {
                Debug.LogError("No Tile parent selected!");
                return;
            }

            CheckListDirty();

            var tempList = new List<TileConfiguration>();
            for (int i = 0; i < listTitles.Count; i++)
            {
                tempList.Add(listTitles[i].GetComponent<TileConfiguration>());
            }
            
            int currentTypeIndex = 0;
            for (int i = 0; i < tilesParent.childCount; i++)
            {
                
                if (i % 2 == 0)
                    currentTypeIndex++;
                if (currentTypeIndex >= arrTileTypesOnThisStage.Length)
                    currentTypeIndex = 0;
                
                var rand = Random.Range(0, tempList.Count);
                tempList[rand].NewType(arrTileTypesOnThisStage[currentTypeIndex]);
                tempList.RemoveAt(rand);
            }
        }

        private void CheckListDirty()
        {
            if (listTitles.Count == tilesParent.childCount) 
                return;
            
            listTitles = new List<GameObject>();
            for (int i = 0; i < tilesParent.childCount; i++)
            {
                listTitles.Add(tilesParent.GetChild(i).gameObject);
            }
        }

        private IEnumerator StartAnimation()
        {
            for (int i = 0; i < listTitles.Count; i++)
            {
                if (listTitles[i].activeInHierarchy)
                    listTitles[i].SetActive(false);
            }
            
            for (int i = 0; i < listTitles.Count; i++)
            {
                listTitles[i].SetActive(true);
                yield return _waitStartAnimation;
            }
        }

        public Transform GetTileParent()
        {
            return tilesParent;
        }
        
        #region Select
        public void OnTileSelect(TileConfiguration newSelect)
        {   
            //If there's no tile selected
            if (!_firstTileSelected)
            {
                _firstTileSelected = newSelect;
                OnSelect();
            }
            //Same tile selected
            else if (_firstTileSelected.transform == newSelect.transform)
            {
                UnSelect();
            }
            //If there's a tile selected and the new is the same type, it's a match!
            else if (_firstTileSelected.GetThisTileType() == newSelect.GetThisTileType())
            {
                OnRightCombination();
                _firstTileSelected.StartRightCombinationAnim();
                newSelect.StartRightCombinationAnim();
                UnSelect();
            }
            //There's a tile selected but it's not the same type
            else
            {
                _firstTileSelected = newSelect;
                OnSelect();
            }
        }

        private void OnRightCombination()
        {
            ScoreController.I.OnRightCombination();
            _audio.Play();
            
            _totalTiles--;
            if (_totalTiles <= 0)
            {
                ScoreController.I.SaveCurrentScore();
                SceneManager.LoadScene("Victory");
            }
        }

        private void OnSelect()
        {
            _mouseEnterHighlight.gameObject.SetActive(false);
            _selectedHighlight.SetPositionAndRotation(_firstTileSelected.transform.position, _firstTileSelected.transform.rotation);
            _selectedHighlight.gameObject.SetActive(true);
        }

        private void UnSelect()
        {
            _firstTileSelected = null;
            _selectedHighlight.gameObject.SetActive(false);
            _mouseEnterHighlight.gameObject.SetActive(false);
        }

        public void OnMouseEnterHighlight(Transform t)
        {
            _mouseEnterHighlight.SetPositionAndRotation(t.position, t.rotation);
            _mouseEnterHighlight.gameObject.SetActive(true);
        }
        #endregion
    }
}