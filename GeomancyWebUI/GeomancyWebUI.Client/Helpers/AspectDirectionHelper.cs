using System;

namespace GeomancyWebUI.Client.Helpers
{
  /// <summary>
  /// Shared dexter / sinister / opposition visualization for aspect UI.
  /// Dexter casts backward on the house wheel (←); sinister casts forward (→).
  /// </summary>
  public static class AspectDirectionHelper
  {
    public record DirectionVis(
      string Glyph,
      string CssClass,
      string ShortLabel,
      string Hint);

    public static DirectionVis Resolve(string? directionField, string? aspectType)
    {
      if (!string.IsNullOrEmpty(aspectType)
          && aspectType.Equals("Opposition", StringComparison.OrdinalIgnoreCase))
      {
        return new DirectionVis("↔", "dir-opposition", "Opposition", "Six houses apart — confrontation or denial.");
      }

      if (!string.IsNullOrEmpty(directionField))
      {
        if (directionField.Contains("Dexter", StringComparison.OrdinalIgnoreCase))
        {
          return new DirectionVis(
            "←",
            "dir-dexter",
            "Dexter",
            "Casts backward on the wheel — tends to act more forcefully.");
        }

        if (directionField.Contains("Sinister", StringComparison.OrdinalIgnoreCase))
        {
          return new DirectionVis(
            "→",
            "dir-sinister",
            "Sinister",
            "Casts forward on the wheel — tends to unfold more gradually.");
        }
      }

      return new DirectionVis("→", "dir-neutral", string.Empty, string.Empty);
    }

    public static string AspectTypeClass(string? aspectType) => aspectType?.Trim().ToLowerInvariant() switch
    {
      "sextile" => "aspect-sextile",
      "trine" => "aspect-trine",
      "square" => "aspect-square",
      "opposition" => "aspect-opposition",
      "conjunction" => "aspect-conjunction",
      _ => "aspect-generic"
    };

    public static string AspectTypeAbbrev(string? aspectType) => aspectType?.Trim().ToLowerInvariant() switch
    {
      "sextile" => "Sxt",
      "trine" => "Tri",
      "square" => "Sq",
      "opposition" => "Opp",
      "conjunction" => "Con",
      _ => aspectType ?? "Asp"
    };
  }
}
