# Geomancy API

A RESTful API for generating geomantic figures and house charts using traditional geomancy methods.

## Overview

This API provides endpoints for:
- Generating individual geomantic figures from elemental patterns
- Creating four figures for the first four houses
- Generating complete house charts with all 12 houses, witnesses, judge, and sentence
- Retrieving figure information by name or elemental pattern

## Recent Fixes

### Web.config Issues Resolved

The following issues have been fixed in the Web.config file:

1. **Removed Entity Framework Configuration**
   - Removed unnecessary `configSections` for Entity Framework
   - Removed `entityFramework` section and related configuration
   - This was causing build errors as the project doesn't use EF

2. **Removed Database Connection Strings**
   - Removed `connectionStrings` section
   - Not needed for this stateless API

3. **Fixed Assembly Binding Redirects**
   - Removed unnecessary redirects for System.Web.Optimization, WebGrease, System.Web.Helpers, System.Web.WebPages, and System.Web.Mvc
   - Added proper redirects for System.Web.Http and System.Net.Http.Formatting
   - This resolves assembly loading conflicts

4. **Updated Project Configuration**
   - Changed OutputType from Library to Exe for proper Web API hosting
   - Added IIS Express configuration properties
   - Moved Web.config from Compile to Content items

## API Endpoints

### Generate Single Figure
- **POST** `/api/geomancy/figure`
- Generates a single geomantic figure from four elemental lines

### Generate Four Figures
- **POST** `/api/geomancy/figures`
- Generates four figures for houses 1-4

### Generate Complete House Chart
- **POST** `/api/geomancy/chart`
- Generates a complete house chart with all calculations

### Get Figure by Name
- **GET** `/api/geomancy/figure/{figureName}`
- Retrieves information about a specific figure

### Get Figure by Pattern
- **GET** `/api/geomancy/figure/pattern?headLine=X&neckLine=X&bodyLine=X&footLine=X`
- Retrieves figure information by elemental pattern

### Get All Figures
- **GET** `/api/geomancy/figures/all`
- Returns information about all 16 geomantic figures

### Multiple Perfections Endpoint

The response now includes top-level fields:
- `Indentation`: The main indent score for the querent/quesited pair.
- `TranslatorIndentation`: The indent score for the querent/translator pair (if a translator is present).

Example response:
```json
{
  "success": true,
  "message": "Found 1 perfections for the specified pair",
  "perfections": [ ... ],
  "totalPerfections": 1,
  "indentation": { ... },
  "translatorIndentation": { ... }
}
```

### Indentation Endpoint

`GET /api/geomancy/indentation?firstMother=Via&secondMother=Populus&thirdMother=Conjunctio&fourthMother=Carcer&querentHouse=1&quesitedHouse=7&translatorHouse=3`

Returns:
```
{
  "indentation": { ... },
  "translatorIndentation": { ... }
}
```
- `translatorHouse` is optional.

## Testing

Use the included `test_api.html` file to test the API endpoints. The file provides a user-friendly interface for testing all API functionality.

## Troubleshooting

If you encounter build errors:

1. **Ensure all NuGet packages are restored**
   - Right-click on the solution and select "Restore NuGet Packages"
   - Or run: `nuget restore`

2. **Check project references**
   - Ensure the GeomancyApp project reference is correct
   - Verify all required assemblies are properly referenced

3. **Verify Web.config configuration**
   - Ensure the Web.config file is properly configured for Web API
   - Check that assembly binding redirects are correct

4. **Build order**
   - Build the GeomancyApp project first
   - Then build the GeomancyAPI project

## Dependencies

- .NET Framework 4.8
- Microsoft.AspNet.WebApi 5.2.9
- Newtonsoft.Json 13.0.1
- Swashbuckle 5.6.0 (for API documentation)
- WebActivatorEx 2.2.0

## Project Structure

```
GeomancyAPI/
├── Controllers/
│   └── GeomancyController.cs
├── Models/
│   └── ApiModels.cs
├── Properties/
│   └── AssemblyInfo.cs
├── Global.asax
├── Global.asax.cs
├── SwaggerConfig.cs
├── Web.config
├── packages.config
└── test_api.html
```

## Swagger Documentation

The API includes Swagger documentation. Once the project is running, you can access the Swagger UI at:
`http://localhost:[port]/swagger`

## Elemental Patterns

Each geomantic figure is generated from four elemental lines:
- **Head Line (Fire)**: Represents energy and will
- **Neck Line (Air)**: Represents intellect and communication
- **Body Line (Water)**: Represents emotion and intuition
- **Foot Line (Earth)**: Represents material manifestation

Each line can be either:
- **1 (Active)**: Single dot (•)
- **2 (Passive)**: Double dot (••)

## Geomantic Figures

The API supports all 16 traditional geomantic figures:
1. Via (Way)
2. Populus (People)
3. Conjunctio (Conjunction)
4. Carcer (Prison)
5. Fortuna Minor (Lesser Fortune)
6. Puer (Boy)
7. Puella (Girl)
8. Amissio (Loss)
9. Fortuna Major (Greater Fortune)
10. Acquisitio (Gain)
11. Tristitia (Sorrow)
12. Laetitia (Joy)
13. Rubeus (Red)
14. Albus (White)
15. Caput Draconis (Head of the Dragon)
16. Cauda Draconis (Tail of the Dragon)

## Features

- Generate individual geomantic figures from elemental patterns
- Generate 4 figures for the first 4 houses
- Generate complete house charts with all 12 houses, witnesses, judge, and sentence
- Retrieve figure information by name or elemental pattern
- Get all available geomantic figures
- Swagger UI documentation

## API Endpoints

### Generate Single Figure
- **POST** `/api/geomancy/figure`
- **Description**: Generate a single geomantic figure from elemental lines
- **Request Body**:
```json
{
  "headLine": 1,
  "neckLine": 2,
  "bodyLine": 1,
  "footLine": 2
}
```

### Generate Four Figures
- **POST** `/api/geomancy/figures`
- **Description**: Generate 4 geomantic figures for houses 1-4
- **Request Body**:
```json
{
  "house1": {
    "headLine": 1,
    "neckLine": 2,
    "bodyLine": 1,
    "footLine": 2
  },
  "house2": {
    "headLine": 2,
    "neckLine": 1,
    "bodyLine": 2,
    "footLine": 1
  },
  "house3": {
    "headLine": 1,
    "neckLine": 1,
    "bodyLine": 2,
    "footLine": 2
  },
  "house4": {
    "headLine": 2,
    "neckLine": 2,
    "bodyLine": 1,
    "footLine": 1
  }
}
```

### Generate Complete House Chart
- **POST** `/api/geomancy/chart`
- **Description**: Generate a complete house chart with all 12 houses, witnesses, judge, and sentence
- **Request Body**: Same as Generate Four Figures
- **Response**: Complete chart with all calculated figures

### Get Figure by Name
- **GET** `/api/geomancy/figure/{figureName}`
- **Description**: Get information about a specific figure by name
- **Example**: `/api/geomancy/figure/Via`

### Get Figure by Pattern
- **GET** `/api/geomancy/figure/pattern?headLine=1&neckLine=2&bodyLine=1&footLine=2`
- **Description**: Get figure information by elemental pattern

### Get All Figures
- **GET** `/api/geomancy/figures/all`
- **Description**: Get all 16 available geomantic figures

## Elemental Line Values

- **1**: Single dot (Active element)
- **2**: Double dots (Passive element)

## Response Format

All responses include:
- `success`: Boolean indicating if the operation was successful
- `message`: Description of the result
- `figure` or `figures`: The generated figure data
- `timestamp`: When the response was generated

## Figure Properties

Each figure includes:
- `name`: The figure's name
- `otherNames`: Alternative names
- `quality`: Good, Evil, or Neutral
- `keyword`: Key concept
- `imagery`: Visual description
- `strongHouse`: House where the figure is strong
- `weakHouse`: House where the figure is weak
- `planet`: Associated planet
- `sign`: Associated zodiac sign
- `fireElement`, `airElement`, `waterElement`, `earthElement`: Elemental states
- `anatomy`, `bodyType`, `characterType`: Physical and character traits
- `colors`: Associated colors
- `commentary`: Detailed commentary
- `divinatoryMeaning`: Divinatory interpretation
- `elementalPattern`: Pattern as "1-2-1-2" format

## House Chart Response

The complete house chart includes:
- `houses`: Array of all 12 houses with their figures
- `rightWitness`: Right witness figure
- `leftWitness`: Left witness figure
- `judge`: Judge figure
- `sentence`: Sentence figure
- `chartSummary`: Text summary of the chart
- `isComplete`: Whether the chart is fully calculated
- `generatedAt`: Timestamp of generation

## Setup and Installation

1. Ensure you have .NET Framework 4.8 installed
2. Build the main GeomancyApp project
3. Build the GeomancyAPI project
4. Deploy to IIS or run in Visual Studio

## Swagger Documentation

Once the API is running, you can access the Swagger UI at:
`http://localhost:[port]/swagger`

This provides interactive documentation and allows you to test the API endpoints directly.

## Example Usage

### Generate a Via figure:
```bash
curl -X POST "http://localhost/api/geomancy/figure" \
  -H "Content-Type: application/json" \
  -d '{"headLine":1,"neckLine":2,"bodyLine":1,"footLine":2}'
```

### Generate a complete chart:
```bash
curl -X POST "http://localhost/api/geomancy/chart" \
  -H "Content-Type: application/json" \
  -d '{
    "house1": {"headLine":1,"neckLine":2,"bodyLine":1,"footLine":2},
    "house2": {"headLine":2,"neckLine":1,"bodyLine":2,"footLine":1},
    "house3": {"headLine":1,"neckLine":1,"bodyLine":2,"footLine":2},
    "house4": {"headLine":2,"neckLine":2,"bodyLine":1,"footLine":1}
  }'
``` 