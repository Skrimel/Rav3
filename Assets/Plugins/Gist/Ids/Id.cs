using System;
using UnityEngine;

namespace Skrimel.Gist.Backbone.Ids
{
    [Serializable]
    public struct Id : IEquatable<Id>
    {
        [SerializeField] private string _guid;
        public string Guid => _guid;
        
        public Id(string guid)
        {
            _guid = guid;
        }
        
        public static bool operator !=(Id a, Id b) =>
            !a.Equals(b);
        
        public static bool operator ==(Id a, Id b) =>
            a.Equals(b);
        
        public bool Equals(Id id) =>
            Guid.Equals(id.Guid);
        
        public override bool Equals(object equatable)
        {
            bool result = false;
            
            if (equatable is Id)
                result = Equals((Id)equatable);
            
            return result;
        }
        
        public override int GetHashCode() =>
            base.GetHashCode();
        
        public override string ToString() => Guid;
        
        public static Id Regular => new Id(System.Guid.NewGuid().ToString());
    }
}