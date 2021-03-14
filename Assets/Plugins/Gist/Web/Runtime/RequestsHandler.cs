using UnityEngine;

namespace Skrimel.Gist.Web
{
    public class RequestsHandler : ScriptableObject
    {
        public RequestsPool CurrentRequestsPool { get; } = new RequestsPool();
        
        public TRequest CreateRequest<TRequest>()
            where TRequest : IRequestBase, new()
        {
            var result = new TRequest();
            result.SetPool(CurrentRequestsPool);
            return result;
        }
    }
}