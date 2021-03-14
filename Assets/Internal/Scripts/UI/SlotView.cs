using System;
using Skrimel.BackpackProject.Backpack.Slots;
using UnityEngine;
using UnityEngine.UI;

namespace Skrimel.BackpackProject.Backpack.UI
{
    public class SlotView : MonoBehaviour
    {
        #region External Views

        [SerializeField] private Slot _relatedSlot = default;
        public Slot RelatedSlot => _relatedSlot;
        [SerializeField] private SlotViewsController _viewController = default;

        #endregion

        #region Line Renderer

        [SerializeField] private Transform _lineStartPosition = default;
        [SerializeField] private Transform _lineEndPosition = default;
        [SerializeField] private LineRenderer _lineRenderer = default;

        #endregion

        [SerializeField] private Image _iconElement = default;

        public bool IsActive { get; private set; } = false;

        private void Awake()
        {
            _lineRenderer.endWidth = 0.1f;
            _lineRenderer.startWidth = 0.1f;
            _lineRenderer.positionCount = 2;
        }

        private void OnMouseOver()
        {
            if (_viewController.SelectedItem != this)
                _viewController.SelectItem(this);
        }

        protected void SetActivity(bool activity)
        {
            if (IsActive == activity)
                throw new OperationCanceledException($"Activity state is already {activity}");

            IsActive = activity;

            UpdateLineRenderer();
            UpdateIcon();
        }

        public void UpdateLineRenderer()
        {
            var positions = new Vector3[2];

            if (IsActive)
            {
                positions[0] = _lineStartPosition.position;
                positions[1] = _lineEndPosition.position;
            }

            _lineRenderer.SetPositions(positions);
        }

        public void UpdateIcon()
        {
            var item = _relatedSlot.CurrentItem;
            var icon = (item != default) ? item.Icon : null;
            // Such a scheme is used not to bypass Unity Object destruction marker

            _iconElement.sprite = icon;
        }

        public void Show() =>
            SetActivity(true);

        public void Hide() =>
            SetActivity(false);
    }
}