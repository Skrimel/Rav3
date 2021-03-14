using System;
using System.Collections.Generic;
using System.Linq;
using Skrimel.BackpackProject.Backpack.Items;
using UnityEngine;

namespace Skrimel.BackpackProject.Backpack.Slots
{
    [RequireComponent(typeof(Collider))]
    public class SlotCollider : MonoBehaviour
    {
        [NonSerialized] private List<Item> _collidingItems = new List<Item>();
        public IEnumerable<Item> CollidingItems => _collidingItems;
        public event Action ItemsChanged;

        public bool HasCollidingItems => _collidingItems.Any();
        public Item GetOldestItem => _collidingItems[0];

        private void OnTriggerEnter(Collider collider)
        {
            var item = collider.gameObject.GetComponent<Item>();

            if (item != default)
            {
                _collidingItems.Add(item);
                ItemsChanged?.Invoke();
            }
        }

        private void OnTriggerExit(Collider collider)
        {
            var item = collider.gameObject.GetComponent<Item>();

            if (item != default)
            {
                _collidingItems.Remove(item);
                ItemsChanged?.Invoke();
            }
        }
    }
}