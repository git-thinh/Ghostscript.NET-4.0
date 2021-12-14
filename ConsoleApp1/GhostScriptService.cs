using Ghostscript.NET;
using Ghostscript.NET.Processor;
using Ghostscript.NET.Rasterizer;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceConsole
{
    public class GhostScriptService
    {
        const string GS_VERSION_DLL = @"C:\Program Files\gs\gs9.55.0\bin\gsdll64.dll";
        static GhostscriptVersionInfo _gs_verssion_info = GhostscriptVersionInfo.GetLastInstalledVersion();

        #region [ PDF_TO_PNG ]

        public static string[] __PDF_TO_PNG(string fileInput, string fileOutput)
        {
            List<string> cf = new List<string>();
            cf.Add("-empty");
            cf.Add("-dSAFER");
            cf.Add("-dBATCH");
            cf.Add("-dNOPAUSE");
            cf.Add("-dNOPROMPT");
            cf.Add("-dFirstPage=1");
            cf.Add("-dLastPage=1");
            cf.Add("-sDEVICE=png16m");
            cf.Add("-r96");
            cf.Add("-dTextAlphaBits=4");
            cf.Add("-dGraphicsAlphaBits=4");
            cf.Add(@"-sOutputFile=" + fileOutput);
            cf.Add(@"-f");
            cf.Add(fileInput);
            return cf.ToArray();
        }
        public static bool __PDF_TO_PNG(string id, byte[] buf, IDatabase _redisWrite, IDatabase _redisRead, int desired_dpi = 96)
        {
            bool ok = false;
            //using (GhostscriptProcessor ghostscript = new GhostscriptProcessor())
            //{
            //    ghostscript.Processing += new GhostscriptProcessorProcessingEventHandler(ghostscript_Processing);
            //    ghostscript.Process(__PDF_TO_PNG(fileInput, fileOutput));
            //}

            //GhostscriptVersionInfo gvi = new GhostscriptVersionInfo(GS_VERSION_DLL);
            //using (var rasterizer = new GhostscriptRasterizer())
            //{
            //    rasterizer.Open(fileInput, gvi, false);

            //    //for (var pageNumber = 1; pageNumber <= rasterizer.PageCount; pageNumber++)/
            //    var img = rasterizer.GetPage(desired_dpi, 1);
            //    var ms = new MemoryStream();
            //    img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            //    return ms.ToArray();
            //}
            return ok;
        }

        public static byte[] __PDF_TO_PNG(byte[] bufInput, int desired_dpi = 96)
        {
            GhostscriptVersionInfo gvi = new GhostscriptVersionInfo(GS_VERSION_DLL);
            using (var rasterizer = new GhostscriptRasterizer())
            {
                rasterizer.Open(new MemoryStream(bufInput), gvi, false);

                //for (var pageNumber = 1; pageNumber <= rasterizer.PageCount; pageNumber++)/
                var img = rasterizer.GetPage(desired_dpi, 1);
                var ms = new MemoryStream();
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
            return null;
        }

        public static void __PDF_TO_PNG2(string fileInput, string fileOutput)
        {
            GhostscriptPngDevice dev = new GhostscriptPngDevice(GhostscriptPngDeviceType.Png16m);
            dev.GraphicsAlphaBits = GhostscriptImageDeviceAlphaBits.V_4;
            dev.TextAlphaBits = GhostscriptImageDeviceAlphaBits.V_4;
            dev.ResolutionXY = new GhostscriptImageDeviceResolution(96, 96);
            dev.InputFiles.Add(fileInput);
            dev.Pdf.FirstPage = 1;
            dev.Pdf.LastPage = 1;
            //dev.PostScript = POSTSCRIPT_APPEND_WATERMARK;
            dev.OutputPath = fileOutput;
            dev.Process();
        }


        void Export_Second_And_Third_Pdf_Page_As_24bit_Png()
        {
            GhostscriptPngDevice dev = new GhostscriptPngDevice(GhostscriptPngDeviceType.Png16m);
            dev.GraphicsAlphaBits = GhostscriptImageDeviceAlphaBits.V_4;
            dev.TextAlphaBits = GhostscriptImageDeviceAlphaBits.V_4;
            dev.ResolutionXY = new GhostscriptImageDeviceResolution(96, 96);
            dev.InputFiles.Add(@"E:\gss_test\indispensable.pdf");
            dev.Pdf.FirstPage = 2;
            dev.Pdf.LastPage = 4;
            dev.CustomSwitches.Add("-dDOINTERPOLATE");
            dev.OutputPath = @"E:\gss_test\output\indispensable_color_page_%03d.png";
            dev.Process();
        }

        void Export_Second_And_Third_Pdf_Page_As_Grayscale_Png()
        {
            GhostscriptPngDevice dev = new GhostscriptPngDevice(GhostscriptPngDeviceType.PngGray);
            dev.GraphicsAlphaBits = GhostscriptImageDeviceAlphaBits.V_4;
            dev.TextAlphaBits = GhostscriptImageDeviceAlphaBits.V_4;
            dev.ResolutionXY = new GhostscriptImageDeviceResolution(96, 96);
            dev.InputFiles.Add(@"E:\gss_test\indispensable.pdf");
            dev.Pdf.FirstPage = 2;
            dev.Pdf.LastPage = 4;
            dev.OutputPath = @"E:\gss_test\output\indispensable_gray_page_%03d.png";
            dev.Process();
        }

        void Export_First_And_Second_Pdf_Page_As_Color_Jpeg()
        {
            GhostscriptJpegDevice dev = new GhostscriptJpegDevice(GhostscriptJpegDeviceType.Jpeg);
            dev.GraphicsAlphaBits = GhostscriptImageDeviceAlphaBits.V_4;
            dev.TextAlphaBits = GhostscriptImageDeviceAlphaBits.V_4;
            dev.ResolutionXY = new GhostscriptImageDeviceResolution(96, 96);
            dev.JpegQuality = 80;
            dev.InputFiles.Add(@"E:\gss_test\indispensable.pdf");
            dev.Pdf.FirstPage = 2;
            dev.Pdf.LastPage = 4;
            dev.OutputPath = @"E:\gss_test\output\indispensable_color_page_%03d.jpeg";
            dev.Process();
        }

        void Export_First_And_Second_Pdf_Page_As_Grayscale_Jpeg()
        {
            GhostscriptJpegDevice dev = new GhostscriptJpegDevice(GhostscriptJpegDeviceType.JpegGray);
            dev.GraphicsAlphaBits = GhostscriptImageDeviceAlphaBits.V_4;
            dev.TextAlphaBits = GhostscriptImageDeviceAlphaBits.V_4;
            dev.ResolutionXY = new GhostscriptImageDeviceResolution(96, 96);
            dev.JpegQuality = 80;
            dev.InputFiles.Add(@"E:\gss_test\indispensable.pdf");
            dev.Pdf.FirstPage = 2;
            dev.Pdf.LastPage = 4;
            dev.OutputPath = @"E:\gss_test\output\indispensable_gray_page_%03d.jpeg";
            dev.Process();
        }

        void Rasterizer_Crop()
        {
            int desired_dpi = 300;

            string inputPdfPath = @"E:\__test_data\test2.pdf";
            string outputPath = @"E:\__test_data\output\";

            using (GhostscriptRasterizer rasterizer = new GhostscriptRasterizer())
            {
                rasterizer.CustomSwitches.Add("-dUseCropBox");
                rasterizer.CustomSwitches.Add("-c");
                rasterizer.CustomSwitches.Add("[/CropBox [24 72 559 794] /PAGES pdfmark");
                rasterizer.CustomSwitches.Add("-f");

                rasterizer.Open(inputPdfPath);

                for (int pageNumber = 1; pageNumber <= rasterizer.PageCount; pageNumber++)
                {
                    string pageFilePath = Path.Combine(outputPath, "Page-" + pageNumber.ToString() + ".png");

                    Image img = rasterizer.GetPage(desired_dpi, pageNumber);
                    img.Save(pageFilePath, System.Drawing.Imaging.ImageFormat.Png);

                    Console.WriteLine(pageFilePath);
                }
            }
        }

        #endregion

        #region [ VECTOR_TO_PNG ]

        public static bool __VECTOR_TO_PNG_v1(string id, byte[] bufInput, IDatabase _redisWrite, IDatabase _redisRead, int desired_dpi = 96)
        {
            bool ok = false;
            //using (GhostscriptProcessor ghostscript = new GhostscriptProcessor())
            //{
            //    ghostscript.Processing += new GhostscriptProcessorProcessingEventHandler(ghostscript_Processing);
            //    ghostscript.Process(__VECTOR_TO_PNG(fileInput, fileOutput));
            //}

            GhostscriptVersionInfo gvi = new GhostscriptVersionInfo(GS_VERSION_DLL);
            using (var r = new GhostscriptRasterizer())
            {
                r.Open(new MemoryStream(bufInput), gvi, false);
                r.GraphicsAlphaBits = 0;

                //for (var pageNumber = 1; pageNumber <= rasterizer.PageCount; pageNumber++)/
                var img = r.GetPage(desired_dpi, 1);
                var ms = new MemoryStream();
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                var rs = ms.ToArray();
                _redisWrite.StringSet("img:png:" + id, rs);
                ok = true;
            }
            return ok;
        }

        static string[] __vector_to_png(string fileInput, string fileOutput)
        {
            List<string> cf = new List<string>();
            cf.Add("-q");
            //cf.Add("-empty");
            //cf.Add("-dSAFER");
            cf.Add("-dNOPAUSE");
            cf.Add("-dBATCH");
            //cf.Add("-dNOPROMPT");

            //cf.Add(@"-sFONTPATH=" + System.Environment.GetFolderPath(System.Environment.SpecialFolder.Fonts));

            //cf.Add("-dFirstPage=1");
            //cf.Add("-dLastPage=1");

            //cf.Add("-sDEVICE=png16m");
            cf.Add("-sDEVICE=pngalpha");
            //cf.Add("-dBackgroundColor=16#CCCC00");

            cf.Add("-r70");
            //cf.Add("-r96");

            //cf.Add("-sPAPERSIZE=a4");
            //cf.Add("-dNumRenderingThreads=" + Environment.ProcessorCount.ToString());

            //cf.Add("-dAlignToPixels=0");
            //cf.Add("-dTextAlphaBits=4");
            //cf.Add("-dGraphicsAlphaBits=4");

            cf.Add(@"-sOutputFile=" + fileOutput);
            cf.Add(@"-f" + fileInput);
            return cf.ToArray();
        }

        public static bool __VECTOR_TO_PNG_v2(string id, byte[] bufInput, IDatabase _redisWrite, IDatabase _redisRead, int desired_dpi = 96)
        {
            string fileInput = Helper.getFileInput_byID(id);
            File.WriteAllBytes(fileInput, bufInput);
            string fileOutput = Helper.getFileOutput_byID(id, "png");
            if (File.Exists(fileOutput)) File.Delete(fileOutput);

            bool ok = false;
            using (GhostscriptProcessor g = new GhostscriptProcessor())
            {
                g.Processing += new GhostscriptProcessorProcessingEventHandler(ghostscript_Processing);
                string[] a = __vector_to_png(fileInput, fileOutput);
                g.Process(a);
            }
            return ok;
        }

        public static bool __VECTOR_TO_INFO_SIZE(string id, byte[] bufInput, IDatabase _redisWrite, IDatabase _redisRead, int desired_dpi = 96)
        {
            bool ok = false;
            GhostscriptVersionInfo gvi = new GhostscriptVersionInfo(GS_VERSION_DLL);
            using (var rasterizer = new GhostscriptRasterizer())
            {
                rasterizer.Open(new MemoryStream(bufInput), gvi, false);
                //rasterizer.GraphicsAlphaBits = 0;

                //for (var pageNumber = 1; pageNumber <= rasterizer.PageCount; pageNumber++)/
                var img = rasterizer.GetPage(desired_dpi, 1);
                string json = JsonConvert.SerializeObject(new { Width = img.Width, Height = img.Height });
                _redisWrite.StringSet("img:size:" + id, json);
                ok = true;
            }
            return ok;
        }


        #endregion

        #region [ VECTOR_TO_PDF ]

        public static string[] __VECTOR_TO_PDF(string fileInput, string fileOutput)
        {
            List<string> cf = new List<string>();
            cf.Add("-dBATCH");
            cf.Add("-dNOPAUSE");
            cf.Add("-dNOPAUSE");

            //cf.Add("-dDOINTERPOLATE");

            cf.Add("-sDEVICE=pdfwrite");
            cf.Add("-sOutputFile=" + fileOutput);
            //switches.Add("-c");
            //switches.Add(POSTSCRIPT_APPEND_WATERMARK);
            cf.Add("-f");
            cf.Add(fileInput);
            return cf.ToArray();
        }

        public static bool __VECTOR_TO_PDF(string id, byte[] bufInput, IDatabase _redisWrite, IDatabase _redisRead, int dpi = 96)
        {
            bool ok = false;
            string fileInput = Helper.getFileInput_byID(id);
            File.WriteAllBytes(fileInput, bufInput);


            //using (GhostscriptProcessor ghostscript = new GhostscriptProcessor())
            //{
            //    ghostscript.Processing += new GhostscriptProcessorProcessingEventHandler(ghostscript_Processing);
            //    ghostscript.Process(__VECTOR_TO_PDF(fileInput, fileOutput));
            //}

            try
            {
                int width = 0, height = 0;
                GhostscriptVersionInfo gvi = new GhostscriptVersionInfo(GS_VERSION_DLL);
                using (var rasterizer = new GhostscriptRasterizer())
                {
                    rasterizer.Open(new MemoryStream(bufInput), gvi, false);
                    //for (var pageNumber = 1; pageNumber <= rasterizer.PageCount; pageNumber++)/
                    var img = rasterizer.GetPage(dpi, 1);
                    width = img.Width;
                    height = img.Height;
                    //img.Save(@"C:\1.png", System.Drawing.Imaging.ImageFormat.Png);
                }

                int pw = width * 72 / dpi;
                int ph = height * 72 / dpi;

                GhostscriptPipedOutput gsPipedOutput = new GhostscriptPipedOutput();

                // pipe handle format: %handle%hexvalue
                string outputPipeHandle = "%handle%" + int.Parse(gsPipedOutput.ClientHandle).ToString("X2");

                using (GhostscriptProcessor processor = new GhostscriptProcessor())
                {
                    List<string> cf = new List<string>();
                    cf.Add("-dBATCH");
                    cf.Add("-dNOPAUSE");

                    cf.Add("-sDEVICE=pdfwrite");

                    cf.Add("-dDOINTERPOLATE");

                    cf.Add("-dFIXEDMEDIA");
                    cf.Add("-dPDFFitPage");

                    cf.Add("-dDEVICEWIDTHPOINTS=" + pw.ToString());
                    cf.Add("-dDEVICEHEIGHTPOINTS=" + ph.ToString());

                    cf.Add("-dALLOWPSTRANSPARENCY");
                    cf.Add("-dEPSCrop");

                    //cf.Add("-sOutputFile=" + fileOutput);
                    cf.Add("-o" + outputPipeHandle);
                    cf.Add("-q");

                    //cf.Add("-dUseCropBox");
                    //cf.Add("-c");
                    //cf.Add("[/CropBox [64 33 198 147] /PAGES pdfmark");

                    //cf.Add("-dUseArtBox");
                    //cf.Add("-c");
                    //cf.Add("[/ArtBox [64 33 98 47] /PAGES pdfmark");

                    cf.Add("-f");
                    cf.Add(fileInput);






                    //////cf.Add("-empty");
                    //////cf.Add("-dQUIET");
                    //////cf.Add("-dSAFER");
                    //////cf.Add("-dBATCH");
                    //////cf.Add("-dNOPAUSE");
                    //////cf.Add("-dNOPROMPT");
                    //////cf.Add("-sDEVICE=pdfwrite");


                    //////cf.Add("-dCompatibilityLevel=1.4");
                    //////cf.Add("-dDOINTERPOLATE");

                    //////cf.Add("-dFIXEDMEDIA");
                    //////cf.Add("-dPDFFitPage");

                    //////cf.Add("-dDEVICEWIDTHPOINTS=" + pw.ToString());
                    //////cf.Add("-dDEVICEHEIGHTPOINTS=" + ph.ToString());

                    ////////cf.Add("-dPDFSETTINGS=/printer"); // /screen, /default, /ebook, /printer, /prepress

                    ////////cf.Add("-r72");
                    //////cf.Add("-r96");

                    ////////cf.Add("-c");
                    ////////cf.Add(POSTSCRIPT_APPEND_WATERMARK);

                    //////cf.Add("-o" + outputPipeHandle);
                    //////cf.Add("-q");
                    //////cf.Add("-f");
                    //////cf.Add(fileInput);

                    try
                    {
                        processor.StartProcessing(cf.ToArray(), null);
                        byte[] rs = gsPipedOutput.Data;

                        _redisWrite.StringSet("vec2pdf:" + id, rs);
                        ok = true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    finally
                    {
                        gsPipedOutput.Dispose();
                        gsPipedOutput = null;
                    }
                }
            }
            catch { }

            return ok;
        }

        #endregion

        static void ghostscript_Processing(object sender, GhostscriptProcessorProcessingEventArgs e)
            => Console.WriteLine(e.CurrentPage.ToString() + " / " + e.TotalPages.ToString());

        static string height = "100", width = "100", left = "90", top = "30";
        static string POSTSCRIPT_APPEND_WATERMARK_2 = @"

%!
<<
    /PageSize [595 342]
    /EndPage {
        exch pop 2 lt {
            currentpagedevice /PageSize get  %% stack has array [width height]

newpath
" + left + @" " + top + @" moveto
0 " + height + @" rlineto
" + width + @" 0 rlineto
0 -" + height + @" rlineto
-" + width + @" 0 rlineto
closepath
gsave
0.5 1 0.5 setrgbcolor
%%fill
grestore
1 0 0 setrgbcolor
4 setlinewidth
stroke

            true
        } { false } ifelse
    }bind
>>setpagedevice";

    }
}
