import { defineConfig } from '@playwright/test';
import path from 'path';

process.env.TEST_DB_PATH = path.resolve(__dirname, '../PlaywrightSandbox.Web/games.test.db');

export default defineConfig({
  testDir: './tests',
  fullyParallel: false,
  workers: 1,
  use: {
    baseURL: 'http://localhost:5161',
  },
  webServer: {
    command: 'dotnet run --project ../PlaywrightSandbox.Web --no-launch-profile',
    url: 'http://localhost:5161',
    reuseExistingServer: false,
    timeout: 120_000,
    stdout: 'pipe',
    stderr: 'pipe',
    env: {
      ASPNETCORE_ENVIRONMENT: 'Test',
    },
  },
});
