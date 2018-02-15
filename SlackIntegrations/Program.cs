using System;
using System.Collections.Generic;

namespace SlackIntegrations
{
    class Program
    {
        static void Main(string[] args)
        {
            string urlWithAccessToken = "https://hooks.slack.com/services/{webhook information from Slack API}";

            SlackClient client = new SlackClient(urlWithAccessToken);

            string dateTime = DateTime.Now.ToString();

            // Posts a simple message with no extra formatting sent to Slack through the webhook
            client.PostMessage(
                username: "Boaty McBoaterson",
                text: dateTime + "- This is a test message generated in C#!"
            );

            // Create a list to place individual Slack attachment objects into.
            // This will be used to send to the PostMessage method later.
            List<Attachment> slackAttachments = new List<Attachment>();

            // Create an attachment object that will go into the List. 
            // Be sure to look at the Attachment class. There are more options for things to insert there.
            // This is a super simple example.
            Attachment attachment = new Attachment()
            {
                text = dateTime + " - Your attachment's text goes here!",
                color = SlackColors.Green,
                pretext = "Your attachment's pretext (that goes above the attachment itself) goes here!"
            };

            // Adds the Attachment object into the List
            slackAttachments.Add(attachment);

            // Serializes and sends out the attachments to Slack through the webhook.
            client.PostMessage(
                username: "Boaty McBoaterson",
                attachments: slackAttachments
            );
        }
    }
}
