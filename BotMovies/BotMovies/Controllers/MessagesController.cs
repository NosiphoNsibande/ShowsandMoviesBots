using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Connector.Utilities;

using Newtonsoft.Json;
using System.Collections.Generic;

namespace BotMovies
{
    public class ids
    {
        public long trakt { get; set; }
        public string slung { get; set; }
        public string imdb { get; set; }
        public long tmdb { get; set; }
    }
    public class movie
    {
        public string title { get; set; }
        public long year { get; set; }
        public ids ids { get; set; }
        public override string ToString()
        {
            return $"{ title}-{year}";

        }
    }
   
    public class showids
    {
        public long trakt { get; set; }
        public string slung { get; set; }
        public long tvdb { get; set; }
        public string imdb { get; set; }
        public long tmdb { get; set; }
        public long tvrage { get; set; }
    }

   
    public class show
    {
        public string title { get; set; }
        public long year { get; set; }
        public showids ids { get; set; }
        public override string ToString()
        {
            return $"{ title}-{year}";

        }
    }
    public class showTrending
    {
        public long watchers { get; set; }
        public show show { get; set; }

    }

    public class showplayed
    {
        public long watcher_count { get; set; }
        public long play_count { get; set; }
        public long collected_count { get; set; }
        public long collector_count { get; set; }
        public show show { get; set; }
    }




    public class played
    {
        public long watcher_count { get; set; }
        public long play_count { get; set; }
        public long collected_count { get; set; }
        public movie movie { get; set; }
    }
    public class Anticipated
    {
        public long list_count { get; set; }
        public movie movie { get; set; }
        
       
    }
    public class Trending
    {
        public long watchers { get; set; }
        public movie movie { get; set; }

    }




    //[BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<Message> Post([FromBody]Message message)
        {
            if (message.Type == "Message")
            {
                // calculate something for us to return
                //int length = (message.Text ?? string.Empty).Length;
                // return our reply to the user
                //return message.CreateReplyMessage($"You sent {length} characters");
                string title = ""; string title2 = ""; string title3 = ""; long year = 0; long year3 = 0; long year2 = 0; int count = 1; long watchers = 0;
                string[] counta = { "a.", "b.", "c.", "d.", "e.", "f.", "g.", "h.", "i.", "j.", "k.", "l.", "m.", "n.", "o.", "p.", "q.", "r.", "s.", "t.", "u.", "y.", "z." };
               // string[] countaa = {"i)", "ii)", "iii)", "iiv)", "iiiv)"};
                if (message.Text.ToLower().Contains("hi")|| message.Text.ToLower().Contains("hellow")|| message.Text.ToLower().Contains("hello"))
                {
                    return message.CreateReplyMessage("Hi,My Name is botsMovieShow, but you can call me happyBots,how can i help you??");
                }
                using (var client = new HttpClient { BaseAddress = new Uri("https://api.trakt.tv/") })
                {

                    client.DefaultRequestHeaders.Add("trakt-api-key", "80cf41a8390988e19f93394bb11894f27969267aad51b4978ffaaffb621bcce9");
                    //Most popular Movies
                    if (message.Text.ToLower().Contains("popular"))
                    {
                        using (var response = client.GetAsync("movies/popular").Result)
                        {
                            var responseString = response.Content.ReadAsStringAsync().Result;
                            var res = JsonConvert.DeserializeObject<List<movie>>(responseString);
                            for (int i = 0; i < res.Count; i++)
                            {
                                if (message.Text.ToLower().Contains("popular") || res[i].title.ToString().ToLower().Contains(message.Text))
                                {
                                    
                                    year = res[i].year;

                                    title += $"{Environment.NewLine }{ Environment.NewLine }>" + counta[i] + res[i].title.ToString() + "\t\t\t\t\t" + ":Year:>" + "\t\t\t\t\t" + year;

                                }


                            }
                        }
                        using (var response = client.GetAsync("shows/popular").Result)
                        {
                            var responseString = response.Content.ReadAsStringAsync().Result;
                            var res = JsonConvert.DeserializeObject<List<show>>(responseString);
                            for (int i = 1; i < res.Count; i++)
                            {
                                if (message.Text.ToLower().Contains("popular") || res[i].title.ToString().ToLower().Contains(message.Text))
                                {
                                    count = i;
                                    year2 = res[i].year;

                                    title2 += $"{Environment.NewLine }{ Environment.NewLine }>" + count + "." + res[i].title.ToString() + "\t\t\t\t\t" + ":Year:>" + "\t\t\t\t\t" + year2;

                                }
                            }
                        }
                        return message.CreateReplyMessage($"available popular movies are:" + title + $"{Environment.NewLine }{ Environment.NewLine }" + "Most popular Shows are:" + title2);

                    }//Most Trending Movies
                    if (message.Text.ToLower().Contains("trending"))
                    {
                        using (var response = client.GetAsync("movies/trending").Result)
                        {
                            var responseString = response.Content.ReadAsStringAsync().Result;
                            var res = JsonConvert.DeserializeObject<List<Trending>>(responseString);
                            for (int i = 0; i < res.Count; i++)
                            {
                                if (message.Text.ToLower().Contains("trending") || res[i].movie.title.ToString().ToLower().Contains(message.Text))
                                {
                                    //count = i;
                                    year = res[i].movie.year;
                                    watchers = res[i].watchers;
                                    title += $"{Environment.NewLine }{ Environment.NewLine }>" + counta[i] + res[i].movie.title.ToString() + "\t\t\t\t\t" + ":Year:" + "\t\t\t\t\t" + year + "\t\t\t\t\t" + "watchers:>" + watchers;

                                }


                            }

                        }
                        using (var response = client.GetAsync("shows/trending").Result)
                        {
                            var responseString = response.Content.ReadAsStringAsync().Result;
                            var res = JsonConvert.DeserializeObject<List<showTrending>>(responseString);
                            for (int i = 1; i < res.Count; i++)
                            {
                                if (message.Text.ToLower().Contains("trending") || res[i].show.title.ToString().ToLower().Contains(message.Text))
                                {
                                    count = i;
                                    year2 = res[i].show.year;
                                    watchers = res[i].watchers;
                                    title2 += $"{Environment.NewLine }{ Environment.NewLine }>" + count + "." + res[i].show.title.ToString() + "\t\t\t\t\t" + ":Year:" + "\t\t\t\t\t" + year + "\t\t\t\t\t" + "watchers:>" + watchers;

                                }


                            }
                        }
                        return message.CreateReplyMessage($"Available Tranding movies are:" + title + $"{Environment.NewLine }{ Environment.NewLine }" + "Most Trending Shows are:" + title2);
                    }
                    //Most Played,watched,collected movies..
                    if (message.Text.ToLower().Contains("played") || (message.Text.ToLower().Contains("watched")) || message.Text.ToLower().Contains("collected"))
                    {
                        using (var response = client.GetAsync("movies/played").Result)
                        {
                            var responseString = response.Content.ReadAsStringAsync().Result;
                            var res = JsonConvert.DeserializeObject<List<played>>(responseString);
                            for (int i = 1; i < res.Count; i++)
                            {
                                if (message.Text.ToLower().Contains("played") || (message.Text.ToLower().Contains("watched")) || message.Text.ToLower().Contains("collected") || res[i].movie.title.ToString().ToLower().Contains(message.Text))
                                {
                                    count = i;
                                    year2 = res[i].movie.year;
                                    title2 += $"{Environment.NewLine }{ Environment.NewLine }>" + count + "." + res[i].movie.title.ToString() + "\t\t\t\t\t" + ":Year:>" + "\t\t\t\t\t" + year2 +
                                    $"{Environment.NewLine }{ Environment.NewLine }>" + "Number of people watch it:" + res[i].collected_count;
                                }


                            }
                        }
                        using (var response = client.GetAsync("shows/played").Result)
                        {
                            var responseString = response.Content.ReadAsStringAsync().Result;
                            var res = JsonConvert.DeserializeObject<List<showplayed>>(responseString);
                            for (int i = 0; i < res.Count; i++)
                            {
                                if (message.Text.ToLower().Contains("played") || (message.Text.ToLower().Contains("watched")) || message.Text.ToLower().Contains("collected") || res[i].show.title.ToString().ToLower().Contains(message.Text))
                                {
                                   // count += i;
                                    year3 = res[i].show.year;
                                    title3 += $"{Environment.NewLine }{ Environment.NewLine }>" + counta[i] + "." + res[i].show.title.ToString() + "\t\t\t\t\t" + ":Year:>" + "\t\t\t\t\t" + year2 +
                                    $"{Environment.NewLine }{ Environment.NewLine }>" + "Number of people watch it:>" + res[i].collected_count;
                                }


                            }

                            return message.CreateReplyMessage($"Movies you are looking for are:" + $"{Environment.NewLine }{ Environment.NewLine }"+ title2 + $"{Environment.NewLine }{ Environment.NewLine }" + "and Shows are:" + title3);
                           
                        }
                    }
                        //Most anticipated movies...
                        if (message.Text.ToLower().Contains("anticipated"))
                        {
                            using (var response = client.GetAsync("movies/anticipated").Result)
                            {
                                var responseString = response.Content.ReadAsStringAsync().Result;
                                var res = JsonConvert.DeserializeObject<List<Anticipated>>(responseString);
                                for (int i = 0; i < res.Count; i++)
                                {
                                    if (message.Text.ToLower().Contains("anticipated ") || res[i].movie.title.ToString().ToLower().Contains(message.Text))
                                    {
                                        count += i;
                                        year3 = res[i].movie.year;

                                        title3 += $"{Environment.NewLine }{ Environment.NewLine }>" + count + "." + res[i].movie.title.ToString() + "\t\t\t\t\t" + ":Year:" + "\t\t\t\t\t" + year3;

                                    }
                                }
                            }
                            return message.CreateReplyMessage($"Most Anticipated  Movies are:>" + title3);

                        }
               return message.CreateReplyMessage($"OOPS!!,i dont's understand what you saying but hey im a good girl and i can help you with the following infor:" + $"{Environment.NewLine}{Environment.NewLine}>"+"1).list of Most Popular Movies and Shows"+$"{Environment.NewLine}{Environment.NewLine}>"+ "2).list of Most Played/Collected/Watched Movies"+ $"{Environment.NewLine}{Environment.NewLine}>" + "3).list of most Anticipated movies and shows"+
              $"{Environment.NewLine}{Environment.NewLine}>"+  "4).list of most Trending movies and shows");
                      
                }
            }
            else
            {
                return HandleSystemMessage(message);
            }
        }

        private Message HandleSystemMessage(Message message)
        {
            if (message.Type == "Ping")
            {
                Message reply = message.CreateReplyMessage();
                reply.Type = "Ping";
                return reply;
            }
            else if (message.Type == "DeleteUserData")
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == "BotAddedToConversation")
            {
            }
            else if (message.Type == "BotRemovedFromConversation")
            {
            }
            else if (message.Type == "UserAddedToConversation")
            {
            }
            else if (message.Type == "UserRemovedFromConversation")
            {
            }
            else if (message.Type == "EndOfConversation")
            {
            }

            return null;
        }
    }
}