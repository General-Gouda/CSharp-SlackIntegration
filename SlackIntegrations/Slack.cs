using Newtonsoft.Json;
using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;

//A simple C# class to post messages to a Slack channel
//Note: This class uses the Newtonsoft Json.NET serializer available via NuGet
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

        //Post a message using simple strings
        public void PostMessage(string text,
                                string pretext = null,
                                string username = null,
                                string channel = null,
                                string color = null
                               )
        {
            Payload payload = new Payload()
            {
                Channel = channel,
                Username = username,
                Text = text,
                Color = color,
                PreText = pretext,
            };

            PostMessage(payload);
        }

        //Post a message using a Payload object
        public void PostMessage(Payload payload)
        {
            string payloadJson = JsonConvert.SerializeObject(payload);

            using (WebClient client = new WebClient())
            {
                NameValueCollection data = new NameValueCollection();
                data["payload"] = payloadJson;

                var response = client.UploadValues(_uri,"POST",data);

                //The response text is usually "ok"
                string responseText = _encoding.GetString(response);
            }
        }
    }

    //This class serializes into the Json payload required by Slack Incoming WebHooks
    public class Payload
    {
        [JsonProperty("channel")]
        public string Channel { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("fallback")]
        public string Fallback { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("pretext")]
        public string PreText { get; set; }
    }

    public static class SlackColors
    {
        public enum colors
        {
            Red,
            Orange,
            Yellow,
            Green,
            LightBlue,
            DarkBlue,
            Black,
            White,
            Gray,
            DarkGray
        }

        public static string slackColors (colors color)
        {
            string output = null;

            switch (color)
            {
                case colors.Red:
                    output = "#FF0000";
                    return output;
                case colors.Orange:
                    output = "#FF9900";
                    return output;
                case colors.Yellow:
                    output = "#FFFF00";
                    return output;
                case colors.Green:
                    output = "#00FF00";
                    return output;
                case colors.LightBlue:
                    output = "#00FFFF";
                    return output;
                case colors.DarkBlue:
                    output = "#0000FF";
                    return output;
                case colors.Black:
                    output = "#000000";
                    return output;
                case colors.White:
                    output = "#FFFFFF";
                    return output;
                case colors.Gray:
                    output = "#CCCCCC";
                    return output;
                case colors.DarkGray:
                    output = "#808080";
                    return output;
                default:
                    output = null;
                    return output;
            }
        }
    }
}

