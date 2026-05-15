using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlaywrightSandbox.Web.Data;
using PlaywrightSandbox.Web.Models;

namespace PlaywrightSandbox.Web.Controllers;

public class GamesController(AppDbContext db) : Controller
{
    public async Task<IActionResult> Index()
    {
        var games = await db.Games.OrderBy(g => g.Title).ToListAsync();
        return View(games);
    }

    public IActionResult Create() => View();

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Game game)
    {
        if (!ModelState.IsValid) return View(game);
        db.Games.Add(game);
        await db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(int id)
    {
        var game = await db.Games.FindAsync(id);
        return game is null ? NotFound() : View(game);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var game = await db.Games.FindAsync(id);
        return game is null ? NotFound() : View(game);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Game game)
    {
        if (id != game.Id) return BadRequest();
        if (!ModelState.IsValid) return View(game);
        db.Update(game);
        await db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var game = await db.Games.FindAsync(id);
        return game is null ? NotFound() : View(game);
    }

    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var game = await db.Games.FindAsync(id);
        if (game is not null)
        {
            db.Games.Remove(game);
            await db.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}
