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
    public class TabletsController : ApiController
    {
        private SkoleDBContext db = new SkoleDBContext();

        // GET: api/Tablets
        public IQueryable<Tablet> GetTablets()
        {
            return db.Tablets;
        }

        // GET: api/Tablets/5
        [ResponseType(typeof(Tablet))]
        public IHttpActionResult GetTablet(int id)
        {
            Tablet tablet = db.Tablets.Find(id);
            if (tablet == null)
            {
                return NotFound();
            }

            return Ok(tablet);
        }

        // PUT: api/Tablets/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTablet(int id, Tablet tablet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tablet.Uid)
            {
                return BadRequest();
            }

            db.Entry(tablet).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TabletExists(id))
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

        // POST: api/Tablets
        [ResponseType(typeof(Tablet))]
        public IHttpActionResult PostTablet(Tablet tablet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tablets.Add(tablet);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (TabletExists(tablet.Uid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tablet.Uid }, tablet);
        }

        // DELETE: api/Tablets/5
        [ResponseType(typeof(Tablet))]
        public IHttpActionResult DeleteTablet(int id)
        {
            Tablet tablet = db.Tablets.Find(id);
            if (tablet == null)
            {
                return NotFound();
            }

            db.Tablets.Remove(tablet);
            db.SaveChanges();

            return Ok(tablet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TabletExists(int id)
        {
            return db.Tablets.Count(e => e.Uid == id) > 0;
        }
    }
}