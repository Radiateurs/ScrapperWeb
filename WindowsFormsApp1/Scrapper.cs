using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Scrapper
    {
        private Uri addr;
        private HttpWebRequest request;
        private HttpWebResponse response;
        public List<string> datas;

        public Scrapper()
        {
        }

        public Scrapper(string URL)
        {
            addr = new Uri(URL);
            datas = new List<string>();
        }

        public void Scrap(string wish)
        {
            string RowData;
            string tmp;
            int start, end, lastPos;
            CookieContainer cookie = new CookieContainer();

            request = (HttpWebRequest)WebRequest.Create(addr);
            request.Method = "GET";
            request.CookieContainer = cookie;
            request.Credentials = CredentialCache.DefaultCredentials;
            response = (HttpWebResponse)request.GetResponse();
            Stream received = response.GetResponseStream();
            StreamReader reader = new StreamReader(received, Encoding.UTF8);
            RowData = reader.ReadToEnd();
            Console.WriteLine(RowData);
            lastPos = 0;
            while (RowData.Contains(wish))
            {
                start = RowData.IndexOf(wish, lastPos);
                end = start + wish.Length - 1;
                while (RowData[start] != '\"' && start > 0)
                    start--;
                if (start == 0)
                    break;
                tmp = RowData.Substring(start + 1, end - start);
                Console.WriteLine(tmp);
                if (tmp.Substring(0, 4).Contains("http") || tmp.Substring(0, 5).Contains("https"))
                    datas.Add(tmp);
                RowData = RowData.Substring(end, RowData.Length - end);
            }
            Console.WriteLine("Founded {0} matches", datas.Count);
            response.Close();
        }

        public void ExportDatas(string folderPath)
        {
            string completedPath;
            WebClient wc = new WebClient();

            foreach (string row in datas)
            {
                Console.WriteLine(row);
                completedPath = folderPath + "\\" + row.Substring(row.LastIndexOf('/') + 1);
                wc.DownloadFile(row, completedPath);
            }
        }
    }
}
