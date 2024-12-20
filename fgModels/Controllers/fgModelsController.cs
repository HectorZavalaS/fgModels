using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using fgModels.Models;
using Microsoft.AspNet.Identity.Owin;

namespace fgModels.Controllers
{
    public class fgModelsController : Controller
    {
        private siixsem_main_dbEntities db = new siixsem_main_dbEntities();
        private ApplicationUserManager _userManager;
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        // GET: fgModels
        public ActionResult Index()
        {
            var siixsem_sidesModel_td = db.siixsem_sidesModel_td.Include(s => s.siixsem_models_t).Include(s => s.siixsem_sides_t);
            return View(siixsem_sidesModel_td.ToList());
        }

        // GET: fgModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            siixsem_sidesModel_td siixsem_sidesModel_td = db.siixsem_sidesModel_td.Find(id);
            if (siixsem_sidesModel_td == null)
            {
                return HttpNotFound();
            }
            return View(siixsem_sidesModel_td);
        }

        // GET: fgModels/Create
        public ActionResult Create()
        {
            ViewBag.se_id_model = new SelectList(db.siixsem_models_t, "se_id_model", "se_description");
            ViewBag.se_id_side = new SelectList(db.siixsem_sides_t, "se_id_side", "se_description");
            return View();
        }

        // POST: fgModels/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "se_id_detail,se_id_model,se_id_side,se_part_num_tr,se_int_part_num,se_cust_part_num,se_oracle_par_num,se_assembly_name")] siixsem_sidesModel_td siixsem_sidesModel_td)
        {
            if (ModelState.IsValid)
            {
                db.siixsem_sidesModel_td.Add(siixsem_sidesModel_td);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.se_id_model = new SelectList(db.siixsem_models_t, "se_id_model", "se_description", siixsem_sidesModel_td.se_id_model);
            ViewBag.se_id_side = new SelectList(db.siixsem_sides_t, "se_id_side", "se_description", siixsem_sidesModel_td.se_id_side);
            return View(siixsem_sidesModel_td);
        }

        // GET: fgModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            siixsem_sidesModel_td siixsem_sidesModel_td = db.siixsem_sidesModel_td.Find(id);
            if (siixsem_sidesModel_td == null)
            {
                return HttpNotFound();
            }
            ViewBag.se_id_model = new SelectList(db.siixsem_models_t, "se_id_model", "se_description", siixsem_sidesModel_td.se_id_model);
            ViewBag.se_id_side = new SelectList(db.siixsem_sides_t, "se_id_side", "se_description", siixsem_sidesModel_td.se_id_side);
            return View(siixsem_sidesModel_td);
        }

        // POST: fgModels/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "se_id_detail,se_id_model,se_id_side,se_part_num_tr,se_int_part_num,se_cust_part_num,se_oracle_par_num,se_assembly_name")] siixsem_sidesModel_td siixsem_sidesModel_td)
        {
            if (ModelState.IsValid)
            {
                if (siixsem_sidesModel_td.se_assembly_name == null)
                    siixsem_sidesModel_td.se_assembly_name = "";
                if (siixsem_sidesModel_td.se_int_part_num == null)
                    siixsem_sidesModel_td.se_int_part_num = "";
                if (siixsem_sidesModel_td.se_part_num_tr == null)
                    siixsem_sidesModel_td.se_part_num_tr = "";
                db.Entry(siixsem_sidesModel_td).State = EntityState.Modified;
                db.SaveChanges();

                string cookieName = FormsAuthentication.FormsCookieName; //Find cookie name
                HttpCookie authCookie = HttpContext.Request.Cookies[cookieName]; //Get the cookie by it's name
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value); //Decrypt it
                string UserName = ticket.Name; //You have the UserName!
                getModelByID_Result model = db.getModelByID(siixsem_sidesModel_td.se_id_model).First();
                getSideById_Result side = db.getSideById(siixsem_sidesModel_td.se_id_side).First();
                
                logger.Info("Se cambio numero de parte del modelo: " + model.se_description + " Lado: " + side.se_description + " Usuario: " + UserName);

                return RedirectToAction("Index","Home");
            }
            ViewBag.se_id_model = new SelectList(db.siixsem_models_t, "se_id_model", "se_description", siixsem_sidesModel_td.se_id_model);
            ViewBag.se_id_side = new SelectList(db.siixsem_sides_t, "se_id_side", "se_description", siixsem_sidesModel_td.se_id_side);
            return View(siixsem_sidesModel_td);
        }

        // GET: fgModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            siixsem_sidesModel_td siixsem_sidesModel_td = db.siixsem_sidesModel_td.Find(id);
            if (siixsem_sidesModel_td == null)
            {
                return HttpNotFound();
            }
            return View(siixsem_sidesModel_td);
        }

        // POST: fgModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            siixsem_sidesModel_td siixsem_sidesModel_td = db.siixsem_sidesModel_td.Find(id);
            db.siixsem_sidesModel_td.Remove(siixsem_sidesModel_td);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }
            }
            base.Dispose(disposing);
        }
    }
}
