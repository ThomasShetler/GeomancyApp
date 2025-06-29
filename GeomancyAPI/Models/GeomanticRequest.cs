using System.ComponentModel.DataAnnotations;

namespace GeomancyAPI.Models
{
    /// <summary>
    /// Request model for generating a single geomantic figure
    /// </summary>
    public class GenerateFigureRequest
    {
        /// <summary>
        /// Fire element state (1 for active, 2 for passive)
        /// </summary>
        [Required]
        [Range(1, 2)]
        public int FireElement { get; set; }

        /// <summary>
        /// Air element state (1 for active, 2 for passive)
        /// </summary>
        [Required]
        [Range(1, 2)]
        public int AirElement { get; set; }

        /// <summary>
        /// Water element state (1 for active, 2 for passive)
        /// </summary>
        [Required]
        [Range(1, 2)]
        public int WaterElement { get; set; }

        /// <summary>
        /// Earth element state (1 for active, 2 for passive)
        /// </summary>
        [Required]
        [Range(1, 2)]
        public int EarthElement { get; set; }
    }

    /// <summary>
    /// Request model for generating four geomantic figures
    /// </summary>
    public class GenerateFourFiguresRequest
    {
        /// <summary>
        /// First figure elemental pattern
        /// </summary>
        [Required]
        public GenerateFigureRequest Figure1 { get; set; } = new();

        /// <summary>
        /// Second figure elemental pattern
        /// </summary>
        [Required]
        public GenerateFigureRequest Figure2 { get; set; } = new();

        /// <summary>
        /// Third figure elemental pattern
        /// </summary>
        [Required]
        public GenerateFigureRequest Figure3 { get; set; } = new();

        /// <summary>
        /// Fourth figure elemental pattern
        /// </summary>
        [Required]
        public GenerateFigureRequest Figure4 { get; set; } = new();
    }

    /// <summary>
    /// Request model for generating a complete house chart
    /// </summary>
    public class GenerateHouseChartRequest
    {
        /// <summary>
        /// First house elemental pattern
        /// </summary>
        [Required]
        public GenerateFigureRequest House1 { get; set; } = new();

        /// <summary>
        /// Second house elemental pattern
        /// </summary>
        [Required]
        public GenerateFigureRequest House2 { get; set; } = new();

        /// <summary>
        /// Third house elemental pattern
        /// </summary>
        [Required]
        public GenerateFigureRequest House3 { get; set; } = new();

        /// <summary>
        /// Fourth house elemental pattern
        /// </summary>
        [Required]
        public GenerateFigureRequest House4 { get; set; } = new();
    }
} 