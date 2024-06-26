using Catalog.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Microsoft.Win32;
using System.Windows;
using Windows.Storage;
using Windows.Storage.Pickers;
using System.Xml.Linq;
using Windows.Storage.Streams;
using Windows.Graphics.Imaging;
using System.Collections.ObjectModel;

namespace Catalog
{
    public sealed partial class EatingEditor : Page
    {
        private int? id;
        //byte[] photo = null;
        //private StorageFile photoFile = null;
        public EatingEditor()
        {
            this.InitializeComponent();
        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter == null)
            {

            }
            else
            {
                var eating = e.Parameter as Services.Eating;

                id = eating.id;

                NameEating.Text = eating.name;
                //photo
                Description.Text = eating.description;
            }
        }
        public bool Validate()
        {
            if (String.IsNullOrEmpty(NameEating.Text) /* String.IsNullOrEmpty(ConvertIBufferToBase64(EatingService.EatingImageBuffer))*/ || String.IsNullOrEmpty(Description.Text))
            {
                var dialog = new ContentDialog()
                {
                    Title = "Ошибка при заполнении формы",
                    IsPrimaryButtonEnabled = true,
                    PrimaryButtonText = "Ok"
                };
                dialog.ShowAsync();
                return false;
            }
            return true;
        }
        //private async Task<IBuffer> GetImageBufferAsync(StorageFile file)
        //{
        //    using (IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.Read))
        //    {
        //        // Инициализация кодера для JPEG изображения (замените на нужный формат при необходимости)
        //        var decoder = await BitmapDecoder.CreateAsync(stream);
        //        var pixelData = await decoder.GetPixelDataAsync(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Straight, new BitmapTransform(), ExifOrientationMode.IgnoreExifOrientation, ColorManagementMode.DoNotColorManage);

        //        // Конвертация пиксельных данных в буфер
        //        var imageBuffer = pixelData.DetachPixelData().AsBuffer();

        //        return imageBuffer;
        //    }
        //}
        //private BitmapImage bitmap = new BitmapImage();
        private async void Photo(object sender, RoutedEventArgs e)
        {
            FileOpenPicker filePicker = new FileOpenPicker();
            filePicker.FileTypeFilter.Add(".png");
            filePicker.FileTypeFilter.Add(".jpg");
            filePicker.FileTypeFilter.Add(".jpeg");
            filePicker.FileTypeFilter.Add(".bmp");

            StorageFile file = await filePicker.PickSingleFileAsync();
            if (file != null)
            {
                BitmapImage bitmap = new BitmapImage();

                using (IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.Read))
                {
                    bitmap.SetSource(stream);
                }

                //// Сохранение фотографии в папке проекта "Images"
                //StorageFolder imagesFolder = await ApplicationData.Current.LocalFolder.CreateFolderAsync("Images", CreationCollisionOption.OpenIfExists);
                //StorageFile photoFile = await imagesFolder.CreateFileAsync(file.Name, CreationCollisionOption.ReplaceExisting);

                //using (IRandomAccessStream sourceStream = await file.OpenAsync(FileAccessMode.Read))
                //{
                //    using (IRandomAccessStream destinationStream = await photoFile.OpenAsync(FileAccessMode.ReadWrite))
                //    {
                //        await RandomAccessStream.CopyAndCloseAsync(sourceStream, destinationStream);
                //    }
                //}

                //// Получение массива байтов из сохраненной фотографии
                //byte[] photoBytes;
                //using (var photoStream = await photoFile.OpenAsync(FileAccessMode.Read))
                //{
                //    photoBytes = new byte[photoStream.Size];
                //    using (var dataReader = new DataReader(photoStream))
                //    {
                //        await dataReader.LoadAsync((uint)photoStream.Size);
                //        dataReader.ReadBytes(photoBytes);
                //    }
                //}

                //// Отображение выбранной фотографии
                SelectedImage.Source = bitmap;
                photoPath.Text = file.Path;
            }
        }
        //private async Task<byte[]> GetImageBytesAsync(StorageFile file)
        //    {
        //        byte[] imageBytes;
        //        using (IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.Read))
        //        {
        //            DataReader reader = new DataReader(stream.GetInputStreamAt(0));
        //            imageBytes = new byte[stream.Size];
        //            await reader.LoadAsync((uint)stream.Size);
        //            reader.ReadBytes(imageBytes);
        //        }
        //        return imageBytes;
        //}
        //private string ConvertIBufferToBase64(IBuffer buffer)
        //{
        //    byte[] byteArray;
        //    using (DataReader dataReader = DataReader.FromBuffer(buffer))
        //    {
        //        byteArray = new byte[buffer.Length];
        //        dataReader.ReadBytes(byteArray);
        //    }
        //    return Convert.ToBase64String(byteArray);
        //}
        private async void Add(object sender, RoutedEventArgs e)
        {
            if (Validate())
            {
                var eating = new Services.Eating
                {
                    name = NameEating.Text,
                    photoPath = photoPath.Text,
                    //photo = photo, // Сохранение массива байтов фотографии
                    description = Description.Text
                };
                await EatingService.Add(eating);
                Frame rootFrame = Window.Current.Content as Frame;
                rootFrame.GoBack();
            }
        }
        private void Cancel(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.GoBack();
        }
    }
}
