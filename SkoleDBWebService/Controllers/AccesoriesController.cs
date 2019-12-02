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
        public IQueryable<Accesories> GetAccesories()
        {
            return db.Accesories;
        }

        // GET: api/Accesories/5
        [ResponseType(typeof(Accesories))]
        public IHttpActionResult GetAccesories(int id)
        {
            Accesories accesories = db.Accesories.Find(id);
            if (accesories == null)
            {
                return NotFound();
            }

            return Ok(accesories);
        }

        // PUT: api/Accesories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAccesories(int id, Accesories accesories)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != accesories.Tid)
            {
                return BadRequest();
            }

            db.Entry(accesories).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccesoriesExists(id))
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
        [ResponseType(typeof(Accesories))]
        public IHttpActionResult PostAccesories(Accesories accesories)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Accesories.Add(accesories);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = accesories.Tid }, accesories);
        }

        // DELETE: api/Accesories/5
        [ResponseType(typeof(Accesories))]
        public IHttpActionResult DeleteAccesories(int id)
        {
            Accesories accesories = db.Accesories.Find(id);
            if (accesories == null)
            {
                return NotFound();
            }

            db.Accesories.Remove(accesories);
            db.SaveChanges();

            return Ok(accesories);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AccesoriesExists(int id)
        {
            return db.Accesories.Count(e => e.Tid == id) > 0;
        }
    }
}