namespace iProcedure.ViewModel;

using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using iProcedure.Controls;

using iProcedure.Platforms.Services;

using iProcedure.Services;
using iProcedure.View;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Model;
using System.Collections.ObjectModel;
using iProcedure.Crop;
using SkiaSharp;

public partial class BackgroundImageViewModel: ObservableObject
{
    private ObservableCollection<StepImageItem> _stepImageItems = new ObservableCollection<StepImageItem>();
    public ObservableCollection<StepImageItem> stepImageItems
    {
        get => _stepImageItems;
        set
        {
            SetProperty(ref _stepImageItems, value);
        }
    }


    public ICommand EditFunc { get; private set; }
    public ICommand SystemResourceFunc { get; private set; }
    public ICommand CameraFunc { get; private set; }

    public ICommand OnEntryUnfocused { get; private set; }

    private string _entryText;

    [ObservableProperty]
    private bool _deleteButtonVisible = false;

    [ObservableProperty]
    private string _glyph = "\uf078";//x078

    [ObservableProperty]
    private Color _color = Colors.White;

    public Color color
    {
        get => _color;
        set
        {
            SetProperty(ref _color, value);
        }
    }

    public string glyph
    {
        get => _glyph;
        set
        {
            SetProperty(ref _glyph, value);
        }
    }

    public string entryText
    {
        get => _entryText;
        set
        {
            SetProperty(ref _entryText, value); //_entryText = value;

            if (value.ToString().Equals(""))
                color = Colors.White;
            else
                color = Colors.Black;
        }
    }

    [ObservableProperty]
    private GridLength _gridHeight = 0;
    public GridLength gridHeight
    {
        get => _gridHeight;
        set
        {
            SetProperty(ref _gridHeight, value);
        }
    }

    [ObservableProperty]
    private GridLength _gridWidth = 0;
    public GridLength gridWidth
    {
        get => _gridWidth;
        set
        {
            SetProperty(ref _gridWidth, value);
        }
    }

    public BackgroundImageViewModel()
    {
        OnEntryUnfocused = new Command(Entry_Unfocused);

        EditFunc = new Command(OnEditFunc);
        SystemResourceFunc = new Command(OnSystemResourceFunc);
        CameraFunc = new Command(OnCameraFunc);

        gridHeight = (Microsoft.Maui.Devices.DeviceDisplay.MainDisplayInfo.Width / 3) / 3 * 2;
        gridWidth = Microsoft.Maui.Devices.DeviceDisplay.MainDisplayInfo.Width / 3;

        
        // https://github.com/dotnet/maui/issues/6525       https://stackoverflow.com/questions/72513093/how-to-display-local-image-as-well-as-resources-image-in-net-maui-blazor
        string mainDir = "/storage/emulated/0/Pictures/";//FileSystem.Current.AppDataDirectory;

        bool bbb = File.Exists(Path.Combine(mainDir, "02 start game.png"));


        stepImageItems.Add(new StepImageItem
        {
            strName = "Gelada",
            strPath = Path.Combine(mainDir , "02 start game.png")
        });
        stepImageItems.Add(new StepImageItem
        {
            strName = "Gelada",
            strPath = Path.Combine(mainDir, "02 start game.png")
        });
        stepImageItems.Add(new StepImageItem
        {
            strName = "Gelada",
            strPath = Path.Combine(mainDir, "02 start game.png")
        });
        stepImageItems.Add(new StepImageItem
        {
            strName = "Gelada",
            strPath = Path.Combine(mainDir, "02 start game.png")
        });
        stepImageItems.Add(new StepImageItem
        {
            strName = "Gelada",
            strPath = Path.Combine(mainDir, "02 start game.png")
        });
        stepImageItems.Add(new StepImageItem
        {
            strName = "Gelada",
            strPath = Path.Combine(mainDir, "02 start game.png")
        });
        stepImageItems.Add(new StepImageItem
        {
            strName = "Gelada",
            strPath = Path.Combine(mainDir, "02 start game.png")
        });
        stepImageItems.Add(new StepImageItem
        {
            strName = "Gelada",
            strPath = Path.Combine(mainDir, "02 start game.png")
        });
        stepImageItems.Add(new StepImageItem
        {
            strName = "Gelada",
            strPath = Path.Combine(mainDir, "02 start game.png")
        });
        stepImageItems.Add(new StepImageItem
        {
            strName = "Gelada",
            strPath = Path.Combine(mainDir, "02 start game.png")
        });
        stepImageItems.Add(new StepImageItem
        {
            strName = "Gelada",
            strPath = Path.Combine(mainDir, "02 start game.png")
        });
        stepImageItems.Add(new StepImageItem
        {
            strName = "Gelada",
            strPath = Path.Combine(mainDir, "02 start game.png")
        });
        stepImageItems.Add(new StepImageItem
        {
            strName = "Gelada",
            strPath = Path.Combine(mainDir, "02 start game.png")
        });
        stepImageItems.Add(new StepImageItem
        {
            strName = "Gelada",
            strPath = Path.Combine(mainDir, "02 start game.png")
        });
        stepImageItems.Add(new StepImageItem
        {
            strName = "Gelada",
            strPath = Path.Combine(mainDir, "02 start game.png")
        });
        stepImageItems.Add(new StepImageItem
        {
            strName = "Gelada",
            strPath = Path.Combine(mainDir, "02 start game.png")
        });
        stepImageItems.Add(new StepImageItem
        {
            strName = "Gelada",
            strPath = Path.Combine(mainDir, "02 start game.png")
        });
        stepImageItems.Add(new StepImageItem
        {
            strName = "Gelada",
            strPath = Path.Combine(mainDir, "02 start game.png")
        });
        stepImageItems.Add(new StepImageItem
        {
            strName = "Gelada",
            strPath = Path.Combine(mainDir, "02 start game.png")
        });
        stepImageItems.Add(new StepImageItem
        {
            strName = "Gelada",
            strPath = Path.Combine(mainDir, "02 start game.png")
        });
        stepImageItems.Add(new StepImageItem
        {
            strName = "Gelada",
            strPath = Path.Combine(mainDir, "02 start game.png")
        });
        stepImageItems.Add(new StepImageItem
        {
            strName = "Gelada",
            strPath = Path.Combine(mainDir, "02 start game.png")
        });
        stepImageItems.Add(new StepImageItem
        {
            strName = "Gelada",
            strPath = Path.Combine(mainDir, "02 start game.png")
        });
        stepImageItems.Add(new StepImageItem
        {
            strName = "Gelada",
            strPath = Path.Combine(mainDir, "02 start game.png")
        });
    }

    private void Entry_Unfocused()
    {
        int i = 0;
    }

    async private void OnEditFunc()
    {
        await App.Current.MainPage.Navigation.PushAsync(new EditImagePage());
    }

    async private void OnSystemResourceFunc()
    {
        //FileResult photo = await MediaPicker.Default.PickPhotoAsync();
        //return;

        DialogService dlgService = new DialogService();
        CancellationToken cancellationToken = CancellationToken.None;

        await using var stream = await dlgService.OpenFileDialog(cancellationToken);

        if (stream == Stream.Null)
        {
            return;
        }

        byte[] header = new byte[4];
        stream.Read(header, 0, 4);
        if (header[1] == 0x50 && header[2] == 0x4E && header[3] == 0x47)
        {

        }
        else
        {
            await Toast.Make("Invalid file").Show(cancellationToken);
        }
    }

    async private void OnCameraFunc()
    {

    }

    private async void takephoto(object sender, EventArgs e)
    {
        if (MediaPicker.Default.IsCaptureSupported)
        {
            FileResult photo = await MediaPicker.Default.CapturePhotoAsync();

            if (photo != null)
            {
                // save the file into local storage
                string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

                using Stream sourceStream = await photo.OpenReadAsync();
                Stream stream = await photo.OpenReadAsync();
                using FileStream localFileStream = File.OpenWrite(localFilePath);

                await sourceStream.CopyToAsync(localFileStream);

                /*DisplayPhoto.Source = ImageSource.FromStream(() =>
                {
                    return stream;
                });*/
            }
        }
    }
}
