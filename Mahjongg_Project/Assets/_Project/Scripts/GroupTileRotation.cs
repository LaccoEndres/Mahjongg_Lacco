using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts
{
    public class GroupTileRotation : MonoBehaviour
    {
        [Header("Fixed Rotation")] 
        [SerializeField] private bool useFixedRotation = false;
        [SerializeField] private float rotAngle = 90f;
        
        [Header("Mouse/Drag Rotation")]
        [SerializeField] private float mouseRotSpeed = 20f;
        private Coroutine _mouseCoroutine = null;
        private float _currentMousePos;
        
        [Header("Button Rotation")]
        [SerializeField] private float clickRotSpeed = 20f;
        private int _clickDirection;
        private Coroutine _clickCoroutine = null;
        private bool _isRotating;
        
        private readonly WaitForSeconds _wait = new WaitForSeconds(0.05f);
        private Transform _transform;
        private Vector3 _currentEuler;

        private void Start()
        {
            _transform = this.transform;
            _currentEuler = _transform.localEulerAngles;
        }

        #region Rotation with mouse drag
        public void OnPointerDown()
        {
            _mouseCoroutine = StartCoroutine(nameof(MouseRotation));
        }
        public void OnPointerUp()
        {
            _isRotating = false;
            if (_mouseCoroutine != null)
                StopCoroutine(_mouseCoroutine);
            _mouseCoroutine = null;
        }

        private IEnumerator MouseRotation()
        {
            _isRotating = true;
            _currentMousePos = Input.mousePosition.x;
            
            while (_isRotating)
            {
                _currentEuler.y -= (Input.mousePosition.x - _currentMousePos) * mouseRotSpeed * Time.deltaTime;
                _transform.localEulerAngles = _currentEuler;
                _currentMousePos = Input.mousePosition.x;
                
                yield return _wait;
            }
        }
        #endregion


        #region Rotation with arrows click
        public void OnRotateLeft()
        {
            _clickDirection = -1;
            _clickCoroutine = StartCoroutine(nameof(ClickRotation));
        }
        public void OnRotateRight()
        {
            _clickDirection = 1;
            _clickCoroutine = StartCoroutine(nameof(ClickRotation));
        }
        
        private IEnumerator ClickRotation()
        {
            _isRotating = true;
            while (_isRotating)
            {
                _currentEuler.y -= (clickRotSpeed * _clickDirection) * Time.deltaTime;
                _transform.localEulerAngles = _currentEuler;
                
                yield return _wait;    
            }
        }
        
        public void OnStopClickRotation()
        {
            _isRotating = false;
            _clickCoroutine = null;
        }
        #endregion
    }
}