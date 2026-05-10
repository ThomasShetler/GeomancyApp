using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GeomancyAPI.Models;
using Newtonsoft.Json;

namespace GeomancyAPI.Services
{
    /// <summary>
    /// Loads and caches the static reference data for houses (1-12) and court placements
    /// (Right Witness, Left Witness, Judge, Reconciler) from the JSON files copied alongside
    /// the API binaries at <c>HouseAndCourtDirectory/</c>.
    /// </summary>
    public static class HouseDirectoryLoader
    {
        private static readonly object _gate = new object();
        private static List<HouseDirectoryEntryResponse> _houses;
        private static List<CourtDirectoryEntryResponse> _courts;

        private const string HousesFileName = "HouseData.json";
        private const string CourtsFileName = "CourtData.json";
        private const string DirectoryFolderName = "HouseAndCourtDirectory";

        public static IReadOnlyList<HouseDirectoryEntryResponse> GetHouses()
        {
            EnsureHousesLoaded();
            return _houses;
        }

        public static HouseDirectoryEntryResponse GetHouse(int id)
        {
            EnsureHousesLoaded();
            return _houses.FirstOrDefault(h => h.Id == id);
        }

        public static IReadOnlyList<CourtDirectoryEntryResponse> GetCourts()
        {
            EnsureCourtsLoaded();
            return _courts;
        }

        /// <summary>
        /// Looks up a court placement by a normalized URL key:
        /// <c>right-witness</c>, <c>left-witness</c>, <c>judge</c>, <c>reconciler</c>.
        /// Aliases <c>sentence</c> and <c>fallout</c> also resolve to the Reconciler.
        /// </summary>
        public static CourtDirectoryEntryResponse GetCourt(string placementKey)
        {
            EnsureCourtsLoaded();
            if (string.IsNullOrWhiteSpace(placementKey)) return null;

            var key = NormalizeKey(placementKey);
            // Map common aliases
            if (key == "sentence" || key == "fallout" || key == "superjudge" || key == "vindex")
            {
                key = "reconciler";
            }

            return _courts.FirstOrDefault(c => NormalizeKey(c.Placement) == key);
        }

        private static string NormalizeKey(string value)
        {
            if (string.IsNullOrEmpty(value)) return string.Empty;
            return value.Trim().ToLowerInvariant()
                .Replace("the ", string.Empty)
                .Replace(' ', '-')
                .Replace("_", "-");
        }

        private static void EnsureHousesLoaded()
        {
            if (_houses != null) return;
            lock (_gate)
            {
                if (_houses != null) return;
                var path = ResolveDataFile(HousesFileName);
                var json = File.ReadAllText(path);
                var file = JsonConvert.DeserializeObject<HouseDirectoryFile>(json);
                _houses = file?.HouseData?.Houses ?? new List<HouseDirectoryEntryResponse>();
            }
        }

        private static void EnsureCourtsLoaded()
        {
            if (_courts != null) return;
            lock (_gate)
            {
                if (_courts != null) return;
                var path = ResolveDataFile(CourtsFileName);
                var json = File.ReadAllText(path);
                var file = JsonConvert.DeserializeObject<CourtDirectoryFile>(json);
                _courts = file?.CourtData?.Placements ?? new List<CourtDirectoryEntryResponse>();
            }
        }

        /// <summary>
        /// Resolves the absolute path to a JSON file. Looks first next to the running binary
        /// (production / Aspire), then walks up to the repo root for development.
        /// </summary>
        private static string ResolveDataFile(string fileName)
        {
            var baseDir = AppContext.BaseDirectory;

            // 1. Linked content next to binary: <baseDir>/HouseAndCourtDirectory/<file>
            var beside = Path.Combine(baseDir, DirectoryFolderName, fileName);
            if (File.Exists(beside)) return beside;

            // 2. Walk up looking for the source folder (handy for IDE/debug runs)
            var dir = new DirectoryInfo(baseDir);
            while (dir != null)
            {
                var candidate = Path.Combine(dir.FullName, DirectoryFolderName, fileName);
                if (File.Exists(candidate)) return candidate;
                dir = dir.Parent;
            }

            throw new FileNotFoundException(
                $"Could not locate {fileName}. Expected '{beside}' (copied via csproj <Content>) " +
                "or a parent folder named '" + DirectoryFolderName + "'.");
        }
    }
}
