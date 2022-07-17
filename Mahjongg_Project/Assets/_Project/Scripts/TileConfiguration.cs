using Unity.Collections;
using UnityEngine;

namespace _Project.Scripts
{
    public class TileConfiguration : MonoBehaviour
    {
        [SerializeField] private TileTypeEnum thisType;
        private Transform _transform;
        private Animator _animator;
        private static readonly int PlayTrigger = Animator.StringToHash("Play");
        private static readonly int WrongMoveTrigger = Animator.StringToHash("WrongMove");
        
#if UNITY_EDITOR
        private TileTypeEnum oldType;
        private void OnValidate()
        {
            if (oldType == thisType)
                return;
            
            NewType(thisType);
        }
#endif
        public void NewType(TileTypeEnum type)
        {
            thisType = type;
            this.GetComponentInChildren<MeshRenderer>().material = Resources.Load($"Tile_{thisType.ToString()}") as Material;
            
#if UNITY_EDITOR
            oldType = type;
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
        
        private void Start()
        {
            _transform = this.transform;
            _animator = this.GetComponent<Animator>();
        }
        
        private void OnMouseEnter()
        {
            if (Time.timeScale == 0)
                return;
            
            TileController.I.OnMouseEnterHighlight(_transform);
        }

        private void OnMouseDown()
        {
            if (Time.timeScale == 0)
                return;
            
            if (CanSelectThis())
                TileController.I.OnTileSelect(this);
            else
                _animator.SetTrigger(WrongMoveTrigger);
        }
        private bool CanSelectThis()
        {
            var qnt = 0;
            if (CheckByRaycast(_transform.forward))
                qnt++;
            if (CheckByRaycast(-_transform.forward))
                qnt++;
            if (CheckByRaycast(_transform.right))
                qnt++;
            if (CheckByRaycast(-_transform.right))
                qnt++;
            
            return qnt < 3;
        }

        private bool CheckByRaycast(Vector3 direction)
        {
            if (Physics.Raycast(this.transform.position, direction, out RaycastHit hit, 1f))
            {
                if (hit.transform.CompareTag("Tile"))
                    return true;
            }

            return false;
        }

        public TileTypeEnum GetThisTileType()
        {
            return thisType;
        }

        public void StartRightCombinationAnim()
        {
            _animator.SetTrigger(PlayTrigger);
        }

        //Used as event on the "Tile_OnRightCombination" animation clip
        private void DisableThis()
        {
            _transform.gameObject.SetActive(false);
        }
    }
}

