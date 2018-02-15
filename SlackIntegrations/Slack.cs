using System;
using System.Text;
using Newtonsoft.Json;
using System.Collections.Generic;
using RestSharp;

namespace SlackIntegrations
{
    public class SlackClient
    {
        private readonly RestClient _restClient;
        private readonly Uri _uri;
        private readonly Encoding _encoding = new UTF8Encoding();

        public SlackClient(string urlWithAccessToken)
        {
            _uri = new Uri(urlWithAccessToken);
            _restClient = new RestClient(_uri.GetLeftPart(UriPartial.Authority));
        }

        //Post a message with attachments
        public void PostMessage(string text = null, string username = null, List<Attachment> attachments = null)
        {
            SlackMessageWithAttachments slackMessage = new SlackMessageWithAttachments()
            {
                attachments = attachments,
                username = username
            };

            SendToSlack(slackMessage);
        }

        //Post a message with just text and no attachments
        public void PostMessage(string text = null, string username = null)
        {
            SlackMessageNoAttachments slackMessage = new SlackMessageNoAttachments()
            {
                text = text,
                username = username
            };

            SendToSlack(slackMessage);
        }

        //Sends the message with attachments to Slack using RestSharp
        public bool SendToSlack(SlackMessageWithAttachments payload)
        {
            string payloadJson = JsonConvert.SerializeObject(payload);

            var request = new RestRequest(_uri.PathAndQuery, Method.POST);

            request.AddParameter("payload", payloadJson);

            try
            {
                var response = _restClient.Execute(request);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //Sends the message with no attachments to Slack using RestSharp
        public bool SendToSlack(SlackMessageNoAttachments payload)
        {
            string payloadJson = JsonConvert.SerializeObject(payload);

            var request = new RestRequest(_uri.PathAndQuery, Method.POST);

            request.AddParameter("payload", payloadJson);

            try
            {
                var response = _restClient.Execute(request);
                return true;
            }
            catch (Exception)
            {
                return false;
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

    public class SlackMessageWithAttachments : SlackMessageNoAttachments
    {
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
