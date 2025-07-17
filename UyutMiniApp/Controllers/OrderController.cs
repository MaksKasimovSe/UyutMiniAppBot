using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using UyutMiniApp.Service.DTOs.Orders;
using UyutMiniApp.Service.Interfaces;

namespace UyutMiniApp.Controllers
{
    
    [ApiController, Route("[controller]")]
    public class OrderController(IOrderService orderService) : ControllerBase
    {
        private static readonly string clientId = "HN3GGCMDdTgGUfl0kFCo";
        private static readonly string clientSecret = "ftZjkkRNMR";
        private static readonly string chainId = "Ulc3aFNIUm1vb2x";
        private static readonly string requestUrl = "https://dev-pub.apigw.ntruss.com/nppfs/payments/v2/reserve";

        [HttpPost, Authorize]
        public async Task<IActionResult> CreateAsync(CreateOrderDto createOrderDto) =>
            Ok(await orderService.CreateAsync(createOrderDto));

        [HttpGet("{id}"), Authorize]
        public async Task<IActionResult> GetAsync(Guid id) =>
            Ok(await orderService.GetAsync(id));

        [HttpPost("test-pay")]
        public async Task<IActionResult> KakaoPayTest()
        {
            using (HttpClient client = new HttpClient())
            {
                // Set headers
                client.DefaultRequestHeaders.Add("X-Naver-Client-Id", clientId);
                client.DefaultRequestHeaders.Add("X-Naver-Client-Secret", clientSecret);
                client.DefaultRequestHeaders.Add("X-NaverPay-Chain-Id", chainId);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Create reservation payload (example data)
                var jsonBody = @"{
                        ""productName"": ""Test Product"",
                        ""totalPayAmount"": 10000,
                        ""taxScopeAmount"": 9091,
                        ""taxExScopeAmount"": 0,
                        ""taxAmount"": 909,
                        ""returnUrl"": ""https://youtube.com"",
                        ""cancelUrl"": ""https://youtube.com"",
                        ""merchantPayKey"": ""your-unique-order-id"",
                        ""productCount"": 1,
                        ""useCfmYmdt"": ""20250716000000"",
                        ""productItems"": [
                            {
                                ""productName"": ""Test Item"",
                                ""uniqueNo"": ""SKU1234"",
                                ""quantity"": 1,
                                ""unitPrice"": 10000,
                                ""taxType"": ""TAX"",
                                ""infoUrl"": ""https://youtube.com""
                            }
                        ]
                    }";

                // Send POST request
                var response = await client.PostAsync(
                    requestUrl,
                    new StringContent(jsonBody, Encoding.UTF8, "application/json")
                );

                string result = await response.Content.ReadAsStringAsync();
                return Ok("Response:\n" + result);
            }
        }
    }
}
