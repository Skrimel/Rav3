using UnityEngine;

namespace Skrimel.BackpackProject.Controls
{
    public static class InputExtension
    {
        public static bool IsLeftDown() =>
            Input.GetMouseButtonDown(0);
    }
}