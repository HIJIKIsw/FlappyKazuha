#if ENV_LOCAL || ENV_DEVELOPMENT
namespace Flappy.Api
{
	/// <summary>
	/// HelloWorld API リクエスト
	/// </summary>
	public class HelloWorldRequest : ApiRequest
	{
		public override string Url => "helloworld";
	}
}
#endif