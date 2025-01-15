using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using TaskFlow.Client.Models;

namespace TaskFlow.Client.Services
{
    public class TaskItemService
    {
        private readonly HttpClient _httpClient;

        public TaskItemService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<TaskItem>> GetTaskItemsAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<JsonElement>("api/TaskItem");

                if (response.TryGetProperty("$values", out var values))
                {
                    return JsonSerializer.Deserialize<List<TaskItem>>(values.GetRawText(), new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }) ?? new List<TaskItem>();
                }

                return new List<TaskItem>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetTaskItemsAsync: {ex.Message}");
                throw;
            }
        }
    }
}