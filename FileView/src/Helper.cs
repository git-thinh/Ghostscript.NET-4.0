using System.IO;

namespace FileView
{
    public class Helper
    {
        public static string getFileType_byID(string id)
        {
            string[] a = id.Split('-');
            if (a.Length > 1)
                return a[1];
            return string.Empty;
        }

        public static string getFileInput_byID(string id, string ext = "")
        {
            string s = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\inputs\\";
            if (!Directory.Exists(s)) Directory.CreateDirectory(s);
            string fileType = ext;
            if (string.IsNullOrEmpty(ext)) fileType = getFileType_byID(id);
            return s + id + "." + fileType;
        }

        public static string getFileOutput_byID(string id, string ext = "")
        {
            string s = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\outputs\\";
            if (!Directory.Exists(s)) Directory.CreateDirectory(s);
            string fileType = ext;
            if (string.IsNullOrEmpty(ext)) fileType = getFileType_byID(id);
            return s + id + "." + fileType;
        }
    }
}
