using StackExchange.Redis;

namespace ImageServiceConsole
{
    static class VectorService
    {
        static string[] __services = new string[] {
            "VECTOR_TO_PNG",
            "VECTOR_TO_INFO_SIZE",
            "VECTOR_TO_PDF",
            "VECTOR_TO_PDF_SELECTION",
            "PDF_TO_PNG"
        };

        public static bool processBuffer(string id, byte[] buf, IDatabase _redisWrite, IDatabase _redisRead)
        {
            bool ok = false;
            string[] a = id.Split('-');
            if (a.Length > 2)
            {
                int serviceIndex = -1;
                int.TryParse(a[0], out serviceIndex);
                if (serviceIndex >= 0 && serviceIndex < __services.Length)
                {
                    string service = __services[serviceIndex];
                    switch (service)
                    {
                        case "VECTOR_TO_PNG":
                            ok = GhostScriptService.__VECTOR_TO_PNG_v2(id, buf, _redisWrite, _redisRead);
                            break;
                        case "VECTOR_TO_INFO_SIZE":
                            ok = GhostScriptService.__VECTOR_TO_INFO_SIZE(id, buf, _redisWrite, _redisRead);
                            break;
                        case "VECTOR_TO_PDF":
                            ok = GhostScriptService.__VECTOR_TO_PDF(id, buf, _redisWrite, _redisRead);
                            break;
                        case "VECTOR_TO_PDF_SELECTION":
                            break;
                        case "PDF_TO_PNG":
                            ok = GhostScriptService.__PDF_TO_PNG(id, buf, _redisWrite, _redisRead, 96);
                            break;
                    }
                }
            }
            return ok;
        }
    }
}
