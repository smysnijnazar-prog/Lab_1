using JonDou9000.TaskPlanner.Domain.Models1;
using JonDou9000.TaskPlanner.Domain.Models1.Enums; // Необхідно для доступу до Priority

namespace JonDou9000.TaskPlanner.Domain.Logic1
{
    public class SimpleTaskPlanner
    {
        // 9.a. Метод для створення плану
        public WorkItem[] CreatePlan(WorkItem[] items)
        {
            var itemsAsList = items.ToList();

            // 9.b. Використання List.Sort() з делегатом Comparison
            itemsAsList.Sort(CompareWorkItems);

            return itemsAsList.ToArray();
        }

        // 9.b.ii. Метод-порівняльник для сортування
        private static int CompareWorkItems(WorkItem firstItem, WorkItem secondItem)
        {
            // 9.a.i. Критерій 1: Priority - за спаданням (важливіше - спершу)
            // Enum Priority має числові значення, більші значення = вищий пріоритет.
            // Щоб сортувати за спаданням, міняємо місцями firstItem і secondItem.
            int priorityComparison = secondItem.Priority.CompareTo(firstItem.Priority);
            if (priorityComparison != 0)
            {
                return priorityComparison;
            }

            // 9.a.ii. Критерій 2: DueDate - за зростанням (раніше - спершу)
            int dueDateComparison = firstItem.DueDate.CompareTo(secondItem.DueDate);
            if (dueDateComparison != 0)
            {
                return dueDateComparison;
            }

            // 9.a.iii. Критерій 3: Title - в алфавітному порядку (за зростанням)
            return firstItem.Title.CompareTo(secondItem.Title);
        }
    }
}