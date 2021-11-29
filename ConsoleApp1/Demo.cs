using Ghostscript.NET;
using Ghostscript.NET.Processor;
using Ghostscript.NET.Rasterizer;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Demo
    {
        const string GS_VERSION_DLL = @"C:\Program Files\gs\gs9.55.0\bin\gsdll64.dll";
        private GhostscriptVersionInfo _gs_verssion_info = GhostscriptVersionInfo.GetLastInstalledVersion();


        const int MAX_CONNECTION = 10;
        const int PORT_NUMBER = 54321;
        static int _connectionsCount = 0;
        static TcpListener listener;

        static string[] __services = new string[] { "VECTOR_TO_PNG", "VECTOR_TO_INFO_SIZE", "VECTOR_TO_PDF", "VECTOR_TO_PDF_SELECTION", "PDF_TO_PNG" };

        static IDatabase _dbWrite;
        static IDatabase _dbRead;
        public static void Main()
        {
            ConnectionMultiplexer r1 = ConnectionMultiplexer.Connect("localhost:1000");
            _dbWrite = r1.GetDatabase(1);
            ConnectionMultiplexer r2 = ConnectionMultiplexer.Connect("localhost:1001");
            _dbRead = r2.GetDatabase(1);

            IPAddress address = IPAddress.Parse("127.0.0.1");

            listener = new TcpListener(address, PORT_NUMBER);
            Console.WriteLine("Waiting for connection...");
            listener.Start();

            while (_connectionsCount < MAX_CONNECTION || MAX_CONNECTION == 0)
            {
                Socket soc = listener.AcceptSocket();
                _connectionsCount++;
                Thread t = new Thread((obj) => { DoWork((Socket)obj); });
                t.Start(soc);
            }
        }


        static void DoWork(Socket soc)
        {
            string pathInput = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\inputs\\";
            string pathOutput = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\outputs\\";

            if (!Directory.Exists(pathInput)) Directory.CreateDirectory(pathInput);
            if (!Directory.Exists(pathOutput)) Directory.CreateDirectory(pathOutput);

            //Console.WriteLine("Connection received from: {0}", soc.RemoteEndPoint);
            try
            {
                var stream = new NetworkStream(soc);

                byte[] buf = new byte[36];
                int sz = stream.Read(buf, 0, 36);

                string id = System.Text.ASCIIEncoding.ASCII.GetString(buf);
                Console.WriteLine(id);
                buf = _dbRead.StringGet(id);
                if (buf != null)
                {
                    string[] a = id.Split('-');
                    if (a.Length > 2)
                    {
                        int serviceIndex = -1;
                        int.TryParse(a[0], out serviceIndex);
                        string fileType = a[1];
                        if (serviceIndex >= 0 && serviceIndex < __services.Length)
                        {
                            string fileInput = pathInput + id + "." + fileType;
                            if (!File.Exists(fileInput)) File.WriteAllBytes(fileInput, buf);
                            string fileOutput;

                            string service = __services[serviceIndex];
                            switch (service)
                            {
                                case "VECTOR_TO_PNG":
                                    {
                                        fileOutput = pathOutput + id + ".png";
                                        //using (GhostscriptProcessor ghostscript = new GhostscriptProcessor())
                                        //{
                                        //    ghostscript.Processing += new GhostscriptProcessorProcessingEventHandler(ghostscript_Processing);
                                        //    ghostscript.Process(__VECTOR_TO_PNG(fileInput, fileOutput));
                                        //}
                                        var rs = __VECTOR_TO_PNG(buf);
                                        if (rs != null) File.WriteAllBytes(fileOutput, rs);
                                    }
                                    break;
                                case "VECTOR_TO_INFO_SIZE":
                                    break;
                                case "VECTOR_TO_PDF":
                                    {
                                        fileOutput = pathOutput + id + ".pdf";
                                        //using (GhostscriptProcessor ghostscript = new GhostscriptProcessor())
                                        //{
                                        //    ghostscript.Processing += new GhostscriptProcessorProcessingEventHandler(ghostscript_Processing);
                                        //    ghostscript.Process(__VECTOR_TO_PDF(fileInput, fileOutput));
                                        //}
                                        var rs = __VECTOR_TO_PDF(fileInput, buf);
                                        if (rs != null) File.WriteAllBytes(fileOutput, rs);
                                    }
                                    break;
                                case "VECTOR_TO_PDF_SELECTION":
                                    break;
                                case "PDF_TO_PNG":
                                    {
                                        fileOutput = pathOutput + id + ".png";
                                        //using (GhostscriptProcessor ghostscript = new GhostscriptProcessor())
                                        //{
                                        //    ghostscript.Processing += new GhostscriptProcessorProcessingEventHandler(ghostscript_Processing);
                                        //    ghostscript.Process(__PDF_TO_PNG(fileInput, fileOutput));
                                        //}
                                        var rs = __PDF_TO_PNG(fileInput, 96);
                                        if (rs != null) File.WriteAllBytes(fileOutput, rs);
                                    }
                                    break;
                            }
                        }
                    }
                }

                stream.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
            }

            //Console.WriteLine("Client disconnected: {0}", soc.RemoteEndPoint);
            soc.Close();
        }

        #region [ PDF_TO_PNG ]

        static string[] __PDF_TO_PNG(string fileInput, string fileOutput)
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
        static byte[] __PDF_TO_PNG(string fileInput, int desired_dpi = 96)
        {
            GhostscriptVersionInfo gvi = new GhostscriptVersionInfo(GS_VERSION_DLL);
            using (var rasterizer = new GhostscriptRasterizer())
            {
                rasterizer.Open(fileInput, gvi, false);

                //for (var pageNumber = 1; pageNumber <= rasterizer.PageCount; pageNumber++)/
                var img = rasterizer.GetPage(desired_dpi, 1);
                var ms = new MemoryStream();
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
            return null;
        }
        static byte[] __PDF_TO_PNG(byte[] bufInput, int desired_dpi = 96)
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

        static void __PDF_TO_PNG2(string fileInput, string fileOutput)
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

        static byte[] __VECTOR_TO_PNG(byte[] bufInput, int desired_dpi = 96)
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

        static string[] __VECTOR_TO_PNG(string fileInput, string fileOutput)
        {
            List<string> cf = new List<string>();
            //cf.Add("-q");
            cf.Add("-empty");
            cf.Add("-dSAFER");
            cf.Add("-dBATCH");
            cf.Add("-dNOPAUSE");
            cf.Add("-dNOPROMPT");
            cf.Add(@"-sFONTPATH=" + System.Environment.GetFolderPath(System.Environment.SpecialFolder.Fonts));
            cf.Add("-dFirstPage=1");
            cf.Add("-dLastPage=1");
            cf.Add("-sDEVICE=png16m");
            //cf.Add("-r72");
            cf.Add("-r96");
            //cf.Add("-sPAPERSIZE=a4");
            //cf.Add("-dNumRenderingThreads=" + Environment.ProcessorCount.ToString());
            cf.Add("-dTextAlphaBits=4");
            cf.Add("-dGraphicsAlphaBits=4");

            cf.Add(@"-sOutputFile=" + fileOutput);
            cf.Add(@"-f" + fileInput);
            return cf.ToArray();
        }

        #endregion

        #region [ VECTOR_TO_PDF ]

        static string[] __VECTOR_TO_PDF(string fileInput, string fileOutput)
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

        static byte[] __VECTOR_TO_PDF(string fileInput, byte[] bufInput,int dpi = 96)
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
                img.Save(@"C:\1.png", System.Drawing.Imaging.ImageFormat.Png);
            }

            int pw = width * 72 / dpi;
            int ph = height * 72 / dpi;

            GhostscriptPipedOutput gsPipedOutput = new GhostscriptPipedOutput();

            // pipe handle format: %handle%hexvalue
            string outputPipeHandle = "%handle%" + int.Parse(gsPipedOutput.ClientHandle).ToString("X2");

            using (GhostscriptProcessor processor = new GhostscriptProcessor())
            {
                List<string> cf = new List<string>();
                cf.Add("-empty");
                cf.Add("-dQUIET");
                cf.Add("-dSAFER");
                cf.Add("-dBATCH");
                cf.Add("-dNOPAUSE");
                cf.Add("-dNOPROMPT");
                cf.Add("-sDEVICE=pdfwrite");


                cf.Add("-dCompatibilityLevel=1.4");
                cf.Add("-dDOINTERPOLATE");

                cf.Add("-dFIXEDMEDIA");
                cf.Add("-dPDFFitPage");

                cf.Add("-dDEVICEWIDTHPOINTS=" + pw.ToString());
                cf.Add("-dDEVICEHEIGHTPOINTS=" + ph.ToString());

                //cf.Add("-dPDFSETTINGS=/printer"); // /screen, /default, /ebook, /printer, /prepress


                cf.Add("-o" + outputPipeHandle);
                cf.Add("-q");
                cf.Add("-f");
                cf.Add(fileInput);

                try
                {
                    processor.StartProcessing(cf.ToArray(), null);
                    byte[] buf = gsPipedOutput.Data;
                    return buf;
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
            return null;
        }

        #endregion

        static void ghostscript_Processing(object sender, GhostscriptProcessorProcessingEventArgs e)
            => Console.WriteLine(e.CurrentPage.ToString() + " / " + e.TotalPages.ToString());
    }
}
