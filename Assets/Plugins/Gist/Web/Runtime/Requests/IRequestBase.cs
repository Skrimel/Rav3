using System;
using UnityEngine;
using UnityEngine.Networking;

namespace Skrimel.Gist.Web
{
    public interface IRequestBase
    {
        void SetPool(RequestsPool pool);
        event Action OnComplete;
        UnityWebRequest InnerRequest { get; }
        AsyncOperation SendingOperation { get; }
    }
}