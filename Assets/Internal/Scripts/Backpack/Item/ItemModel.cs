using Skrimel.Gist.Backbone.Ids;
using UnityEngine;

namespace Skrimel.BackpackProject.Backpack.Items
{
    [CreateAssetMenu(menuName = "Content/Item/Model", fileName = "Item Model")]
    public class ItemModel : IdentifiedScriptableObject
    {
        [SerializeField] private Sprite _icon = default;
        public Sprite Icon => _icon;

        [SerializeField] private float _inventoryWeight = default;
        public float InventoryWeight => _inventoryWeight;
        
        [SerializeField] private int _physicalWeight = default;
        public int PhysicalWeight => _physicalWeight;

        [SerializeField] private string _name = default;
        public string Name => _name;

        [SerializeField] private ItemTypeModel _typeModel = default;
        public ItemTypeModel TypeModel => _typeModel;
    }
}