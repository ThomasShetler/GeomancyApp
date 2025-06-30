# GeomancyAPI Troubleshooting Guide

## Common Issues and Solutions

### 1. Build Errors

#### Issue: "The type or namespace name 'System.Net.Http.Formatting' could not be found"
**Solution:**
- Ensure the `System.Net.Http.Formatting` reference is properly added to the project
- Check that the reference points to: `..\packages\Microsoft.AspNet.WebApi.Client.5.2.9\lib\net45\System.Net.Http.Formatting.dll`

#### Issue: "The type or namespace name 'Newtonsoft' could not be found"
**Solution:**
- Ensure Newtonsoft.Json package is properly installed
- Check that the reference points to: `..\packages\Newtonsoft.Json.13.0.1\lib\net45\Newtonsoft.Json.dll`

#### Issue: "The type or namespace name 'GeomancyApp' could not be found"
**Solution:**
- Ensure the GeomancyApp project reference is correct
- Build the GeomancyApp project first
- Check that the project GUID in the reference matches: `{4E7070D6-9A74-4C2C-9B15-AEE0CD954FCE}`

### 2. Runtime Errors

#### Issue: "Object reference not set to an instance of an object" in Global.asax.cs
**Solution:**
- The simplified Global.asax.cs should resolve this
- If still occurring, check that all Web API packages are properly installed

#### Issue: "Could not load file or assembly 'System.Net.Http.Formatting'"
**Solution:**
- Ensure the assembly binding redirects in Web.config are correct
- Check that the package is properly restored

### 3. API Endpoint Issues

#### Issue: "404 Not Found" for API endpoints
**Solution:**
- Check that the routes are properly configured in Global.asax.cs
- Verify the controller is properly decorated with `[RoutePrefix]`
- Test the simple `/api/geomancy/test` endpoint first

#### Issue: "500 Internal Server Error"
**Solution:**
- Check the application logs for specific error details
- Test with the simplified endpoints first
- Verify that the GeomancyApp classes are accessible

## Testing Steps

### Step 1: Test Basic API Setup
1. Build the solution
2. Start the API project
3. Navigate to: `http://localhost:[port]/api/geomancy/test`
4. Should return: `{"message":"API is working!","timestamp":"..."}`

### Step 2: Test Simple Figure Generation
1. Use the test HTML file or Postman
2. POST to: `http://localhost:[port]/api/geomancy/figure`
3. Body: `{"headLine":1,"neckLine":1,"bodyLine":1,"footLine":1}`
4. Should return a Via figure

### Step 3: Test Figure by Name
1. GET: `http://localhost:[port]/api/geomancy/figure/Via`
2. Should return Via figure information

## Debugging Steps

### 1. Check Build Output
- Look for specific error messages in the build output
- Check the Error List window in Visual Studio

### 2. Check Runtime Logs
- Check the Output window in Visual Studio
- Look for any exception details

### 3. Test Dependencies
- Ensure GeomancyApp project builds successfully
- Check that all NuGet packages are restored

### 4. Verify Configuration
- Check Web.config for proper Web API configuration
- Verify project references are correct
- Ensure assembly binding redirects are proper

## Common Configuration Issues

### Web.config Issues
- Entity Framework configuration (removed)
- Database connection strings (removed)
- Incorrect assembly binding redirects (fixed)

### Project File Issues
- Missing assembly references
- Incorrect project GUIDs
- Wrong OutputType (should be Exe for Web API)

### Package Issues
- Missing NuGet packages
- Version mismatches
- Incorrect package references

## Getting Help

If you're still experiencing issues:

1. **Provide the exact error message** - Copy and paste the complete error
2. **Specify when the error occurs** - Build time, runtime, specific endpoint
3. **Share the build output** - Include any compiler errors or warnings
4. **Test the simple endpoints first** - Use the `/api/geomancy/test` endpoint

## Quick Fixes

### If JSON formatter is causing issues:
- Use the simplified Global.asax.cs (already applied)
- Remove all JSON configuration temporarily

### If project won't build:
- Clean solution and rebuild
- Restore NuGet packages
- Build GeomancyApp first, then GeomancyAPI

### If API endpoints return 404:
- Check route configuration
- Verify controller attributes
- Test with the simple test endpoint first 