# ===========================
# STAGE 1 — BUILD
# ===========================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o /dist

# ===========================
# STAGE 2 — RUNTIME
# ===========================
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /dist /app

RUN mkdir -p /data
VOLUME ["/data"]

COPY entrypoint.sh /entrypoint.sh
RUN chmod +x /entrypoint.sh

EXPOSE 8080
EXPOSE 8443

ENTRYPOINT ["/entrypoint.sh"]
