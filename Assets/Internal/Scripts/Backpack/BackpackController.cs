using System;
using System.Collections.Generic;
using System.Linq;
using Skrimel.BackpackProject.Backpack.Items;
using Skrimel.BackpackProject.Backpack.Slots;
using Skrimel.BackpackProject.Controls.Mouse;
using Skrimel.BackpackProject.Controls.Selection;
using Skrimel.BackpackProject.Web;
using UnityEngine;
using UnityEngine.Events;

namespace Skrimel.BackpackProject.Backpack
{
    public class BackpackController : MonoBehaviour
    {
        [SerializeField] private MouseController _mouseController = default;
        [SerializeField] private SelectionController _selectionController = default;
        [SerializeField] private List<Slot> _slots = default;
        [SerializeField] private BackpackProjectRequestsHandler _handler = default;

        [SerializeField] private UnityEvent _itemsChanged;
        public event Action ItemsChanged;

        private void Awake()
        {
            foreach (var slot in _slots)
            {
                slot.CurrentItemChanged += HandleItemChange;
                slot.Collider.ItemsChanged += ResolveSlotsCollision;
            }

            _mouseController.PositionChanged += ResolveSlotsCollision;
        }

        private void ResolveSlotsCollision()
        {
            foreach (var slot in _slots)
                ResolveSlotCollision(slot);
        }
        
        private void ResolveSlotCollision(Slot slot)
        {
            if (slot.Collider.HasCollidingItems && slot.CurrentItem == default)
            {
                var items = slot.Collider.CollidingItems
                    .Where(item => item != _selectionController.SelectedItem)
                    .Where(slot.CanAssignItem)
                    .Where(item => item.CanBeAttached);

                if (items.Any())
                    slot.AttachItem(items.First());
            }
        }

        private async void HandleItemChange(Item item)
        {
            _itemsChanged?.Invoke();
            ItemsChanged?.Invoke();

            await _handler.SendTestRequest();
        }

        public IEnumerable<Item> GetCurrentItems => _slots
                .Where(item => item.CurrentItem != default)
                .Select(item => item.CurrentItem);

        public Slot GetSlot(Item item) =>
            _slots.First(slot => slot.CurrentItem == item);
    }
}