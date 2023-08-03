# define base image from which to pull from
FROM mcr.microsoft.com/dotnet/core/sdk:2.1 AS build-env  

# copy the csproj files into the build-env container's /app directory.
# After copying this file, .NET will read from it and then to go out and
# fetch all the dependencies and tools needed by the project.
COPY *.csproj /app/
WORKDIR /app
RUN dotnet restore

# once dependencies have been added into the container, we copy the project's 
# source files into the container
COPY . /app
RUN dotnet publish -c Release -o /app/publish/


# now we must build the image.
#since our application is ASP.NET, we specify an image with this runtime included:
FROM mcr.microsoft.com/dotnet/core/aspnet:2.1 as runtime

# We then copy over all files from the output directory of our temporary container into
# our final container.
COPY --from=build-env /app/publish/ .

EXPOSE 80
ENTRYPOINT ["dotnet", "Plant App_2.dll"]
# We configure our container to run with our new app as its entrypoint when the container starts

# We have written the dockerfile to perform a multi-stage build. When the dockerfile is executed, 
# it will use the temporary container, build-env, with the .NET core 2.1 SDK to build the sample 
# app and then copy the outputted binaries into another container containing only the .NET core 
# 2.1 runtime so that we minimized the size of the final container.