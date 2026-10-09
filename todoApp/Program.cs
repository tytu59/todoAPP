using System.ComponentModel.Design;

namespace todoApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var tasks = new List<Taskltem>();

            new List<Taskltem>();

            if (File.Exists("tasks.txt"))
            {
                var lines = File.ReadAllLines("tasks.txt");
                foreach (var line in lines)
                {
                    var parts = line.Split("|");
                    if(parts.Length == 2)
                    {
                        var task = new Taskltem()
                        {
                            Text = parts[0],
                            IsDone = bool.Parse(parts[1])
                        };
                        tasks.Add(task);
                    }
                }
            }

            while (true)
            {
                Console.WriteLine(">");
                var input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input)) continue;

                var parts = input.Split(' ', 2);
                var command = parts[0].ToLower();


                switch (command)
                {
                    case "add":
                        if (parts.Length < 2)
                        {
                            Console.WriteLine("Что добавить");
                            break;
                        }
                        var newTask = new Taskltem { Text = parts[1],IsDone = false };
                        tasks.Add(newTask);
                        Console.WriteLine($"Добавленно: {parts[1]}");
                        break;

                    case "list":
                        if (tasks.Count == 0)
                        {
                            Console.WriteLine("Список пуст");
                            break;
                        }
                        for (int i = 0; i < tasks.Count; i++)
                        {
                            var mark = tasks[i].IsDone ? "[X]" : "[]";
                            Console.WriteLine($"{i + 1}.{mark} {tasks[i].Text}");
                        }
                        break;

                    case "done":
                        if(parts.Length< 2)
                        {
                            Console.WriteLine("Укажит номер задачи");
                            break;
                        }
                        if (int.TryParse(parts[1],out int donelndex))
                        {
                            donelndex--;
                            if (donelndex >= 0 && donelndex < tasks.Count)
                            {
                                tasks[donelndex].IsDone = true;
                                Console.WriteLine($"Задача {donelndex +1}выполнена ");
                            }
                            else
                            {
                                Console.WriteLine("Нет такой задачи");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Ты лох");
                        }
                        break;

                    case "remove":
                        if(parts.Length < 2)
                        {
                            Console.WriteLine("Укажите номер");
                            break;
                        }

                        if (int.TryParse(parts[1], out int removeindex))
                        {
                            removeindex--;

                            if (removeindex >= 0 && removeindex < tasks.Count)
                            {
                                tasks.Remove(tasks[removeindex]);
                                Console.WriteLine($"Задача{removeindex + 1} удалена");
                            }
                            else
                            {
                                Console.WriteLine("Нету такой задачи");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Нужен номер");
                        }
                            break;

                    case "exit":
                                    var saveLines = new List<string>();
                                    foreach (var task in tasks)
                                    {
                                        saveLines.Add($"{task.Text}|{task.IsDone}");
                                    }
                                    File.WriteAllLines("tasks.txt", saveLines);
                                    Console.WriteLine("Сохраненно");
                                    return;

                                default:
                                    Console.WriteLine("Не знаю такой команды");
                                    break;
                                }
            }
        }
    }

    public class Taskltem
    {
        public string Text { get; set; }
        public bool IsDone { get; set; }


    }

}