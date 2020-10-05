FROM mcr.microsoft.com/dotnet/sdk AS build
WORKDIR /source

COPY *.sln .
COPY ImageGenerator/ ./ImageGenerator
RUN dotnet restore -r linux-musl-x64

WORKDIR /source/ImageGenerator
RUN dotnet publish -c release -o /app -r linux-musl-x64 --self-contained false --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine AS final
WORKDIR /app
COPY --from=build /app ./

ENTRYPOINT ["./ImageGenerator"]