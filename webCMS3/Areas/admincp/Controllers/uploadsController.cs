using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace webCMS3.Areas.admincp.Controllers
{
    public class uploadsController : Controller
    {
        // GET: admincp/uploads
        public JsonResult SaveImage(HttpPostedFileBase fileData)
        {
            fileData = Request.Files[0];

            try
            {
                string strName = Guid.NewGuid().ToString() + Path.GetExtension(fileData.FileName);
                string strPhysical = HttpContext.Request.PhysicalApplicationPath + "uploads\\";
                string strUrl = Path.Combine(strPhysical, strName);
                fileData.SaveAs(strUrl);
                return Json(new { status = true, fileName = strName });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, fileName = ex.Message });
            }
        }

        public JsonResult SaveThumb(string strName, int intCropX, int intCropY, int intWidth, int intHeight)
        {
            try
            {
                string strPhysical = HttpContext.Request.PhysicalApplicationPath + "uploads\\";
                string strPrefix = "1x1_" + strName;
                using (Image image = Image.FromFile(Path.Combine(strPhysical, strName)))
                {
                    using (Bitmap bmp = new Bitmap(intWidth, intHeight))
                    {
                        bmp.SetResolution(image.HorizontalResolution, image.VerticalResolution);
                        using (Graphics Graphic = Graphics.FromImage(bmp))
                        {
                            Graphic.SmoothingMode = SmoothingMode.AntiAlias;
                            Graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            Graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            Graphic.DrawImage(image, new Rectangle(0, 0, intWidth, intHeight), intCropX, intCropY, intWidth, intHeight, GraphicsUnit.Pixel);
                            MemoryStream ms = new MemoryStream();
                            bmp.Save(ms, image.RawFormat);
                            Image imgResult = Image.FromStream(new MemoryStream(ms.ToArray()));
                            imgResult.Save(Path.Combine(strPhysical, strPrefix));
                        }
                    }
                }
                return Json(new { status = true, fileName = strPrefix });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, fileName = ex.Message });
            }
        }

        public JsonResult InsertNews(HttpPostedFileBase fileData)
        {
            fileData = Request.Files[0];
            Image img = Image.FromStream(fileData.InputStream);
            int intWith = 0;
            int intHeight = 0;
            try
            {
                string strExt = Path.GetExtension(fileData.FileName).ToLower();
                string strName = Guid.NewGuid().ToString() + strExt;
                if (strExt != "gif")
                {
                    intWith = img.Width;
                    intHeight = img.Height;
                }
                string strUrl = Path.Combine(HttpContext.Request.PhysicalApplicationPath + "uploads\\", strName);
                fileData.SaveAs(strUrl);
                return Json(new { status = true, fileName = strName, width = intWith, height = intHeight });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, fileName = ex.Message });
            }
        }
    }
}