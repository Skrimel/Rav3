using System;
using System.Linq;
using Skrimel.BackpackProject.Backpack;
using Skrimel.BackpackProject.Backpack.Items;
using Skrimel.BackpackProject.Controls.Mouse;
using UnityEngine;

namespace Skrimel.BackpackProject.Controls.Selection
{
    public class SelectionController : MonoBehaviour
    {
        [SerializeField] private MouseController _mouseController = default;
        [SerializeField] private Camera _camera = default;
        [SerializeField] private BackpackController _backpackController = default;
        [SerializeField] private LayerMask _raycastMask = default;

        public event Action SelectionChanged;
        public Item SelectedItem { get; private set; } = default;

        private void Awake() =>
            _mouseController.LeftButtonStateChanged += HandleButtonStateChange;

        private void HandleButtonStateChange()
        {
            if (_mouseController.IsLeftButtonDown)
                RaycastItem();
            else
                ResetSelected();
        }

        private void RaycastItem()
        {
            var ray = _camera.ScreenPointToRay(_mouseController.ScreenPosition);
            Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, _raycastMask.value);
            var transform = hit.transform;

            if (transform != default)
            {
                var item = transform.gameObject.GetComponent<Item>();

                if (item != default && item != SelectedItem)
                {
                    if (_backpackController.GetCurrentItems.Contains(item))
                        _backpackController.GetSlot(item).DetachCurrentItem();

                    SelectItem(item);
                }
            }
        }

        private void SelectItem(Item item)
        {
            if (SelectedItem == item)
                throw new OperationCanceledException("Item is already selected");

            if (item == default)
                throw new NullReferenceException("Selection of NULL is prohibited");

            SetSelected(item);
        }

        private void ResetSelected() =>
            SetSelected(null);

        private void SetSelected(Item view)
        {
            SelectedItem = view;

            if (SelectedItem != default)
                SelectedItem.MarkAsAttachable();

            SelectionChanged?.Invoke();
        }
    }
}