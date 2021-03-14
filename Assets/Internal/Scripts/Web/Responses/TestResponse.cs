using Skrimel.BackpackProject.Web.Requests;
using Skrimel.Gist.Web;

namespace Skrimel.BackpackProject.Web.Responses
{
    public class TestResponse : Response<TestRequest>
    {
        public TestResponse(TestRequest request) : base(request) {}
    }
}