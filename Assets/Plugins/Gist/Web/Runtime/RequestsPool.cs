using System;
using System.Collections.Generic;

namespace Skrimel.Gist.Web
{
    public class RequestsPool
    {
        List<IRequestBase> _requests = new List<IRequestBase>();

        public event Action RequestsAmountChanged;
        public bool IsEmpty { get => _requests.Count == 0; }

        public void AddRequest(IRequestBase request)
        {
            _requests.Add(request);
            RequestsAmountChanged?.Invoke();
        }

        public void RemoveRequest(IRequestBase request)
        {
            _requests.Remove(request);
            RequestsAmountChanged?.Invoke();
        }
    }
}