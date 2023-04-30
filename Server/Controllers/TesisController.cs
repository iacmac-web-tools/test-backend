using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Newtonsoft.Json;
using System.Text.Json;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Controllers
{
    [ApiController]
    public class TesisController : Controller
    {
        [HttpGet]
        [Route("api/theses/all")]
        public IResult GetAllTheses()
        {
            using (DataBaseContext db = new DataBaseContext())
            {
                var tesises = db.Tesises.ToList();
                var response = new
                {
                    tesesis = tesises,
                    length = tesises.Count,
                    ids = tesises.Select(x => x.id).ToArray()
                };

                return Results.Json(response);
            }
        }

        [HttpPost]
        [Route("api/theses")]
        public void AddNewTheses([FromBody] JsonElement jsonData)
        {
            using (DataBaseContext db = new DataBaseContext())
            {
                var tesises = db.Tesises.ToList();
                Tesis? tesis = System.Text.Json.JsonSerializer.Deserialize<Tesis>(jsonData);
                if(tesises.Count == 0)
                {
                    tesis.id = 1;
                }
                else
                 tesis.id = tesises[tesises.Count - 1].id + 1;
                tesis.autor = tesis.autor.Replace("[object Object]", "").Trim();

                db.Tesises.Add(tesis);
                db.SaveChanges();
                Console.WriteLine();
                foreach (var a in db.Tesises.ToList())
                {
                    
                    Console.WriteLine(a.head + " - ID");
                }
                Console.WriteLine();
            }
        }

        [HttpGet]
        [Route("api/theses/{id}")]
        public IResult GetTesisInfo(int id)
        {
            using (DataBaseContext db = new DataBaseContext())
            {
                var tesises = db.Tesises.ToList();
                return Results.Json(tesises.FirstOrDefault(n => n.id == id));
            }
        }

        [HttpPut]
        [Route("api/theses/{id}")]
        public IResult UpdateTesis(int id, [FromBody] JsonElement jsonData)
        {
            using (DataBaseContext db = new DataBaseContext())
            {
                Tesis? tesises = null;
                Tesis? tesis = System.Text.Json.JsonSerializer.Deserialize<Tesis>(jsonData);
                foreach (var a in db.Tesises.ToList())
                {
                    if (a.id == id) { tesises = a; break; }
                }
                if (tesises != null)
                {
                    tesises.autorEmail = tesis.autorEmail;
                    tesises.autor = tesis.autor;
                    tesises.head = tesis.head;
                    tesises.body = tesis.body;
                    tesises.co_autor = tesis.co_autor;
                    tesises.autorWork = tesis.autorWork;
                    db.Tesises.Update(tesises);
                    db.SaveChanges();
                }
                var tesisesMas = db.Tesises.ToList();
                var response = new
                {
                    tesesis = tesisesMas,
                    length = tesisesMas.Count,
                    ids = tesisesMas.Select(x => x.id).ToArray()
                };

               
                return Results.Json(response);
            }
        }
        [HttpDelete]
        [Route("api/theses/{id}")]
        public IResult TesisDelete(int id)
        {
            using (DataBaseContext db = new DataBaseContext())
            {
                Tesis? tesis = null;
                foreach (var a in db.Tesises.ToList())
                {
                    if (a.id == id) { tesis = a; break; }
                }
                if(tesis != null)
                {
                    db.Tesises.Remove(tesis);
                    db.SaveChanges();
                }
                var tesises = db.Tesises.ToList();
                var response = new
                {
                    tesesis = tesises,
                    length = tesises.Count,
                    ids = tesises.Select(x => x.id).ToArray()
                };
                Console.WriteLine();

                return Results.Json(response);
            }
        }
    }
}
