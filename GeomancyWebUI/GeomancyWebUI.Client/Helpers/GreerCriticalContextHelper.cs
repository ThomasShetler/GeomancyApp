using System;
using System.Collections.Generic;
using GeomancyWebUI.Client.Models;

namespace GeomancyWebUI.Client.Helpers
{
    public sealed class GreerCriticalContextFlag
    {
        public string Id { get; init; } = string.Empty;
        public string Title { get; init; } = string.Empty;
        public string Message { get; init; } = string.Empty;
        public bool IncludeDestroyChartNote { get; init; }
    }

    /// <summary>
    /// Evaluates Greer traditional chart cautions as Critical Context flags
    /// for the figure detail Immediate Context section.
    /// Logic mirrors GeomancyAPI.Services.GreerCriticalContextRules.
    /// </summary>
    public static class GreerCriticalContextHelper
    {
        public const string DestroyChartNote =
            "Traditionally the geomancer is supposed to stop the divination, destroy the chart, and wait at least two hours before trying again when this sign appears here.";

        public const string SoftGuidanceNote =
            "Greer notes it is not strictly necessary to stop; you may mention the traditional meaning and ask whether it has bearing on the reading.";

        public static IReadOnlyList<GreerCriticalContextFlag> Evaluate(
            GreerReferenceMode mode,
            SlotSelection? selection,
            HouseChartModel? chart)
        {
            if (mode == GreerReferenceMode.Off || selection?.Kind != SlotKind.House || chart?.Houses == null)
                return Array.Empty<GreerCriticalContextFlag>();

            if (!selection.HouseNumber.HasValue)
                return Array.Empty<GreerCriticalContextFlag>();

            var viewedHouse = selection.HouseNumber.Value;
            if (viewedHouse != 1 && viewedHouse != 11)
                return Array.Empty<GreerCriticalContextFlag>();

            var house1Name = NormalizeFigureName(GetHouseFigureName(chart, 1));
            var house11Name = NormalizeFigureName(GetHouseFigureName(chart, 11));
            var flags = new List<GreerCriticalContextFlag>();

            if (viewedHouse == 1)
            {
                if (house1Name == "rubeus")
                {
                    flags.Add(new GreerCriticalContextFlag
                    {
                        Id = "rubeus-first",
                        Title = "Rubeus in the First",
                        Message = "If Rubeus appears in the First house it suggests the querent is not being honest.",
                        IncludeDestroyChartNote = true
                    });
                }

                if (house1Name == "cauda draconis")
                {
                    flags.Add(new GreerCriticalContextFlag
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
                flags.Add(new GreerCriticalContextFlag
                {
                    Id = "populus-first-rubeus-eleventh",
                    Title = "Populus in the First with Rubeus in the Eleventh",
                    Message = "If Populus is in the First and Rubeus in the Eleventh, the querent has fabricated a fake question.",
                    IncludeDestroyChartNote = false
                });
            }

            return flags;
        }

        private static string? GetHouseFigureName(HouseChartModel chart, int houseNumber)
        {
            foreach (var house in chart.Houses)
            {
                if (house.HouseNumber == houseNumber)
                    return house.Figure?.Name;
            }

            return null;
        }

        private static string NormalizeFigureName(string? name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return string.Empty;

            var trimmed = name.Trim();
            var paren = trimmed.IndexOf('(', StringComparison.Ordinal);
            if (paren > 0)
                trimmed = trimmed[..paren].Trim();

            return trimmed.ToLowerInvariant();
        }
    }
}
