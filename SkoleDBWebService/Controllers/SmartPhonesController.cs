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
    public class SmartPhonesController : ApiController
    {
        private SkoleDBContext db = new SkoleDBContext();

        // GET: api/SmartPhones
        public IQueryable<SmartPhone> GetSmartPhone()
        {
            return db.SmartPhone;
        }

        // GET: api/SmartPhones/5
        [ResponseType(typeof(SmartPhone))]
        public IHttpActionResult GetSmartPhone(int id)
        {
            SmartPhone smartPhone = db.SmartPhone.Find(id);
            if (smartPhone == null)
            {
                return NotFound();
            }

            return Ok(smartPhone);
        }

        // PUT: api/SmartPhones/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSmartPhone(int id, SmartPhone smartPhone)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != smartPhone.Uid)
            {
                return BadRequest();
            }

            db.Entry(smartPhone).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SmartPhoneExists(id))
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

        // POST: api/SmartPhones
        [ResponseType(typeof(SmartPhone))]
        public IHttpActionResult PostSmartPhone(SmartPhone smartPhone)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SmartPhone.Add(smartPhone);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (SmartPhoneExists(smartPhone.Uid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = smartPhone.Uid }, smartPhone);
        }

        // DELETE: api/SmartPhones/5
        [ResponseType(typeof(SmartPhone))]
        public IHttpActionResult DeleteSmartPhone(int id)
        {
            SmartPhone smartPhone = db.SmartPhone.Find(id);
            if (smartPhone == null)
            {
                return NotFound();
            }

            db.SmartPhone.Remove(smartPhone);
            db.SaveChanges();

            return Ok(smartPhone);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SmartPhoneExists(int id)
        {
            return db.SmartPhone.Count(e => e.Uid == id) > 0;
        }
    }
}