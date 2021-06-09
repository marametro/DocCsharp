using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Nancy.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace APIBCA
{
	class Program
	{

		private static string mainUrl = "";
		private static string apiKey = "";
		private static string apiSecret = "";
		private static string clientId = "";
		private static string clientSecret = "";
		private static string corperateId = "";
		private static string accountNumber = "";
		private static string accessToken = null;
		private static string timeStamp = null;

		static void Main(string[] args)
		{
			DateTime now = DateTime.Now;
			timeStamp = now.ToString("yyyy-MM-dd'T0'HH:mm:00.000+07:00");

			DoBalance().Wait();
			Console.WriteLine();

			DoStatement().Wait();
			Console.WriteLine();

			DoTranferSameBank().Wait();
			Console.WriteLine();

			Console.ReadLine();
		}


		#region Tranfer Bank Same

		private static async Task<string> DoTranferSameBank()
		{
			accessToken = await GenerateAccessToken();
			Console.WriteLine("Token = " + accessToken);

			var jsonString = JObject.Parse(accessToken);
			var token = jsonString["access_token"].ToString();

			HttpClientHandler handler = new HttpClientHandler() { UseDefaultCredentials = false };
			HttpClient client = new HttpClient(handler);
			string response = null;

			string url = "/banking/corporates/transfers";
			string method = "POST";
			try
			{
				ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
				ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
				ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
				
				string body = "{\"CorporateID\":\"BCAAPI2016\",\"SourceAccountNumber\":\"0201245680\",\"TransactionID\":\"00000001\",\"TransactionDate\":\"2021-06-09\",\"ReferenceID\":\"12345/PO/2016\",\"CurrencyCode\":\"IDR\",\"Amount\":\"100000.00\",\"BeneficiaryAccountNumber\":\"0201245681\",\"Remark1\":\"Transfer Test\",\"Remark2\":\"Online Transfer\"}";
				body = body.Replace(" ", String.Empty);


				client.BaseAddress = new Uri(mainUrl + url);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
				client.DefaultRequestHeaders.Add("X-BCA-Signature", GetSignatureTranferSameBank(method, url, body));
				client.DefaultRequestHeaders.Add("X-BCA-Key", apiKey);
				client.DefaultRequestHeaders.Add("X-BCA-Timestamp", timeStamp);
				
				HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, Uri.EscapeUriString(client.BaseAddress.ToString()));
				request.Content = new StringContent(body,
													Encoding.UTF8,
													"application/json");

				using (HttpResponseMessage res = client.PostAsync(client.BaseAddress.ToString(), request.Content).Result)
				{
					if (res.IsSuccessStatusCode)
					{
						response = res.Content.ReadAsStringAsync().Result;
						Console.WriteLine("Transfer = " + response);
					}
				}

			}
			catch (Exception ex)
			{
				throw ex;
			}
			return response;
		}
		private static string GetSignatureTranferSameBank(string method, string relative_url, string request_body = "")
		{
			var jsonString = JObject.Parse(accessToken);
			var token = jsonString["access_token"].ToString();

			request_body = SHA256HexHashString(request_body.Trim()).ToLower();
			string stringToSign = method + ':' + relative_url + ':' + token + ':' + request_body + ':' + timeStamp;
			string signature = CalculateSignature(stringToSign, apiSecret).ToLower();
			return signature;
		}

		private static string ToHex(byte[] bytes, bool upperCase)
		{
			StringBuilder result = new StringBuilder(bytes.Length * 2);
			for (int i = 0; i < bytes.Length; i++)
				result.Append(bytes[i].ToString(upperCase ? "X2" : "x2"));
			return result.ToString();
		}

		private static string SHA256HexHashString(string StringIn)
		{
			string hashString;
			using (var sha256 = SHA256Managed.Create())
			{
				var hash = sha256.ComputeHash(Encoding.Default.GetBytes(StringIn));
				hashString = ToHex(hash, false);
			}

			return hashString;
		}

		#endregion

		#region Area Signature

		static private string CalculateSignature(String stringToSign, String key)
		{
			byte[] key_byte = Encoding.UTF8.GetBytes(key);
			byte[] stringToSign_byte = Encoding.UTF8.GetBytes(stringToSign);

			//Check Signature
			HMACSHA256 hmac = new HMACSHA256(key_byte);
			byte[] hashValue = hmac.ComputeHash(stringToSign_byte);

			return BitConverter.ToString(hashValue).Replace("-", "");
		}


		private static string GetSignature(string method, string relative_url, string request_body = "")
		{
			var jsonString = JObject.Parse(accessToken);
			var token = jsonString["access_token"].ToString();

			request_body = ComputeSha256Hash(request_body.ToLower());

			//StringToSign = HTTPMethod+":"+RelativeUrl+":"+AccessToken+":"+Lowercase(HexEncode(SHA-256(RequestBody)))+":"+Timestamp
			string stringToSign = method + ':' + relative_url + ':' + token + ':' + request_body + ':' + timeStamp;
			string signature = CalculateSignature(stringToSign, apiSecret).ToLower();
			return signature;
		}

		static string ComputeSha256Hash(string rawData)
		{
			// Create a SHA256   
			using (SHA256 sha256Hash = SHA256.Create())
			{
				// ComputeHash - returns byte array  
				byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

				// Convert byte array to a string   
				StringBuilder builder = new StringBuilder();
				for (int i = 0; i < bytes.Length; i++)
				{
					builder.Append(bytes[i].ToString("x2"));
				}
				return builder.ToString();
			}
		}


		#endregion

		#region Account Statement / Rekening Koran

		private static async Task<string> DoStatement()
		{
			string response = null;
			// Get the Access Token.
			accessToken = await GenerateAccessToken();
			Console.WriteLine("Token = " + accessToken);

			var jsonString = JObject.Parse(accessToken);
			var token = jsonString["access_token"].ToString();

			try
			{
				HttpClient client = GetStatements(token, "2016-09-01", "2016-09-01");

				HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, Uri.EscapeUriString(client.BaseAddress.ToString()));
				HttpResponseMessage tokenResponse = await client.GetAsync(Uri.EscapeUriString(client.BaseAddress.ToString()));
				if (tokenResponse.IsSuccessStatusCode)
				{
					response = tokenResponse.Content.ReadAsStringAsync().Result;
					Console.WriteLine("Statement = " + response);
				}

			}
			catch (HttpRequestException ex)
			{
				throw ex;
			}


			return response;
		}

		private static HttpClient GetStatements(string accessToken, string startDate, string endDate)
		{
			HttpClientHandler handler = new HttpClientHandler() { UseDefaultCredentials = false };
			HttpClient client = new HttpClient(handler);

			string url = "/banking/v3/corporates/" + corperateId + "/accounts/" + accountNumber + "/statements?EndDate=" + endDate + "&StartDate=" + startDate;
			string method = "GET";
			try
			{
				client.BaseAddress = new Uri(mainUrl + url);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				client.DefaultRequestHeaders.Add("X-BCA-Signature", GetSignature(method, url));
				client.DefaultRequestHeaders.Add("StartDate", startDate);
				client.DefaultRequestHeaders.Add("EndDate", endDate);
				client.DefaultRequestHeaders.Add("CorporateID", corperateId);
				client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
				client.DefaultRequestHeaders.Add("X-BCA-Key", apiKey);
				client.DefaultRequestHeaders.Add("AccountNumbers", accountNumber);
				client.DefaultRequestHeaders.Add("HTTPMethod", "GET");
				//2021-06-08T007:06:00.000+07:00
				//2021-06-08T07:06:00.000+07:00
				client.DefaultRequestHeaders.Add("X-BCA-Timestamp", timeStamp);

			}
			catch (Exception ex)
			{
				throw ex;
			}
			return client;
		}

		#endregion

		#region Balance

		private static async Task<string> DoBalance()
		{
			string response = null;
			// Get the Access Token.
			accessToken = await GenerateAccessToken();
			Console.WriteLine("Token = " + accessToken);

			var jsonString = JObject.Parse(accessToken);
			var token = jsonString["access_token"].ToString();

			try
			{
				HttpClient client = GetBalance(token, mainUrl);

				HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, Uri.EscapeUriString(client.BaseAddress.ToString()));
				HttpResponseMessage tokenResponse = await client.GetAsync(Uri.EscapeUriString(client.BaseAddress.ToString()));
				if (tokenResponse.IsSuccessStatusCode)
				{
					response = tokenResponse.Content.ReadAsStringAsync().Result;
					Console.WriteLine("Balance = " + response);
				}

			}
			catch (HttpRequestException ex)
			{
				throw ex;
			}


			return response;
		}

		private static HttpClient GetBalance(string accessToken, string endpointURL)
		{
			HttpClientHandler handler = new HttpClientHandler() { UseDefaultCredentials = false };
			HttpClient client = new HttpClient(handler);

			string url = "/banking/v3/corporates/" + corperateId + "/accounts/" + accountNumber;
			string method = "GET";
			try
			{
				client.BaseAddress = new Uri(endpointURL + url);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				client.DefaultRequestHeaders.Add("X-BCA-Signature", GetSignature(method, url));
				client.DefaultRequestHeaders.Add("CorporateID", corperateId);
				client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
				client.DefaultRequestHeaders.Add("X-BCA-Key", apiKey);
				client.DefaultRequestHeaders.Add("AccountNumbers", accountNumber);
				client.DefaultRequestHeaders.Add("HTTPMethod", "GET");
				//2021-06-08T007:06:00.000+07:00
				//2021-06-08T07:06:00.000+07:00
				client.DefaultRequestHeaders.Add("X-BCA-Timestamp", timeStamp);

			}
			catch (Exception ex)
			{
				throw ex;
			}
			return client;
		}

		#endregion

		#region Generate Token
		public static async Task<string> GenerateAccessToken()
		{
			string token = null;
			try
			{
				HttpClient client = SetHeadersToken();
				string body = "grant_type=client_credentials";
				string url = "/api/oauth/token";

				client.BaseAddress = new Uri(mainUrl + url);
				ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
				ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;

				ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

				HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, client.BaseAddress);
				request.Content = new StringContent(body,
													Encoding.UTF8,
													"application/x-www-form-urlencoded");

				List<KeyValuePair<string, string>> postData = new List<KeyValuePair<string, string>>();

				postData.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));

				request.Content = new FormUrlEncodedContent(postData);

				HttpResponseMessage tokenResponse = client.PostAsync(client.BaseAddress, new FormUrlEncodedContent(postData)).Result;
				token = tokenResponse.Content.ReadAsStringAsync().Result;
			}


			catch (HttpRequestException ex)
			{
				throw ex;
			}

			return token != null ? token : null;
		}

		private static HttpClient SetHeadersToken()
		{
			HttpClientHandler handler = new HttpClientHandler() { UseDefaultCredentials = false };
			HttpClient client = new HttpClient(handler);
			try
			{
				string url = "/api/oauth/token";
				client.BaseAddress = new Uri(mainUrl + url);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

				var encodedData = System.Convert.ToBase64String(
					System.Text.Encoding.GetEncoding("ISO-8859-1")
					  .GetBytes(clientId + ":" + clientSecret)
					);
				client.DefaultRequestHeaders.Authorization =
				   new AuthenticationHeaderValue("Basic", encodedData);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return client;
		}


		#endregion

	}


	public class transfer
	{
		public string CorporateID { get; set; }
		public string SourceAccountNumber { get; set; }
		public string TransactionID { get; set; }
		public string TransactionDate { get; set; }
		public string ReferenceID { get; set; }
		public string CurrencyCode { get; set; }
		public string Amount { get; set; }
		public string BeneficiaryAccountNumber { get; set; }
		public string Remark1 { get; set; }
		public string Remark2 { get; set; }

	}

}
