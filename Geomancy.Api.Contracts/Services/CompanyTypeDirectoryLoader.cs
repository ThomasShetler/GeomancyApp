using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GeomancyAPI.Models;
using Newtonsoft.Json;

namespace GeomancyAPI.Services
{
    /// <summary>
    /// Loads reader-centric Company of Houses glossary entries from
    /// databank/PerfectionDirectory/CompanyTypeData.json.
    /// </summary>
    public static class CompanyTypeDirectoryLoader
    {
        private static readonly object Gate = new object();
        private static CompanyTypeDirectoryResponse _directory;

        private const string DatabankRoot = "databank";
        private const string DirectoryFolderName = "PerfectionDirectory";
        private const string FileName = "CompanyTypeData.json";

        public static CompanyTypeDirectoryResponse GetDirectory()
        {
            EnsureLoaded();
            return _directory ?? new CompanyTypeDirectoryResponse();
        }

        public static IReadOnlyList<CompanyTypeEntryResponse> GetCompanyTypes()
        {
            EnsureLoaded();
            return _directory?.CompanyTypes ?? new List<CompanyTypeEntryResponse>();
        }

        public static CompanyTypeOverviewResponse GetOverview()
        {
            EnsureLoaded();
            return _directory?.Overview;
        }

        public static CompanyTypeEntryResponse GetCompanyType(string id)
        {
            EnsureLoaded();
            if (string.IsNullOrWhiteSpace(id) || _directory?.CompanyTypes == null)
                return null;

            var normalized = NormalizeId(id);
            return _directory.CompanyTypes.FirstOrDefault(e =>
                string.Equals(NormalizeId(e.Id), normalized, StringComparison.OrdinalIgnoreCase)
                || string.Equals(e.ShortLabel, id.Trim(), StringComparison.OrdinalIgnoreCase)
                || string.Equals(e.Name, id.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        private static string NormalizeId(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return string.Empty;
            return id.Trim()
                .Replace("-", string.Empty)
                .Replace(" ", string.Empty);
        }

        private static void EnsureLoaded()
        {
            if (_directory != null) return;
            lock (Gate)
            {
                if (_directory != null) return;
                try
                {
                    var path = ResolveDataFile(FileName);
                    if (path == null)
                    {
                        _directory = EmptyDirectory();
                        return;
                    }

                    var json = File.ReadAllText(path);
                    var file = JsonConvert.DeserializeObject<CompanyTypeFile>(json);
                    _directory = new CompanyTypeDirectoryResponse
                    {
                        Overview = file?.CompanyTypeData?.Overview,
                        CompanyTypes = file?.CompanyTypeData?.CompanyTypes ?? new List<CompanyTypeEntryResponse>()
                    };
                }
                catch
                {
                    // Fail soft: wiki/workspace can render without glossary copy.
                    _directory = EmptyDirectory();
                }
            }
        }

        private static CompanyTypeDirectoryResponse EmptyDirectory() =>
            new CompanyTypeDirectoryResponse
            {
                Overview = null,
                CompanyTypes = new List<CompanyTypeEntryResponse>()
            };

        private static string ResolveDataFile(string fileName)
        {
            var baseDir = AppContext.BaseDirectory;
            var beside = Path.Combine(baseDir, DatabankRoot, DirectoryFolderName, fileName);
            if (File.Exists(beside)) return beside;

            // Cap parent walks — never scan all the way to filesystem root on PaaS.
            var dir = new DirectoryInfo(baseDir);
            for (var depth = 0; dir != null && depth < 8; depth++, dir = dir.Parent)
            {
                var candidate = Path.Combine(dir.FullName, DatabankRoot, DirectoryFolderName, fileName);
                if (File.Exists(candidate)) return candidate;
            }

            return null;
        }
    }
}
