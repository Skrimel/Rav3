using Skrimel.BackpackProject.Web.Responses;

namespace Skrimel.BackpackProject.Web.Requests
{
    public class TestRequest : BackpackProjectRequest<TestResponse>
    {
        public override string ConcreteUrlPart => "inventory/status";

        protected override string CreateRequestBody() => " ";

        protected override TestResponse FormatResponse()
        {
            return new TestResponse(this);
        }
    }
}