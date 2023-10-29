using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FileProcessing.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(HttpPostedFileBase file)
        {
            return View();
        }

        public ActionResult CopyEvenLinesWithCycle(HttpPostedFileBase sourceFile, HttpPostedFileBase targetFile)
        {
            if (sourceFile != null && sourceFile.ContentLength > 0 && targetFile != null && targetFile.ContentLength > 0)
            {
                string sourceFilePath = Server.MapPath("~/App_Data/source.txt");
                string targetFilePath = Server.MapPath("~/App_Data/target.txt");

                using (StreamReader sourceReader = new StreamReader(sourceFile.InputStream))
                using (StreamWriter targetWriter = new StreamWriter(targetFilePath, false, System.Text.Encoding.Default))
                {
                    string line;
                    int lineNumber = 1;

                    while ((line = sourceReader.ReadLine()) != null)
                    {
                        if (lineNumber % 2 == 0)
                        {
                            targetWriter.WriteLine(line);
                        }
                        else
                        {
                            // Если строка нечетная, она не будет записана во второй файл.
                        }

                        lineNumber++;
                    }
                }

                byte[] targetFileBytes = System.IO.File.ReadAllBytes(targetFilePath);
                return File(targetFileBytes, "application/octet-stream", targetFile.FileName);
            }
            else
            {
                return View();
            }
        }



        public ActionResult CopyEvenLinesWithLINQ(HttpPostedFileBase sourceFile, HttpPostedFileBase targetFile)
        {
            if (sourceFile != null && sourceFile.ContentLength > 0 && targetFile != null && targetFile.ContentLength > 0)
            {
                string sourceFilePath = Server.MapPath("~/App_Data/source.txt");
                string targetFilePath = Server.MapPath("~/App_Data/target.txt");

                using (StreamReader sourceReader = new StreamReader(sourceFile.InputStream))
                using (StreamWriter targetWriter = new StreamWriter(targetFilePath, false, System.Text.Encoding.Default))
                {
                    var evenLines = sourceReader
                        .ReadToEnd()
                        .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                        .Where((line, index) => (index + 1) % 2 == 0);

                    foreach (var line in evenLines)
                    {
                        targetWriter.WriteLine(line);
                    }
                }

                byte[] targetFileBytes = System.IO.File.ReadAllBytes(targetFilePath);
                return File(targetFileBytes, "application/octet-stream", targetFile.FileName);
            }
            else
            {
                return View();
            }
        }
    }
}