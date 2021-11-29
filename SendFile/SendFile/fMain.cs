using StackExchange.Redis;
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace SendFile
{
    public partial class fMain : Form
    {
        const string IP = "127.0.0.1";
        const ushort PORT = 54321;
        string __id = "";
        IDatabase _dbWrite;
        public fMain()
        {
            InitializeComponent();
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:1000");
            _dbWrite = redis.GetDatabase(1);
        }

        private void fMain_Load(object sender, EventArgs e)
        {
            this.ddlService.Items.AddRange(new object[] { "VECTOR_TO_PNG", "VECTOR_TO_INFO_SIZE", "VECTOR_TO_PDF", "VECTOR_TO_PDF_SELECTION", "PDF_TO_PNG"});
            ddlService.SelectedIndex = 3;
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
                catch (Exception ex) {
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
                    TcpClient client = new TcpClient();
                    client.Connect(IP, PORT);

                    var bufID = Encoding.ASCII.GetBytes(__id);
                    NetworkStream stream = client.GetStream();
                    stream.Write(bufID, 0, bufID.Length);
                    stream.Flush();

                    stream.Close();
                    client.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + " - " + ex.Message);
                }
            }
        }

        void cacheFile() {
            string file = labelFile.Text;
            if (!string.IsNullOrEmpty(file))
            {
                string service = ddlService.SelectedIndex.ToString();

                __id = service + "-" + Path.GetExtension(file).Substring(1) + "-" + DateTime.Now.ToString("yyMMdd-HHmmss") + "." + Guid.NewGuid().ToString();
                __id = __id.Substring(0, 36).ToLower();
                this.Text = __id + " | " + file;

                byte[] bufData = System.IO.File.ReadAllBytes(file);
                _dbWrite.StringSet(__id, bufData);
            }
        }
    }
}
