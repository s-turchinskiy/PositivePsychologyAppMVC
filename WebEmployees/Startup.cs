using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PositivePsychologyAppHtml.Startup))]
namespace PositivePsychologyAppHtml
{
	public partial class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			ConfigureAuth(app);
		}
	}
}
