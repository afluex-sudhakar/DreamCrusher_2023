﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DreamCrusher.Models;
using DreamCrusher.Filter;
using System.IO;

namespace DreamCrusher.Controllers
{
    public class MasterController : AdminBaseController
    {
        #region ProductMaster
        public ActionResult ProductList(Master model)
        {
            List<Master> lst = new List<Master>();
            DataSet ds = model.ProductList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.ProductID = r["Pk_ProductId"].ToString();
                    obj.ProductName = r["ProductName"].ToString();
                    obj.ProductPrice = r["ProductPrice"].ToString();
                    obj.IGST = r["IGST"].ToString();
                    obj.CGST = r["CGST"].ToString();
                    obj.SGST = (r["SGST"].ToString());
                    obj.BinaryPercent = (r["BinaryPercent"].ToString());
                    obj.DirectPercent = (r["DirectPercent"].ToString());
                    obj.ROIPercent = (r["ROIPercent"].ToString());
                    obj.BV = (r["BV"].ToString());
                    obj.DirectIncome = (r["DirectIncome"].ToString());
                    obj.SelfDirectIncomeLimit = (r["SelfDirectIncomeLimit"].ToString());
                    obj.status = (r["Status"].ToString());
                    lst.Add(obj);
                }
                model.lstproduct = lst;
            }
            return View(model);
        }

        public ActionResult DeleteProduct(string id)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Master obj = new Master();
                obj.ProductID = id;
                obj.AddedBy = Session["PK_AdminId"].ToString();
                DataSet ds = obj.DeleteProduct();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if ((ds.Tables[0].Rows[0][0].ToString() == "1"))
                    {
                        TempData["Product"] = "Product Deleted successfully";
                        FormName = "ProductList";
                        Controller = "Master";
                    }
                    else
                    {
                        TempData["Product"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "ProductList";
                        Controller = "Master";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Product"] = ex.Message;
                FormName = "ProductList";
                Controller = "Master";
            }

            return RedirectToAction(FormName, Controller);
        }

        public ActionResult ProductMaster(string productID)
        {
            if (productID != null)
            {
                Master obj = new Master();
                try
                {
                    obj.ProductID = productID;

                    DataSet ds = obj.ProductList();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        obj.ProductID = ds.Tables[0].Rows[0]["Pk_ProductId"].ToString();
                        obj.ProductName = ds.Tables[0].Rows[0]["ProductName"].ToString();
                        obj.ProductPrice = ds.Tables[0].Rows[0]["ProductPrice"].ToString();
                        obj.IGST = ds.Tables[0].Rows[0]["IGST"].ToString();
                        obj.CGST = ds.Tables[0].Rows[0]["CGST"].ToString();
                        obj.SGST = ds.Tables[0].Rows[0]["SGST"].ToString();
                        obj.BinaryPercent = ds.Tables[0].Rows[0]["BinaryPercent"].ToString();
                        obj.DirectPercent = ds.Tables[0].Rows[0]["DirectPercent"].ToString();
                        obj.ROIPercent = ds.Tables[0].Rows[0]["ROIPercent"].ToString();
                        obj.BV = ds.Tables[0].Rows[0]["BV"].ToString();
                        obj.DirectIncome = ds.Tables[0].Rows[0]["DirectIncome"].ToString();
                        obj.SelfDirectIncomeLimit = ds.Tables[0].Rows[0]["SelfDirectIncomeLimit"].ToString();

                    }
                }
                catch (Exception ex)
                {
                    TempData["Product"] = ex.Message;
                }
                return View(obj);
            }
            else
            {
                return View();
            }
            
        }

        public ActionResult SaveProduct(string ProductName, string ProductPrice, string IGST, string CGST, string SGST, string BinaryPercent, string DirectPercent, string ROIPercent,string BV, string DirectIncome, string SelfDirectIncomeLimit)
        {
            Master obj = new Master();
            try
            {
                obj.ProductName = ProductName;
                obj.ProductPrice = ProductPrice;
                obj.IGST = IGST;
                obj.CGST = CGST;
                obj.SGST = SGST;
                obj.BinaryPercent = BinaryPercent;
                obj.DirectPercent = DirectPercent;
                obj.ROIPercent = ROIPercent;
                obj.BV = BV;
                obj.DirectIncome = DirectIncome;
                obj.SelfDirectIncomeLimit = SelfDirectIncomeLimit;
                obj.AddedBy = Session["PK_AdminId"].ToString();

                DataSet ds = obj.SaveProduct();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if ((ds.Tables[0].Rows[0][0].ToString() == "1"))
                    {
                        obj.Result = "Product Saved Successfully";
                    }
                    else
                    {
                        obj.Result = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                obj.Result = ex.Message;
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateProduct(string ProductID, string ProductName, string ProductPrice, string IGST, string CGST, string SGST, string BinaryPercent, string DirectPercent, string ROIPercent,string BV, string DirectIncome, string SelfDirectIncomeLimit)
        {
            Master obj = new Master();
            try
            {
                obj.ProductID = ProductID;
                obj.ProductName = ProductName;
                obj.ProductPrice = ProductPrice;
                obj.IGST = IGST;
                obj.CGST = CGST;
                obj.SGST = SGST;
                obj.BinaryPercent = BinaryPercent;
                obj.DirectPercent = DirectPercent;
                obj.ROIPercent = ROIPercent;
                obj.BV = BV;
                obj.DirectIncome = DirectIncome;
                obj.SelfDirectIncomeLimit = SelfDirectIncomeLimit;
                obj.UpdatedBy = Session["PK_AdminId"].ToString();

                DataSet ds = obj.UpdateProduct();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if ((ds.Tables[0].Rows[0][0].ToString() == "1"))
                    {
                        obj.Result = "Product Updated Successfully";
                    }
                    else
                    {
                        obj.Result = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
              
            }
            catch (Exception ex)
            {
                obj.Result = ex.Message;
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region NewsMaster

        public ActionResult AddNews(string NewsID)
        {
            if (NewsID != null)
            {
                Master obj = new Master();
                try
                {
                    obj.NewsID = NewsID;

                    DataSet ds = obj.NewsList();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        obj.NewsID = ds.Tables[0].Rows[0]["PK_NewsID"].ToString();
                        obj.NewsDate = ds.Tables[0].Rows[0]["NewsDate"].ToString();
                        obj.NewsHeading = ds.Tables[0].Rows[0]["NewsHeading"].ToString();
                        obj.NewsBody = ds.Tables[0].Rows[0]["NewsBody"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    TempData["News"] = ex.Message;
                }
                return View(obj);
            }
            else
            {
                return View();
            }
        }

        public ActionResult SaveNews(string NewsHeading, string NewsBody)
        {
            Master obj = new Master();
            try
            {
                obj.NewsHeading = NewsHeading;
                obj.NewsBody = NewsBody;
                obj.AddedBy = Session["PK_AdminId"].ToString();

                DataSet ds = obj.SaveNews();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if ((ds.Tables[0].Rows[0][0].ToString() == "1"))
                    {
                        obj.Result = "News Saved Successfully";
                    }
                    else
                    {
                        obj.Result = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                obj.Result = ex.Message;
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateNews(string NewsID, string NewsHeading, string NewsBody)
        {
            Master obj = new Master();
            try
            {
                obj.NewsID = NewsID;
                obj.NewsHeading = NewsHeading;
                obj.NewsBody = NewsBody;
                obj.UpdatedBy = Session["PK_AdminId"].ToString();

                DataSet ds = obj.UpdateNews();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if ((ds.Tables[0].Rows[0][0].ToString() == "1"))
                    {
                        obj.Result = "News Updated Successfully";
                    }
                    else
                    {
                        obj.Result = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                obj.Result = ex.Message;
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult NewsList(Master model)
        {
            List<Master> lst = new List<Master>();
            DataSet ds = model.NewsList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.NewsID = r["PK_NewsID"].ToString();
                    obj.NewsDate = r["NewsDate"].ToString();
                    obj.NewsHeading = r["NewsHeading"].ToString();
                    obj.NewsBody = r["NewsBody"].ToString();
                    
                    lst.Add(obj);
                }
                model.lstNews = lst;
            }
            return View(model);
        }

        public ActionResult DeleteNews(string id)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Master obj = new Master();
                obj.NewsID = id;
                obj.AddedBy = Session["PK_AdminId"].ToString();
                DataSet ds = obj.DeleteNews();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if ((ds.Tables[0].Rows[0][0].ToString() == "1"))
                    {
                        TempData["Product"] = "News Deleted Successfully";
                        FormName = "NewsList";
                        Controller = "Master";
                    }
                    else
                    {
                        TempData["Product"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "NewsList";
                        Controller = "Master";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Product"] = ex.Message;
                FormName = "NewsList";
                Controller = "Master";
            }

            return RedirectToAction(FormName, Controller);
        }

        #endregion

        #region GalleryMaster
        
        public ActionResult GalleryMaster(Master model)
        {
            return View(model);
        }


        [HttpPost]
        [OnAction(ButtonName = "btnSave")]
        [ActionName("GalleryMaster")]
        public ActionResult SaveGalleryMaster(Master model, IEnumerable<HttpPostedFileBase> postedFile)
        {
            model.AddedBy = Session["Pk_AdminId"].ToString();
            try
            {
                foreach (var file in postedFile)
                {
                    if (file != null && file.ContentLength > 0)
                    {

                        model.Image = "/images/GalleryImages/" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                        file.SaveAs(Path.Combine(Server.MapPath(model.Image)));
                    }
                }

                DataSet ds = model.SavingGalleryMaster();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {

                        TempData["GalleryMaster"] = "Image Saved Successully";
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        TempData["GalleryMaster"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }

            }
            catch (Exception ex)
            {

                TempData["GalleryMaster"] = ex.Message;
            }
            return RedirectToAction("GalleryMaster");
        }


        public ActionResult GalleryMasterList(Master model)
        {
            List<Master> list = new List<Master>();
            DataSet ds = model.GetGalleryList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.PK_GalleryID = r["PK_GalleryID"].ToString();
                    obj.Image = r["Image"].ToString();

                    list.Add(obj);
                }
                model.lstGallery1 = list;
            }
            return View(model);
        }

        public ActionResult DeleteImage(string PK_GalleryID)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Master obj = new Master();
                obj.PK_GalleryID = PK_GalleryID;
                obj.AddedBy = Session["PK_AdminId"].ToString();
                DataSet ds = obj.DeleteGalleryImages();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["GalleryMasterList"] = "Image Deleted Successfully";
                        FormName = "GalleryMasterList";
                        Controller = "Master";
                    }
                    else
                    {
                        TempData["GalleryMasterList"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["GalleryMasterList"] = ex.Message;
            }

            return RedirectToAction(FormName,Controller);
        }
        #endregion

        #region CourseMaster

        public ActionResult CourseMaster(string CourseID)
        {
            if (CourseID != null)
            {
                Master obj = new Master();
                try
                {
                    obj.CourseID = CourseID;
                    DataSet ds = obj.CourseList();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        obj.CourseID = ds.Tables[0].Rows[0]["Pk_CourseId"].ToString();
                        obj.CourseName = ds.Tables[0].Rows[0]["CourseName"].ToString();
                        obj.CourseImage = ds.Tables[0].Rows[0]["CourseImage"].ToString();
                        obj.CourseLink = ds.Tables[0].Rows[0]["CourseLink"].ToString();
                        obj.Title = ds.Tables[0].Rows[0]["Title"].ToString();
                        obj.Description = ds.Tables[0].Rows[0]["Description"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    TempData["Course"] = ex.Message;
                }
                return View(obj);
            }
            else
            {
                return View();
            }
        }


        [HttpPost]
        [ActionName("CourseMaster")]
        [OnAction(ButtonName = "btnSaveCourse")]
        public ActionResult SaveCourse(Master obj, HttpPostedFileBase CourseImage)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                if (CourseImage != null)
                {
                    obj.CourseImage = "../CourseImages/" + Guid.NewGuid() + Path.GetExtension(CourseImage.FileName);
                    CourseImage.SaveAs(Path.Combine(Server.MapPath(obj.CourseImage)));
                }
                obj.AddedBy = Session["PK_AdminId"].ToString();
                DataSet ds = obj.SaveCourse();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if ((ds.Tables[0].Rows[0][0].ToString() == "1"))
                    {
                        TempData["Course"] = "Course saved successfully";
                    }
                    else
                    {
                        TempData["ErrCourse"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                obj.Result = ex.Message;
            }
             FormName = "CourseMaster";
             Controller = "Master";
            
            return RedirectToAction(FormName, Controller);
        }

        public ActionResult CourseList(Master model)
        {
            List<Master> lst = new List<Master>();
            DataSet ds = model.CourseList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.CourseID = r["Pk_CourseId"].ToString();
                    obj.CourseName = r["CourseName"].ToString();
                    obj.CourseImage = r["CourseImage"].ToString();
                    obj.CourseDate = r["CourseDate"].ToString();
                    obj.CourseLink = r["CourseLink"].ToString();
                    obj.Title = r["Title"].ToString();
                    obj.Description = r["Description"].ToString();
                    lst.Add(obj);
                }
                model.lstCourse = lst;
            }
            return View(model);
        }


        [HttpPost]
        [ActionName("CourseMaster")]
        [OnAction(ButtonName = "btnUpdateCourse")]
        public ActionResult UpdateCourse(Master obj, HttpPostedFileBase CourseImage)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                if (CourseImage != null)
                {
                    obj.CourseImage = "../CourseImages/" + Guid.NewGuid() + Path.GetExtension(CourseImage.FileName);
                    CourseImage.SaveAs(Path.Combine(Server.MapPath(obj.CourseImage)));
                }
                obj.UpdatedBy = Session["PK_AdminId"].ToString();
                DataSet ds = obj.UpdateCourse();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if ((ds.Tables[0].Rows[0][0].ToString() == "1"))
                    {
                        TempData["Course"] = "Course updated successfully";
                    }
                    else
                    {
                        TempData["ErrCourse"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                obj.Result = ex.Message;
            }
            FormName = "CourseMaster";
            Controller = "Master";

            return RedirectToAction(FormName, Controller);
        }


        public ActionResult DeleteCourse(string id)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Master obj = new Master();
                obj.CourseID = id;
                obj.AddedBy = Session["PK_AdminId"].ToString();
                DataSet ds = obj.DeleteCourse();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if ((ds.Tables[0].Rows[0][0].ToString() == "1"))
                    {
                        TempData["Course"] = "Course deleted successfully";
                        FormName = "CourseList";
                        Controller = "Master";
                    }
                    else
                    {
                        TempData["Course"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "CourseList";
                        Controller = "Master";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Course"] = ex.Message;
                FormName = "CourseList";
                Controller = "Master";
            }

            return RedirectToAction(FormName, Controller);
        }
        #endregion

        #region Allot Courses On Package

        public ActionResult AllotCoursesOnPackage(Master model)
        {
            #region ddlPackage
            Master obj = new Master();
            int count = 0;
            List<SelectListItem> ddlPackage = new List<SelectListItem>();
            DataSet dsPackage = obj.GetPackageList();
            if (dsPackage != null && dsPackage.Tables.Count > 0 && dsPackage.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPackage.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlPackage.Add(new SelectListItem { Text = "Select Package", Value = "0" });
                    }
                    ddlPackage.Add(new SelectListItem { Text = r["ProductName"].ToString(), Value = r["PK_ProductID"].ToString() });
                    count = count + 1;
                }
            }
            ViewBag.ddlPackage = ddlPackage;
            #endregion

            List<Master> lst = new List<Master>();
            DataSet ds = model.GetCourseListForAllotCourses();

            //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //{
            //    foreach (DataRow r in ds.Tables[0].Rows)
            //    {
            //        Master obj1 = new Master();
            //        obj1.CourseID = r["Pk_CourseId"].ToString();
            //        obj1.CourseName = r["CourseName"].ToString();
            //        obj1.CourseImage = r["CourseImage"].ToString();
            //        obj1.CourseDate = r["CourseDate"].ToString();
            //        obj1.CourseLink = r["CourseLink"].ToString();
            //        lst.Add(obj1);
            //    }
            //    model.lstCourse = lst;
            //}
            return View(model);
        }

        [HttpPost]
        [ActionName("AllotCoursesOnPackage")]
        [OnAction(ButtonName = "btnsearch")]
        public ActionResult AllotCoursesOnPackage(Master model,string Package)
        {
            #region ddlPackage
            Master obj = new Master();
            int count = 0;
            List<SelectListItem> ddlPackage = new List<SelectListItem>();
            DataSet dsPackage = obj.GetPackageList();
            if (dsPackage != null && dsPackage.Tables.Count > 0 && dsPackage.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPackage.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlPackage.Add(new SelectListItem { Text = "Select Package", Value = "0" });
                    }
                    ddlPackage.Add(new SelectListItem { Text = r["ProductName"].ToString(), Value = r["PK_ProductID"].ToString() });
                    count = count + 1;
                }
            }
            ViewBag.ddlPackage = ddlPackage;
            #endregion

            List<Master> lst1 = new List<Master>();
            List<Master> lst = new List<Master>();
            model.Package = Package;
            DataSet ds1 = model.GetCourseListForAllotCourses();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    Master obj11 = new Master();
                    obj11.CourseID = r["PK_CourseID"].ToString();
                    obj11.CourseName = r["CourseName"].ToString();
                    obj11.CourseImage = r["CourseImage"].ToString();
                    obj11.CourseDate = r["CourseDate"].ToString();
                    obj11.CourseLink = r["CourseLink"].ToString();
                    obj11.status = r["status"].ToString();
                    lst1.Add(obj11);
                }
                model.lstCourses = lst1;
            }
            return View(model);
        }



        [HttpPost]
        [ActionName("AllotCoursesOnPackage")]
        [OnAction(ButtonName = "btnsave")]
        public ActionResult AllotCoursesOnPackageAction(Master model)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                string ctrRowCount = Request["hdRows"].ToString();
                string chk = "";
                string CourseID = "";
                string CourseName = "";
                //string CourseImage = "";
                int Id = 0;
                string Status = "";
                DataTable dtpayment = new DataTable();
                dtpayment.Columns.Add("Id");
                dtpayment.Columns.Add("CourseID");
                dtpayment.Columns.Add("CourseName");
                dtpayment.Columns.Add("Status");

                //dtpayment.Columns.Add("CourseImage");
                for (int i = 1; i < int.Parse(ctrRowCount); i++)
                {
                    chk = Request["chkpayment_" + i];

                    if (chk == "on")
                    {
                        Id = dtpayment.Rows.Count + 1;
                        CourseID = Request["CourseID_" + i].ToString();
                        CourseName = Request["CourseName_" + i].ToString();
                        Status = "1";

                        //CourseImage = Request["CourseImage_" + i].ToString();
                        //dtpayment.Rows.Add(Id, CourseID, CourseName, CourseImage);
                        dtpayment.Rows.Add(Id, CourseID, CourseName, Status);
                    }
                    else
                    {
                        Id = dtpayment.Rows.Count + 1;
                        CourseID = Request["CourseID_" + i].ToString();
                        CourseName = Request["CourseName_" + i].ToString();
                        Status = "0";
                        dtpayment.Rows.Add(Id, CourseID, CourseName, Status);
                        
                    }
                }
                model.dtTable = dtpayment;
                if (dtpayment.Rows.Count == 0 )
                {
                    TempData["ErrCourse"] = "In This Package This Course Already Exist Please Select Another Course !!";
                    FormName = "AllotCoursesOnPackage";
                    Controller = "Master";
                    return RedirectToAction(FormName, Controller);
                }
                else
                {
                model.AddedBy = Session["PK_AdminId"].ToString();
                DataSet ds = model.SaveAllotCoursesOnPackage();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Course"] = "Courses On Package Alloted Successfully !!";
                    }
                    else
                    {
                        TempData["ErrCourse"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrCourse"] = ex.Message;
            }
            FormName = "AllotCoursesOnPackage";
            Controller = "Master";
            return RedirectToAction(FormName, Controller);
        }

        #endregion

        public ActionResult CourseView(Master model)
        {
            List<Master> lst = new List<Master>();
            DataSet ds = model.GetCourseListForAllotCourses();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.CourseID = r["Pk_CourseId"].ToString();
                    obj.CourseName = r["CourseName"].ToString();
                    obj.CourseImage = r["CourseImage"].ToString();
                    obj.CourseDate = r["CourseDate"].ToString();
                    obj.CourseLink = r["CourseLink"].ToString();
                    lst.Add(obj);
                }
                model.lstCourse = lst;
            }
            return View(model);
        }

        #region Status
        public ActionResult ActiveProduct(string ProductID)
        {
            string FormName = " ";
            string Controller = "";
            try
            {
                if (ProductID != null)
                {
                    Master model = new Master();
                    model.ProductID = ProductID;
                    model.AddedBy = Session["PK_AdminId"].ToString();
                    DataSet ds = model.ActiveProduct();
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0][0].ToString() == "1")
                        {
                            TempData["Product"] = "Product Status Active!";
                            FormName = "ProductList";
                            Controller = "Master";
                        }
                        else
                        {
                            TempData["Product"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                            FormName = "ProductList";
                            Controller = "Master";
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return RedirectToAction(FormName, Controller);
        }

        public ActionResult InactiveProduct(string ProductID)
        {
            string FormName = " ";
            string Controller = "";
            try
            {
                if (ProductID != null)
                {
                    Master model = new Master();
                    model.ProductID = ProductID;
                    model.AddedBy = Session["PK_AdminId"].ToString();
                    DataSet ds = model.InactiveProduct();
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0][0].ToString() == "1")
                        {
                            TempData["ErrProduct"] = "Product Status Inactive!";
                            FormName = "ProductList";
                            Controller = "Master";
                        }
                        else
                        {
                            TempData["ErrProduct"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                            FormName = "ProductList";
                            Controller = "Master";
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return RedirectToAction(FormName, Controller);
        }

        #endregion

        #region ProductandServices

        public ActionResult ProductandServices(string Pk_ProductServiceID)
        {
            Master obj = new Master();
            if (Pk_ProductServiceID != null)
            {
                try
                {
                    obj.Pk_ProductServiceID = Pk_ProductServiceID;

                    DataSet ds = obj.ProductandServicesList();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        obj.Pk_ProductServiceID = ds.Tables[0].Rows[0]["Pk_ProductServiceID"].ToString();
                        obj.ProductName = ds.Tables[0].Rows[0]["ProductName"].ToString();
                        obj.ProductPrice = ds.Tables[0].Rows[0]["Amount"].ToString();
                        obj.Image = ds.Tables[0].Rows[0]["Image"].ToString();
                        obj.Description = ds.Tables[0].Rows[0]["Description"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    TempData["ProductandServices"] = ex.Message;
                }
            }
              return View(obj);
        }


        [HttpPost]
        [OnAction(ButtonName = "btnProductServices")]
        [ActionName("ProductandServices")]
        public ActionResult SaveProductandServices(Master obj, IEnumerable<HttpPostedFileBase> postedFile)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                obj.AddedBy = Session["PK_AdminId"].ToString();
                foreach (var file in postedFile)
                {
                    if (file != null && file.ContentLength > 0)
                    {

                        obj.Image = "/images/ProductandServices/" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                        file.SaveAs(Path.Combine(Server.MapPath(obj.Image)));
                    }
                }
                DataSet ds = obj.SaveProductandServices();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if ((ds.Tables[0].Rows[0][0].ToString() == "1"))
                    {
                        TempData["ProductandServices"] = "Product Saved Successfully";
                        FormName = "ProductandServices";
                        Controller = "Master";
                    }
                    else
                    {
                        TempData["ErrProductandServices"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "ProductandServices";
                        Controller = "Master";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrProductandServices"] = ex.Message;
                FormName = "ProductandServices";
                Controller = "Master";
            }
            return RedirectToAction(FormName, Controller);
        }


        public ActionResult ProductandServicesList(Master model)
        {
            List<Master> lst = new List<Master>();
            DataSet ds = model.ProductandServicesList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.Pk_ProductServiceID = r["Pk_ProductServiceID"].ToString();
                    obj.ProductName = r["ProductName"].ToString();
                    obj.ProductPrice = r["Amount"].ToString();
                    obj.Image = r["Image"].ToString();
                    obj.Description = r["Description"].ToString();
                    obj.UploadDate = r["UploadDate"].ToString();
                    lst.Add(obj);
                }
                model.lstproductandServices = lst;
            }
            return View(model);
        }

        
        public ActionResult DeleteProductandServices(string Pk_ProductServiceID)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Master obj = new Master();
                obj.DeletedBy = Session["PK_AdminId"].ToString();
                obj.Pk_ProductServiceID = Pk_ProductServiceID;
                DataSet ds = obj.DeleteProductandServices();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if ((ds.Tables[0].Rows[0][0].ToString() == "1"))
                    {
                        TempData["ProductandServices"] = "Product Deleted Successfully";
                        FormName = "ProductandServicesList";
                        Controller = "Master";
                    }
                    else
                    {
                        TempData["ErrProductandServices"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "ProductandServicesList";
                        Controller = "Master";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrProductandServices"] = ex.Message;
                FormName = "ProductandServicesList";
                Controller = "Master";
            }
            return RedirectToAction(FormName, Controller);
        }

        [HttpPost]
        [OnAction(ButtonName = "btnUpdate")]
        [ActionName("ProductandServices")]
        public ActionResult UpdateProductandServices(Master obj,string Pk_ProductServiceID, IEnumerable<HttpPostedFileBase> postedFile)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                //Master obj = new Master();
                obj.UpdatedBy = Session["PK_AdminId"].ToString();
                obj.Pk_ProductServiceID = Pk_ProductServiceID;

                foreach (var file in postedFile)
                {
                    if (file != null && file.ContentLength > 0)
                    {

                        obj.Image = "/images/ProductandServices/" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                        file.SaveAs(Path.Combine(Server.MapPath(obj.Image)));
                    }
                }
                DataSet ds = obj.UpdateProductandServices();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if ((ds.Tables[0].Rows[0][0].ToString() == "1"))
                    {
                        TempData["ProductandServices"] = "Product Updated Successfully";
                        FormName = "ProductandServices";
                        Controller = "Master";
                    }
                    else
                    {
                        TempData["ErrProductandServices"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "ProductandServices";
                        Controller = "Master";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrProductandServices"] = ex.Message;
                FormName = "ProductandServices";
                Controller = "Master";
            }
            return RedirectToAction(FormName, Controller);
        }
     
        #endregion

    }
}
