namespace PengelolaMahasiswa.Model;

/// <summary>
/// Merepresentasikan satu catatan mahasiswa.
/// </summary>
public class DataMahasiswa
{
    public string NoInduk { get; set; } = string.Empty;
    public string NamaLengkap { get; set; } = string.Empty;
    public string ProgramStudi { get; set; } = string.Empty;
    public double Ipk { get; set; }

    public override string ToString() => $"{NoInduk} - {NamaLengkap}";
}
