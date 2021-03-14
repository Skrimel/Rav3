using Skrimel.Gist.Backbone.Ids;
using UnityEngine;

namespace Skrimel.BackpackProject.Backpack.Items
{
    [CreateAssetMenu(menuName = "Content/Item/Type Model", fileName = "Item Type Model")]
    public class ItemTypeModel : IdentifiedScriptableObject
    {
        [SerializeField] private string _name = default;
        public string Name => _name;
    }
}