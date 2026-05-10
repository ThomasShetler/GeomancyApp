using System;
using System.Collections.Generic;
using GeomancyWebUI.Client.Models;

namespace GeomancyWebUI.Client.Services
{
    /// <summary>
    /// Encodes / decodes the four Mothers as a short, human-readable seed string
    /// suitable for query-string sharing.
    ///
    /// Format: "M1.M2.M3.M4" where each Mn is four digits (Head/Neck/Body/Foot)
    /// each being '1' (single dot) or '2' (double dot). Example: "2122.1212.2222.1122".
    ///
    /// The seed is fully self-descriptive: a glance reveals all four mothers
    /// without decoding, and it round-trips byte-for-byte through any URL/query
    /// pipeline without escaping.
    /// </summary>
    public static class ChartSeedCodec
    {
        /// <summary>The query-string parameter name used in shareable URLs.</summary>
        public const string ParamName = "seed";

        /// <summary>
        /// Build a seed string from the four Mother figures. Lines are clamped to
        /// the valid {1, 2} range; out-of-range or zero lines (e.g. an
        /// uninitialised figure) are coerced to 2 so the seed is always a
        /// loadable chart rather than a parser-poison value.
        /// </summary>
        public static string Encode(FigureModel m1, FigureModel m2, FigureModel m3, FigureModel m4)
        {
            return string.Join(
                ".",
                EncodeOne(m1), EncodeOne(m2), EncodeOne(m3), EncodeOne(m4));
        }

        /// <summary>
        /// Try to parse a seed string back into four (HeadLine, NeckLine, BodyLine,
        /// FootLine) tuples. Returns false if the seed is missing, malformed, or
        /// doesn't contain exactly four 4-digit clusters.
        /// </summary>
        public static bool TryDecode(string? seed, out (int Head, int Neck, int Body, int Foot)[] mothers)
        {
            mothers = Array.Empty<(int, int, int, int)>();
            if (string.IsNullOrWhiteSpace(seed))
            {
                return false;
            }

            var clusters = seed.Trim().Split(new[] { '.', '-', '_', ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (clusters.Length != 4)
            {
                return false;
            }

            var parsed = new (int, int, int, int)[4];
            for (int i = 0; i < 4; i++)
            {
                if (!TryDecodeOne(clusters[i], out var lines))
                {
                    return false;
                }
                parsed[i] = lines;
            }

            mothers = parsed;
            return true;
        }

        /// <summary>
        /// Build a relative path with the seed appended as a query parameter,
        /// e.g. "/workspace?seed=2122.1212.2222.1122". Use this with
        /// NavigationManager.ToAbsoluteUri(...) to produce the link to share.
        /// </summary>
        public static string BuildSharePath(string basePath, FigureModel m1, FigureModel m2, FigureModel m3, FigureModel m4)
        {
            var seed = Encode(m1, m2, m3, m4);
            return $"{basePath}?{ParamName}={seed}";
        }

        private static string EncodeOne(FigureModel m)
        {
            return new string(new[]
            {
                ToDigit(m.HeadLine),
                ToDigit(m.NeckLine),
                ToDigit(m.BodyLine),
                ToDigit(m.FootLine)
            });
        }

        private static char ToDigit(int line)
        {
            // Geomancy lines are {1, 2}. Anything else is an unset / invalid
            // value; coerce to 2 so the seed is always playable.
            return line == 1 ? '1' : '2';
        }

        private static bool TryDecodeOne(string cluster, out (int Head, int Neck, int Body, int Foot) lines)
        {
            lines = default;
            if (cluster == null || cluster.Length != 4)
            {
                return false;
            }
            int[] parsed = new int[4];
            for (int i = 0; i < 4; i++)
            {
                var c = cluster[i];
                if (c != '1' && c != '2')
                {
                    return false;
                }
                parsed[i] = c - '0';
            }
            lines = (parsed[0], parsed[1], parsed[2], parsed[3]);
            return true;
        }
    }
}
