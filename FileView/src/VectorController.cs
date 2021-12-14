using Ghostscript.NET;
using Ghostscript.NET.Processor;
using Ghostscript.NET.Rasterizer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using IOFile = System.IO.File;

namespace FileView
{
    [Route("api/[controller]")]
    public class VectorController : Controller
    {
        const string GS_VERSION_DLL = @"C:\Program Files\gs\gs9.55.0\bin\gsdll64.dll";
        //static GhostscriptVersionInfo GS_VERSION_INFO = GhostscriptVersionInfo.GetLastInstalledVersion();
        static GhostscriptVersionInfo GS_VERSION_INFO = new GhostscriptVersionInfo(GS_VERSION_DLL);
        const string __SCOPE_REDIS = "file";

        readonly ILogger _logger;
        readonly IConfiguration _configuration;
        readonly IWebHostEnvironment _environment;
        readonly IDatabase _redisWrite = null;
        readonly IDatabase _redisRead = null;
        public VectorController(ILoggerFactory loggerFactory,
            IConfiguration configuration,
            IWebHostEnvironment environment,
            RedisService redis)
        {
            _logger = loggerFactory.CreateLogger(GetType());
            _configuration = configuration;
            _environment = environment;

            _redisWrite = redis.GetDB(REDIS_TYPE.WRITE);
            _redisRead = redis.GetDB(REDIS_TYPE.READ1);
        }

        [HttpGet("clean/{scope}/{id}")]
        public async Task<IActionResult> clean_redis(string scope, string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                string key = scope + ":" + id;
                bool exist = await _redisRead.KeyExistsAsync(key);
                if (exist)
                {
                    await _redisWrite.StringGetDeleteAsync(key);
                    return Ok();
                }
            }
            return NotFound();
        }

        #region [ VECTOR -> PNG, SIZE ]

        string[] __vector_to_png(int dpi, string fileInput, string outputPipeHandle)
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

            cf.Add("-r" + dpi.ToString());
            //cf.Add("-r96");

            //cf.Add("-sPAPERSIZE=legal");
            //cf.Add("-sPAPERSIZE=a4");
            //cf.Add("-dNumRenderingThreads=" + Environment.ProcessorCount.ToString());

            //cf.Add("-dAlignToPixels=0");
            //cf.Add("-dTextAlphaBits=4");
            //cf.Add("-dGraphicsAlphaBits=4");

            //cf.Add(@"-sOutputFile=" + fileOutput);
            cf.Add("-o" + outputPipeHandle);
            cf.Add("-q");

            cf.Add(@"-f" + fileInput);
            return cf.ToArray();
        }

        [HttpGet("png/{dpi}/{scope}/{id}")]
        public async Task<IActionResult> toPNG(int dpi, string scope, string id)
        {
            if (dpi < 1) dpi = 70;
            if (!string.IsNullOrEmpty(id))
            {
                try
                {
                    string key = scope + ":" + id;
                    byte[] buf = await _redisRead.StringGetAsync(key);
                    if (buf != null)
                    {
                        byte[] rs = null;

                        string path = _environment.WebRootPath + "\\temps\\";
                        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                        string fileInput = path + id + "." + Helper.getFileType_byID(id);
                        IOFile.WriteAllBytes(fileInput, buf);

                        GhostscriptPipedOutput gsPipedOutput = new GhostscriptPipedOutput();
                        string outputPipeHandle = "%handle%" + int.Parse(gsPipedOutput.ClientHandle).ToString("X2");
                        using (GhostscriptProcessor g = new GhostscriptProcessor())
                        {
                            try
                            {
                                g.StartProcessing(__vector_to_png(dpi, fileInput, outputPipeHandle), null);
                                rs = gsPipedOutput.Data;
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

                        IOFile.Delete(fileInput);
                        _redisWrite.StringSet(__SCOPE_REDIS + ":png:" + id, rs);
                        if (rs != null)
                            return File(new MemoryStream(rs), "application/octet-stream");
                    }
                }
                catch (Exception ex)
                {
                }
            }
            return NotFound();
        }

        [HttpGet("size/{dpi}/{scope}/{id}")]
        public async Task<IActionResult> getSize(int dpi, string scope, string id)
        {
            if (dpi < 1) dpi = 70;
            if (!string.IsNullOrEmpty(id))
            {
                try
                {
                    string key = scope + ":" + id;
                    byte[] buf = await _redisRead.StringGetAsync(key);
                    if (buf != null)
                    {
                        byte[] rs = null;

                        string path = _environment.WebRootPath + "\\temps\\";
                        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                        string fileInput = path + id + "." + Helper.getFileType_byID(id);
                        IOFile.WriteAllBytes(fileInput, buf);

                        GhostscriptPipedOutput gsPipedOutput = new GhostscriptPipedOutput();
                        string outputPipeHandle = "%handle%" + int.Parse(gsPipedOutput.ClientHandle).ToString("X2");
                        using (GhostscriptProcessor g = new GhostscriptProcessor())
                        {
                            try
                            {
                                g.StartProcessing(__vector_to_png(dpi, fileInput, outputPipeHandle), null);
                                rs = gsPipedOutput.Data;
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

                        IOFile.Delete(fileInput);
                        _redisWrite.StringSet(__SCOPE_REDIS + ":png:" + id, rs);
                        if (rs != null)
                        {
                            var img = Bitmap.FromStream(new MemoryStream(rs));
                            var size = new { Width = img.Width, Height = img.Height };
                            img.Dispose();
                            return Json(size);
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
            return NotFound();
        }

        #endregion

        #region [ VECTOR -> PDF ]

        string[] __vector_to_pdf(int dpi, string fileInput, string outputPipeHandle)
        {
            List<string> cf = new List<string>();
            cf.Add("-dBATCH");
            cf.Add("-dNOPAUSE");
            cf.Add("-dDOINTERPOLATE");

            cf.Add("-sDEVICE=pdfwrite");

            //cf.Add("-dFIXEDMEDIA");
            //cf.Add("-dPDFFitPage");
            //cf.Add("-dDEVICEWIDTHPOINTS=" + pw.ToString());
            //cf.Add("-dDEVICEHEIGHTPOINTS=" + ph.ToString());

            cf.Add("-dALLOWPSTRANSPARENCY");
            cf.Add("-dEPSCrop");

            cf.Add("-r" + dpi.ToString());

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

            return cf.ToArray();
        }

        [HttpGet("pdf/{dpi}/{scope}/{id}")]
        public async Task<IActionResult> toPDF(int dpi, string scope, string id)
        {
            if (dpi < 1) dpi = 70;
            if (!string.IsNullOrEmpty(id))
            {
                try
                {
                    string key = scope + ":" + id;
                    byte[] buf = await _redisRead.StringGetAsync(key);
                    if (buf != null)
                    {
                        byte[] rs = null;

                        string path = _environment.WebRootPath + "\\temps\\";
                        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                        string fileInput = path + id + "." + Helper.getFileType_byID(id);
                        IOFile.WriteAllBytes(fileInput, buf);

                        GhostscriptPipedOutput gsPipedOutput = new GhostscriptPipedOutput();
                        string outputPipeHandle = "%handle%" + int.Parse(gsPipedOutput.ClientHandle).ToString("X2");
                        using (GhostscriptProcessor g = new GhostscriptProcessor())
                        {
                            try
                            {
                                g.StartProcessing(__vector_to_pdf(dpi, fileInput, outputPipeHandle), null);
                                rs = gsPipedOutput.Data;
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

                        IOFile.Delete(fileInput);
                        _redisWrite.StringSet(__SCOPE_REDIS + ":pdf:" + id, rs);
                        if (rs != null)
                            return File(new MemoryStream(rs), "application/octet-stream");
                    }
                }
                catch (Exception ex)
                {
                }
            }
            return NotFound();
        }

        #endregion
    }

}
