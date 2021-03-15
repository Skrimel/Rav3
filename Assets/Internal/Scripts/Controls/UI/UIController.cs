using System;
using Skrimel.BackpackProject.Controls.Mouse;
using Skrimel.BackpackProject.Controls.Selection;
using UnityEngine;

namespace Skrimel.BackpackProject.Controls.UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private Backpack.Backpack _backpack = default;
        [SerializeField] private Camera _camera = default;
        [SerializeField] private MouseController _mouseController = default;
        [SerializeField] private SelectionController _selectionController = default;
        [SerializeField] private Canvas _canvas = default;
        [SerializeField] private LayerMask _layerMask = default;

        private void Awake()
        {
            _mouseController.LeftButtonStateChanged += HandleMouseButtonChange;
            _mouseController.PositionChanged += HandleMousePositionChange;
        }

        private void OnDestroy()
        {
            _mouseController.LeftButtonStateChanged -= HandleMouseButtonChange;
            _mouseController.PositionChanged -= HandleMousePositionChange;
        }

        private void HandleMouseButtonChange()
        {
            if (_mouseController.IsLeftButtonDown && _selectionController.SelectedItem == default)
            {
                var ray = _camera.ScreenPointToRay(_mouseController.ScreenPosition);
                Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, _layerMask);

                if (hit.transform == _backpack.transform && !_backpack.UiVisible)
                    _backpack.ShowUI();
            }
        }

        private void HandleMousePositionChange()
        {
            if (_mouseController.IsLeftButtonDown && _selectionController.SelectedItem == default)
            {
                var ray = _camera.ScreenPointToRay(_mouseController.ScreenPosition);
                Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, _layerMask);

                if (hit.transform != _backpack.transform && hit.transform != _canvas.transform && _backpack.UiVisible)
                    _backpack.HideUI();
            }
            else if (_backpack.UiVisible)
                _backpack.HideUI();
        }
    }
}