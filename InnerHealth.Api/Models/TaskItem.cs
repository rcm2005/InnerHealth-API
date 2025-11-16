namespace InnerHealth.Api.Models;

/// <summary>
/// Representa uma tarefa a ser realizada.
/// </summary>
public class TaskItem
{
    public int Id { get; set; }
    /// <summary>
    /// Título da tarefa.
    /// </summary>
    public string? Title { get; set; }
    /// <summary>
    /// Descrição detalhada da tarefa.
    /// </summary>
    public string? Description { get; set; }
    /// <summary>
    /// Data da tarefa. Só a data conta; são tarefas diárias.
    /// </summary>
    public DateOnly Date { get; set; }
    /// <summary>
    /// Indica se a tarefa foi concluída.
    /// </summary>
    public bool IsComplete { get; set; }
    /// <summary>
    /// Prioridade opcional: 0=Baixa, 1=Média, 2=Alta.
    /// </summary>
    public int? Priority { get; set; }

    public int UserProfileId { get; set; }
    public UserProfile? UserProfile { get; set; }
}