using System;
using System.Text;
using Newtonsoft.Json;
using System.Collections.Generic;
using RestSharp;

namespace SlackIntegrations
{
    public class SlackClient<T>
    {
        private readonly RestClient _restClient;
        private readonly Uri _uri;
        private readonly Encoding _encoding = new UTF8Encoding();

        public SlackClient(string urlWithAccessToken)
        {
            _uri = new Uri(urlWithAccessToken);
            _restClient = new RestClient(_uri.GetLeftPart(UriPartial.Authority));
        }

        //Post a message with attachments and no channel
        public bool PostMessage(string text = null, string username = null, List<Attachment> attachments = null)
        {
            SlackMessageWithAttachmentsNoChannel slackMessage = new SlackMessageWithAttachmentsNoChannel()
            {
                attachments = attachments,
                username = username
            };

            if (SendToSlack(slackMessage))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Post a message with attachments and with a channel
        public bool PostMessage(string channel, string text = null, string username = null, List<Attachment> attachments = null)
        {
            SlackMessageWithAttachmentsWithChannel slackMessage = new SlackMessageWithAttachmentsWithChannel()
            {
                attachments = attachments,
                username = username,
                channel = channel
            };

            if (SendToSlack(slackMessage))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Post a message with just text and no attachments and no channel
        public bool PostMessage(string text = null, string username = null)
        {
            SlackMessageNoAttachmentsNoChannel slackMessage = new SlackMessageNoAttachmentsNoChannel()
            {
                text = text,
                username = username
            };

            if (SendToSlack(slackMessage))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Post a message with just text and no attachments and with a channel
        public bool PostMessage(string channel, string text = null, string username = null)
        {
            SlackMessageNoAttachmentsWithChannel slackMessage = new SlackMessageNoAttachmentsWithChannel()
            {
                text = text,
                username = username,
                channel = channel
            };

            if (SendToSlack(slackMessage))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        //Sends the message to Slack using RestSharp
        public bool SendToSlack<T>(T payload)
        {
            string payloadJson = JsonConvert.SerializeObject(payload);

            var request = new RestRequest(_uri.PathAndQuery, Method.POST);

            request.AddParameter("payload", payloadJson);

            try
            {
                var response = _restClient.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    return true;
                }
                else
                {
                    return false;
                }
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

    public class SlackMessageNoAttachmentsNoChannel
    {
        public string text { get; set; }
        public string username { get; set; }
    }

    public class SlackMessageWithAttachmentsNoChannel : SlackMessageNoAttachmentsNoChannel
    {
        public List<Attachment> attachments { get; set; }
    }

    public class SlackMessageNoAttachmentsWithChannel : SlackMessageNoAttachmentsNoChannel
    {
        public string channel { get; set; }
    }

    public class SlackMessageWithAttachmentsWithChannel : SlackMessageNoAttachmentsNoChannel
    {
        public string channel { get; set; }
        public List<Attachment> attachments { get; set; }
    }

    public class SlackColors
    {
        public static string Red = "#FF0000";
        public static string Amber = "#FFBF00";
        public static string Orange = "#FF9900";
        public static string Yellow = "#FFFF00";
        public static string Green = "#00FF00";
        public static string LightBlue = "#00FFFF";
        public static string DarkBlue = "#0000FF";
        public static string White = "#FFFFFF";
        public static string Gray = "#CCCCCC";
        public static string DarkGray = "#808080";
        public static string Black = "#000000";
        public static string Chartreuse = "#7FFF00";
        public static string Purple = "#800080";
    }
}
