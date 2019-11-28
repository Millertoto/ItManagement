using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using SkoleDBWebService;

namespace SkoleDBWebService.Controllers
{
    public class AccesoriesController : ApiController
    {
        private SkoleDBContext db = new SkoleDBContext();

        // GET: api/Accesories
        public IQueryable<Accesory> GetAccesories()
        {
            return db.Accesories;
        }

        // GET: api/Accesories/5
        [ResponseType(typeof(Accesory))]
        public IHttpActionResult GetAccesory(int id)
        {
            Accesory accesory = db.Accesories.Find(id);
            if (accesory == null)
            {
                return NotFound();
            }

            return Ok(accesory);
        }

        // PUT: api/Accesories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAccesory(int id, Accesory accesory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != accesory.Tid)
            {
                return BadRequest();
            }

            db.Entry(accesory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccesoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Accesories
        [ResponseType(typeof(Accesory))]
        public IHttpActionResult PostAccesory(Accesory accesory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Accesories.Add(accesory);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = accesory.Tid }, accesory);
        }

        // DELETE: api/Accesories/5
        [ResponseType(typeof(Accesory))]
        public IHttpActionResult DeleteAccesory(int id)
        {
            Accesory accesory = db.Accesories.Find(id);
            if (accesory == null)
            {
                return NotFound();
            }

            db.Accesories.Remove(accesory);
            db.SaveChanges();

            return Ok(accesory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AccesoryExists(int id)
        {
            return db.Accesories.Count(e => e.Tid == id) > 0;
        }
    }
}