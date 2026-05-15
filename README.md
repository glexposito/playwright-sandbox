# PlaywrightSandbox

ASP.NET Core MVC app with a Games CRUD used as a target for Playwright e2e tests.

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Node.js](https://nodejs.org)

## Running the app

```bash
cd PlaywrightSandbox.Web
dotnet run
```

Open http://localhost:5161. The SQLite database is created and seeded automatically on first run.

## Running the tests

```bash
cd e2e
npm install
npx playwright install chromium
npm test
```

| Command | Description |
|---|---|
| `npm test` | Run all tests headless |
| `npm run test:headed` | Run with browser visible |
| `npm run test:ui` | Open Playwright UI mode |
| `npx playwright test --debug` | Step through with inspector |
| `npx playwright test games.spec.ts` | Run a specific file |

The app starts automatically via `webServer` in `playwright.config.ts`.

## How tests work

Each test resets `games.test.db` directly via `better-sqlite3` in `beforeEach` — full isolation, no shared state between tests. The app runs in the `Test` environment using `appsettings.Test.json`, keeping `games.db` (dev) untouched.
