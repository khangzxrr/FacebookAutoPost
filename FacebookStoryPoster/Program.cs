using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Net;

namespace FacebookStoryPoster
{
    class Program
    {
        static string GenerateIdempotanceToken()
        {
            string template = "h1d8d493-09gf-4fa2-df95-21b55c916919";
            string result = "";

            Random random = new Random();

            foreach (char c in template)
            {
                if (Char.IsDigit(c))
                {
                    result += random.Next(0, 9);
                }
                else
                if (Char.IsLetter(c))
                {
                    result += (char)random.Next('a', 'z');
                }
                else
                {
                    result += c;
                }
            }

            return result;
        }
        static void Main(string[] args)
        {

            Console.WriteLine("Hello Facebooker!!");
            var client = new RestClient("https://www.facebook.com/api/graphql/");
            client.Timeout = -1;


            for (int i = 1; i <= 1; i++)
            {
                var token = GenerateIdempotanceToken();
                var message = "created by VNK's new bot: " + i + "😎";
                var request = new RestRequest(Method.POST);

                var variables = "{\"input\":{\"composer_entry_point\":\"inline_composer\",\"composer_source_surface\":\"timeline\",\"idempotence_token\":\"" + token + "_FEED\",\"source\":\"WWW\",\"attachments\":[],\"audience\":{\"privacy\":{\"allow\":[],\"base_state\":\"SELF\",\"deny\":[],\"tag_expansion_state\":\"UNSPECIFIED\"}},\"message\":{\"ranges\":[],\"text\":\"" + message + "\"},\"with_tags_ids\":[],\"inline_activities\":[],\"explicit_place_id\":\"0\",\"logging\":{\"composer_session_id\":\"" + token + "\"},\"tracking\":[null],\"actor_id\":\"100009985927020\",\"client_mutation_id\":\"1\"},\"displayCommentsFeedbackContext\":null,\"displayCommentsContextEnableComment\":null,\"displayCommentsContextIsAdPreview\":null,\"displayCommentsContextIsAggregatedShare\":null,\"displayCommentsContextIsStorySet\":null,\"feedLocation\":\"TIMELINE\",\"feedbackSource\":0,\"focusCommentID\":null,\"gridMediaWidth\":230,\"scale\":1,\"privacySelectorRenderLocation\":\"COMET_STREAM\",\"renderLocation\":\"timeline\",\"useDefaultActor\":false,\"isFeed\":false,\"isFundraiser\":false,\"isFunFactPost\":false,\"isGroup\":false,\"isTimeline\":true,\"isLivingRoom\":false,\"isSocialLearning\":false,\"isPageNewsFeed\":false,\"isProfileReviews\":false,\"prefetchRecentMediaPhotos\":true,\"UFI2CommentsProvider_commentsKey\":\"ProfileCometTimelineRoute\"}";

                //Console.WriteLine(variables);
                Console.WriteLine("Requesting....");
                try
                {
                    request.AddHeader("authority", "www.facebook.com");
                    request.AddHeader("content-type", "application/x-www-form-urlencoded");
                    request.AddHeader("cookie", "sb=LAtsYOB7B9GFGjLdfnhmKDqh; datr=LAtsYEhAA5ZT5sTUT5SNPuQ5; c_user=100009985927020; _fbp=fb.1.1617888692279.1853432961; wd=1920x880; presence=C%7B%22t3%22%3A%5B%7B%22i%22%3A%22u.100006814843282%22%7D%2C%7B%22i%22%3A%22p.358931334698267%22%7D%5D%2C%22utc3%22%3A1617896646821%2C%22v%22%3A1%7D; spin=r.1003591387_b.trunk_t.1617897586_s.1_v.2_; xs=7%3AIdXeVg8jT-FVbQ%3A2%3A1617693499%3A17496%3A6232%3A%3AAcU-QqemgsHCz6WjlIrFg3fUyRoxCA_Sglw74aFANjc; fr=1qCubSjQCqGYrgZuK.AWW3ZWTRpzxrc-DN2QeYo7gm78I.Bgbyh0.mB.AAA.0.0.Bgbyh0.AWWcLbOeL6E; c_user=100009985927020; fr=1IyvaAVZwjN1j2o0C.AWUcw-eltdMuOUXec3VoOaYDCOk.Bgbyr0.mB.AAA.0.0.Bgbyr0.AWVQbTh8LbY; xs=7%3AIdXeVg8jT-FVbQ%3A2%3A1617693499%3A17496%3A6232%3A%3AAcVn1y--OVegJVenj5B5MtXfMZuHGz0_9pB_libL59E");
                    request.AddParameter("fb_dtsg", "AQG689-_cL4T:AQFz77R_4mrq");
                    request.AddParameter("variables", variables);
                    request.AddParameter("doc_id", "3891530390963263");
                    IRestResponse response = client.Execute(request);
                    //Console.WriteLine(response.Content);

                    var responseJson = JObject.Parse(response.Content);
                    Console.WriteLine($"Created post: {responseJson["data"]["story_create"]["story"]["url"]}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("error with message: " + ex.Message);
                }
            }



        }
    }
}
