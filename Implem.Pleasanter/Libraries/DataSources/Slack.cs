﻿using Implem.Libraries.Utilities;
using Implem.Pleasanter.Libraries.Requests;
using Implem.Pleasanter.Models;
using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
namespace Implem.Pleasanter.Libraries.DataSources
{
    [Serializable]
    public class Slack
    {
        [NonSerialized]
        public Context context;
        public string text;
        public string username;
        public string icon_emoji;

        public Slack(Context _context, string _text, string _username, string _icon_emoji = null)
        {
            context = _context;
            text = _text;
            username = _username;
            icon_emoji = _icon_emoji;
        }

        public void Send(string url)
        {
            Task.Run(() =>
            {
                try
                {
                    var client = new NotificationHttpClient();
                    client.NotifyString(url, this.ToJson());
                }
                catch (Exception e)
                {
                    new SysLogModel(context, e);
                }
            });
        }
    }
}