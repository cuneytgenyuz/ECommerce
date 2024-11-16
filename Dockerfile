# Kullanılacak .NET runtime image (Runtime aşaması için .NET 8.0 ASP.NET Core)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Build aşaması için kullanılan SDK image (Build ve Publish aşamaları için .NET 8.0 SDK)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Projeleri kopyalama ve restore işlemi
COPY ["ECommerce.WebAPI/ECommerce.WebAPI.csproj", "ECommerce.WebAPI/"]
COPY ["ECommerce.Business/ECommerce.Business.csproj", "ECommerce.Business/"]
COPY ["ECommerce.DataAccess/ECommerce.DataAccess.csproj", "ECommerce.DataAccess/"]

# Restore bağımlılıkları
RUN dotnet restore "ECommerce.WebAPI/ECommerce.WebAPI.csproj"

# Tüm kaynak kodlarını kopyalama
COPY . .

# Build işlemi
WORKDIR "/src/ECommerce.WebAPI"
RUN dotnet build "ECommerce.WebAPI.csproj" -c Release -o /app/build

# Publish aşaması
FROM build AS publish
RUN dotnet publish "ECommerce.WebAPI.csproj" -c Release -o /app/publish

# Runtime image'ına uygulama dosyalarını kopyala
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Uygulamayı başlatma
ENTRYPOINT ["dotnet", "ECommerce.WebAPI.dll"]
