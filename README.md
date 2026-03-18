# TexasHolDem Poker API

## Prérequis

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- SQL Server ou MSSQLLocalDB (inclus avec Visual Studio)

## Installation

### 1. Restaurer les dépendances

```bash
dotnet restore
```

### 2. Configurer la base de données

La connexion par défaut dans `appsettings.json` pointe sur MSSQLLocalDB :

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=TexasHolDemPokerApi;Trusted_Connection=True;TrustServerCertificate=True"
}
```

Modifier la chaîne de connexion si besoin, puis appliquer les migrations :

```bash
dotnet ef database update
```

### 3. Lancer l'API

```bash
dotnet run
or
dotnet run --launch-profile https
```

L'API est accessible sur `https://localhost:{port}`.
La documentation interactive (Scalar) est disponible sur `/scalar/v1` en mode développement.
