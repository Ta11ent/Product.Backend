FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/ShoppingCart/src/Presentation/ShoppingCart.API/ShoppingCart.API.csproj", "Services/ShoppingCart/src/Presentation/ShoppingCart.API/"]
COPY ["SharedModels/SharedModels.csproj", "SharedModels/"]
COPY ["Services/ShoppingCart/src/Core/ShoppingCart.Application/ShoppingCart.Application.csproj", "Services/ShoppingCart/src/Core/ShoppingCart.Application/"]
COPY ["Services/ShoppingCart/src/Core/ChoppingCart.Domain/ShoppingCart.Domain.csproj", "Services/ShoppingCart/src/Core/ChoppingCart.Domain/"]
COPY ["Services/ShoppingCart/src/Infrastructure/ShoppingCart.Infrastructure/ShoppingCart.Infrastructure.csproj", "Services/ShoppingCart/src/Infrastructure/ShoppingCart.Infrastructure/"]
COPY ["Services/ShoppingCart/src/Infrastructure/ShoppingCatalog.Persistence/ShoppingCart.Persistence.csproj", "Services/ShoppingCart/src/Infrastructure/ShoppingCatalog.Persistence/"]
RUN dotnet restore "./Services/ShoppingCart/src/Presentation/ShoppingCart.API/ShoppingCart.API.csproj"
COPY . .
WORKDIR "/src/Services/ShoppingCart/src/Presentation/ShoppingCart.API"
RUN dotnet build "./ShoppingCart.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ShoppingCart.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShoppingCart.API.dll"]