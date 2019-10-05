FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 44383

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["MimiriumTest/MimiriumTest.csproj", "MimiriumTest/"]
RUN dotnet restore "MimiriumTest/MimiriumTest.csproj"
COPY . ./
WORKDIR "/src/MimiriumTest"
RUN dotnet build "MimiriumTest.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MimiriumTest.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MimiriumTest.dll"]