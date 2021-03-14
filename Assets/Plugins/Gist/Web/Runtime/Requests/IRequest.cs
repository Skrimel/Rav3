using System.Threading.Tasks;

namespace Skrimel.Gist.Web
{
    public interface IRequest<TResponse> : IRequestBase
        where TResponse : IResponse
    {
        Task<TResponse> Send();
    }
}