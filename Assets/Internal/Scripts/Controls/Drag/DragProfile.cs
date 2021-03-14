using UnityEngine;

namespace Skrimel.BackpackProject.Controls
{
    [CreateAssetMenu(menuName = "Functional/Profiles/Drag", fileName = "Drag Profile")]
    public class DragProfile : ScriptableObject
    {
        [SerializeField] private float _springStiffnessRatio = default;
        public float SpringStiffnessRatio => _springStiffnessRatio;
    }
}