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
    public class ErrorsController : ApiController
    {
        private SkoleDBContext db = new SkoleDBContext();

        // GET: api/Errors
        public IQueryable<Errors> GetErrors()
        {
            return db.Errors;
        }

        // GET: api/Errors/5
        [ResponseType(typeof(Errors))]
        public IHttpActionResult GetErrors(int id)
        {
            Errors errors = db.Errors.Find(id);
            if (errors == null)
            {
                return NotFound();
            }

            return Ok(errors);
        }

        // PUT: api/Errors/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutErrors(int id, Errors errors)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != errors.Fid)
            {
                return BadRequest();
            }

            db.Entry(errors).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ErrorsExists(id))
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

        // POST: api/Errors
        [ResponseType(typeof(Errors))]
        public IHttpActionResult PostErrors(Errors errors)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Errors.Add(errors);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = errors.Fid }, errors);
        }

        // DELETE: api/Errors/5
        [ResponseType(typeof(Errors))]
        public IHttpActionResult DeleteErrors(int id)
        {
            Errors errors = db.Errors.Find(id);
            if (errors == null)
            {
                return NotFound();
            }

            db.Errors.Remove(errors);
            db.SaveChanges();

            return Ok(errors);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ErrorsExists(int id)
        {
            return db.Errors.Count(e => e.Fid == id) > 0;
        }
    }
}