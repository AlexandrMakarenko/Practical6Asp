using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using ScheduleApp.Models;

namespace ScheduleApp.Controllers
{
    public class HomeController : Controller
    {
        // изменить на путь к App_Data, где скачан файл
        private const string BasePath =
            @"D:\Рабочий стол\Практические\Practical6Asp\ScheduleApp\App_Data\";

        public ActionResult СhangeWeek(string type)
        {
            var config = new Config() { ThisWeek = type};
            using (var sw = new StreamWriter(BasePath + "Config.json"))
            {
                sw.Write(JsonConvert.SerializeObject(config));
            }
            return RedirectToAction("Index");
        }

        // вызов смены недели
        public ActionResult Index()
        {
            ViewBag.Title = "Вся недели"; // подпись вкладки
            ViewBag.Json = GetSchedule();
            return View();
        }

        // выбор дня, ДЛЯ ПОКАЗА в зависимости от недели меняется расписание
        public ActionResult Schedule(int idDay)
        {
            switch (idDay)
            {
                case 1:
                    ViewBag.Json = GetDay("Первый день");
                    break;
                case 2:
                    ViewBag.Json = GetDay("Второй день");
                    break;
                case 3:
                    ViewBag.Json = GetDay("Третий день");
                    break;
                case 4:
                    ViewBag.Json = GetDay("Четвертый день");
                    break;
                case 5:
                    ViewBag.Json = GetDay("Пятый день");
                    break;
                case 6:
                    ViewBag.Json = GetDay("Нулевой день");
                    break;
                default:
                    return Redirect("https://localhost:44320/"); 
            }
            return View();
        }

        //смена недели
        private Schedule GetSchedule()
        {
            using (var sr = new StreamReader(PathToData()))
            {
                return JsonConvert.DeserializeObject<Schedule>(sr.ReadToEnd());
            }
        }

        // считывание из json файла инфы за определенный день
        private Day GetDay(string dayOfWeek)
        {
            using (var sr = new StreamReader(PathToData()))
            {
                return JsonConvert.DeserializeObject<Schedule>(sr.ReadToEnd()).Days.First(x => x.Type == dayOfWeek);
            }
        }

        // смена недели, по дефолту стоит верхняя
        private string PathToData()
        {
            using (var sr = new StreamReader(BasePath + "Config.json"))
            {
                var thisWeek = JsonConvert.DeserializeObject<Config>(sr.ReadToEnd()).ThisWeek;
                switch (thisWeek)
                {
                    case "Lower":
                        return BasePath + "LowerSchedule.json";
                    case "Upper":
                        return BasePath + "UpperSchedule.json";
                    default:
                        return "UpperSchedule.json";
                }
            }
        }
    }
}