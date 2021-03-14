using Skrimel.BackpackProject.Controls.Mouse;
using UnityEngine;

namespace Skrimel.BackpackProject.Controls
{
    public class InputController : MonoBehaviour
    {
        [SerializeField] private MouseController _mouseController = default;

        private void LateUpdate()
        {
            if (_mouseController.LeftButtonState != Input.GetMouseButton(0))
                _mouseController.SetLeftButtonState(Input.GetMouseButton(0));
            
            if (_mouseController.ScreenPosition != Input.mousePosition)
                _mouseController.SetScreenPosition(Input.mousePosition);
        }
    }
}