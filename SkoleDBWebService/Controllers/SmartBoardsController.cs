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
    public class SmartBoardsController : ApiController
    {
        private SkoleDBContext db = new SkoleDBContext();

        // GET: api/SmartBoards
        public IQueryable<SmartBoard> GetSmartBoards()
        {
            return db.SmartBoards;
        }

        // GET: api/SmartBoards/5
        [ResponseType(typeof(SmartBoard))]
        public IHttpActionResult GetSmartBoard(int id)
        {
            SmartBoard smartBoard = db.SmartBoards.Find(id);
            if (smartBoard == null)
            {
                return NotFound();
            }

            return Ok(smartBoard);
        }

        // PUT: api/SmartBoards/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSmartBoard(int id, SmartBoard smartBoard)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != smartBoard.Uid)
            {
                return BadRequest();
            }

            db.Entry(smartBoard).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SmartBoardExists(id))
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

        // POST: api/SmartBoards
        [ResponseType(typeof(SmartBoard))]
        public IHttpActionResult PostSmartBoard(SmartBoard smartBoard)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SmartBoards.Add(smartBoard);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (SmartBoardExists(smartBoard.Uid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = smartBoard.Uid }, smartBoard);
        }

        // DELETE: api/SmartBoards/5
        [ResponseType(typeof(SmartBoard))]
        public IHttpActionResult DeleteSmartBoard(int id)
        {
            SmartBoard smartBoard = db.SmartBoards.Find(id);
            if (smartBoard == null)
            {
                return NotFound();
            }

            db.SmartBoards.Remove(smartBoard);
            db.SaveChanges();

            return Ok(smartBoard);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SmartBoardExists(int id)
        {
            return db.SmartBoards.Count(e => e.Uid == id) > 0;
        }
    }
}