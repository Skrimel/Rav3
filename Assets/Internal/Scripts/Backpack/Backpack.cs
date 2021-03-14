using Skrimel.BackpackProject.Backpack.UI;
using UnityEngine;

namespace Skrimel.BackpackProject.Backpack
{
    public class Backpack : MonoBehaviour
    {
        [SerializeField] private SlotViewsController _viewsController = default;

        public bool UiVisible => _viewsController.UiVisible;
        public void ShowUI() => _viewsController.ShowUI();
        public void HideUI() => _viewsController.HideUI();
    }
}