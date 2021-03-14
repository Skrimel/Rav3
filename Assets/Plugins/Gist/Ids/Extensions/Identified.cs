using System;
using UnityEngine;

namespace Skrimel.Gist.Backbone.Ids
{
    [Serializable]
    public class Identified : IIdentified
    {
        [SerializeField] private Id _id = Id.Regular;
        public Id Id => _id;

        protected void SetIdentifier(Id id) =>
            _id = id;
    }
}