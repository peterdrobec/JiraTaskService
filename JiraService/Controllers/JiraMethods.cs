using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;
using System.Timers;
using System.Web;


namespace JiraService.Controllers
{
    public class JiraMethods
    {
                //Method for deserialization of JSON for particular jira issue
        public static JiraTicket deserializeTicket(string url, CookieContainer cookies)
        {
            string ticketJson;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.CookieContainer = cookies;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            ticketJson = readHttpResponse(response);

            JiraTicket ticket = JsonConvert.DeserializeObject<JiraTicket>(ticketJson, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            return ticket;
        }

        public static FilterResults deserializeFilterResults(string url, CookieContainer cookies)
        {
            string resultJson;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.CookieContainer = cookies;

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            resultJson = readHttpResponse(response);

            FilterResults results = JsonConvert.DeserializeObject<FilterResults>(resultJson, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            return results;
        }

        public static FilterResults deserializeFilterResults(string username, string password, string jql)
        {
            string resultJson;
            string url = "https://jira.kmiservicehub.com/rest/api/2/search?jql=" + jql + "&os_username=" + username + "&os_password=" + password;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);


            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            resultJson = readHttpResponse(response);

            FilterResults results = JsonConvert.DeserializeObject<FilterResults>(resultJson, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            return results;
        }

        //project = "Unified IT Manager" and Sprint = "Sprint 1 SNI - Legend"


        //method returns start and end date of running sprints
       

        public static string readHttpResponse(HttpWebResponse response)
        {
            string resultJson;
            using (var s = new StreamReader(response.GetResponseStream()))
            {
                resultJson = s.ReadToEnd();
            }
            return resultJson;
        }
    }
}