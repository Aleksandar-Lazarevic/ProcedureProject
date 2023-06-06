using iProcedure.Model;

namespace iProcedure;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        //MainPage = new AppShell();
        MainPage = new NavigationPage(new MainPage());
    }

    static iProcedureDatabase database = null;
    public static iProcedureDatabase Database
    {
        get
        {
            if (database == null)
            {
                bool bExistDB = false;
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "iProcedure.db");

                bExistDB = File.Exists(dbPath);
                database = new iProcedureDatabase(dbPath);

                if (!bExistDB)
                {
                    Unit unit = new Unit();
                    unit.unit_name = "+ Add new";
                    database.SaveUnitDataAsync(unit);

                    unit = new Unit();
                    unit.unit_name = "ml";
                    database.SaveUnitDataAsync(unit);

                    unit = new Unit();
                    unit.unit_name = "mm";
                    database.SaveUnitDataAsync(unit);

                    unit = new Unit();
                    unit.unit_name = "meter";
                    database.SaveUnitDataAsync(unit);

                    unit = new Unit();
                    unit.unit_name = "liter";
                    database.SaveUnitDataAsync(unit);
                }
            }

            return database;
        }
    }
}
