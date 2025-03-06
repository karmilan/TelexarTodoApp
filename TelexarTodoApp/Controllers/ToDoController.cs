using Microsoft.AspNetCore.Mvc;
using TelexarTodoApp.Models;

public class ToDoController : Controller
{
    private static List<TodoModel> _todos = new List<TodoModel>();
    //private static List<TodoModel> _todos = [];


    // Index action to list all to-do items and show the create form
    public IActionResult Index()
    {
        return View(_todos);
    }

    // Create action to handle form submission
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

    // Edit action to show the edit form
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var item = _todos.FirstOrDefault(t => t.Id == id);
        if (item == null) return NotFound();
        return View(item);
    }

    // Post action to update an existing to-do item
    [HttpPost]
    public IActionResult Edit(TodoModel item)
    {
        var existingItem = _todos.FirstOrDefault(t => t.Id == item.Id);
        if (existingItem == null) return NotFound();

        existingItem.Title = item.Title;
        existingItem.IsCompleted = item.IsCompleted;

        return RedirectToAction(nameof(Index));
    }

    // Delete action to delete a to-do item
    public IActionResult Delete(int id)
    {
        var item = _todos.FirstOrDefault(t => t.Id == id);
        if (item == null) return NotFound();

        _todos.Remove(item);
        return RedirectToAction(nameof(Index));
    }
}
