using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace PlayWithRemoteRepos
{
    class Program
    {
        //https://developer.github.com/v3/repos/
        static void Main(string[] args)
        {
            //create repo
            var uname = "no2fatestuser@gmail.com";
            var upass = "123456test";
            var json = "{" +
                "\"name\": \"newtestrepo\"," +
                "\"description\": \"Test Creation Repo\"," +
                "\"private\": false" +
                "}";
            HttpWebRequest req = WebRequest.Create("https://api.github.com/user/repos") as HttpWebRequest;
            req.Method = "POST";
            req.ContentType = "application/json";
            req.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)";
            req.Headers.Add("Authorization", "Basic " 
                + Convert.ToBase64String(new ASCIIEncoding().GetBytes(uname + ":" + upass)));
            StreamWriter sw = new StreamWriter(req.GetRequestStream());
            sw.Write(json);
            sw.Flush();
            sw.Close();
            var result = "";
            using (HttpWebResponse resp = req.GetResponse() as HttpWebResponse)
            {
                StreamReader sr = new StreamReader(resp.GetResponseStream());
                result = sr.ReadToEnd();
            }
            //verify repo
            req = WebRequest.Create("https://api.github.com/repos/GitHubNoTwoStepVerification/newtestrepo") as HttpWebRequest;
            req.Method = "GET";
            req.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)";
            req.Headers.Add("Authorization", "Basic "
                + Convert.ToBase64String(new ASCIIEncoding().GetBytes(uname + ":" + upass)));
            using (HttpWebResponse resp = req.GetResponse() as HttpWebResponse)
            {
                StreamReader sr = new StreamReader(resp.GetResponseStream());
                result = sr.ReadToEnd();
            }
            //delete repo
            req = WebRequest.Create("https://api.github.com/repos/GitHubNoTwoStepVerification/newtestrepo") as HttpWebRequest;
            req.Method = "DELETE";
            req.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)";
            req.Headers.Add("Authorization", "Basic "
                + Convert.ToBase64String(new ASCIIEncoding().GetBytes(uname + ":" + upass)));
            using (HttpWebResponse resp = req.GetResponse() as HttpWebResponse)
            {
                StreamReader sr = new StreamReader(resp.GetResponseStream());
                result = sr.ReadToEnd(); //on success - empty string is returned
            }
        }
    }
}
