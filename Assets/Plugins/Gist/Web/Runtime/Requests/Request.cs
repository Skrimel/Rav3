using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Skrimel.Gist.Web
{
    public abstract class Request<TResponse> : IRequest<TResponse>
        where TResponse : IResponse
    {
        public UnityWebRequest InnerRequest { get; protected set; }
        public long ResponseCode => InnerRequest.responseCode;

        public AsyncOperation SendingOperation { get; private set; }

        public abstract string ApiBaseUrl { get; }
        public abstract string ConcreteUrlPart { get; }
        public string ApiUrl => ApiBaseUrl + ConcreteUrlPart;
        public virtual string HttpVerb => UnityWebRequest.kHttpVerbPOST;
        public RequestsPool Pool { get; private set; }

        protected event Action<UnityWebRequest> RespondDisposing;
        public event Action OnComplete;

        protected virtual string CreateRequestBody() => "";

        protected virtual void ApplyHeaders(UnityWebRequest request) =>
            request.SetRequestHeader("content-type", "application/json");

        protected virtual UnityWebRequest CreateRequest()
        {
            var uploader = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(CreateRequestBody()));
            uploader.contentType = "application/json";

            var downloader = new DownloadHandlerBuffer();

            var result = new UnityWebRequest(ApiUrl, HttpVerb);
            result.uploadHandler = uploader;
            result.downloadHandler = downloader;
            ApplyHeaders(result);
            return result;
        }

        protected abstract TResponse FormatResponse();

        public async Task<TResponse> Send()
        {
            InnerRequest = CreateRequest();

            Pool.AddRequest(this);

            SendingOperation = InnerRequest.SendWebRequest();
            await SendingOperation;

            Debug.Log(CreateRequestBody());
            Debug.Log(ResponseCode);
            Debug.Log(ApiUrl);
            Debug.Log(InnerRequest.error);

            return HandleResponse(InnerRequest);
        }

        protected virtual TResponse HandleResponse(UnityWebRequest response)
        {
            var result = FormatResponse();

            RespondDisposing?.Invoke(response);

            Pool.RemoveRequest(this);
            OnComplete?.Invoke();
            response.Dispose();

            return result;
        }

        public void SetPool(RequestsPool pool)
        {
            Pool = pool;
        }
    }
}