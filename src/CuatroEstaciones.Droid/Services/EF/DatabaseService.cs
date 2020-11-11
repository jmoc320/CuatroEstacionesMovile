using CuatroEstaciones.Core;
using CuatroEstaciones.Core.Services.EF;
using CuatroEstaciones.Droid.Services.EF;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(DatabaseService))]

namespace CuatroEstaciones.Droid.Services.EF {

    public class DatabaseService : IDatabaseService {

        public string GetDatabasePath() {
            return Path.Combine(
            System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal),
            AppSettings.DatabaseName);
        }
    }
}