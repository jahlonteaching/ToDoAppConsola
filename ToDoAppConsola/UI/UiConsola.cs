using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoAppConsola.Modelo;

namespace ToDoAppConsola.UI
{
    public class UiConsola
    {
        public LibretaTareas Libreta { get; set; }
        public UiConsola(LibretaTareas libreta)
        {
            Libreta = libreta;
        }

        public static void MostrarMensajeBienvenida()
        {
            Console.WriteLine("=========================");
            Console.WriteLine("BIENVENIDO A LA TODO APP");
            Console.WriteLine("=========================");
        }

        public static int MostrarMenu()
        {
            Console.WriteLine("\nOPTIONS:");
            Console.WriteLine("1. Agregar nueva tarea");
            Console.WriteLine("2. Listar todas las tareas");
            Console.WriteLine("3. Agregar categorías a una tarea");
            Console.WriteLine("4. Listar tareas pendientes");
            Console.WriteLine("5. Listar tareas completadas");
            Console.WriteLine("6. Marcar tarea como completada");
            Console.WriteLine("7. Eliminar una tarea");
            Console.WriteLine("8. Mostrar contadore de tareas por categoría");
            Console.WriteLine("9. Salir del programa");
            Console.Write("Ingrese una opción: ");
            int opcion = int.Parse(Console.ReadLine());
            while(opcion < 1 && opcion > 10)
            {
                Console.WriteLine(">>> ERROR: Opción inválida. Intente nuevamente.");
                opcion = int.Parse(Console.ReadLine());
            }
            return opcion;
        }

        public void IniciarAplicacion()
        {
            UiConsola.MostrarMensajeBienvenida();
            bool fin = false;
            while(!fin)
            {
                int opcion = UiConsola.MostrarMenu();
                fin = ProcesarOpcionDeUsuario(opcion);
            }
        }

        public bool ProcesarOpcionDeUsuario(int opcion)
        {
            switch(opcion)
            {
                case 1:
                    AgregarNuevaTarea();
                    break;
                case 2:
                    ListarTareas();
                    break;
                case 3:
                    AgregarCategoriasATarea();
                    break;
                case 4:
                    ListarTareasPendientes();
                    break;
                case 5:
                    ListarTareasCompletadas();
                    break;
                case 6:
                    CompletarTarea();
                    break;
                case 7:
                    EliminarTarea();
                    break;
                case 8:
                    MostrarContadorDeTareasPorCategoria();
                    break;
                case 9:
                    FinalizarAplicacion();
                    return true;
            }
            return false;
        }

        public void FinalizarAplicacion()
        {
            Console.WriteLine("========================");
            Console.WriteLine("=== FIN DEL PROGRAMA ===");
            Console.WriteLine("========================");
        }

        public void MostrarContadorDeTareasPorCategoria()
        {
            Console.WriteLine("\n=== MOSTRAR CONTADOR DE TAREAS POR CATEGORÍA ===\n");
            Dictionary<string, int> contadores = Libreta.TareasPorCategoria;
            foreach (string categoria in contadores.Keys)
            {
                Console.WriteLine($"- Categoría {categoria} tiene {contadores[categoria]} tareas");
            }
        }

        public void EliminarTarea()
        {
            Console.WriteLine("\n=== ELIMINAR TAREA ===\n");
            ListarTareas();
            Console.Write("Ingrese el ID de la tarea: ");
            int id = int.Parse(Console.ReadLine());
            Libreta.Tareas.Remove(id);
            Console.WriteLine($"Se ha eliminado la tarea con ID {id}");
        }

        public void CompletarTarea()
        {
            Console.WriteLine("\n=== COMPLETAR TAREA ===\n");
            ListarTareas();
            Console.Write("Ingrese el ID de la tarea: ");
            int id = int.Parse(Console.ReadLine());
            Libreta.Tareas[id].MarcarCompletada();
            Console.WriteLine($"Se ha completado la tarea con ID {id}");
        }

        public void ListarTareasCompletadas()
        {
            Console.WriteLine("\n=== TAREAS COMPLETADAS ===\n");
            List<Tarea> completadas = Libreta.TareasCompletadas;
            if (completadas.Count > 0)
            {
                foreach (Tarea tarea in completadas)
                {
                    Console.WriteLine(tarea);
                }
            }
            else
            {
                Console.WriteLine("No hay tareas para mostrar");
            }
        }

        public void ListarTareasPendientes()
        {
            Console.WriteLine("\n=== TAREAS PENDIENTES ===\n");
            List<Tarea> pendientes = Libreta.TareasPendientes;
            if (pendientes.Count > 0)
            {
                foreach (Tarea tarea in pendientes)
                {
                    Console.WriteLine(tarea);
                }
            }
            else
            {
                Console.WriteLine("No hay tareas para mostrar");
            }
        }

        public void AgregarCategoriasATarea()
        {
            Console.WriteLine("\n=== AGREGAR CATEGORÍAS A TAREA ===\n");
            ListarTareas();
            Console.Write("Ingrese el ID de la tarea: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Ingrese las categorías separadas por coma: ");
            string categorias = Console.ReadLine();
            foreach(string c in categorias.Split(','))
            {
                Libreta.Tareas[id].AgregarCategoria(c);
            }
            Console.Write($"Se agregaron las categorías a la tareas con ID {id}");
        }

        public void ListarTareas()
        {
            Console.WriteLine("\n=== LISTA DE TAREAS ===\n");
            
            if (Libreta.Tareas.Count > 0)
            {
                foreach (Tarea tarea in Libreta.Tareas.Values)
                {
                    Console.WriteLine(tarea);
                }
            }
            else
            {
                Console.WriteLine("No hay tareas para mostrar");
            }
        }

        public void AgregarNuevaTarea()
        {
            Console.WriteLine("\n=== AGREGAR NUEVA TAREA ===\n");
            Console.Write("Ingrese título de la tarea: ");
            string titulo = Console.ReadLine();
            Console.Write("Ingrese descripción de la tarea: ");
            string descripcion = Console.ReadLine();
            int id = Libreta.AgregarTarea(titulo, descripcion);
            Console.WriteLine($"Se creo una nueva tarea con el código {id}");
        }
    }

    
}
