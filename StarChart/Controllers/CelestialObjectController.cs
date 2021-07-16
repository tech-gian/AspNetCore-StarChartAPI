using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using StarChart.Data;
using StarChart.Models;

namespace StarChart.Controllers
{
    [Route("")]
    [ApiController]
    public class CelestialObjectController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CelestialObjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id:int}", Name ="GetById")]
        public IActionResult GetById(int id)
        {
            var celestialObject = _context.CelestialObjects.FirstOrDefault(c => c.Id == id);

            if (celestialObject == null)
            {
                return NotFound();
            }
            var orbitedObject = _context.CelestialObjects.FirstOrDefault(c => c.OrbitedObjectId == id);
            orbitedObject.Satellites = new List<CelestialObject>();

            return Ok(celestialObject);
        }

        [HttpGet("{name}")]
        public IActionResult GetByName(string name)
        {
            var celestialObject = _context.CelestialObjects.FirstOrDefault(c => c.Name == name);
            if (celestialObject == null)
            {
                return NotFound();
            }
            var orbitedObject = _context.CelestialObjects.FirstOrDefault(c => c.OrbitedObjectId == celestialObject.Id);
            orbitedObject.Satellites = new List<CelestialObject>();

            return Ok(celestialObject);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var CelestialObjects = _context.CelestialObjects;
            foreach (var cel in CelestialObjects)
            {
                cel.Satellites = new List<CelestialObject>();
            }

            return Ok(CelestialObjects);
        }
    }
}
