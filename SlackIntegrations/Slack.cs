using System;
using System.Net;
using System.Text;
using System.Collections.Specialized;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace SlackIntegrations
{
    public class SlackClient
    {
        private readonly Uri _uri;
        private readonly Encoding _encoding = new UTF8Encoding();

        public SlackClient(string urlWithAccessToken)
        {
            _uri = new Uri(urlWithAccessToken);
        }

        //Post a message with attachments
        public void PostMessage(string text = null, string username = null, List<Attachment> attachments = null)
        {
            SlackMessageWithAttachments slackMessage = new SlackMessageWithAttachments()
            {
                attachments = attachments,
                username = username
            };

            PostPayload(slackMessage);
        }

        //Post a message with just text and no attachments
        public void PostMessage(string text = null, string username = null)
        {
            SlackMessageNoAttachments slackMessage = new SlackMessageNoAttachments()
            {
                text = text,
                username = username
            };

            PostPayload(slackMessage);
        }

        //Sends the payload (with attachments) to Slack
        public void PostPayload(SlackMessageWithAttachments payload)
        {
            string payloadJson = JsonConvert.SerializeObject(payload);

            using (WebClient client = new WebClient())
            {
                NameValueCollection data = new NameValueCollection();
                data["payload"] = payloadJson;

                var response = client.UploadValues(_uri, "POST", data);

                //The response text is usually "ok"
                string responseText = _encoding.GetString(response);
            }
        }

        //Sends the payload (without attachments) to Slack
        public void PostPayload(SlackMessageNoAttachments payload)
        {
            string payloadJson = JsonConvert.SerializeObject(payload);

            using (WebClient client = new WebClient())
            {
                NameValueCollection data = new NameValueCollection();
                data["payload"] = payloadJson;

                var response = client.UploadValues(_uri, "POST", data);

                //The response text is usually "ok"
                string responseText = _encoding.GetString(response);
            }
        }
    }

    //Classes used for Slack messages
    public class Field
    {
        public string title { get; set; }
        public string value { get; set; }
        public bool @short { get; set; }
    }

    public class Attachment
    {
        public string color { get; set; }
        public string pretext { get; set; }
        public string text { get; set; }
        public string fallback { get; set; }
        public string author_name { get; set; }
        public string author_link { get; set; }
        public string author_icon { get; set; }
        public string title { get; set; }
        public string title_link { get; set; }
        public List<Field> fields { get; set; }
        public string image_url { get; set; }
        public string thumb_url { get; set; }
        public string footer { get; set; }
        public string footer_icon { get; set; }
        public int ts { get; set; }
    }

    public class SlackMessageWithAttachments
    {
        public string text { get; set; }
        public string username { get; set; }
        public List<Attachment> attachments { get; set; }
    }

    public class SlackMessageNoAttachments
    {
        public string text { get; set; }
        public string username { get; set; }
    }

    public class SlackColors
    {
        public static string Red = "#FF0000";
        public static string Orange = "#FF9900";
        public static string Yellow = "#FFFF00";
        public static string Green = "#00FF00";
        public static string LightBlue = "#00FFFF";
        public static string DarkBlue = "#0000FF";
        public static string Black = "#000000";
        public static string White = "#FFFFFF";
        public static string Gray = "#CCCCCC";
        public static string DarkGray = "#808080";
    }
}
