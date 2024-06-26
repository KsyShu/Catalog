using Catalog.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;

namespace Catalog
{
    public sealed partial class EatingUpdate : Page
    {
        private int? id;
        //byte[] photoBytes = null;
        public EatingUpdate()
        {
            this.InitializeComponent();
        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            this.EatingName.ItemsSource = await EatingService.getAll();
           

            if (e.Parameter == null)
            {

            }
            else
            {
                var eating = e.Parameter as Services.Eating;

                id = eating.id;

                NameEating.Text = eating.name;
                //ConvertIBufferToBase64(EatingService.EatingImageBuffer) = eating.photo;
                Description.Text = eating.description;
            }
        }
        public bool Validate()
        {
            if (String.IsNullOrEmpty(NameEating.Text) || /*String.IsNullOrEmpty(ConvertIBufferToBase64(EatingService.EatingImageBuffer)) ||*/
                String.IsNullOrEmpty(Description.Text)) 
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
                //IBuffer imageBuffer = await GetImageBufferAsync(file);
                //EatingService.EatingImageBuffer = imageBuffer;
                SelectedImage.Source = bitmap;
                photoPath.Text = file.Path;
            }
        }
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
        private async void Update(object sender, RoutedEventArgs e)
        {
            if (Validate())
            {
                var eating = new Services.Eating
                {
                        id=(this.EatingName.SelectedItem as Catalog.Services.Eating).id,
                        name = NameEating.Text,
                        //photo = photoBytes,
                        photoPath = photoPath.Text,
                        description = Description.Text
                };
                await EatingService.Update(eating);
                Frame rootFrame = Window.Current.Content as Frame;
                rootFrame.GoBack();
            }
        }
        private async void Delete(object sender, RoutedEventArgs e)
        {
            // Показать диалоговое окно подтверждения
            ContentDialog deleteConfirmationDialog = new ContentDialog
            {
                Title = "Подтверждение удаления",
                Content = "Вы уверены, что хотите удалить элемент?",
                PrimaryButtonText = "Удалить",
                CloseButtonText = "Отмена"
            };

            ContentDialogResult result = await deleteConfirmationDialog.ShowAsync();

            // Если выбрана опция "Удалить", выполнить удаление
            if (result == ContentDialogResult.Primary)
            {
                var eating = new Services.Eating
                {
                    id = (this.EatingName.SelectedItem as Catalog.Services.Eating).id
                };
                await EatingService.Delete(eating);

                // Переход назад после удаления
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

