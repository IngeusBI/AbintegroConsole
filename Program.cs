using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {

        public static async Task<string> GetAuthorizeToken()
        {
            try
            {

                // Initialization...
                string responseObj = string.Empty;

                // Posting.  
                using (var client = new HttpClient())
                {
                    // Setting Base address.  
                    client.BaseAddress = new Uri("https://api.abintegro.com/token");

                    // Setting content type.  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    
                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();
                    List<KeyValuePair<string, string>> allIputParams = new List<KeyValuePair<string, string>>
                        {
                        new KeyValuePair<string, string>( "grant_type", "password" ),
                        new KeyValuePair<string, string>( "username", "ab_iworks" ),
                        new KeyValuePair<string, string> ( "Password", "^t$qOWi7GA#4cC#fYVfh1k*" )
                    };

                    // Convert Request Params to Key Value Pair.  

                    // URL Request parameters.  
                    HttpContent requestParams = new FormUrlEncodedContent(allIputParams);
                    
                    // HTTP POST  
                    response = await client.PostAsync("TOKEN", requestParams).ConfigureAwait(false);
                    //System.Diagnostics.Debug.Write(response.Content.ReadAsStringAsync());
                    responseObj = response.Content.ReadAsStringAsync().Result;


                    // Verification  
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("ppp");
                        System.Diagnostics.Debug.Write(response.Content.ReadAsStringAsync());
                        // Reading Response.  

                    }

                    return responseObj;


                }

                
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e.Message);
                 
                 
                return " ";
            }
            
             
        }
        public static async Task<string> GetInfo(string authorizeToken)
        {
            // Initialization.  
            string responseObj = string.Empty;

            // HTTP GET.  
            using (var client = new HttpClient())
            {
                // Initialization  
                string authorization = authorizeToken;

                // Setting Authorization.  
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authorization);

                // Setting Base address.  
                client.BaseAddress = new Uri("https://api.abintegro.com/api/reporting/getactivityusage");

                // Setting content type.  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Initialization.  
                HttpResponseMessage response = new HttpResponseMessage();

                // HTTP GET  
                response = await client.GetAsync("api/WebApi").ConfigureAwait(false);

                // Verification  
                if (response.IsSuccessStatusCode)
                {
                    // Reading Response.  
                     
                }
            }

            return responseObj;
        }
        static void Main(string[] args)
        {
            // Generate Authorize Access Token to authenticate REST Web API.  
            string oAuthInfo = Program.GetAuthorizeToken().Result;
            // Call REST Web API method with authorize access token.  
            string responseObj = Program.GetInfo(oAuthInfo).Result;
        }

       
        }
}
