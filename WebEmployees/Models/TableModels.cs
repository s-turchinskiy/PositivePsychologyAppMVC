namespace PositivePsychologyAppHtml
{
	using System;
	using System.ComponentModel.DataAnnotations;

	public class DepartmentNotModel
	{
		public Guid IDDepartment { get; set; }
		public string Name { get; set; }
		public Guid Parent { get; set; }
	}

	public class EmployeeDetailsModel
	{
		public System.Guid IDEmployee { get; set; }
		public string LastName { get; set; }
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string Phone { get; set; }
		public string Office { get; set; }
		public string Floor { get; set; }
		public string Room { get; set; }
		public string Code { get; set; }

		public string PositionName { get; set; }
		public string DepartmentName { get; set; }
		public string BlockName { get; set; }
		public string DivizionName { get; set; }
		public string OrganizationName { get; set; }
		public string PathToImage { get; set; }

		public byte[] Photo { get; set; }
		public string Birthday { get; set; }
		public string ZodiacSign { get; set; }

		public string[] Phones { get; set; }
	}

	public class Phone
	{
		public string Number { get; set; }
	}

	public class FocusModification
	{
		public int positiveImageNumberForm { get; set; }
	}

}