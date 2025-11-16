namespace InnerHealth.Api.Models;

/// <summary>
/// Representa o perfil do usuário com informações usadas pra calcular recomendações. Hoje só tem um usuário, mas o esquema já suporta vários.
/// </summary>
public class UserProfile
{
    public int Id { get; set; }
    /// <summary>
    /// Peso em quilos. Usado pra calcular a ingestão diária recomendada de água.
    /// </summary>
    public decimal Weight { get; set; }

    /// <summary>
    /// Altura em centímetros. Ainda não usamos nos cálculos, mas deixamos guardada pro futuro.
    /// </summary>
    public decimal Height { get; set; }

    /// <summary>
    /// Idade do usuário em anos.
    /// </summary>
    public int Age { get; set; }

    /// <summary>
    /// Qualidade do sono de 0 a 100 no dia atual. Esse valor reseta todo dia.
    /// </summary>
    public int SleepQuality { get; set; }

    /// <summary>
    /// Horas dormidas no dia atual. Esse valor reseta todo dia.
    /// </summary>
    public decimal SleepHours { get; set; }

    // Propriedades de navegação para métricas relacionadas
    public ICollection<WaterIntake>? WaterIntakes { get; set; }
    public ICollection<SunlightSession>? SunlightSessions { get; set; }
    public ICollection<MeditationSession>? MeditationSessions { get; set; }
    public ICollection<SleepRecord>? SleepRecords { get; set; }
    public ICollection<PhysicalActivity>? PhysicalActivities { get; set; }
    public ICollection<TaskItem>? TaskItems { get; set; }
}