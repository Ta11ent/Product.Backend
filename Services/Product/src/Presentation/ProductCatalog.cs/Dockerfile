FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
#RUN apt-get update && \
    #apt-get install -y software-properties-common && \
    #add-apt-repository universe && \
    #apt-get update && \
    #apt-get install -y libgssapi-krb5-2
EXPOSE 7000
EXPOSE 7001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/Product/src/Presentation/ProductCatalog.cs/ProductCatalog.API.csproj", "Services/Product/src/Presentation/ProductCatalog.cs/"]
COPY ["Services/Product/src/Core/ProductCatalog.Application/ProductCatalog.Application.csproj", "Services/Product/src/Core/ProductCatalog.Application/"]
COPY ["Services/Product/src/Core/ProductCatalog.Domain/ProductCatalog.Domain.csproj", "Services/Product/src/Core/ProductCatalog.Domain/"]
COPY ["Services/Product/src/Infrastructure/ProductCatalog.Data/ProductCatalog.Persistence.csproj", "Services/Product/src/Infrastructure/ProductCatalog.Data/"]
COPY ["Services/Product/src/Infrastructure/ProductCatalog.Infrastructure/ProductCatalog.Infrastructure.csproj", "Services/Product/src/Infrastructure/ProductCatalog.Infrastructure/"]
RUN dotnet restore "./Services/Product/src/Presentation/ProductCatalog.cs/ProductCatalog.API.csproj"
COPY . .
WORKDIR "/src/Services/Product/src/Presentation/ProductCatalog.cs"
RUN dotnet build "./ProductCatalog.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ProductCatalog.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductCatalog.API.dll"]