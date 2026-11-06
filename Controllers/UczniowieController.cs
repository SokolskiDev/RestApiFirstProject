using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestApiFirstProject.Models;

namespace RestApiFirstProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UczniowieController : ControllerBase
    {
        private static readonly List<Uczen> uczniowie =
        [
            new Uczen { Id = 1, Imie = "Jan", Nazwisko = "Kowalski", Wiek = 15, Klasa = "1A" },
            new Uczen { Id = 2, Imie = "Anna", Nazwisko = "Nowak", Wiek = 16, Klasa = "2B" },
            new Uczen { Id = 3, Imie = "Piotr", Nazwisko = "Wiśniewski", Wiek = 17, Klasa = "3C" }
        ];

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(uczniowie);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var uczen = uczniowie.FirstOrDefault(u => u.Id == id);
            if (uczen == null)
            {
                return NotFound();
            }
            return Ok(uczen);
        }

        [HttpPost]
        public IActionResult NewUczen(Uczen uczen)
        {
            Console.WriteLine("controller: nowy uczeń");
            uczen.Id = uczniowie.Max(u => u.Id) + 1;
            uczniowie.Add(uczen);
            foreach (var item in uczniowie)
            {
                Console.WriteLine(item.Imie);
            }
            return CreatedAtAction(nameof(GetById), new { id = uczen.Id }, uczen);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Uczen dane)
        {
            var uczen = uczniowie.FirstOrDefault(u => u.Id == id);
            if (uczen == null)
            {
                return NotFound();
            }
            uczen.Imie = dane.Imie;
            uczen.Nazwisko = dane.Nazwisko;
            uczen.Wiek = dane.Wiek;
            uczen.Klasa = dane.Klasa;
            return Ok(uczen);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var uczen = uczniowie.FirstOrDefault(u => u.Id == id);
            if (uczen == null)
            {
                return NotFound();
            }
            uczniowie.Remove(uczen);
            return NoContent();
        }
    }
}
