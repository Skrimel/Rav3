using UnityEngine;

namespace Skrimel.Gist.Backbone.Ids
{
    public abstract class IdentifiedScriptableObject : ScriptableObject, IIdentified
    {
        [SerializeField, HideInInspector] private Id _id = Id.Regular;
        public Id Id => _id;
    }
}
