FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/OICTCaseStudy.Api/OICTCaseStudy.Api.csproj", "src/OICTCaseStudy.Api/"]
COPY ["src/OICTCaseStudy.App/OICTCaseStudy.App.csproj", "src/OICTCaseStudy.App/"]
COPY ["src/OICTCaseStudy.ApiClient.Litacka/OICTCaseStudy.ApiClient.Litacka.csproj", "src/OICTCaseStudy.ApiClient.Litacka/"]
RUN dotnet restore "src/OICTCaseStudy.Api/OICTCaseStudy.Api.csproj"
COPY . .
WORKDIR "/src/src/OICTCaseStudy.Api"
RUN dotnet build "OICTCaseStudy.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "OICTCaseStudy.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OICTCaseStudy.Api.dll"]
