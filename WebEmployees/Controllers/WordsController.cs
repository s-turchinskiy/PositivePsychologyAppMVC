using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PositivePsychologyAppHtml.Controllers
{
	[RequireHttps]
	public class WordsArrayAndCorrectNumberForm
	{
		public string[] WordsArray { get; set; }
		public string CorrectNumber { get; set; }
	}

	[RequireHttps]
	public class WordsController : Controller
	{

		private int GetNumber(int maxSize)
		{
			Random rnd = new Random();
			return rnd.Next(1, maxSize);
		}

		private int[] GetNegativeNumbersImages()
		{
			Random rnd = new Random();
			int[] negativeNumbersImages = new int[8];
			int currentNumber = 0;
			int nextValue;
			while (true)
			{
				nextValue = rnd.Next(1, 18);
				if (negativeNumbersImages.Where(a => a == nextValue).Count() != 0)
				{
					continue;
				}

				negativeNumbersImages[currentNumber] = nextValue;
				currentNumber++;
				if (currentNumber == 8)
				{
					break;
				}
			}

			return negativeNumbersImages;
		}

		public WordsArrayAndCorrectNumberForm GetWordsArrayAndCorrectNumberForm()
		{
			var wordsArray = new string[6];

			var correctNumber = GetNumber(6);
			wordsArray[correctNumber-1] = "Правильное слово";

			int currentNumber = 0;
			for (int i = 1; i <= 6; i++)
			{
				if (i == correctNumber)
				{
					continue;
				}

				wordsArray[i-1] = "Слово";
				currentNumber++;
			}

			return
				new WordsArrayAndCorrectNumberForm
				{
					WordsArray = wordsArray,
					CorrectNumber = correctNumber.ToString()
				};
		}

	}

}