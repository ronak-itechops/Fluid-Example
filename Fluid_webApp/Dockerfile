FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ENV DOTNET_USE_POLLING_FILE_WATCHER=1

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
# Copy the project file
COPY ["Fluid_webApp.csproj", "./"]
RUN dotnet restore "Fluid_webApp.csproj"
# Copy everything else
COPY . .
# Remove the extra Fluid_webApp directory from the path
RUN dotnet build "Fluid_webApp.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Fluid_webApp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Fluid_webApp.dll"]