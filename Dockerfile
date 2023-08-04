# Learn about building .NET container images:
# https://github.com/dotnet/dotnet-docker/blob/main/samples/README.md
FROM mcr.microsoft.com/dotnet/sdk:6.0-jammy AS build

# copy csproj and restore as distinct layers
COPY ["Plant App_2/Plant App_2.csproj", "/source/"]
WORKDIR /source/
RUN dotnet restore --use-current-runtime

# copy everything else and build app
COPY ["/Plant App_2", "."]
RUN dotnet publish --use-current-runtime --self-contained false --no-restore -o /app


# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:6.0-jammy
WORKDIR /app/
COPY --from=build /app .
EXPOSE 80
ENTRYPOINT ["./Plant App_2"]