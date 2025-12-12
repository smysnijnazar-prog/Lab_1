using JonDou9000.TaskPlanner.Domain.Logic1;
using JonDou9000.TaskPlanner.Domain.Models1;
using JonDou9000.TaskPlanner.Domain.Models1.Enums;
using JonDou9000.TaskPlanner.Domain.Models1.Enums;

// 10.b. Явне визначення класу Program і методу Main
internal static class Program
{
    public static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("✨ Планувальник Завдань JonDou9000 ✨");

        var workItems = new List<WorkItem>();
        bool continueInput = true;

        // 10.c.i. Ввід довільної кількості WorkItem
        while (continueInput)
        {
            Console.WriteLine("\n--- Введіть нове завдання ---");

            // Title
            Console.Write("Назва (Title): ");
            var title = Console.ReadLine() ?? string.Empty;
            if (string.IsNullOrEmpty(title))
            {
                Console.WriteLine("Завдання без назви не може бути додано.");
                continue;
            }

            // DueDate
            DateTime dueDate;
            Console.Write("Кінцевий термін (Due Date, наприклад, 17.10.2025): ");
            while (!DateTime.TryParse(Console.ReadLine(), out dueDate))
            {
                Console.Write("Некоректний формат дати. Спробуйте ще (dd.MM.yyyy): ");
            }

            // Priority
            Priority priority;
            Console.Write($"Пріоритет ({string.Join(", ", Enum.GetNames<Priority>())}): ");
            var priorityInput = Console.ReadLine() ?? string.Empty;
            // Використання Enum.Parse з ignoreCase: true
            while (!Enum.TryParse(priorityInput, ignoreCase: true, out priority))
            {
                Console.Write("Некоректний пріоритет. Спробуйте ще: ");
                priorityInput = Console.ReadLine() ?? string.Empty;
            }

            // Створення та додавання WorkItem (CreationDate встановлюємо як поточну)
            workItems.Add(new WorkItem
            {
                Title = title,
                DueDate = dueDate,
                Priority = priority,
                CreationDate = DateTime.Now,
                Complexity = Complexity.Hours, // Можна додати ввід, але за завданням не вимагається
                Description = string.Empty,
                IsCompleted = false
            });

            // Запит на продовження
            Console.Write("Додати ще одне завдання? (так/ні): ");
            continueInput = Console.ReadLine()?.Trim().ToLower() == "так";
        }

        if (workItems.Count == 0)
        {
            Console.WriteLine("\nСписок завдань порожній. Завершення роботи.");
            return;
        }

        // 10.c.ii. Створення об'єкта SimpleTaskPlanner та впорядкування
        Console.WriteLine("\n--- Створення Плану Завдань ---");
        var planner = new SimpleTaskPlanner();
        var plannedItems = planner.CreatePlan(workItems.ToArray());

        // 10.c.iii. Виведення результату
        Console.WriteLine("\n✅ Упорядкований список завдань:");
        int index = 1;
        // Використання конструкції foreach
        foreach (var item in plannedItems)
        {
            // Використовується перевизначений метод ToString() класу WorkItem
            Console.WriteLine($"[{index++}] {item}");
        }

        Console.WriteLine("\nНатисніть будь-яку клавішу для виходу...");
        Console.ReadKey();
    }
}