namespace Example
{
    using System;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using SendGrid;
    using SendGrid.Helpers.Mail;
    using System.Collections.Generic;

    internal class Example
    {
        private static void Main()
        {
            Execute().Wait();
        }

        static async Task Execute()
        {
            // Retrieve the API key from the environment variables. See the project README for more info about setting this up.
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            var fromEmail = "phong.ha@sage.com";
            var fromEmailAliasName = "Phong Ha";
            var toEmail = "phong89.ha@outlook.com";
            var toEmailAlias = "Phong89";
            
            if (apiKey == null)
            {
                Console.WriteLine("Error: SendGrid API Key environment variable: SENDGRID_API_KEY not found. Please configure System Variables.");
                Console.WriteLine("\n\nPress <Enter> to continue.");
                Console.ReadLine();
                Environment.Exit(0);
            }
            
            var client = new SendGridClient(apiKey);

            // Send a Single Email using the Mail Helper
            var from = new EmailAddress(fromEmail, fromEmailAliasName);
            var subject = "Hello World from the SendGrid CSharp Library Helper!";
            var to = new EmailAddress(toEmail, toEmailAlias);
            var plainTextContent = "Hello, Email from the helper [SendSingleEmailAsync]!";
            var htmlContent = "<strong>Hello, Email from the helper! [SendSingleEmailAsync]</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            var response = await client.SendEmailAsync(msg);
            Console.WriteLine(msg.Serialize());
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Headers);
            Console.WriteLine("\n\nPress <Enter> to continue.");
            Console.ReadLine();

            // Send a Single Email using the Mail Helper with convenience methods and initialized SendGridMessage object
            msg = new SendGridMessage()
            {
                From = new EmailAddress(fromEmail, fromEmailAliasName),
                Subject = "Hello World from the SendGrid CSharp Library Helper!",
                PlainTextContent = "Hello, Email from the helper [SendSingleEmailAsync]!",
                HtmlContent = "<strong>Hello, Email from the helper! [SendSingleEmailAsync]</strong>"
            };
            msg.AddTo(new EmailAddress(toEmail, toEmailAlias));

            response = await client.SendEmailAsync(msg);
            Console.WriteLine(msg.Serialize());
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Headers);
            Console.WriteLine("\n\nPress <Enter> to continue.");
            Console.ReadLine();

            // Send a Single Email using the Mail Helper, entirely with convenience methods
            msg = new SendGridMessage();
            msg.SetFrom(new EmailAddress(fromEmailAliasName, "Phong Ha"));
            msg.SetSubject("Hello World from the SendGrid CSharp Library Helper!");
            msg.AddContent(MimeType.Text, "Hello, Email from the helper [SendSingleEmailAsync]!");
            msg.AddContent(MimeType.Html, "<strong>Hello, Email from the helper! [SendSingleEmailAsync]</strong>");
            msg.AddTo(new EmailAddress(toEmail, toEmailAlias));

            response = await client.SendEmailAsync(msg);
            Console.WriteLine(msg.Serialize());
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Headers);
            Console.WriteLine("\n\nPress <Enter> to continue.");
            Console.ReadLine();

            
        }
    }
}
