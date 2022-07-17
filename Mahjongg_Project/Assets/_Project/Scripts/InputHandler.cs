using System;
using UnityEngine;

namespace _Project.Scripts
{
    public class InputHandler : MonoBehaviour
    {
        [SerializeField] private GroupTileRotation groupTileRotation;

        private void Start()
        {
            if (!groupTileRotation)
                groupTileRotation = TileController.I.GetTileParent().GetComponent<GroupTileRotation>();
        }

        //Called on EventTrigger of MouseInput
        public void OnPointerDown()
        {
            groupTileRotation.OnPointerDown();
        }
        //Called on EventTrigger of MouseInput
        public void OnPointerUp()
        {
            groupTileRotation.OnPointerUp();
        }

        public void OnRotateLeft()
        {
            groupTileRotation.OnRotateLeft();
        }
        public void OnRotateRight()
        {
            groupTileRotation.OnRotateRight();
        }
        public void OnStopClickRotation()
        {
            groupTileRotation.OnStopClickRotation();
        }
    }
}