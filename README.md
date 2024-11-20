

# Liquid File Changes Not Reflecting in Live Environment with Docker

## Problem Overview
When changes are made to any Liquid file in the project, they do not take effect immediately in the live environment. This is caused by a full rebuild of the entire codebase when a single Liquid file is modified, leading to delays of **5 to 10 minutes** for changes to appear. This significantly impacts development speed and productivity.


## Steps to Reproduce

1. **Create an ASP.NET Core Web App (MVC) Project using Fluid**:
   - Install NuGet packages for `Fluid` and `Fluid.MVCViewEngine`.
   - Add a controller and view.
   - Create a file named `Index.liquid` for the view.

2. **Update `Program.cs`**:
   Add the following configurations:
   ```csharp
   builder.Services.AddMvc().AddFluid();
   builder.Services.AddControllersWithViews().AddFluid();
   app.UseStaticFiles();
   ```

3. **Add a Dockerfile**:
   Include the following in your `Dockerfile`:
   ```dockerfile
   # Add file change detection
   ENV DOTNET_USE_POLLING_FILE_WATCHER=1
   ```

4. **Build and Run Docker Image**:
   - Build the Docker image:
     ```bash
     docker build -t your_image_name .
     ```
   - Run the Docker container with volume mapping for dynamic file updates:
     ```bash
     docker run -p 5050:80 -v "dynamic_volume_path:/app/Views/:rw" your_image_name
     ```

5. **Reproduce the Issue**:
   - Modify any Liquid file (e.g., `Index.liquid`) and observe the delay.
   - Refresh the browser to verify changes (delays may last 5–10 minutes).


## Root Cause
The current configuration triggers a **full rebuild of all files** whenever a single file is modified. This rebuild time increases with the size of the project and volume, causing significant delays in reflecting changes.



## Current Impact
- **Affected Files**: Liquid files in the `/Views/` directory.
- **Current Behavior**: Any single file change causes a full project rebuild.
- **Delay**: 5–10 minutes per change (depending on volume size).
- **Impact**: Significant loss of productivity and increased development times.
