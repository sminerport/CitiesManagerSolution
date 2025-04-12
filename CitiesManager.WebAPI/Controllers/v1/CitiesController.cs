using Asp.Versioning;

using CitiesManager.Core.Models;
using CitiesManager.Infrastructure.DatabaseContext;

using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CitiesManager.WebAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    public class CitiesController : CustomControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Cities
        /// <summary>
        /// To get list of cities (including city id and city name) from 'cities' table
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<City>>> GetCities()
        {
            var cities =
                await _context.Cities
                .OrderBy(temp => temp.CityName).ToListAsync();

            return cities;
        }

        // GET: api/Cities/5
        [HttpGet("{cityId}")]
        public async Task<ActionResult<City>> GetCity(Guid cityId)
        {
            var city = await _context.Cities.FirstOrDefaultAsync(temp => temp.CityID == cityId);

            if (city == null)
            {
                return Problem(detail: "Invalid CityID", statusCode: 400, title: "City Search");
            }

            return Ok(city);
        }

        // PUT: api/Cities/5
        [HttpPut("{cityId}")]
        public async Task<IActionResult> PutCity(Guid cityId, [Bind(nameof(City.CityID), nameof(City.CityName))] City city)
        {
            if (cityId != city.CityID)
            {
                return BadRequest();
            }

            var existingCity = await _context.Cities.FindAsync(cityId);

            if (existingCity == null)
            {
                return NotFound();
            }

            existingCity.CityName = city.CityName;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CityExists(cityId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Cities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<City>> PostCity([Bind(nameof(City.CityID), nameof(City.CityName))] City city)
        {
            _context.Cities.Add(city);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCity", new { cityId = city.CityID }, city);
        }

        // DELETE: api/Cities/5
        [HttpDelete("{cityId}")]
        public async Task<IActionResult> DeleteCity(Guid cityId)
        {
            var city = await _context.Cities.FindAsync(cityId);
            if (city == null)
            {
                return NotFound(); // HTTP 404
            }

            _context.Cities.Remove(city);
            await _context.SaveChangesAsync();

            return NoContent(); // HTTP 204
        }

        private bool CityExists(Guid id)
        {
            return _context.Cities.Any(e => e.CityID == id);
        }
    }
}