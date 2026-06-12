using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GeomancyAPI.Models;
using Newtonsoft.Json;

namespace GeomancyAPI.Services
{
    /// <summary>
    /// Loads and caches licensed Greer house reference data from
    /// databank/HouseAndCourtDirectory/GreersHouseData.json.
    /// </summary>
    public static class GreerHouseDirectoryLoader
    {
        private static readonly object Gate = new object();
        private static GreerHouseDirectoryResponse _directory;
        private static List<GreerHouseEntryResponse> _houses;

        private const string DatabankRoot = "databank";
        private const string DirectoryFolderName = "HouseAndCourtDirectory";
        private const string HousesFileName = "GreersHouseData.json";

        public static GreerHouseDirectoryResponse GetDirectory()
        {
            EnsureLoaded();
            return _directory;
        }

        public static IReadOnlyList<GreerHouseEntryResponse> GetHouses()
        {
            EnsureLoaded();
            return _houses;
        }

        public static GreerHouseEntryResponse GetHouse(int id)
        {
            EnsureLoaded();
            return _houses.FirstOrDefault(h => h.Id == id);
        }

        private static void EnsureLoaded()
        {
            if (_houses != null)
                return;

            lock (Gate)
            {
                if (_houses != null)
                    return;

                var path = ResolveDataFile();
                var json = File.ReadAllText(path);
                var file = JsonConvert.DeserializeObject<GreerHouseDirectoryFile>(json);
                var root = file?.GreerHouseData;

                if (root?.Houses == null || root.Houses.Count == 0)
                    throw new InvalidDataException("Greer house directory file contains no houses.");

                _houses = root.Houses;
                _directory = new GreerHouseDirectoryResponse
                {
                    ChartCautions = root.ChartCautions,
                    Houses = root.Houses,
                    License = root.License
                };
            }
        }

        private static string ResolveDataFile()
        {
            var baseDir = AppContext.BaseDirectory;

            var beside = Path.Combine(baseDir, DatabankRoot, DirectoryFolderName, HousesFileName);
            if (File.Exists(beside))
                return beside;

            var dir = new DirectoryInfo(baseDir);
            while (dir != null)
            {
                var candidate = Path.Combine(dir.FullName, DatabankRoot, DirectoryFolderName, HousesFileName);
                if (File.Exists(candidate))
                    return candidate;
                dir = dir.Parent;
            }

            throw new FileNotFoundException(
                $"Could not locate {HousesFileName}. Expected '{beside}' (copied via csproj <Content>) " +
                "or a parent folder named '" + DatabankRoot + "/" + DirectoryFolderName + "'.");
        }

        private sealed class GreerHouseDirectoryFile
        {
            [JsonProperty("GreerHouseData")]
            public GreerHouseDirectoryRoot GreerHouseData { get; set; }
        }

        private sealed class GreerHouseDirectoryRoot
        {
            [JsonProperty("chart_cautions")]
            public string ChartCautions { get; set; }

            [JsonProperty("license")]
            public GreerLicenseResponse License { get; set; }

            [JsonProperty("houses")]
            public List<GreerHouseEntryResponse> Houses { get; set; }
        }
    }
}
