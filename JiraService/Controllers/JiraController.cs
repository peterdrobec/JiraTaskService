﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JiraService.Controllers
{
    public class JiraController : ApiController
    {
        [System.Web.Http.HttpGet]
        public Object GetTickets([FromUri]string name, [FromUri]string password, [FromUri]string jql)
        {
            try
            {
                var res = JiraMethods.deserializeFilterResults(name, password, jql);
                var response = from jt in res.issues
                               where jt.fields.issuetype.name != "Sub-Task"
                               select new { jt.key, issuetype = jt.fields.issuetype.name, jt.fields.summary, storyPoints = jt.fields.customfield_10008, jt.fields.subtasks };
                //var response = from jt in res.issues
                //               group new { jt.key, issuetype = jt.fields.issuetype.name, jt.fields.summary, storyPoints = jt.fields.customfield_10008 } by jt.fields.issuetype.name into xGroup
                //               select xGroup;
                return response;
            }
            catch (Exception ex)
            {
                return ("Bad Request with parameters: username=" + name + " password:" + password + " jql:" + jql + " " + ex.Message);
            }
        }
    }
}
