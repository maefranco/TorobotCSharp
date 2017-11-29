using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi;
using TweetSharp;
using System.Timers;
using System.Net;
using System.IO;

namespace FirstYoutube
{
    class Program
    {
        private static string customer_key = "4oRhSMKUB3d5Sd364CjrIQSOd";
        private static string customer_key_secret = "sULa9DU1dD7Npym4DDQKLuILsmfPXQvfMjG8Qti5sN4pNzFtgr";
        private static string access_token = "926157826342371329-z1HRWsNes6zSLvzMyUmvy3nB4Kg2qOd";
        private static string access_token_secret = "2041mVPf7a4qU3aNKkWJmOYd1MaSr6EP7mu81TBLMPUbF";

        private static TwitterService service = new TwitterService(customer_key, customer_key_secret, access_token, access_token_secret);
        private static int currentImageID = 0;
        private static List<string> imageList = new List<string> { $"C:/Users/carlos/Downloads/earthquakewired.jpg" };

        static void Main(string[] args)
        {
            Console.WriteLine($"<{DateTime.Now}> - Bot Started");
            //SendTweet("Hello, World!" + ( DateTime.Now.ToString("yyyyMMddHHmmss") )  );//add text to avoid "Duplicate" error
            //SendPictureTweet("Test Picture Earthquake", currentImageID);
            GetStream();
            Console.Read();

        }

        private static void SendTweet(string _status)
        {
            //May need to change text in Tweet to avoid "Duplicate" errors
            service.SendTweet(new SendTweetOptions { Status = (_status) }, (tweet, response) =>
             {
                 if (response.StatusCode == HttpStatusCode.OK)
                 {
                     Console.ForegroundColor = ConsoleColor.Green;
                     Console.WriteLine($"<{DateTime.Now}> - Tweet Sent!");
                     Console.ResetColor();
                 }
                 else
                 {
                     Console.ForegroundColor = ConsoleColor.Red;
                     Console.WriteLine($"<ERROR>" + response.Error.Message);
                     Console.WriteLine(response.Error.RawSource);
                     Console.ResetColor();
                 }
             }
            );
        }
        public static void SendPictureTweet(string _status, int imageID)
        {
            using (var stream = new FileStream(imageList[imageID], FileMode.Open))
            {
                service.SendTweetWithMedia(new SendTweetWithMediaOptions
                {
                    Status = _status,
                    Images = new Dictionary<string, System.IO.Stream> { { imageList[imageID], stream } }
                    //
                });

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"<{DateTime.Now}> - Tweet Sent!");
                Console.ResetColor();

                if ((currentImageID + 1) >= imageList.Count)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("<BOT> - End of Image Array");
                    Console.ResetColor();
                    currentImageID = 0;
                }
                else
                {
                    currentImageID++;//increment
                }
            }
        }
        public static void GetStream()
        {
            //
            Auth.SetUserCredentials(customer_key, customer_key_secret, access_token, access_token_secret);
            var stream = Tweetinvi.Stream.CreateFilteredStream();

            //stream.AddTrack("earthquake");
            //stream.AddTrack("Calif");
            //stream.ContainsTrack("califearthquake");

            // OR tweets containing both 'tweetinvi' AND 'rocks'.
            //stream.AddTrack("tweetinvi rocks");

            //Training: What we are looking for

            //OR Search
            stream.AddTrack("earthquake");// california");
            //stream.AddTrack("earthquake california");// california");

            //And Search - tweets containing both 'earthquake' AND 'southern' AND 'cal'.
            //stream.AddTrack("earthquake cal");// california");

            //test
            //stream.AddTrack("csc 500 rocks");// california");
            //stream.AddTrack("ysl");// california");
            //stream.AddTrack("gucci");
            //stream.AddTrack("lv");
            //stream.AddTrack("new york times");

            ////reply?
            //var tweetIdtoReplyTo = arguments.Tweet.CreatedBy.Id; //1;//long twetid
            //var tweetToReplyTo = Tweet.GetTweet(tweetIdtoReplyTo);            
            //// We must add @screenName of the author of the tweet we want to reply to
            //var textToPublish = string.Format("@{0} {1}", tweetToReplyTo.CreatedBy.ScreenName, text);
            //var tweet = Tweet.PublishTweetInReplyTo(textToPublish, tweetIdtoReplyTo);
            //Console.WriteLine("Publish success? {0}", tweet != null);
            ////end replyto code

            stream.AddTweetLanguageFilter(Tweetinvi.Models.LanguageFilter.English);

            Console.Write("I am listening to twitter stream:");
            //Console.Write("Searching:  " +);

            stream.MatchingTweetReceived += (sender, arguments) =>
            {
                Console.WriteLine("\n\n##NEWTWEET##");
                Console.WriteLine(arguments.Tweet.Text);
                //Console.WriteLine(arguments.Tweet.CreatedBy.Id);
                //Console.WriteLine(arguments.Tweet.CreatedBy.ScreenName);

                //reply?
                var text = "Earthquake Verification: *INCONCLUSIVE*" + (DateTime.Now.ToString("yyyyMMddHHmmss"));
                //var tweetIdtoReplyTo = arguments.Tweet.CreatedBy.Id; //
                var tweetToReplyTo = Tweet.GetTweet(arguments.Tweet.CreatedBy.Id);
                //var two = Tweet.GetTweet

                if (arguments.Tweet.CreatedBy.ScreenName != null)
                {
                    // We must add @screenName of the author of the tweet we want to reply to
                    //Console.WriteLine(found.FirstName);

                    /* Code to reply to the User */
                    /*  ~~ UNCOMMENT / COMMENT HERE ~~ */

                    //var textToPublish = string.Format("@{0} {1}", (arguments.Tweet.CreatedBy.ScreenName), text);
                    //var tweet = Tweet.PublishTweetInReplyTo(textToPublish, (arguments.Tweet.CreatedBy.Id));
                    //Console.WriteLine("Publish success? {0}", tweet != null);

                    /* ~~~~ ~~~~ ~~~~ ~~~~ */
                }
                else
                {
                    Console.WriteLine("{0} not found.", arguments.Tweet.CreatedBy.Id);//just print ID
                }


                //end replyto code

            };
            //stream.StartStreamMatchingAllConditions(); //user for AND
            stream.StartStreamMatchingAnyCondition();// user for OR
        }
    }
}
