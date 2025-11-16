namespace InnerHealth.Api.Models;

/// <summary>
/// Representa uma sessão de atividade física.
/// </summary>
public class PhysicalActivity
{
    public int Id { get; set; }
    /// <summary>
    /// Data da atividade.
    /// </summary>
    public DateOnly Date { get; set; }

    /// <summary>
    /// Modalidade do exercício (ex.: corrida, ciclismo, yoga).
    /// </summary>
    public string? Modality { get; set; }

    /// <summary>
    /// Duração da atividade em minutos.
    /// </summary>
    public int DurationMinutes { get; set; }

    public int UserProfileId { get; set; }
    public UserProfile? UserProfile { get; set; }
}