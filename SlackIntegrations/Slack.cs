using Newtonsoft.Json;
using System;
using System.Collections.Specialized;
using System.Net;
using System.Reflection;
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

    public enum SlackColors
    {
        [StringValue("#FF0000")]
        Red,

        [StringValue("#FFFF00")]
        Yellow,

        [StringValue("#00FF00")]
        Green,

        [StringValue("#00FFFF")]
        LightBlue,

        [StringValue("#0000FF")]
        DarkBlue,

        [StringValue("#000000")]
        Black,

        [StringValue("#FFFFFF")]
        White,

        [StringValue("#CCCCCC")]
        Gray
    }

    public class StringValue : System.Attribute
    {
        private readonly string _value;

        public StringValue(string value)
        {
            _value = value;
        }

        public string Value
        {
            get { return _value; }
        }
    }

    public static class StringEnum
    {
        public static string GetStringValue(Enum value)
        {
            string output = null;
            Type type = value.GetType();

            //Check first in our cached results...

            //Look for our 'StringValueAttribute' 

            //in the field's custom attributes

            FieldInfo fi = type.GetField(value.ToString());
            StringValue[] attrs =
               fi.GetCustomAttributes(typeof(StringValue),
                                       false) as StringValue[];
            if (attrs.Length > 0)
            {
                output = attrs[0].Value;
            }

            return output;
        }
    }
}

