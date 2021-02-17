using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Google.Youtube.Subscriptions
{
    public enum YouTubeRating
    {
        Sub, Unsub, Like, Unlike, Dislike, Comment
    }

    public class Program
    {
        static void Main(string[] args)
        {
#if DEBUG
            string chanelIdORVideoId = "VeblPqTAT-s"; // "UCAuUUnT6oDeKwE6v1NGQxug";
            YouTubeRating action = YouTubeRating.Like; // YoutubeAction.Sub;
#else
            string chanelIdORVideoId = String.Empty; // "UCAuUUnT6oDeKwE6v1NGQxug";
            YouTubeRating action = YouTubeRating.Sub; // YoutubeAction.Sub;
#endif

            string comment = "Love and hearts, so sweet <3";

            for (int i = 0; i < args.Length; i++)
            {
                var check = args[i];

                if (check == "/id")
                    chanelIdORVideoId = args[++i];
                else if (check == "/sub")
                    action = YouTubeRating.Sub;
                else if (check == "/unsub")
                    action = YouTubeRating.Unsub;
                else if (check == "/like")
                    action = YouTubeRating.Like;
                else if (check == "/unlike")
                    action = YouTubeRating.Unlike;
                else if (check == "/dislike")
                    action = YouTubeRating.Dislike;
                else if (check == "/comment")
                {
                    action = YouTubeRating.Comment;
                    comment = args[++i];
                }
            }

            if (String.IsNullOrEmpty(chanelIdORVideoId))
            {
                Console.WriteLine($"No chanel valid.");
            }

            Console.WriteLine($"*** {chanelIdORVideoId} #{action.ToString()}.");
            Console.WriteLine("========================");

            try
            {
                new Program().Run(chanelIdORVideoId, action, comment).Wait();
            }
            catch (AggregateException ex)
            {
                foreach (var e in ex.InnerExceptions)
                {
                    Console.WriteLine("Error: " + e.Message);
                }
            }

#if DEBUG
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
#endif
        }

        private async Task Run(string chanelIdORVideoId, YouTubeRating action, string comment)
        {
            var baseDir = $"{AppDomain.CurrentDomain.BaseDirectory}\\ClientSecrets";
            var clientSecretFile = $"{baseDir}\\Config";

            var infos = File.ReadAllLines(clientSecretFile);
            var index = 0;
            
            var s = new ConsoleSpinner();            

            do
            {
                var stillValid = index + 5 <= infos.Length;
                if (!stillValid)
                    break;

                // This is will be skip at first and final
                if (index > 0)
                {
                    var csCount = 0;
                    while (csCount < 30)
                    {
                        Thread.Sleep(100); // simulate some work being done
                        s.UpdateProgress();

                        csCount++;
                    }
                }
                //

                var email = infos[index++];
                var clientID = infos[index++];
                var clientSecret = infos[index++];
                var apiKey = infos[index++];
                var chanelId = infos[index++];

                Console.WriteLine($"{email}");
                Console.WriteLine("-----");

                var credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    new ClientSecrets() { ClientId = clientID, ClientSecret = clientSecret },
                    // This OAuth 2.0 access scope allows for full read/write access to the
                    // authenticated user's account.
                    new[] { YouTubeService.Scope.Youtube, YouTubeService.Scope.YoutubeForceSsl, YouTubeService.Scope.Youtubepartner },
                    chanelId,
                    CancellationToken.None,
                    new FileDataStore(baseDir, true)
                );

                var youtubeService = new YouTubeService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApiKey = apiKey,
                });

                // Chanels List
                //var chanelsList = youtubeService.Channels.List("snippet,contentDetails,statistics");
                //chanelsList.Id = chanelId;
                //chanelsList.MaxResults = 1;

                //var chanels = await chanelsList.ExecuteAsync();
                //if (chanels != null)
                //    Console.WriteLine($"{chanels.Items[0].Snippet.Title}");
                //Console.WriteLine("-----");

                // SUB or UNSUB
                try
                {
                    switch (action)
                    {
                        case YouTubeRating.Sub:
                            var subBody = new Subscription();
                            subBody.Snippet = new SubscriptionSnippet();
                            subBody.Snippet.ChannelId = chanelId;

                            var resourceId = new ResourceId();
                            resourceId.ChannelId = chanelIdORVideoId; // "UCAuUUnT6oDeKwE6v1NGQxug";
                            subBody.Snippet.ResourceId = resourceId;

                            var subInsert = youtubeService.Subscriptions.Insert(subBody, "snippet");
                            var ressubInsert = await subInsert.ExecuteAsync();

                            break;

                        case YouTubeRating.Unsub:
                            var subList = youtubeService.Subscriptions.List("snippet,contentDetails");
                            subList.ChannelId = chanelId;

                            var subs = await subList.ExecuteAsync();
                            var deleteId = String.Empty;

                            foreach (var item in subs.Items)
                            {
                                var needDelete = item.Snippet.ResourceId.ChannelId == chanelIdORVideoId;
                                if (needDelete)
                                {
                                    deleteId = item.Id;
                                    break;
                                }
                            }

                            if (String.IsNullOrEmpty(deleteId))
                            {
                                throw new Exception($"{chanelIdORVideoId} not found.");
                            }

                            var subDelete = youtubeService.Subscriptions.Delete(deleteId);
                            var ressubDelete = await subDelete.ExecuteAsync();

                            break;

                        case YouTubeRating.Like:
                            var ratingLike = youtubeService.Videos.Rate(chanelIdORVideoId, VideosResource.RateRequest.RatingEnum.Like);
                            var resratingLike = await ratingLike.ExecuteAsync();

                            //var aaa = youtubeService.Videos.GetRating(chanelIdORVideoId);
                            //var bbb = await aaa.ExecuteAsync();
                            break;

                        case YouTubeRating.Unlike:
                            var ratingUnlike = youtubeService.Videos.Rate(chanelIdORVideoId, VideosResource.RateRequest.RatingEnum.None);
                            var resratingUnlike = await ratingUnlike.ExecuteAsync();
                            break;

                        case YouTubeRating.Dislike:
                            var ratingDislike = youtubeService.Videos.Rate(chanelIdORVideoId, VideosResource.RateRequest.RatingEnum.Dislike);
                            var resratingDislike = await ratingDislike.ExecuteAsync();
                            break;

                        case YouTubeRating.Comment:
                            var comThreadBody = new CommentThread();

                            comThreadBody.Snippet = new CommentThreadSnippet() { ChannelId = chanelId, VideoId = chanelIdORVideoId };
                            comThreadBody.Snippet.TopLevelComment = new Comment();
                            comThreadBody.Snippet.TopLevelComment.Snippet = new CommentSnippet() { TextOriginal = comment };

                            var commentThreadsInsert = youtubeService.CommentThreads.Insert(comThreadBody, "snippet");
                            var rescommentThreadsInsert = await commentThreadsInsert.ExecuteAsync();
                            break;

                        default:
                            break;
                    }

                    Console.WriteLine($"{chanelIdORVideoId} OK");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{chanelIdORVideoId} failed with error: {ex.Message}");
                }

                Console.WriteLine("-----");
                Console.WriteLine(" ");                
            } while (true);
        }
    }

    internal class ConsoleSpinner
    {
        private int _currentAnimationFrame;

        public ConsoleSpinner()
        {
            SpinnerAnimationFrames = new[] { '|', '/', '-', '\\'};
        }

        public char[] SpinnerAnimationFrames { get; set; }

        public void UpdateProgress()
        {
            // Store the current position of the cursor
            var originalX = Console.CursorLeft;
            var originalY = Console.CursorTop;

            // Write the next frame (character) in the spinner animation
            Console.Write(SpinnerAnimationFrames[_currentAnimationFrame]);

            // Keep looping around all the animation frames
            _currentAnimationFrame++;

            if (_currentAnimationFrame == SpinnerAnimationFrames.Length)
            {
                _currentAnimationFrame = 0;
            }

            // Restore cursor to original position
            Console.SetCursorPosition(originalX, originalY);
        }
    }
}
