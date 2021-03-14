using System.Threading.Tasks;
using Skrimel.BackpackProject.Web.Requests;
using Skrimel.BackpackProject.Web.Responses;
using Skrimel.Gist.Web;
using UnityEngine;

namespace Skrimel.BackpackProject.Web
{
    [CreateAssetMenu(fileName = "Requests Handler", menuName = "Functional/Web/Requests Handler")]
    public class BackpackProjectRequestsHandler : RequestsHandler
    {
        public async Task<TestResponse> SendTestRequest()
        {
            var request = CreateRequest<TestRequest>();
            return await request.Send();
        }
    }
}