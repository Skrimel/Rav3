using System;
using Skrimel.BackpackProject.Backpack;
using UnityEngine;

namespace Skrimel.BackpackProject.Controls.Mouse
{
    public class MouseController : MonoBehaviour
    {
        [SerializeField] private Camera _camera = default;
        [SerializeField] private Backpack.Backpack _backpack = default;

        #region Left Button

        public event Action LeftButtonStateChanged;
        public bool LeftButtonState { get; private set; } = false;
        public bool IsLeftButtonUp => !LeftButtonState;
        public bool IsLeftButtonDown => LeftButtonState;

        #endregion

        #region Drag Data

        public event Action PositionChanged;
        public Vector3 DragTargetPosition { get; private set; } = Vector3.zero;
        public Vector3 ScreenPosition { get; private set; }

        #endregion

        public void SetLeftButtonState(bool state)
        {
            if (LeftButtonState == state)
                throw new OperationCanceledException($"State is already {state}");

            LeftButtonState = state;
            LeftButtonStateChanged?.Invoke();
        }

        public void SetScreenPosition(Vector3 position)
        {
            if (ScreenPosition == position)
                throw new OperationCanceledException($"Position is already {position}");

            var backpackPosition = _backpack.transform.position;
            var normal = backpackPosition - _camera.transform.position;
            normal.y = 0f;
            var plane = new Plane(normal, backpackPosition);
            var ray = _camera.ScreenPointToRay(position);

            plane.Raycast(ray, out float distance);

            DragTargetPosition = ray.GetPoint(distance);
            ScreenPosition = position;
            PositionChanged?.Invoke();
        }
    }
}