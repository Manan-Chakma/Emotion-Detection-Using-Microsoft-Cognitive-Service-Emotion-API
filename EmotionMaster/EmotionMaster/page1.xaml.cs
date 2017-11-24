using System;
using System.Collections.Generic;
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


using Windows.Media.Capture;
using Windows.Storage;
using Windows.Graphics.Imaging;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Storage.Streams;

using Microsoft.ProjectOxford.Emotion;
using Microsoft.ProjectOxford.Emotion.Contract;
using System.IO;

using System.Windows;
using Microsoft.Win32;
using Windows.Storage.Pickers;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace EmotionMaster
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class page1 : Page
    {

        CameraCaptureUI captureUI = new CameraCaptureUI();
        StorageFile photo;
        IRandomAccessStream imageStream;

        const string APIKEY = "0f62c6d737294bcf9fb1ec5fdab5bccb";
        EmotionServiceClient emotionServiceClient = new EmotionServiceClient(APIKEY);
        Emotion[] emotionResult;

        public page1()
        {
            this.InitializeComponent();
            captureUI.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Jpeg;
            captureUI.PhotoSettings.CroppedSizeInPixels = new Size(400, 400);
        }


        private async void clickButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                photo = await captureUI.CaptureFileAsync(CameraCaptureUIMode.Photo);

                if (photo == null)
                {
                    return;
                }
                else
                {
                    imageStream = await photo.OpenAsync(FileAccessMode.Read);
                    BitmapDecoder decoder = await BitmapDecoder.CreateAsync(imageStream);
                    SoftwareBitmap softwareBitmap = await decoder.GetSoftwareBitmapAsync();

                    SoftwareBitmap softwareBitmapBGRB = SoftwareBitmap.Convert(softwareBitmap, BitmapPixelFormat.Bgra8, BitmapAlphaMode.Premultiplied);
                    SoftwareBitmapSource bitmapSource = new SoftwareBitmapSource();
                    await bitmapSource.SetBitmapAsync(softwareBitmapBGRB);

                    image.Source = bitmapSource;
                }

            }
            catch
            {
                textBox.Text = "Something wrong in the image";
            }
        }

        private async void GetEmotion_Click(object sender, RoutedEventArgs e)
        {
            try {
                emotionResult = await emotionServiceClient.RecognizeAsync(imageStream.AsStream());
                var score = emotionResult[0].Scores;

                if (emotionResult != null)

                {
                    textBox.Text = "Your Emotions are : \n" +

                        "Happiness: " + (score.Happiness) * 100 + " " + "\n" +

                        "Sadness: " + (score.Sadness) * 100 + " " + "\n" +

                        "Surprise: " + (score.Surprise) * 100 + " " + "\n" +

                        "Neutral: " + (score.Neutral) * 100 + " " + "\n" +

                        "Anger: " + (score.Anger) * 100 + " " + "\n" +

                        "Contempt: " + (score.Contempt) * 100 + " " + "\n" +

                        "Disgust: " + (score.Disgust) * 100 + " " + "\n" +

                        "Fear: " + (score.Fear) * 100 + " " + "\n";

                }


            }
            catch {
                textBox.Text = "Something wrong in the image";
            }
        }

        private async void Browse_Click(object sender, RoutedEventArgs e)
        {

            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");

            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                
                imageStream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);

                BitmapDecoder decoder = await BitmapDecoder.CreateAsync(imageStream);
                SoftwareBitmap softwareBitmap = await decoder.GetSoftwareBitmapAsync();

                SoftwareBitmap softwareBitmapBGRB = SoftwareBitmap.Convert(softwareBitmap, BitmapPixelFormat.Bgra8, BitmapAlphaMode.Premultiplied);
                SoftwareBitmapSource bitmapSource = new SoftwareBitmapSource();
                await bitmapSource.SetBitmapAsync(softwareBitmapBGRB);

                image.Source = bitmapSource;


            }
            else
            {
                textBox.Text = "Something wrong in the image";
            }


        }
    }
}
