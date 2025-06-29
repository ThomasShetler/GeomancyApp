# Geomancy API

A RESTful API for generating geomantic figures and house charts, built with ASP.NET Core 6 and Swagger UI.

## Features

- Generate individual geomantic figures from elemental patterns
- Generate four geomantic figures at once
- Generate complete house charts from the first four houses
- Retrieve all available geomantic figures
- Get specific figures by name
- Full Swagger UI documentation
- JSON responses with comprehensive figure data

## Getting Started

### Prerequisites

- .NET 6.0 SDK or later
- Visual Studio 2022 or VS Code

### Installation

1. Clone the repository
2. Navigate to the GeomancyAPI directory
3. Run the following commands:

```bash
dotnet restore
dotnet build
dotnet run
```

The API will be available at:
- **HTTP**: http://localhost:5000
- **HTTPS**: https://localhost:5001
- **Swagger UI**: http://localhost:5000 (root URL)

## API Endpoints

### 1. Generate Single Figure

**POST** `/api/geomantic/figure`

Generates a single geomantic figure from an elemental pattern.

**Request Body:**
```json
{
  "fireElement": 1,
  "airElement": 2,
  "waterElement": 1,
  "earthElement": 2
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "name": "Via",
    "otherNames": "The Way",
    "quality": "Good",
    "keyword": "Journey",
    "imagery": "A road or path",
    "strongHouse": "9th House",
    "weakHouse": "3rd House",
    "planet": "Mercury",
    "sign": "Gemini",
    "innerElement": "Air",
    "outerElement": "Fire",
    "fireElement": "Active",
    "airElement": "Passive",
    "waterElement": "Active",
    "earthElement": "Passive",
    "anatomy": "Lungs, arms, hands",
    "bodyType": "Thin, wiry",
    "characterType": "Intellectual, communicative",
    "colors": "Yellow, orange",
    "commentary": "Via represents journeys and communication...",
    "divinatoryMeaning": "Travel, communication, learning...",
    "elementalPattern": "1-2-1-2",
    "houseNumber": null
  },
  "error": null,
  "timestamp": "2024-01-15T10:30:00Z"
}
```

### 2. Generate Four Figures

**POST** `/api/geomantic/figures`

Generates four geomantic figures at once.

**Request Body:**
```json
{
  "figure1": {
    "fireElement": 1,
    "airElement": 2,
    "waterElement": 1,
    "earthElement": 2
  },
  "figure2": {
    "fireElement": 2,
    "airElement": 1,
    "waterElement": 2,
    "earthElement": 1
  },
  "figure3": {
    "fireElement": 1,
    "airElement": 1,
    "waterElement": 2,
    "earthElement": 2
  },
  "figure4": {
    "fireElement": 2,
    "airElement": 2,
    "waterElement": 1,
    "earthElement": 1
  }
}
```

### 3. Generate House Chart

**POST** `/api/geomantic/chart`

Generates a complete house chart from the first four houses, automatically calculating houses 5-12, witnesses, judge, and sentence.

**Request Body:**
```json
{
  "house1": {
    "fireElement": 1,
    "airElement": 2,
    "waterElement": 1,
    "earthElement": 2
  },
  "house2": {
    "fireElement": 2,
    "airElement": 1,
    "waterElement": 2,
    "earthElement": 1
  },
  "house3": {
    "fireElement": 1,
    "airElement": 1,
    "waterElement": 2,
    "earthElement": 2
  },
  "house4": {
    "fireElement": 2,
    "airElement": 2,
    "waterElement": 1,
    "earthElement": 1
  }
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "houses": {
      "House1": { /* figure data */ },
      "House2": { /* figure data */ },
      "House3": { /* figure data */ },
      "House4": { /* figure data */ },
      "House5": { /* calculated figure data */ },
      "House6": { /* calculated figure data */ },
      "House7": { /* calculated figure data */ },
      "House8": { /* calculated figure data */ },
      "House9": { /* calculated figure data */ },
      "House10": { /* calculated figure data */ },
      "House11": { /* calculated figure data */ },
      "House12": { /* calculated figure data */ }
    },
    "rightWitness": { /* figure data */ },
    "leftWitness": { /* figure data */ },
    "judge": { /* figure data */ },
    "sentence": { /* figure data */ },
    "chartSummary": "=== GEOMANTIC CHART SUMMARY ===\nHouse 1: Via (1-2-1-2)\n...",
    "isComplete": true
  },
  "error": null,
  "timestamp": "2024-01-15T10:30:00Z"
}
```

### 4. Get All Figures

**GET** `/api/geomantic/figures/all`

Retrieves all 16 available geomantic figures.

### 5. Get Figure by Name

**GET** `/api/geomantic/figures/{name}`

Retrieves a specific figure by name (e.g., "Via", "Populus", "Conjunctio").

### 6. Health Check

**GET** `/api/geomantic/health`

Returns the API health status.

## Elemental Values

- **1**: Active (single dot)
- **2**: Passive (double dots)

## Available Figure Names

1. Via (The Way)
2. Populus (The People)
3. Conjunctio (The Conjunction)
4. Carcer (The Prison)
5. Fortuna Major (Greater Fortune)
6. Fortuna Minor (Lesser Fortune)
7. Acquisitio (The Acquisition)
8. Amissio (The Loss)
9. Laetitia (The Joy)
10. Tristitia (The Sorrow)
11. Puella (The Girl)
12. Puer (The Boy)
13. Rubeus (The Red)
14. Albus (The White)
15. Caput Draconis (The Dragon's Head)
16. Cauda Draconis (The Dragon's Tail)

## Error Handling

All endpoints return consistent error responses:

```json
{
  "success": false,
  "data": null,
  "error": "Error message describing what went wrong",
  "timestamp": "2024-01-15T10:30:00Z"
}
```

## CORS

The API is configured to allow CORS from any origin for development purposes. In production, you should configure appropriate CORS policies.

## Development

### Building the Project

```bash
dotnet build
```

### Running Tests

```bash
dotnet test
```

### Publishing

```bash
dotnet publish -c Release
```

## Dependencies

- ASP.NET Core 6.0
- Swashbuckle.AspNetCore (Swagger UI)
- Newtonsoft.Json
- GeomancyApp (referenced project)

## License

This project is licensed under the MIT License. 