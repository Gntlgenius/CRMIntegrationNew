using Microsoft.Identity.Client;
using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;

namespace CRMIntegration.ConnectionMaster
{
    public class CrmHttpClient
    {
        // Configuration for authentication
        private static string clientId = ConfigurationManager.AppSettings["ClientId"];
        private static string clientSecret = ConfigurationManager.AppSettings["ClientSecret"];
        private static string authority = ConfigurationManager.AppSettings["AuthorityUri"];
        private static string resource = ConfigurationManager.AppSettings["BaseUri"];
        private static string[] scopes = new string[] { $"{resource}/.default" };  // API permission scope for Dynamics CRM

        public static HttpClient GetClient()
        {
            // Initialize MSAL Confidential Client Application
            var confidentialClientApp = ConfidentialClientApplicationBuilder
                .Create(clientId)
                .WithClientSecret(clientSecret)
                .WithAuthority(new Uri(authority))
                .Build();

            // Synchronously acquire token for Dynamics CRM using client credentials
            AuthenticationResult authResult;
            try
            {
                authResult = confidentialClientApp.AcquireTokenForClient(scopes).ExecuteAsync().Result;
            }
            catch (MsalUiRequiredException ex)
            {
                throw new InvalidOperationException("Failed to acquire access token for Dynamics CRM: " + ex.Message, ex);
            }
            catch (MsalServiceException ex)
            {
                throw new InvalidOperationException("Failed to acquire access token for Dynamics CRM: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred: " + ex.Message, ex);
            }


            // Initialize the HTTP client
            HttpClient httpClient = new HttpClient
            {
                BaseAddress = new Uri(resource),
                Timeout = TimeSpan.FromMinutes(2)  // Set timeout duration
            };

            // Set HTTP request headers
            httpClient.DefaultRequestHeaders.Add("OData-MaxVersion", "4.0");
            httpClient.DefaultRequestHeaders.Add("OData-Version", "4.0");
            httpClient.DefaultRequestHeaders.Add("Prefer", "odata.include-annotations=OData.Community.Display.V1.FormattedValue");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authResult.AccessToken);

            return httpClient;
        }
    }
}
