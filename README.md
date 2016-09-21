# CSharp-SlackIntegration
C# Slack Integration

Based on code found https://gist.github.com/jogleasonjr/7121367 and http://www.c-sharpcorner.com/uploadfile/bfb43a/integration-with-slack-using-c-sharp/

The code above did not have ways of including color or pretext in the Slack notifications. 
This project adds those feature sets.

Requirements:
Newtonsoft.json package from NuGet. The package is already installed in the project here.

The Slack.cs includes the code used to create a JSON payload that will be uploaded to the Slack API. 

The Program.cs is a example console program that will send the JSON payload to Slack. A Slack Incoming Webhook is required. A class with some included colors was also input into this file. These are Web Hex Color values for reference. I'm hoping to find a way to move the color class into the Slack.cs code at some point. 
