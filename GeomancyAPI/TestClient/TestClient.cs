using System.Text;
using System.Text.Json;

namespace GeomancyAPI.TestClient
{
    /// <summary>
    /// Simple test client to demonstrate API usage
    /// </summary>
    public class TestClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public TestClient(string baseUrl = "https://localhost:5001")
        {
            _baseUrl = baseUrl;
            _httpClient = new HttpClient();
        }

        /// <summary>
        /// Test generating a single figure
        /// </summary>
        public async Task TestGenerateFigure()
        {
            Console.WriteLine("Testing Generate Figure...");
            
            var request = new
            {
                fireElement = 1,
                airElement = 2,
                waterElement = 1,
                earthElement = 2
            };

            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await _httpClient.PostAsync($"{_baseUrl}/api/geomantic/figure", content);
                var responseContent = await response.Content.ReadAsStringAsync();
                
                Console.WriteLine($"Status: {response.StatusCode}");
                Console.WriteLine($"Response: {responseContent}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Test generating four figures
        /// </summary>
        public async Task TestGenerateFourFigures()
        {
            Console.WriteLine("\nTesting Generate Four Figures...");
            
            var request = new
            {
                figure1 = new { fireElement = 1, airElement = 2, waterElement = 1, earthElement = 2 },
                figure2 = new { fireElement = 2, airElement = 1, waterElement = 2, earthElement = 1 },
                figure3 = new { fireElement = 1, airElement = 1, waterElement = 2, earthElement = 2 },
                figure4 = new { fireElement = 2, airElement = 2, waterElement = 1, earthElement = 1 }
            };

            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await _httpClient.PostAsync($"{_baseUrl}/api/geomantic/figures", content);
                var responseContent = await response.Content.ReadAsStringAsync();
                
                Console.WriteLine($"Status: {response.StatusCode}");
                Console.WriteLine($"Response: {responseContent}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Test generating a house chart
        /// </summary>
        public async Task TestGenerateHouseChart()
        {
            Console.WriteLine("\nTesting Generate House Chart...");
            
            var request = new
            {
                house1 = new { fireElement = 1, airElement = 2, waterElement = 1, earthElement = 2 },
                house2 = new { fireElement = 2, airElement = 1, waterElement = 2, earthElement = 1 },
                house3 = new { fireElement = 1, airElement = 1, waterElement = 2, earthElement = 2 },
                house4 = new { fireElement = 2, airElement = 2, waterElement = 1, earthElement = 1 }
            };

            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await _httpClient.PostAsync($"{_baseUrl}/api/geomantic/chart", content);
                var responseContent = await response.Content.ReadAsStringAsync();
                
                Console.WriteLine($"Status: {response.StatusCode}");
                Console.WriteLine($"Response: {responseContent}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Test getting all figures
        /// </summary>
        public async Task TestGetAllFigures()
        {
            Console.WriteLine("\nTesting Get All Figures...");

            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/geomantic/figures/all");
                var responseContent = await response.Content.ReadAsStringAsync();
                
                Console.WriteLine($"Status: {response.StatusCode}");
                Console.WriteLine($"Response: {responseContent}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Test getting a figure by name
        /// </summary>
        public async Task TestGetFigureByName(string name = "Via")
        {
            Console.WriteLine($"\nTesting Get Figure by Name: {name}...");

            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/geomantic/figures/{name}");
                var responseContent = await response.Content.ReadAsStringAsync();
                
                Console.WriteLine($"Status: {response.StatusCode}");
                Console.WriteLine($"Response: {responseContent}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Test health check
        /// </summary>
        public async Task TestHealth()
        {
            Console.WriteLine("\nTesting Health Check...");

            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/geomantic/health");
                var responseContent = await response.Content.ReadAsStringAsync();
                
                Console.WriteLine($"Status: {response.StatusCode}");
                Console.WriteLine($"Response: {responseContent}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Run all tests
        /// </summary>
        public async Task RunAllTests()
        {
            Console.WriteLine("=== Geomancy API Test Client ===");
            Console.WriteLine($"Base URL: {_baseUrl}");
            Console.WriteLine();

            await TestHealth();
            await TestGenerateFigure();
            await TestGenerateFourFigures();
            await TestGenerateHouseChart();
            await TestGetAllFigures();
            await TestGetFigureByName("Via");
            await TestGetFigureByName("Populus");

            Console.WriteLine("\n=== All tests completed ===");
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
} 