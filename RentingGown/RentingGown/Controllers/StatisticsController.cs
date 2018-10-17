using RentingGown.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentingGown.Controllers
{
    public class StatisticsController : Controller
    {
        private static RentingGownDB db = new RentingGownDB();
        // GET: Statistics
        public ActionResult Statistics()
        {
            string s = "'s','s','s','s','s'";
            ViewBag.colors = string.Join(",", PopularColors().Keys.Take(5)).Trim();
            ViewBag.colorsValues = string.Join(",", PopularColors().Values.Take(5)).Trim();

            ViewBag.catgories = string.Join(",", PopularCatgory().Keys.Take(5)).Trim();
            ViewBag.catgoriesValues = string.Join(",", PopularCatgory().Values.Take(5)).Trim();

            return View();
        }

        public static Dictionary<string, string> PopularColors()
        {
            Dictionary<string, string> colors = new Dictionary<string, string>();
            foreach (Colors color in db.Colors)
            {
                int sum = 0;
                foreach (Rents rent in db.Rents)
                {
                    if (rent.Gowns.color == color.id_color)
                        sum++;

                }
                if (sum > 0)
                    colors.Add("'"+color.color+"'", sum.ToString());
            }
            List<KeyValuePair<string, string>> myList = colors.ToList();

           myList.Sort((pair1,pair2) => pair1.Value.CompareTo(pair2.Value));
            myList.Reverse();
            colors = myList.ToDictionary(x => x.Key, x => x.Value); 
            return colors;
        }

        public static Dictionary<string, string> PopularCatgory()
        {
            Dictionary<string, string> catgories = new Dictionary<string, string>();
            foreach (Catgories catgory in db.Catgories)
            {
                int sum = 0;
                foreach (Rents rent in db.Rents)
                {
                    if (rent.Gowns.id_catgory == catgory.id_catgory)
                        sum++;

                }
                if (sum > 0)
                    catgories.Add("'" + catgory.id_catgory + "'", sum.ToString());
            }
            List<KeyValuePair<string, string>> myList = catgories.ToList();

            myList.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));
            myList.Reverse();
            catgories = myList.ToDictionary(x => x.Key, x => x.Value);
            return catgories;
        }
    }
}