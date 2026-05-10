using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GeomancyAPI.Models;
using Newtonsoft.Json;

namespace GeomancyAPI.Services
{
    /// <summary>
    /// Loads and caches the static reference data for the four Way Of Points elements
    /// (Fire/Air/Water/Earth) and the four path-type classifications (Strong / StrongPassive
    /// / WeakerPassive / Passive) from the JSON files copied alongside the API binaries
    /// at <c>WayOfPointsDirectory/</c>.
    /// </summary>
    public static class WayOfPointsDirectoryLoader
    {
        private static readonly object _gate = new object();
        private static List<WayOfPointsElementEntryResponse> _elements;
        private static List<WayOfPointsPathTypeEntryResponse> _pathTypes;

        private const string ElementsFileName = "ElementData.json";
        private const string PathTypesFileName = "PathTypeData.json";
        private const string DirectoryFolderName = "WayOfPointsDirectory";

        public static IReadOnlyList<WayOfPointsElementEntryResponse> GetElements()
        {
            EnsureElementsLoaded();
            return _elements;
        }

        /// <summary>
        /// Looks up an element entry by its numeric id (1=Fire, 2=Air, 3=Water, 4=Earth)
        /// or by element name (case-insensitive: "fire", "air", "water", "earth").
        /// </summary>
        public static WayOfPointsElementEntryResponse GetElement(string key)
        {
            EnsureElementsLoaded();
            if (string.IsNullOrWhiteSpace(key)) return null;

            if (int.TryParse(key, out var id))
            {
                return _elements.FirstOrDefault(e => e.Id == id);
            }

            var normalized = key.Trim();
            return _elements.FirstOrDefault(e =>
                string.Equals(e.Element, normalized, StringComparison.OrdinalIgnoreCase));
        }

        public static IReadOnlyList<WayOfPointsPathTypeEntryResponse> GetPathTypes()
        {
            EnsurePathTypesLoaded();
            return _pathTypes;
        }

        /// <summary>
        /// Looks up a path-type entry by its enum id ("Strong", "Passive", "StrongPassive",
        /// "WeakerPassive"). Case-insensitive.
        /// </summary>
        public static WayOfPointsPathTypeEntryResponse GetPathType(string id)
        {
            EnsurePathTypesLoaded();
            if (string.IsNullOrWhiteSpace(id)) return null;
            return _pathTypes.FirstOrDefault(p =>
                string.Equals(p.Id, id, StringComparison.OrdinalIgnoreCase));
        }

        private static void EnsureElementsLoaded()
        {
            if (_elements != null) return;
            lock (_gate)
            {
                if (_elements != null) return;
                var path = ResolveDataFile(ElementsFileName);
                var json = File.ReadAllText(path);
                var file = JsonConvert.DeserializeObject<WayOfPointsElementFile>(json);
                _elements = file?.ElementData?.Elements ?? new List<WayOfPointsElementEntryResponse>();
            }
        }

        private static void EnsurePathTypesLoaded()
        {
            if (_pathTypes != null) return;
            lock (_gate)
            {
                if (_pathTypes != null) return;
                var path = ResolveDataFile(PathTypesFileName);
                var json = File.ReadAllText(path);
                var file = JsonConvert.DeserializeObject<WayOfPointsPathTypeFile>(json);
                _pathTypes = file?.PathTypeData?.PathTypes ?? new List<WayOfPointsPathTypeEntryResponse>();
            }
        }

        /// <summary>
        /// Resolves the absolute path to a JSON file. Looks first next to the running binary
        /// (production / Aspire), then walks up to the repo root for development.
        /// </summary>
        private static string ResolveDataFile(string fileName)
        {
            var baseDir = AppContext.BaseDirectory;

            // 1. Linked content next to binary: <baseDir>/WayOfPointsDirectory/<file>
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
