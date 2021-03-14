using Skrimel.BackpackProject.Controls.Mouse;
using Skrimel.BackpackProject.Controls.Selection;
using UnityEngine;

namespace Skrimel.BackpackProject.Controls.Drag
{
    public class DragController : MonoBehaviour
    {
        [SerializeField] private SelectionController _selectionController = default;
        [SerializeField] private MouseController _mouseController = default;
        [SerializeField] private DragProfile _dragProfile = default;

        private void FixedUpdate()
        {
            var targetView = _selectionController.SelectedItem;

            if (targetView != default)
            {
                var distance = _mouseController.DragTargetPosition - targetView.transform.position;
                var force = distance * _dragProfile.SpringStiffnessRatio;
                targetView.Rigidbody.AddForce(force);
            }
        }
    }
}