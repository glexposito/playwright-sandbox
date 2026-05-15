import { test, expect } from '@playwright/test';
import Database from 'better-sqlite3';

test.beforeEach(() => {
  const db = new Database(process.env.TEST_DB_PATH!);
  db.exec(`DELETE FROM Games`);
  db.prepare(`INSERT INTO Games (Title, Genre, ReleaseYear, Rating, Description) VALUES (?, ?, ?, ?, ?)`).run('Street Fighter II', 'Fighting', 1991, 9.5, 'The iconic 1v1 fighter that defined the genre.');
  db.prepare(`INSERT INTO Games (Title, Genre, ReleaseYear, Rating, Description) VALUES (?, ?, ?, ?, ?)`).run('Mortal Kombat', 'Fighting', 1992, 9.0, 'Brutally realistic fighter famous for its fatalities.');
  db.prepare(`INSERT INTO Games (Title, Genre, ReleaseYear, Rating, Description) VALUES (?, ?, ?, ?, ?)`).run('Art of Fighting', 'Fighting', 1992, 8.5, "SNK's power-gauge fighter that introduced super moves.");
  db.close();
});

test('index shows all seeded games', async ({ page }) => {
  await page.goto('/Games');

  const rows = page.locator('#games-table tbody tr');
  await expect(rows).toHaveCount(3);
  await expect(page.locator('#games-table')).toContainText('Street Fighter II');
});

test('create adds a new game', async ({ page }) => {
  await page.goto('/Games/Create');

  await page.fill('#Title', 'King of Fighters');
  await page.selectOption('#Genre', 'Fighting');
  await page.fill('#ReleaseYear', '1994');
  await page.fill('#Rating', '9.2');
  await page.fill('#Description', 'SNK team-based fighter.');
  await page.click('button[type=submit]');

  await expect(page.locator('#games-table')).toBeVisible();
  await expect(page.locator('#games-table')).toContainText('King of Fighters');
});

test('create shows validation errors when required fields are missing', async ({ page }) => {
  await page.goto('/Games/Create');

  await page.click('button[type=submit]');

  await expect(page.locator('[data-valmsg-for="Title"]')).toBeVisible();
  await expect(page.locator('[data-valmsg-for="Genre"]')).toBeVisible();
});

test('details shows correct game info', async ({ page }) => {
  await page.goto('/Games');

  await page.locator('tr', { hasText: 'Street Fighter II' }).getByText('Details').click();

  await expect(page.locator('#detail-genre')).toContainText('Fighting');
  await expect(page.locator('#detail-year')).toContainText('1991');
  await expect(page.locator('#detail-rating')).toContainText('9.5');
});

test('edit updates a game', async ({ page }) => {
  await page.goto('/Games');

  await page.locator('tr', { hasText: 'Street Fighter II' }).getByText('Edit').click();

  await page.fill('#Title', 'Street Fighter II: Champion Edition');
  await page.click('button[type=submit]');

  await expect(page.locator('#games-table')).toBeVisible();
  await expect(page.locator('#games-table')).toContainText('Street Fighter II: Champion Edition');
});

test('delete removes a game', async ({ page }) => {
  await page.goto('/Games');

  await page.locator('tr', { hasText: 'Mortal Kombat' }).getByText('Delete').click();
  await page.click('button[type=submit]');

  await expect(page.locator('#games-table')).toBeVisible();
  await expect(page.locator('#games-table')).not.toContainText('Mortal Kombat');
});
