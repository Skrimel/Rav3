using System;
using DG.Tweening;
using Skrimel.BackpackProject.Backpack.Items;
using UnityEngine;

namespace Skrimel.BackpackProject.Backpack.Slots
{
    [RequireComponent(typeof(Slot))]
    public class SlotAnimator : MonoBehaviour
    {
        private Slot _slotCache = default;
        public Slot Slot => (_slotCache != default) ? _slotCache : _slotCache = GetComponent<Slot>();

        [SerializeField] private float _snapTime = 0.2f;
        [SerializeField] private float _intermediateSnapTime = 0.2f;
        [SerializeField] private HingeJoint _joint = default;

        public Sequence CurrentSequence { get; private set; }

        private void Awake()
        {
            Slot.CurrentItemChanging += AnimateDetach;
            Slot.CurrentItemChanged += AnimateAttach;
        }

        public void AnimateAttach(Item item)
        {
            if (Slot.CurrentItem != default)
            {
                if (CurrentSequence != default)
                    ResetSequence();

                CurrentSequence = DOTween.Sequence();

                CurrentSequence
                    .Append(Slot.CurrentItem.transform.DOMove(Slot.SnapIntermediatePoint.position, _intermediateSnapTime))
                    .Insert(0, Slot.CurrentItem.transform.DORotate(Vector3.zero, _intermediateSnapTime))
                    .Append(Slot.CurrentItem.transform.DOMove(Slot.SnapPoint.position, _snapTime))
                    .AppendCallback(() => _joint.connectedBody = Slot.CurrentItem.Rigidbody);

                CurrentSequence.SetLoops(1);
            }
        }

        public void AnimateDetach(Item item)
        {
            if (Slot.CurrentItem != default)
            {
                if (CurrentSequence != default)
                    ResetSequence();

                CurrentSequence = DOTween.Sequence();

                CurrentSequence
                    .AppendCallback(() => _joint.connectedBody = default)
                    .Append(Slot.CurrentItem.transform.DOMove(Slot.SnapIntermediatePoint.position, _intermediateSnapTime));

                CurrentSequence.SetLoops(1);
            }
        }

        private void ResetSequence()
        {
            CurrentSequence.Kill();
            CurrentSequence = default;
        }
    }
}