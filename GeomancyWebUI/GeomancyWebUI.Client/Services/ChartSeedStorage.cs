using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using GeomancyWebUI.Client.Models;
using Microsoft.JSInterop;

namespace GeomancyWebUI.Client.Services
{
    public sealed class ChartSeedSnapshot
    {
        [JsonPropertyName("seed")]
        public string Seed { get; set; } = string.Empty;

        [JsonPropertyName("savedAt")]
        public string SavedAt { get; set; } = string.Empty;

        [JsonPropertyName("source")]
        public string Source { get; set; } = string.Empty;
    }

    /// <summary>
    /// Persists the four-Mother chart seed in device localStorage so a reload
    /// can offer to restore the last reading.
    /// </summary>
    public static class ChartSeedStorage
    {
        public const string StorageKey = "geofancy.lastChartSeed.v1";

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        public static string Encode(FigureModel m1, FigureModel m2, FigureModel m3, FigureModel m4) =>
            ChartSeedCodec.Encode(m1, m2, m3, m4);

        public static async Task SaveAsync(
            IJSRuntime js,
            FigureModel m1,
            FigureModel m2,
            FigureModel m3,
            FigureModel m4,
            string source)
        {
            var seed = Encode(m1, m2, m3, m4);
            if (string.IsNullOrWhiteSpace(seed))
            {
                return;
            }

            var snapshot = new ChartSeedSnapshot
            {
                Seed = seed,
                SavedAt = DateTime.UtcNow.ToString("o"),
                Source = source ?? string.Empty
            };

            var json = JsonSerializer.Serialize(snapshot, JsonOptions);
            await js.InvokeVoidAsync("geofancyWriteStorage", StorageKey, json);
        }

        public static async Task<ChartSeedSnapshot?> ReadAsync(IJSRuntime js)
        {
            try
            {
                var json = await js.InvokeAsync<string?>("geofancyReadStorage", StorageKey);
                if (string.IsNullOrWhiteSpace(json))
                {
                    return null;
                }

                var snapshot = JsonSerializer.Deserialize<ChartSeedSnapshot>(json, JsonOptions);
                if (snapshot == null || string.IsNullOrWhiteSpace(snapshot.Seed))
                {
                    return null;
                }

                if (!ChartSeedCodec.TryDecode(snapshot.Seed, out _))
                {
                    return null;
                }

                return snapshot;
            }
            catch
            {
                return null;
            }
        }

        public static async Task ClearAsync(IJSRuntime js)
        {
            try
            {
                await js.InvokeVoidAsync("geofancyRemoveStorage", StorageKey);
            }
            catch
            {
                // Private mode or blocked storage — ignore.
            }
        }

        public static bool SeedsMatch(string? a, string? b) =>
            string.Equals(a?.Trim(), b?.Trim(), StringComparison.Ordinal);
    }
}
