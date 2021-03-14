using Skrimel.BackpackProject.Backpack.Items;
using UnityEngine;

namespace Skrimel.BackpackProject.Backpack.Slots
{
    [CreateAssetMenu(menuName = "Content/Slot/Model", fileName = "Slot Model")]
    public class SlotModel : ScriptableObject
    {
        [SerializeField] private ItemTypeModel _targetModel = default;
        public ItemTypeModel TargetModel => _targetModel;
        
        public bool CanAssignItem(ItemModel itemModel) =>
            itemModel.TypeModel == _targetModel;
    }
}