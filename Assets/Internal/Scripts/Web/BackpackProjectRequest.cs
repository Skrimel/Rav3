using Skrimel.Gist.Web;

namespace Skrimel.BackpackProject.Web
{
    public abstract class BackpackProjectRequest<TResponse> : Request<TResponse>
        where TResponse : IResponse
    {
        public override string ApiBaseUrl => " https://dev3r02.elysium.today/";
    }
}