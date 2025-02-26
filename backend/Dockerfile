# Usa una imagen oficial de .NET para compilación
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copiar archivos de proyecto y restaurar paquetes antes de compilar
COPY ./HotelAPI/*.csproj ./HotelAPI/
RUN dotnet restore ./HotelAPI/HotelAPI.csproj

# Copiar el resto de la aplicación
COPY ./HotelAPI ./HotelAPI/
WORKDIR /app/HotelAPI

# Construir la aplicación
RUN dotnet build --configuration Release

# Publicar la aplicación
RUN dotnet publish --configuration Release --no-build --output /app/publish

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Copiar la aplicación publicada desde la etapa anterior
COPY --from=build /app/publish .

# Exponer el puerto de la API
EXPOSE 15000

# Comando de inicio
ENTRYPOINT ["dotnet", "HotelAPI.dll"]
