using System;
using Skrimel.BackpackProject.Backpack.Items;
using UnityEngine;
using UnityEngine.Events;

namespace Skrimel.BackpackProject.Backpack.Slots
{
    [RequireComponent(typeof(SlotCollider))]
    public class Slot : MonoBehaviour
    {
        private SlotCollider _colliderCache = default;

        public SlotCollider Collider => (_colliderCache != default)
            ? _colliderCache
            : _colliderCache = GetComponent<SlotCollider>();

        [SerializeField] private SlotModel _model = default;
        public SlotModel Model => _model;

        #region Animation Related

        [SerializeField] private Transform _snapPoint = default;
        public Transform SnapPoint => _snapPoint;

        [SerializeField] private Transform _snapIntermediatePoint = default;
        public Transform SnapIntermediatePoint => _snapIntermediatePoint;

        #endregion

        #region Current Item

        public event Action<Item> CurrentItemChanged;
        public event Action<Item> CurrentItemChanging;

        public Item CurrentItem { get; private set; }
        public UnityEvent ItemAttached = default;
        public UnityEvent ItemDetached = default;

        public bool IsFilled => CurrentItem != default;

        #endregion

        public bool CanAssignItem(Item item) =>
            Model.CanAssignItem(item.Model);
        
        public void AttachItem(Item item)
        {
            if (item == default)
                throw new NullReferenceException("Item can not be NULL");

            if (item == CurrentItem)
                throw new OperationCanceledException("Item is already set");

            if (!Model.CanAssignItem(item.Model))
                throw new OperationCanceledException("Item model is not assignable");

            SetItem(item);
            ItemAttached?.Invoke();
        }

        public void DetachCurrentItem()
        {
            if (CurrentItem == default)
                throw new OperationCanceledException("Item is not attached yet");

            CurrentItem.MarkAsUnattachable();
            SetItem(default);
            ItemDetached?.Invoke();
        }

        private void SetItem(Item item)
        {
            CurrentItemChanging?.Invoke(item);
            CurrentItem = item;
            CurrentItemChanged?.Invoke(item);
        }
    }
}