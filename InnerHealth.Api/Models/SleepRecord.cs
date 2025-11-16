namespace InnerHealth.Api.Models;

/// <summary>
/// Representa o registro de sono de uma data.
/// </summary>
public class SleepRecord
{
    public int Id { get; set; }
    /// <summary>
    /// Data do registro de sono. Representa a noite que antecede o dia.
    /// </summary>
    public DateOnly Date { get; set; }

    /// <summary>
    /// Número de horas dormidas.
    /// </summary>
    public decimal Hours { get; set; }

    /// <summary>
    /// Qualidade do sono (0 a 100).
    /// </summary>
    public int Quality { get; set; }

    public int UserProfileId { get; set; }
    public UserProfile? UserProfile { get; set; }
}