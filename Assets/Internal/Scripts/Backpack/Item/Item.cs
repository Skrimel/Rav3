using UnityEngine;

namespace Skrimel.BackpackProject.Backpack.Items
{
    [RequireComponent(typeof(Rigidbody))]
    public class Item : MonoBehaviour
    {
        [SerializeField] private ItemModel _model = default;
        public ItemModel Model => _model;

        public Sprite Icon => Model.Icon;

        private Rigidbody _rigidbodyCache = default;
        public Rigidbody Rigidbody => (_rigidbodyCache == default)
            ? _rigidbodyCache = GetComponent<Rigidbody>()
            : _rigidbodyCache;

        public bool CanBeAttached { get; private set; } = true;
        
        private void Awake()
        {
            Rigidbody.mass = _model.PhysicalWeight;
        }

        public void MarkAsAttachable() =>
            CanBeAttached = true;

        public void MarkAsUnattachable() =>
            CanBeAttached = false;
    }
}