using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using Owin.Security.Providers.GitHub;
using Owin.Security.Providers.VKontakte;
using AspNet.Security.OAuth.Yandex;
using PositivePsychologyAppHtml.Models;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace PositivePsychologyAppHtml
{
	public partial class Startup
	{
		// Дополнительные сведения о настройке проверки подлинности см. по адресу: http://go.microsoft.com/fwlink/?LinkId=301864
		public void ConfigureAuth(IAppBuilder app)
		{
			// Настройка контекста базы данных, диспетчера пользователей и диспетчера входа для использования одного экземпляра на запрос
			app.CreatePerOwinContext(ApplicationDbContext.Create);
			app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
			app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

			// Включение использования файла cookie, в котором приложение может хранить информацию для пользователя, выполнившего вход,
			// и использование файла cookie для временного хранения информации о входах пользователя с помощью стороннего поставщика входа
			// Настройка файла cookie для входа
			app.UseCookieAuthentication(new CookieAuthenticationOptions
			{
				AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
				LoginPath = new Microsoft.Owin.PathString("/Account/Login"),
				Provider = new CookieAuthenticationProvider
				{
					// Позволяет приложению проверять метку безопасности при входе пользователя.
					// Эта функция безопасности используется, когда вы меняете пароль или добавляете внешнее имя входа в свою учетную запись.  
					OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
						validateInterval: TimeSpan.FromMinutes(30),
						regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
				}
			});
			app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

			// Позволяет приложению временно хранить информацию о пользователе, пока проверяется второй фактор двухфакторной проверки подлинности.
			app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

			// Позволяет приложению запомнить второй фактор проверки имени входа. Например, это может быть телефон или почта.
			// Если выбрать этот параметр, то на устройстве, с помощью которого вы входите, будет сохранен второй шаг проверки при входе.
			// Точно так же действует параметр RememberMe при входе.
			app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

			// Раскомментируйте приведенные далее строки, чтобы включить вход с помощью сторонних поставщиков входа
			//app.UseMicrosoftAccountAuthentication(
			//    clientId: "",
			//    clientSecret: "");

			app.UseTwitterAuthentication(
			   consumerKey: "uwGX4pUgiTkB2nEGj7pZEm0da",
			   consumerSecret: "GnaSYv49c9Swpk4UtdSZgMlCUf0n7yD6CognYZ7Uk1GdNJVkmT");

			app.UseFacebookAuthentication(
			   appId: "137683213475437",
			   appSecret: "066963271de926e68ed2006c8f4fa17e");

			app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
			{
				ClientId = "43754408343-v2tlj65pbdum72mul11dcej88e11pnna.apps.googleusercontent.com",
				ClientSecret = "SzIfqOvvG43vXETV_ltT7mj_"
			});

			app.UseGitHubAuthentication(new GitHubAuthenticationOptions
			{
				ClientId = "49e302895d8b09ea5656",
				ClientSecret = "98f1bf028608901e9df91d64ee61536fe562064b",
				Scope = { "user:email" }
			});

			app.UseVKontakteAuthentication(new VKontakteAuthenticationOptions
			{
				ClientId = "6086159",
				ClientSecret = "yLempg1m929rXSXwz0hR",
				Scope = { "user:email" }
			});

			//app.UseYandexAuthentication(new YandexAuthenticationOptions
			//{
			//	ClientId = "6086159",
			//	ClientSecret = "yLempg1m929rXSXwz0hR",
			//	Scope = { "user:email" }
			//});
		}
	}
}