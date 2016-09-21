using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlackIntegrations
{
    class Program
    {
        static void Main(string[] args)
        {
            string urlWithAccessToken = "https://hooks.slack.com/services/{webhook information from Slack API}";

            SlackClient client = new SlackClient(urlWithAccessToken);

            string dateTime = DateTime.Now.ToString();

            SlackColors slackColor = new SlackColors()
            {
                Red = "#FF0000",
                Yellow = "#FFFF00",
                Green = "#00FF00",
                LightBlue = "#00FFFF",
                DarkBlue = "#0000FF",
                Black = "#000000",
                White = "#FFFFFF",
                Gray = "#CCCCCC"
            };

            client.PostMessage(
                username: "Boaty McBoaterson",
                text: dateTime + "\nThis is a test message generated in C#!",
                color: slackColor.Red,
                pretext: "Testing!"
            );
        }
    }
}
