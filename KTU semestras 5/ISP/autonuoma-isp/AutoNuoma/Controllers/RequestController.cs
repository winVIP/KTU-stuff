using AutoNuoma.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Web.UI.DataVisualization.Charting;
using Color = System.Drawing.Color;
using Font = System.Drawing.Font;


namespace AutoNuoma.Controllers
{
    public class RequestController : Controller
    {
        private AutonuomaDbContext db = new AutonuomaDbContext();
        // GET: Request
        [Authorize(Roles = "Admin")]
        public ActionResult RequestList()
        {
            return View(db.Requests.Include("Car").Include("User"));
        }

        // GET: Request/RequestHistory/1
        [Authorize(Roles = "Member")]
        public ActionResult RequestHistory(int id)
        {
            var requests = db.Requests.Where(col => col.UserId == id).ToList(); // TODO: Replace with real member id
            return View(requests);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Report()
        {
            Document document = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(Server.MapPath("/Ataskaita") + @"\Ataskaita.pdf", FileMode.Create));
            document.Open();

            BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);

            iTextSharp.text.Font font = new iTextSharp.text.Font(bf, 20, iTextSharp.text.Font.NORMAL);

            Paragraph paragraph = new Paragraph(new Chunk("Ataskaita", font));
            paragraph.Alignment = Element.ALIGN_CENTER;
            document.Add(paragraph);

            font = new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.NORMAL);
            paragraph = new Paragraph(new Chunk("Lentelė, kurioje matomas užsakymų kiekis pagal markę \n \n", font));
            paragraph.Alignment = Element.ALIGN_CENTER;
            document.Add(paragraph);

            PdfPTable table = new PdfPTable(db.Cars.Select(b => b.Manufacturer).Distinct().Count());
            List<string> manufacturers = new List<string>();
            foreach(string manufacturer in db.Cars.Select(b => b.Manufacturer).Distinct())
            {
                table.AddCell(manufacturer);
                manufacturers.Add(manufacturer);
            }
            Dictionary<string, int> requests = new Dictionary<string, int>();
            foreach(string manufacturer in manufacturers)
            {
                requests.Add(manufacturer, db.Requests.Include("Car").Where(b => b.Car.Manufacturer == manufacturer).Count());
            }

            foreach(string manufacturer in manufacturers)
            {
                table.AddCell(requests[manufacturer].ToString());
            }
            document.Add(table);

            iTextSharp.text.Image chartImage = iTextSharp.text.Image.GetInstance(Chart(requests));

            document.Add(chartImage);

            document.Close();
            return View();
        }

        private Byte[] Chart(Dictionary<string, int> requests)
        {
            var chart = new Chart
            {
                Width = 300,
                Height = 450,
                RenderType = RenderType.ImageTag,
                AntiAliasing = AntiAliasingStyles.All,
                TextAntiAliasingQuality = TextAntiAliasingQuality.High
            };

            chart.Titles.Add("Uzsakymai pagal marke");

            chart.ChartAreas.Add("");
            chart.ChartAreas[0].AxisX.Title = "Markes";
            chart.ChartAreas[0].AxisY.Title = "Uzsakymai";
            chart.ChartAreas[0].AxisX.LabelStyle.Angle = -90;
            chart.ChartAreas[0].BackColor = Color.White;

            chart.Series.Add("");
            chart.Series[0].ChartType = SeriesChartType.Column;

            foreach (KeyValuePair<string, int> entry in requests)
            {
                chart.Series[0].Points.AddXY(entry.Key, entry.Value);
            }
            using (var chartimage = new MemoryStream())
            {
                chart.SaveImage(chartimage, ChartImageFormat.Png);
                return chartimage.GetBuffer();
            }
        }

        // GET: Request/Details/5
        [Authorize]
        public ActionResult RequestDetails(int id) //int id
        {
            return View(db.Requests.Include("User").Include("Car").Where(b => b.Id == id).First());
        }

        public ActionResult RequestAccept(int id)
        {
            db.Requests.Find(id).IsConfirmed = true;
            db.SaveChanges();
            return RedirectToAction("RequestList");
        }

        public ActionResult RequestDeny(int id)
        {
            db.Requests.Find(id).IsConfirmed = false;
            db.SaveChanges();
            return RedirectToAction("RequestList");
        }

        // GET: Request/Create
        [Authorize(Roles = "Member")]
        public ActionResult RequestCreate(int id)
        {
            var car = db.Cars.Find(id);
            ViewBag.CarId = car.Id;
            return View();
        }

        // POST: Request/Create
        [HttpPost]
        [Authorize(Roles = "Member")]
        [ValidateAntiForgeryToken]
        public ActionResult RequestCreate(FormCollection collection)
        {
            try
            {
                var startingDateValues = collection["StartingDate"].Split('-');
                var startingDate = new DateTime(int.Parse(startingDateValues[0]), int.Parse(startingDateValues[1]), int.Parse(startingDateValues[2]));
                var endingDateValues = collection["EndingDate"].Split('-');
                var endingDate = new DateTime(int.Parse(endingDateValues[0]), int.Parse(endingDateValues[1]), int.Parse(endingDateValues[2]));
                var car = db.Cars.Find(int.Parse(collection["car-id"]));
                var request = new Request();
                request.Car = car;
                request.OdometerReadingAtStart = car.OdometerReading;
                request.OdometerReadingAtEnd = 0;
                request.Deposit = 100;
                request.IsReturned = false;
                request.IsConfirmed = false;
                request.IsPaid = false;
                request.StartingDate = startingDate;
                request.EndingDate = endingDate;
                request.UserId = 11; // TODO: Replace with real member id
                request.CarId = car.Id;
                db.Requests.Add(request);
                db.SaveChanges();
                return RedirectToAction("RequestHistory/" + request.UserId.ToString());
            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        // GET: Request/Edit/5
        [Authorize(Roles = "Member")]
        public ActionResult RequestEdit(int id)
        {
            var request = db.Requests.Find(id);
            var startingDate = request.StartingDate.ToString().Split(' ')[0];
            var endingDate = request.EndingDate.ToString().Split(' ')[0];
            ViewBag.StartingDate = startingDate;
            ViewBag.EndingDate = endingDate;
            return View(request);
        }

        // POST: Request/Edit/5
        [HttpPost]
        [Authorize(Roles = "Member")]
        [ValidateAntiForgeryToken]
        public ActionResult RequestEdit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                var request = db.Requests.Find(id);
                var startingDateValues = collection["StartingDate"].Split('-');
                var startingDate = new DateTime(int.Parse(startingDateValues[0]), int.Parse(startingDateValues[1]), int.Parse(startingDateValues[2]));
                var endingDateValues = collection["EndingDate"].Split('-');
                var endingDate = new DateTime(int.Parse(endingDateValues[0]), int.Parse(endingDateValues[1]), int.Parse(endingDateValues[2]));
                request.StartingDate = startingDate;
                request.EndingDate = endingDate;
                db.SaveChanges();
                return RedirectToAction("RequestHistory/" + request.UserId.ToString());
            }
            catch
            {
                return View();
            }
        }

        // GET: Request/Delete/5
        [Authorize(Roles = "Member")]
        public ActionResult Delete(int id)
        {
            var request = db.Requests.Find(id);
            return View(request);
        }

        // POST: Request/Delete/5
        [HttpPost]
        [Authorize(Roles = "Member")]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                var request = db.Requests.Find(id);
                db.Requests.Remove(request);
                db.SaveChanges();
                return RedirectToAction("RequestList");
            }
            catch (Exception e)
            {
                throw e;
                //return View();
            }
        }
    }
}
