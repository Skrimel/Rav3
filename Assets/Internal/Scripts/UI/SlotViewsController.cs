using System;
using System.Collections.Generic;
using UnityEngine;

namespace Skrimel.BackpackProject.Backpack.UI
{
    public class SlotViewsController : MonoBehaviour
    {
        [SerializeField] private GameObject _canvasGameObject = default;
        [SerializeField] private List<SlotView> _items = default;

        public bool UiVisible { get; private set; } = false;
        public event Action SelectedItemChanged;
        public SlotView SelectedItem { get; private set; } = null;

        public void ShowUI()
        {
            if (SelectedItem != default)
                SelectItem(default);

            SetUiVisibility(true);

            foreach (var slotUi in _items)
                slotUi.Show();
        }

        public void HideUI()
        {
            if (SelectedItem != default)
                if (SelectedItem.RelatedSlot.CurrentItem != default)
                    SelectedItem.RelatedSlot.DetachCurrentItem();

            SetUiVisibility(false);

            foreach (var slotUi in _items)
                slotUi.Hide();
        }

        public void SelectItem(SlotView item)
        {
            if (item == SelectedItem)
                throw new OperationCanceledException("Item is already selected");

            SelectedItem = item;
            SelectedItemChanged?.Invoke();
        }

        private void SetUiVisibility(bool state)
        {
            _canvasGameObject.SetActive(state);
            UiVisible = state;
        }
    }
}