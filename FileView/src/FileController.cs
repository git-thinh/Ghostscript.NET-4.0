﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FileView
{
    [Route("api/[controller]")]
    public class FileController : Controller
    {
        readonly ILogger _logger;
        readonly IConfiguration _configuration;
        readonly IWebHostEnvironment _environment;
        readonly IDatabase _redisRead = null;
        public FileController(ILoggerFactory loggerFactory,
            IConfiguration configuration,
            IWebHostEnvironment environment,
            RedisService redis)
        {
            _logger = loggerFactory.CreateLogger(GetType());
            _configuration = configuration;
            _environment = environment;
            _redisRead = redis.GetDB(REDIS_TYPE.READ1);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> getFile(string scope, string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                string key = scope + ":" + id;
                byte[] buf = await _redisRead.StringGetAsync(key);
                if (buf != null)
                    return File(new MemoryStream(buf), "image/png");
            }
            return NotFound();
        }
    }

}
