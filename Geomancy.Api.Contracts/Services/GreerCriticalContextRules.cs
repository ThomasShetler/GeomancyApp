using System;
using System.Collections.Generic;

namespace GeomancyAPI.Services
{
    public sealed class GreerCriticalContextFlagDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public bool IncludeDestroyChartNote { get; set; }
    }

    /// <summary>
    /// Pure evaluation of Greer chart-caution flags from figure names and viewed house.
    /// </summary>
    public static class GreerCriticalContextRules
    {
        public const string DestroyChartNote =
            "Traditionally the geomancer is supposed to stop the divination, destroy the chart, and wait at least two hours before trying again when this sign appears here.";

        public const string SoftGuidanceNote =
            "Greer notes it is not strictly necessary to stop; you may mention the traditional meaning and ask whether it has bearing on the reading.";

        /// <summary>
        /// Evaluate flags when a Greer reference mode is active and the user is viewing a house slot.
        /// </summary>
        public static IReadOnlyList<GreerCriticalContextFlagDto> Evaluate(
            bool greerModeActive,
            int? viewedHouseNumber,
            string house1FigureName,
            string house11FigureName)
        {
            if (!greerModeActive || !viewedHouseNumber.HasValue)
                return Array.Empty<GreerCriticalContextFlagDto>();

            var viewedHouse = viewedHouseNumber.Value;
            if (viewedHouse != 1 && viewedHouse != 11)
                return Array.Empty<GreerCriticalContextFlagDto>();

            var house1Name = NormalizeFigureName(house1FigureName);
            var house11Name = NormalizeFigureName(house11FigureName);
            var flags = new List<GreerCriticalContextFlagDto>();

            if (viewedHouse == 1)
            {
                if (house1Name == "rubeus")
                {
                    flags.Add(new GreerCriticalContextFlagDto
                    {
                        Id = "rubeus-first",
                        Title = "Rubeus in the First",
                        Message = "If Rubeus appears in the First house it suggests the querent is not being honest.",
                        IncludeDestroyChartNote = true
                    });
                }

                if (house1Name == "cauda draconis")
                {
                    flags.Add(new GreerCriticalContextFlagDto
                    {
                        Id = "cauda-first",
                        Title = "Cauda Draconis in the First",
                        Message = "If Cauda Draconis is in the First house, it indicates the querent has already decided what to do and won't listen.",
                        IncludeDestroyChartNote = true
                    });
                }
            }

            var fakeQuestion = house1Name == "populus" && house11Name == "rubeus";
            if (fakeQuestion && (viewedHouse == 1 || viewedHouse == 11))
            {
                flags.Add(new GreerCriticalContextFlagDto
                {
                    Id = "populus-first-rubeus-eleventh",
                    Title = "Populus in the First with Rubeus in the Eleventh",
                    Message = "If Populus is in the First and Rubeus in the Eleventh, the querent has fabricated a fake question.",
                    IncludeDestroyChartNote = false
                });
            }

            return flags;
        }

        public static string NormalizeFigureName(string figureName)
        {
            if (string.IsNullOrWhiteSpace(figureName))
                return string.Empty;

            var trimmed = figureName.Trim();
            var paren = trimmed.IndexOf('(');
            if (paren > 0)
                trimmed = trimmed.Substring(0, paren).Trim();

            return trimmed.ToLowerInvariant();
        }
    }
}
