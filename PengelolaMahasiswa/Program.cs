using PengelolaMahasiswa.Data;
using PengelolaMahasiswa.UI;

namespace PengelolaMahasiswa;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();

        IRepositoriMahasiswa repositori = new RepositoriMahasiswaSqlite();
        Application.Run(new FormPengelolaMahasiswa(repositori));
    }
}
