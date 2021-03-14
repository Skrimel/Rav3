using UnityEngine.Networking;

namespace Skrimel.Gist.Web
{
    public class Response<TRequest> : IResponse
        where TRequest : IRequestBase
    {
        public long ResponseCode { get; private set; }
        protected UnityWebRequest Request { get; private set; }

        public Response(TRequest request)
        {
            ResponseCode = request.InnerRequest.responseCode;
            Request = request.InnerRequest;
        }
    }
}