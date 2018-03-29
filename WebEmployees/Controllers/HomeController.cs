using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Dynamic;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Xml.Linq;

namespace PositivePsychologyAppHtml.Controllers
{
	[RequireHttps]
	public class HomeController : Controller
	{
		public static List<string> GuessedWords;
		public static List<string> NoGuessedWords;

		public int GetPoints()
		{
			using (var dc = new MySQLEntities())
			{
				var query = (from users in dc.users
							 where users.Login=="Istur"
							 select new
							 {
								 users.Points
							 });
				if (query.Count()==0) return 0;
				else return query.First().Points;
			}
		}

		[HttpGet]
		[Authorize]
		public ActionResult IDataFillingWindow()
		{
			try
			{
				using (var dc = new MySQLEntities())
				{
					//string Login = "Istur";
					//var result = (from w in dc.usersdata
					//			  where w.User == Login
					//			  select w) as usersdata;

					string Login = "Istur";
					var user = dc.usersdata.Find(Login);

					//user.BirthdateNew = user.Birthdate==null ? (DateTime) user.Birthdate : (DateTime) user.Birthdate;
					//try
					//{
					//	user.BirthdateAsString = user.Birthdate.ToString().Substring(0, user.Birthdate.ToString().Length - 8);
					//}
					//catch
					//{
					//	user.BirthdateAsString = "";
					//}

					return View(user);
				}
			}
			catch
			{
				return View();
			}
		}

		public int UpdatePointsInDatabase(int seconds)
		{
			using (var dc = new MySQLEntities())
			{
				var points = 4*seconds;
				string Login = "Istur";
				var user = dc.users.Find(Login);
				if (user == null)
				{
					user = dc.users.Add(user = new users
					{
						Login = "Istur",
						Points = points,
					});
				}
				else
				{
					user.Points += points;
				}
				dc.SaveChanges();

				return user.Points;
				//TempData["points"] = user.Points;
			}

		}

		public void UpdatePersonalDataInDatabase(string propertyName, string[] value)
		{
			//try
			//{
			using (var dc = new MySQLEntities())
			{
				string Login = "Istur";
				var user = dc.usersdata.Find(Login);
				if (user == null)
				{
					user = dc.usersdata.Add(user = new usersdata
					{
						User = Login,
					});
				}

				PropertyInfo propertyInfo = user.GetType().GetProperty(propertyName);
				if (propertyName=="Birthdate")
				{
					var newValue = DateTime.ParseExact(value[0], "dd.MM.yyyy", CultureInfo.CurrentCulture);
					propertyInfo.SetValue(user, newValue);
				}
				else
				{
					propertyInfo.SetValue(user, value[0]);
				}

				dc.SaveChanges();
			}
			//}
			//catch (Exception ex)
			//{

			//}

		}

		[WebMethod]
		public JsonResult GetImagesPaths()
		{
			var pathsArrayAndPositiveImageNumberForm = new ImagePageController().GetImagesPathsAndPositiveImageNumberForm();
			return Json(pathsArrayAndPositiveImageNumberForm, JsonRequestBehavior.AllowGet);
		}

		[WebMethod]
		public JsonResult GetWords()
		{
			var answer = new WordsController().GetWordsArrayAndCorrectNumberForm();
			return Json(answer, JsonRequestBehavior.AllowGet);
		}

		//public bool PositiveImageSelected(String ID)
		//{
		//	var IDAsInt = ID.Substring(4).ToInt();
		//	return IDAsInt == positiveImageNumberForm;
		//}

		[Authorize]
		public ActionResult Index()
		{
			return View();
		}

		[Authorize]
		public ActionResult GameFillWord()
		{
			//var answer = new FillWordWork().GetFillword();
			//NoGuessedWords = answer.NoGuessedWords;

			//return View(answer);
			return View();
		}

		[WebMethod]
		public JsonResult GetTableFillWord()
		{
			var answer = new FillWordWork().GetFillword();
			NoGuessedWords = answer.NoGuessedWords;

			//JavaScriptSerializer serializer = new JavaScriptSerializer();
			//string json = serializer.Serialize(mm);

			return Json(answer, JsonRequestBehavior.AllowGet);
		}


		[Authorize]
		public ActionResult GeneralDescription()
		{
			return View();
		}

		[Authorize]
		public ActionResult FocusModificationDescriptionWindow()
		{
			return View();
		}

		[Authorize]
		public ActionResult IGame()
		{
			return View();
		}

		[Authorize]
		public ActionResult FocusModificationWindow()
		{
			return View();
		}

		[Authorize]
		public ActionResult IndexDepartment()
		{
			ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
			return RedirectToAction("Index", "Home");
		}

		//[WebMethod]
		//public JsonResult GetData() //ActionResult
		//{
		//	using (var dc = new MyDatabaseEntities())
		//	{
		//		var query = (from emp in dc.Employees
		//						 //where emp.IsWorking
		//					 join pos in dc.Positions on emp.PositionID equals pos.IDPosition
		//					 join dp in dc.Departments on emp.DepartmentID equals dp.IDDepartment
		//					 join empPhoto in dc.EmployeePhotoes on emp.IDEmployee equals empPhoto.EmployeeID into ps
		//					 from empPhoto in ps.DefaultIfEmpty()
		//					 select new
		//					 {
		//						 emp.IDEmployee,
		//						 Fio = emp.LastName + " " + emp.FirstName + " " + emp.MiddleName,
		//						 emp.LastName,
		//						 emp.FirstName,
		//						 emp.MiddleName,
		//						 Phone = emp.Phones,
		//						 emp.DateWorkEnd,
		//						 PositionName = pos.Name,
		//						 DepartmentName = dp.Name,
		//						 Place = emp.Office + " " + emp.Floor + " " + emp.Room,
		//						 emp.Office,
		//						 emp.Floor,
		//						 emp.Room,
		//						 //PhotoByte = empPhoto.Photo,
		//						 Photo = ""
		//					 })
		//		.OrderBy(a => a.Fio);

		//		var result = query.AsEnumerable()
		//				   .Select(item => new
		//				   {
		//					   IDEmployee = item.IDEmployee,
		//					   Fio = item.LastName + " " + item.FirstName + " " + item.MiddleName,
		//					   LastName = item.LastName,
		//					   FirstName = item.FirstName,
		//					   MiddleName = item.MiddleName,
		//					   Phone = item.Phone == null ? null : (string) XElement.Parse(item.Phone).Elements("i").Select(i => (string) i.Attribute("num")).First(),
		//					   DateWorkEnd = item.DateWorkEnd,
		//					   PositionName = item.PositionName,
		//					   DepartmentName = item.DepartmentName,
		//					   Place = item.Place,
		//					   Office = item.Office,
		//					   Floor = item.Floor,
		//					   Room = item.Room
		//				   }).ToArray();

		//		return Json(result, JsonRequestBehavior.AllowGet);
		//	}
		//}

		//		public ActionResult Details(Guid? id)
		//		{
		//			if (id == null)
		//			{
		//				return HttpNotFound();
		//			}

		//			try
		//			{
		//				using (var dc = new MyDatabaseEntities())
		//				{

		//					var request = (from emp in dc.Employees
		//								   join pos in dc.Positions on emp.PositionID equals pos.IDPosition
		//								   join dp in dc.Departments on emp.DepartmentID equals dp.IDDepartment
		//								   join block in dc.Departments on dp.ParentID equals block.IDDepartment into blockvr
		//								   from block in blockvr.DefaultIfEmpty()
		//								   join div in dc.Departments on emp.DivisionID equals div.IDDepartment
		//								   join org in dc.Organizations on emp.OrganizationID equals org.IDOrganization
		//								   join empPhoto in dc.EmployeePhotoes on emp.EmployeePhotoID equals empPhoto.IDEmployeePhoto into ps
		//								   from empPhoto in ps.DefaultIfEmpty()
		//								   where emp.IDEmployee == id
		//								   select new
		//								   {
		//									   emp.IDEmployee,
		//									   emp.LastName,
		//									   emp.FirstName,
		//									   emp.MiddleName,
		//									   emp.Phones,
		//									   emp.Office,
		//									   emp.Floor,
		//									   emp.Room,
		//									   emp.Code,
		//									   emp.Birthday,
		//									   PositionName = pos.Name,
		//									   DepartmentName = dp.Name,
		//									   BlockName = block.Name,
		//									   DivizionName = div.Name,
		//									   OrganizationName = org.Name,
		//									   Photo = empPhoto.Photo,
		//								   });
		//					var v = request.First();

		//					var edm = new EmployeeDetailsModel();
		//					edm.IDEmployee = v.IDEmployee;
		//					edm.FirstName = v.FirstName;
		//					edm.LastName = v.LastName;
		//					edm.MiddleName = v.MiddleName;
		//					edm.Office = v.Office;
		//					edm.Floor = v.Floor;
		//					edm.Room = v.Room;
		//					edm.Code = v.Code;
		//					edm.PositionName = v.PositionName;
		//					edm.DepartmentName = v.DepartmentName;
		//					edm.BlockName = v.BlockName;
		//					edm.DivizionName = v.DivizionName;
		//					edm.OrganizationName = v.OrganizationName;
		//					edm.Birthday = v.Birthday  == null ? "" : ((DateTime) v.Birthday).ToString("dd MMMM");
		//					edm.Code = edm.Code.Replace(" ", string.Empty);
		//					edm.Photo = v.Photo;


		//					////string pathToCasheDirectory = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache);
		//					//// string pathToDirectory = pathToCasheDirectory + @"\ImagesEmployee";
		//					////string pathToImage = pathToDirectory + @"\" + edm.Code.ToString() + ".jpg";
		//					////string pathToImage = edm.Code.ToString() + ".jpg"; //@"~/" + 
		//					////if (!System.IO.File.Exists(pathToImage))
		//					////{
		//					//    //if (!System.IO.Directory.Exists(pathToDirectory))
		//					//    //{
		//					//    //    System.IO.Directory.CreateDirectory(pathToDirectory);
		//					//    //}

		//					////    if (v.Photo != null)
		//					////    {
		//					////        using (BinaryWriter writer = new BinaryWriter(System.IO.File.Open(pathToImage, FileMode.Create)))
		//					////        {
		//					////            writer.Write(v.Photo);
		//					////        }
		//					////    }

		//					////}

		//					////edm.PathToImage = pathToImage;

		//					edm.Phones = v.Phones == null ? new string[0] : XElement.Parse(v.Phones).Elements("i").Select(i => (string) i.Attribute("num")).ToArray();
		//					//ICollection<Phone> phones = dc.Phones.Where(a => a.EmployeeID == id).OrderBy(a => a.LineNumber).ToList();


		//					return PartialView(edm);
		//				}
		//			}
		//#pragma warning disable CS0168 // The variable 'ex' is declared but never used
		//			catch (Exception ex)
		//#pragma warning restore CS0168 // The variable 'ex' is declared but never used
		//			{
		//				return HttpNotFound();
		//			}

		//			//if (c != null)
		//			//    return PartialView(c);
		//			//return HttpNotFound();
		//		}

		//public ActionResult GetImage(string Id)
		//{
		//	using (var dc = new MyDatabaseEntities())
		//	{

		//		var v = (from p in dc.EmployeePhotoes where p.EmployeeID == new Guid("54F611A2-8242-11E6-814F-000C29DD9978") select p).First();
		//		return File(v.Photo, "image/jpg");
		//	}
		//}

	}

	/////

	//int amountOfAttemps = 0;
	//int amountWindow = 0;
	//int positiveImageNumberForm;
	//Button positiveButton;



	//private void Window_Loaded(object sender, RoutedEventArgs e)
	//{
	//	SetImage();
	//	amountWindow++;
	//}

	//private void ButtonImage_Click(object sender, RoutedEventArgs e)
	//{

	//	amountOfAttemps = amountOfAttemps + 1;

	//	if (sender != positiveButton)
	//	{
	//		return;
	//	}

	//	amountWindow++;
	//	SetImage();

	//	if (amountWindow!=2) //1
	//	{
	//		return;
	//	}

	//	PreliminaryResultWindow w1 = new PreliminaryResultWindow();
	//	w1.Percent = 100*10 / amountOfAttemps;
	//	w1.NextWindow = "WordsSearchDescriptionWindow";

	//	App currentApp = (App) Application.Current;
	//	currentApp.RootGrid.Children.Clear();
	//	currentApp.RootGrid.Children.Add(w1);

}

