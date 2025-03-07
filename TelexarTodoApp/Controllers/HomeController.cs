using System.Diagnostics;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using TelexarTodoApp.Models;

namespace TelexarTodoApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private static List<TodoModel> _todos = new List<TodoModel>();

    // Add new todo
    [HttpPost]
    public IActionResult Create(TodoModel item)
    {
        if (ModelState.IsValid)
        {
            item.Id = _todos.Count + 1;
            _todos.Add(item);
        }
        return RedirectToAction(nameof(Index));
    }



    // Update the todo item
    [HttpPost]
    public IActionResult Edit(TodoModel updatedItem)
    {
        var existingItem = _todos.FirstOrDefault(t => t.Id == updatedItem.Id);
        if (existingItem == null) return NotFound();

        existingItem.Title = updatedItem.Title;
        existingItem.IsCompleted = updatedItem.IsCompleted;

        return RedirectToAction(nameof(Index));
    }

    // Delete a todo
    public IActionResult Delete(int id)
    {
        var item = _todos.FirstOrDefault(t => t.Id == id);
        if (item == null) return NotFound();

        _todos.Remove(item);
        return RedirectToAction(nameof(Index));
    }

    //Get todos
    public IActionResult Index(int? editId)
    {
        ViewBag.EditId = editId;
        return View(_todos);
    }
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }


    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
