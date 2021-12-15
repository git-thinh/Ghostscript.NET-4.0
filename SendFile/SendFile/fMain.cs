using StackExchange.Redis;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace SendFile
{
    public partial class fMain : Form
    {
        object[] __services = new object[] { "VECTOR_TO_PNG", "VECTOR_TO_INFO_SIZE", "VECTOR_TO_PDF", "VECTOR_TO_PDF_CROP", "VECTOR_TO_PDF_ARTBOARD" };
        const string __uri = "http://localhost:42269";

        const string IP = "127.0.0.1";
        const ushort PORT = 54321;
        string __id = "";
        string __url = "";
        string __dpi = "70";
        string __scope_raw = "file:raw";

        IDatabase _dbWrite;
        public fMain()
        {
            InitializeComponent();
            ddlRedis_IP.SelectedIndex = 0;
        }
        private void ddlRedis_IP_SelectedIndexChanged(object sender, EventArgs e)
        {
            string s = string.Format("{0}:{1}", ddlRedis_IP.Text, txtRedis_Port.Text);
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(s);
            int db = 0;
            int.TryParse(txtRedis_DB.Text, out db);
            _dbWrite = redis.GetDatabase(db);
        }

        private void fMain_Load(object sender, EventArgs e)
        {
            this.ddlService.Items.AddRange(__services);
            ddlService.SelectedIndex = 0;
        }

        private void btnBrowserFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"D:\",
                Title = "Browse Text Files",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "txt",
                Filter = "Vectors (*.eps;*.svg;*.pdf,*.ai,*.ps)|*.eps;*.svg;*.pdf;*.ai;*.ps|" + "All files (*.*)|*.*",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string file = openFileDialog1.FileName;
                    labelFile.Text = file;
                    cacheFile();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + " - " + ex.Message);
                }
            }
        }

        private void btnSendFile(object sender, EventArgs e) => sendFile();

        private void ddlService_SelectedIndexChanged(object sender, EventArgs e) => cacheFile();

        void sendFile()
        {
            if (!string.IsNullOrEmpty(__id))
            {
                try
                {
                    labelOK.Text = "";
                    //TcpClient client = new TcpClient();
                    //client.Connect(IP, PORT);
                    //var bufID = Encoding.ASCII.GetBytes(__id);
                    //NetworkStream stream = client.GetStream();
                    //stream.Write(bufID, 0, bufID.Length);
                    //stream.Flush();
                    //bool ok = stream.ReadByte() == 1;
                    //labelOK.Text = ok ? "SUCCESS" : "FAIL";
                    //stream.Close();
                    //client.Close();

                    if (!Directory.Exists("./outputs"))
                        Directory.CreateDirectory("./outputs");

                    apiUrlCreate();

                    var buf = new WebClient().DownloadData(__url);
                    if (buf != null)
                    {
                        switch (ddlService.SelectedIndex)
                        {
                            case 0: //VECTOR_TO_PNG
                                File.WriteAllBytes("./outputs/" + __id + ".png", buf);
                                labelOK.Text = "SUCCESS: " + __id;
                                break;
                            case 1: //VECTOR_TO_INFO_SIZE
                                labelOK.Text = Encoding.UTF8.GetString(buf);
                                break;
                            case 2: //VECTOR_TO_PDF
                                File.WriteAllBytes("./outputs/" + __id + ".pdf", buf);
                                labelOK.Text = "SUCCESS: " + __id;
                                break;
                            case 3: //VECTOR_TO_PDF_CROP
                                File.WriteAllBytes("./outputs/" + __id + ".crop.pdf", buf);
                                labelOK.Text = "SUCCESS: " + __id;
                                break;
                            case 4: //VECTOR_TO_PDF_ARTBOARD
                                File.WriteAllBytes("./outputs/" + __id + ".artboard.pdf", buf);
                                labelOK.Text = "SUCCESS: " + __id;
                                break;
                        }
                    }
                    else labelOK.Text = "FAIL";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + " - " + ex.Message);
                }
            }
        }

        void cacheFile()
        {
            string file = labelFile.Text;
            if (!string.IsNullOrEmpty(file))
            {
                string service = ddlService.SelectedIndex.ToString();
                __id = service + "-" + Path.GetExtension(file).Substring(1) + "-1";// + DateTime.Now.ToString("yyMMdd-HHmmss") + "." + Guid.NewGuid().ToString();
                if (__id.Length > 36) __id = __id.Substring(0, 36).ToLower();
                this.Text = __id + " | " + file;

                apiUrlCreate();

                byte[] bufData = File.ReadAllBytes(file);
                _dbWrite.StringSet(__scope_raw + ":" + __id, bufData);
            }
        }

        void apiUrlCreate()
        {
            __dpi = textBoxDPI.Text.Trim();
            switch (ddlService.SelectedIndex)
            {
                case 0: //VECTOR_TO_PNG
                    __url = __uri + "/api/vector/png/" + __dpi + "/" + __scope_raw + "/" + __id;
                    break;
                case 1: //VECTOR_TO_INFO_SIZE
                    __url = __uri + "/api/vector/size/" + __dpi + "/" + __scope_raw + "/" + __id;
                    break;
                case 2: //VECTOR_TO_PDF
                    __url = __uri + "/api/vector/pdf/" + __dpi + "/" + __scope_raw + "/" + __id;
                    break;
                case 3: //VECTOR_TO_PDF_CROP
                    __url = __uri + "/api/vector/pdf/crop/" + __scope_raw + "/" + __id
                        + "/" + cropTop.Text + "/" + cropRight.Text
                        + "/" + cropBottom.Text + "/" + cropLeft.Text;
                    break;
                case 4: //VECTOR_TO_PDF_ARTBOARD
                    __url = __uri + "/api/vector/pdf/crop/" + __scope_raw + "/" + __id
                        + "/" + cropTop.Text + "/" + cropRight.Text
                        + "/" + cropBottom.Text + "/" + cropLeft.Text;
                    break;
            }
            textBoxAPI.Text = __url;
        }

    }
}
