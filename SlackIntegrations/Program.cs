using System;

namespace SlackIntegrations
{
    class Program
    {
        static void Main(string[] args)
        {
            string urlWithAccessToken = "https://hooks.slack.com/services/{webhook information from Slack API}";

            SlackClient client = new SlackClient(urlWithAccessToken);

            string dateTime = DateTime.Now.ToString();

            client.PostMessage(
                username: "Boaty McBoaterson",
                text: dateTime + "\nThis is a test message generated in C#!",
                color: SlackColors.Set_SlackColor(SlackColors.Colors.Red),
                pretext: "Testing!"
            );
        }
    }
}
