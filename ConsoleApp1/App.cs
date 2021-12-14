using StackExchange.Redis;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ImageServiceConsole
{
    class App
    {
        const string IP = "127.0.0.1";
        const string REDIS_PORT_WRITE = "1000";
        const string REDIS_PORT_READ = "1001";

        const int MAX_CONNECTION = 10;
        const int PORT_NUMBER = 54321;
        static int _connectionsCount = 0;
        static TcpListener listener;

        static IDatabase _redisWrite;
        static IDatabase _redisRead;
        public static void Main()
        {
            ConnectionMultiplexer r1 = ConnectionMultiplexer.Connect(IP + ":" + REDIS_PORT_WRITE);
            _redisWrite = r1.GetDatabase(1);
            ConnectionMultiplexer r2 = ConnectionMultiplexer.Connect(IP + ":" + REDIS_PORT_READ);
            _redisRead = r2.GetDatabase(1);

            IPAddress address = IPAddress.Parse(IP);

            listener = new TcpListener(address, PORT_NUMBER);
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
            try
            {
                var stream = new NetworkStream(soc);

                byte[] buf = new byte[36];
                int sz = stream.Read(buf, 0, 36);

                string id = ASCIIEncoding.ASCII.GetString(buf).Split('\0')[0].Trim();
                Console.WriteLine(id);
                buf = _redisRead.StringGet("img:raw:" + id);
                byte val = 0;
                if (buf != null)
                {
                    var ok = VectorService.processBuffer(id, buf, _redisWrite, _redisRead);
                    if (ok) val = 1;
                }
                stream.WriteByte(val);
                stream.Flush();
                stream.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
            }

            soc.Close();
        }

    }
}
